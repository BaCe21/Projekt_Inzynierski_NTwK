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
        public SortModel ApplySort(string sortExpression)
        {
            ViewData["SortParamName"] = "name";
            ViewData["SortParamDesc"] = "description";
            ViewData["SortIconName"] = "";
            ViewData["SortIconDesc"] = "";
            SortModel sortModel = new SortModel();
            switch (sortExpression.ToLower())
            {
                case "name_desc":
                    sortModel.SortedOrder = SortOrder.Descending;
                    sortModel.SortedProperty = "name";
                    ViewData["SortParamName"] = "name";
                    ViewData["SortIconName"] = "";
                    break;
                case "description":
                    sortModel.SortedOrder = SortOrder.Ascending;
                    sortModel.SortedProperty = "description";
                    ViewData["SortParamDesc"] = "description_desc";
                    ViewData["SortIconDesc"] = "fa fa-arrow-down";
                    break;
                case "description_desc":
                    sortModel.SortedOrder = SortOrder.Descending;
                    sortModel.SortedProperty = "description";

                    ViewData["SortParamDesc"] = "description";
                    ViewData["SortIconDesc"] = "fa fa-arrow-up";
                    break;
                default:
                    sortModel.SortedOrder = SortOrder.Ascending;
                    sortModel.SortedProperty = "name";
                    ViewData["SortIconName"] = "fa fa-arrow-down";
                    ViewData["SortParamName"] = "name_desc";
                    break;
            }
            return sortModel;
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
        public IActionResult Create(Category category)
        {
            bool myBool = false;
            string errMessage = "";
            try
            {
                if (category.Name.Length < 3 || category.Name == null)
                    errMessage = "Category name must be at least 3 characters";
                if (_unitRepo.IsExisting(category.Name) == true)
                    errMessage = "Name " + category.Name + " already exists in database";
                if (errMessage == "")
                {
                    category = _unitRepo.Create(category);
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
                return View(category);
            }
            else
            {
                TempData["SuccessMessage"] = "Category " + category.Name + " created successfully!";
                return RedirectToAction(nameof(Index));
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
        public IActionResult Edit(Category category)
        {
            bool myBool = false;
            string errMessage = "";
            try
            {
                if (category.Name.Length < 3 || category.Name == null)
                    errMessage = "Category name must be at least 3 characters";
                if (_unitRepo.IsExisting(category.Name, category.Id) == true)
                    errMessage += "Category " + category.Name + " already exists in database";
                if (errMessage == "")
                {
                    category = _unitRepo.Edit(category);
                    TempData["SuccessMessage"] = "Category " + category.Name + " saved successfully!";
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
                return View(category);
            }
            else
            {
                return RedirectToAction(nameof(Index), new { pageIndex = currentPage });
            }
        }

        public IActionResult Delete(int id)
        {
            Category category = _unitRepo.GetCategory(id);
            TempData.Keep();
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            try
            {
                category = _unitRepo.Delete(category);
            }
            catch(Exception exc)
            {
                string errMessage = exc.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(category);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
            {
                currentPage = (int)TempData["CurrentPage"];
            }
            TempData["SuccessMessage"] = "Category " + category.Name + " deleted successfully!";
            return RedirectToAction(nameof(Index), new { pageIndex = currentPage });
        }
    }
}
