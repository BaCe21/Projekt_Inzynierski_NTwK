namespace TrikProjekt56.Controllers
{
    [Authorize]
    public class AgeController : Controller
    {
        [AcceptVerbs("Get","Post")]
        public JsonResult IsNameExisting(string Name, int Id=0)
        {
            bool existing = _repo.IsExisting(Name, Id);
            if (existing)
                return Json(data: false);
            else
                return Json(data: true);
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            Pagination<Age> items = _repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pageIndex, pageSize);
            var site = new SiteModel(items.TotalRecords, pageIndex, pageSize);
            site.SortExpression = sortExpression;
            this.ViewBag.Site = site;
            TempData["CurrentPage"] = pageIndex;
            TempData["PageSize"] = pageSize;
            return View(items);
        }

        private readonly IAge _repo;

        public AgeController(IAge repo)
        {
            _repo = repo;
        }

        public IActionResult Create()
        {
            Age item = new Age();
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Create(Age item)
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
                    errMessage = "Name must be at least 3 characters";
                if (_repo.IsExisting(item.Name) == true)
                    errMessage = "Name " + item.Name + " already exists in database";
                if (errMessage == "")
                {
                    item = _repo.Create(item);
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
            Age item = _repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            Age item = _repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Age item)
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
                    errMessage = "Name must be at least 3 characters";
                if (_repo.IsExisting(item.Name, item.Id) == true)
                    errMessage += item.Name + " already exists in database";
                if (errMessage == "")
                {
                    item = _repo.Edit(item);
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
            Age item = _repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(Age item)
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
                    item = _repo.Delete(item);
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