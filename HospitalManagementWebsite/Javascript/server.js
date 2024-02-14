const express = require('express');
const bodyParser = require('body-parser');
const fs = require('fs');
const path = require('path');

const app = express();
const PORT = 3000; // You can change this port number if needed

app.use(bodyParser.json());

// Serve static files from the folder containing your HTML, CSS, etc.
app.use(express.static(path.join(__dirname, 'public')));

// Endpoint to save the captured photo
app.post('/savephoto', (req, res) => {
    const imageData = req.body.imageData.replace(/^data:image\/jpeg;base64,/, '');
    const imageBuffer = Buffer.from(imageData, 'base64');

    const imagePath = path.join(__dirname, 'TDMWEBCAM', `photo_${Date.now()}.jpg`);

    // Save the image to the specified folder
    fs.writeFile(imagePath, imageBuffer, (err) => {
        if (err) {
            console.error('Error saving photo:', err);
            res.status(500).send('Error saving photo');
        } else {
            console.log('Photo saved successfully');
            res.send(imagePath); // Send the path to the saved photo
        }
    });
});

// Start the server
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});
