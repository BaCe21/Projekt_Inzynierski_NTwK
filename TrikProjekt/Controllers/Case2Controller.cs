namespace TrikProjekt56.Controllers
{
    [Authorize]
    public class Case2Controller : Controller
    {
        private void AddViewbags()
        {
            ViewBag.Categories = GetCategories();
            ViewBag.Features = GetDistFeatures();
            ViewBag.Locations = GetLocations();
            ViewBag.Ages = GetAges();
            ViewBag.Hairs = GetHairs();
            ViewBag.Corpses = GetCorpses();
            ViewBag.Educations = GetEducations();
            ViewBag.Heights = GetHeights();
            ViewBag.Weights = GetWeights();
        }

        private readonly ICase _repo;
        private readonly ICategory _categoryrepo;
        private readonly ILocation _locationrepo;
        private readonly IAge _agerepo;
        private readonly IHair _hairrepo;
        private readonly IFeature _featurerepo;
        private readonly ICorpse _corpserepo;
        private readonly IEducation _educationrepo;
        private readonly IWeight _weightrepo;
        private readonly IHeight _heightrepo;

        public Case2Controller(ICase repo, ICategory categoryrepo, ILocation locationrepo, IAge agerepo, IHair hairrepo, IFeature featurerepo, ICorpse corpserepo, IEducation educationrepo, IWeight weightrepo, IHeight heightrepo)
        {
            _repo = repo;
            _categoryrepo = categoryrepo;
            _locationrepo = locationrepo;
            _agerepo = agerepo;
            _hairrepo = hairrepo;
            _featurerepo = featurerepo;
            _corpserepo = corpserepo;
            _educationrepo = educationrepo;
            _heightrepo = heightrepo;
            _weightrepo = weightrepo;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Code");
            sortModel.AddColumn("name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            Pagination<Case> items = _repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pageIndex, pageSize);
            var site = new SiteModel(items.TotalRecords, pageIndex, pageSize);
            site.SortExpression = sortExpression;
            this.ViewBag.Site = site;
            TempData["CurrentPage"] = pageIndex;
            TempData["PageSize"] = pageSize;
            return View(items);
        }

        public IActionResult Create()
        {
            Case item = new Case();
            AddViewbags();
            return View(item);
        }

        [HttpPost]
        public IActionResult Create(Case item)
        {
            AddViewbags();
            bool myBool = false;
            string errMessage = "";
            try
            {
                if (item.Name.Length < 3 || item.Name == null)
                    errMessage = "Name must be at least 3 characters";
                if (_repo.IsCodeExisting(item.Code) == true)
                    errMessage = "Code " + item.Code + " already exists in database";
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
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Details(string id)
        {
            Case item = _repo.GetItem(id);
            AddViewbags();
            TempData.Keep();
            return View(item);
        }
        public IActionResult Edit(string id)
        {
            Case item = _repo.GetItem(id);
            AddViewbags();
            TempData.Keep();
            return View(item);
        }


        [HttpPost]
        public IActionResult Edit(Case item)
        {
            AddViewbags();
            bool myBool = false;
            string errMessage = "";
            try
            {
                if (item.Name.Length < 3 || item.Name == null)
                    errMessage = "Name must be at least 3 characters";
                if (_repo.IsExisting(item.Name, item.Code) == true)
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
            int pageSize = 5;
            if (TempData["PageSize"] != null)
            {
                pageSize = (int)TempData["PageSize"];
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

        public IActionResult Delete(string id)
        {
            Case item = _repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(Case item)
        {
            try
            {
                item = _repo.Delete(item);
            }
            catch (Exception exc)
            {
                string errMessage = exc.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
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
            TempData["SuccessMessage"] = item.Name + " deleted successfully!";
            return RedirectToAction(nameof(Index), new { pageIndex = currentPage, pageSize = pageSize });
        }
        private List<SelectListItem> GetCategories()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Category> categories = _categoryrepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = categories.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select Item"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetLocations()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Location> items = _locationrepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select location"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetAges()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Age> items = _agerepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select age"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetHairs()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Hair> items = _hairrepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select hair"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetDistFeatures()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<DistFeature> items = _featurerepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select distinguish feature"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetCorpses()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Corpse> items = _corpserepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select other/body"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetEducations()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Education> items = _educationrepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select education"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetHeights()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Height> items = _heightrepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select education"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetWeights()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Weight> items = _weightrepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select education"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
    }
}
