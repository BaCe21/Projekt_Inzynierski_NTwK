namespace TrikProjekt56.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public JsonResult IsNameExisting(string Name, int Id = 0)
        {
            bool existing = _unitRepo.IsExisting(Name, Id);
            if (existing)
                return Json(data: false);
            else
                return Json(data: true);
        }
        public IActionResult Index(string sortExpression="", string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            Pagination<Category> categories = _unitRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pageIndex, pageSize);
            var site = new SiteModel(categories.TotalRecords, pageIndex, pageSize);
            site.SortExpression = sortExpression;
            this.ViewBag.Site = site;
            TempData["CurrentPage"] = pageIndex;
            TempData["PageSize"] = pageSize;
            return View(categories);
        }

        private readonly ICategory _unitRepo;

        public CategoryController(ICategory unitrepo)
        {
            _unitRepo = unitrepo;
        }

        public IActionResult Create()
        {
            Category category = new Category();
            TempData.Keep();
            return View(category);
        }

        [HttpPost]
        public IActionResult Create(Category item)
        {
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
            {
                currentPage = (int)TempData["CurrentPage"];
            }
            int pageSize = 5;
            if (TempData["PageSize"] != null)
            {
                pageSize = (int)TempData["PageSize"];
            }
            bool myBool = false;
            string errMessage = "";
            try
            {
                if (item.Name.Length < 3 || item.Name == null)
                    errMessage = "Category name must be at least 3 characters";
                if (_unitRepo.IsExisting(item.Name) == true)
                    errMessage = "Name " + item.Name + " already exists in database";
                if (errMessage == "")
                {
                    item = _unitRepo.Create(item);
                    myBool = true;
                }
            }
            catch (Exception exc)
            {
                errMessage += " " + exc.Message;
            }
            if (myBool == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = item.Name + " created successfully!";
                return RedirectToAction(nameof(Index), new { pageIndex = currentPage, pageSize = pageSize });
            }
        }

        public IActionResult Details(int id)
        {
            Category category = _unitRepo.GetCategory(id);
            TempData.Keep();
            return View(category);
        }
        public IActionResult Edit(int id)
        {
            Category category = _unitRepo.GetCategory(id);
            TempData.Keep();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category item)
        {
            int pageSize = 5;
            if (TempData["PageSize"] != null)
            {
                pageSize = (int)TempData["PageSize"];
            }
            bool myBool = false;
            string errMessage = "";
            try
            {
                if (item.Name.Length < 3 || item.Name == null)
                    errMessage = "Category name must be at least 3 characters";
                if (_unitRepo.IsExisting(item.Name, item.Id) == true)
                    errMessage += "Category " + item.Name + " already exists in database";
                if (errMessage == "")
                {
                    item = _unitRepo.Edit(item);
                    TempData["SuccessMessage"] = item.Name + " saved successfully!";
                    myBool = true;
                }
            }
            catch (Exception exc)
            {
                errMessage += " " + exc.Message;
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
            {
                currentPage = (int)TempData["CurrentPage"];
            }
            if (myBool == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
            {
                return RedirectToAction(nameof(Index), new { pageIndex = currentPage, pageSize = pageSize }); 
            }
        }

        public IActionResult Delete(int id)
        {
            Category category = _unitRepo.GetCategory(id);
            TempData.Keep();
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category item)
        {
            int pageSize = 5;
            if (TempData["PageSize"] != null)
            {
                pageSize = (int)TempData["PageSize"];
            }
            bool myBool = false;
            string errMessage = "";
            try
            {
                if (errMessage == "")
                {
                    item = _unitRepo.Delete(item);
                    TempData["SuccessMessage"] = "Deleted successfully!";
                    myBool = true;
                }
            }
            catch (Exception exc)
            {
                errMessage += " " + exc.Message;
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
            {
                currentPage = (int)TempData["CurrentPage"];
            }
            if (myBool == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
            {
                return RedirectToAction(nameof(Index), new { pageIndex = currentPage, pageSize = pageSize });
            }
        }
    }
}
