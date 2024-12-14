using HospitalManagementWebsite.Models.MenuModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HospitalManagementWebsite.Controllers
{
    public class MenuControlController : Controller
    {
        private readonly MenuControlRepository _repository;

        public MenuControlController()
        {
            _repository = new MenuControlRepository();
        }

        // Index Action to list menu controls with filters and pagination
        public ActionResult Index(string department, string searchQuery, int page = 1)
        {
            int pageSize = 10;  // Number of items per page
            var menuControls = _repository.GetAllMenuControls().AsQueryable();  // Convert List to IQueryable

            // Apply filters
            menuControls = ApplyFilters(menuControls, department, searchQuery);

            // Pagination
            var totalItems = menuControls.Count();
            var pagedList = menuControls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Prepare data for the view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(pagedList);  // Return the paginated results to the view
        }

        // Helper function to apply filters
        private IQueryable<MenuControl> ApplyFilters(IQueryable<MenuControl> menuControls, string department, string searchQuery)
        {
            if (!string.IsNullOrEmpty(department))
            {
                menuControls = menuControls.Where(m => m.DepartmentCode.Contains(department));
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                menuControls = menuControls.Where(m => m.MenuName.Contains(searchQuery) || m.MenuDescription.Contains(searchQuery));
            }

            return menuControls;
        }

        // Details Action for viewing a single menu control
        public ActionResult Details(int id)
        {
            var menu = _repository.GetMenuById(id);
            if (menu == null) return HttpNotFound();
            return View(menu);
        }

        // Create Action (Get)
        public ActionResult Create()
        {
            return View();
        }

        // Create Action (Post)
        [HttpPost]
        public ActionResult Create(MenuControl menu)
        {
            if (ModelState.IsValid)
            {
                menu.CreatedOn = DateTime.Now;
                _repository.AddMenu(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // Edit Action (Get)
        public ActionResult Edit(int id)
        {
            var menu = _repository.GetMenuById(id);
            if (menu == null) return HttpNotFound();
            return View(menu);
        }

        // Edit Action (Post)
        [HttpPost]
        public ActionResult Edit(MenuControl menu)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateMenu(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // Delete Action (Get)
        public ActionResult Delete(int id)
        {
            var menu = _repository.GetMenuById(id);
            if (menu == null) return HttpNotFound();
            return View(menu);
        }

        // Delete Action (Post)
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteMenu(id);
            return RedirectToAction("Index");
        }

        // Optional: A method to return filtered data for AJAX requests (e.g., for pagination and dynamic filtering)
        public JsonResult GetFilteredMenuData(string department, string searchQuery, int page = 1)
        {
            int pageSize = 10;
            var menuControls = _repository.GetAllMenuControls().AsQueryable();  // Convert List to IQueryable

            // Apply filters
            menuControls = ApplyFilters(menuControls, department, searchQuery);

            // Pagination logic
            var totalItems = menuControls.Count();
            var pagedList = menuControls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Return the filtered menu data and pagination info as JSON
            return Json(new { menuData = pagedList, totalPages = totalPages }, JsonRequestBehavior.AllowGet);
        }
    }
}
