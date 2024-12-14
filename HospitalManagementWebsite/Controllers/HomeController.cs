using System.Web.Mvc;

namespace HospitalManagementWebsite.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult TDMWelcomePage()
        {
            return View("TDMWELCOMEPAGE");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
    }
}