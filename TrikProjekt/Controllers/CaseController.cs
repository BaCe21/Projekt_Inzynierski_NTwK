using System.IO;
using System.Text;

namespace TrikProjekt56.Controllers
{
    [Authorize]
    public class CaseController : Controller
    {
        private void AddViewbags()
        {
            ViewBag.Categories = GetCategories();
            ViewBag.Genders = GetGenders();
            ViewBag.Locations = GetLocations();
            ViewBag.Ages = GetAges();
            ViewBag.Hairs = GetHairs();
            ViewBag.Religions = GetReligions();
            ViewBag.Educations = GetEducations();
            ViewBag.Heights = GetHeights();
            ViewBag.Weights = GetWeights();
        }

        private readonly ICase _repo;
        private readonly ICategory _categoryrepo;
        private readonly ILocation _locationrepo;
        private readonly IAge _agerepo;
        private readonly IHair _hairrepo;
        private readonly IGender _genderrepo;
        private readonly IReligion _religionrepo;
        private readonly IEducation _educationrepo;
        private readonly IWeight _weightrepo;
        private readonly IHeight _heightrepo;

        public CaseController(ICase repo, ICategory categoryrepo, ILocation locationrepo, IAge agerepo, IHair hairrepo, IGender genderrepo, IReligion religionrepo, IEducation educationrepo, IWeight weightrepo, IHeight heightrepo)
        {
            _repo = repo;
            _categoryrepo = categoryrepo;
            _locationrepo = locationrepo;
            _agerepo = agerepo;
            _hairrepo = hairrepo;
            _genderrepo = genderrepo;
            _religionrepo = religionrepo;
            _educationrepo = educationrepo;
            _heightrepo = heightrepo;
            _weightrepo = weightrepo;
        }

        public IActionResult ShowCaseData()
        {

            var caseCount = _repo.GetAllItems();
            ViewBag.Cases = caseCount.Count();
            ViewBag.Admins = Admins.admins.Count();

            //Category data
            List<Case> catitems = _repo.GetCategiores();
            var catlabel = catitems.Select(x => x.Categories.Name).Distinct().ToList();
            List<int> catdata = new List<int>();
            var counter = catitems.Select(x => x.Categories).Distinct();
            foreach (var category in counter)
            {
                catdata.Add(catitems.Count(x => x.Categories == category));
            }
            ViewBag.Category = catlabel;
            ViewBag.Categories = catdata;

            //Location data
            List<Case> locitems = _repo.GetLocations();
            var loclabel = locitems.Select(l => l.Locations.Name).Distinct().ToList();
            List<int> locdata = new List<int>();
            var counter2 = locitems.Select(x => x.Locations).Distinct();
            foreach (var location in counter2)
            {
                locdata.Add(locitems.Count(x => x.Locations == location));
            }
            ViewBag.Location = loclabel;
            ViewBag.Locations = locdata;

            //Age data
            List<Case> ageitems = _repo.GetAges();
            var agelabel = ageitems.Select(l => l.Ages.Name).Distinct().ToList();
            List<int> agedata = new List<int>();
            var counter3 = ageitems.Select(x => x.Ages).Distinct();
            foreach (var age in counter3)
            {
                agedata.Add(ageitems.Count(x => x.Ages == age));
            }
            ViewBag.Age = agelabel;
            ViewBag.Ages = agedata;


            //Hair data
            List<Case> hairitems = _repo.GetHairs();
            var hairlabel = hairitems.Select(l => l.Hairs.Name).Distinct().ToList();
            List<int> hairdata = new List<int>();
            var counter4 = hairitems.Select(x => x.Hairs).Distinct();
            foreach (var hair in counter4)
            {
                hairdata.Add(hairitems.Count(x => x.Hairs == hair));
            }
            ViewBag.Hair = hairlabel;
            ViewBag.Hairs = hairdata;

            //Gender data
            List<Case> genitems = _repo.GetGenders();
            var genlabel = genitems.Select(l => l.Genders.Name).Distinct().ToList();
            List<int> gendata = new List<int>();
            var counter5 = genitems.Select(x => x.Genders).Distinct();
            foreach (var gender in counter5)
            {
                gendata.Add(genitems.Count(x => x.Genders == gender));
            }
            ViewBag.Gender = genlabel;
            ViewBag.Genders = gendata;

            //Religion data
            List<Case> relitems = _repo.GetReligions();
            var rellabel = relitems.Select(l => l.Religions.Name).Distinct().ToList();
            List<int> reldata = new List<int>();
            var counter6 = relitems.Select(x => x.Religions).Distinct();
            foreach (var religion in counter6)
            {
                reldata.Add(relitems.Count(x => x.Religions == religion));
            }
            ViewBag.Religion = rellabel;
            ViewBag.Religions = reldata;

            //Education data
            List<Case> eduitems = _repo.GetEducations();
            var edulabel = eduitems.Select(l => l.Educations.Name).Distinct().ToList();
            List<int> edudata = new List<int>();
            var counter7 = eduitems.Select(x => x.Educations).Distinct();
            foreach (var education in counter7)
            {
                edudata.Add(eduitems.Count(x => x.Educations == education));
            }
            ViewBag.Education = edulabel;
            ViewBag.Educations = edudata;

            //Height data
            List<Case> heiitems = _repo.GetHeights();
            var heilabel = heiitems.Select(l => l.Heights.Name).Distinct().ToList();
            List<int> heidata = new List<int>();
            var counter8 = heiitems.Select(x => x.Heights).Distinct();
            foreach (var height in counter8)
            {
                heidata.Add(heiitems.Count(x => x.Heights == height));
            }
            ViewBag.Height = heilabel;
            ViewBag.Heights = heidata;

            //Weight data
            List<Case> weiitems = _repo.GetWeights();
            var weilabel = weiitems.Select(l => l.Weights.Name).Distinct().ToList();
            List<int> weidata = new List<int>();
            var counter9 = weiitems.Select(x => x.Weights).Distinct();
            foreach (var weight in counter9)
            {
                weidata.Add(weiitems.Count(x => x.Weights == weight));
            }
            ViewBag.Weight = weilabel;
            ViewBag.Weights = weidata;

            //Generate View
            return View();
        }

        public IActionResult Index(string sortExpression = "", string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Code");
            sortModel.AddColumn("name");
            sortModel.AddColumn("startdate");
            sortModel.AddColumn("isClosed");
            sortModel.AddColumn("CategoryId");
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
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Create(Case item)
        {
            AddViewbags();
            TempData.Keep();
            bool myBool = false;
            string errMessage = "";
            int pageSize = 5;
            if (TempData["PageSize"] != null)
            {
                pageSize = (int)TempData["PageSize"];
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
            {
                currentPage = (int)TempData["CurrentPage"];
            }
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
                return RedirectToAction(nameof(Index), new { pageIndex = currentPage, pageSize = pageSize });
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
                return RedirectToAction(nameof(Index), new { pageIndex = currentPage , pageSize = pageSize});
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
        public FileResult ExportToCSV()
        {
            string[] columnNames = new string[] { "Code", "Name", "StartDate", "isClosed", "Categories", "Locations", "Ages", "Hairs", "Genders", "Religions", "Educations", "Heights", "Weights" };
            var Cases = _repo.GetAllItems();
            string csv = string.Empty;
            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }
            csv = csv.Remove(csv.Length - 1);
            csv += "\r\n";
            foreach (var idcase in Cases)
            {
                csv += idcase.Code.ToString().Replace(",", ";") + ',';
                csv += idcase.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.StartDate.ToString("MM/dd/yyyy").Replace(",", ";") + ',';
                csv += idcase.isClosed.ToString().Replace(",", ";") + ',';
                csv += idcase.Categories.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.Locations.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.Ages.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.Hairs.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.Genders.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.Religions.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.Educations.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.Heights.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.Weights.Name.ToString().Replace(",", ";");

                csv += "\r\n";
            }
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "Cases.csv");
        }
        public FileResult ExportToCSVNumeric()
        {
            string[] columnNames = new string[] { "Code", "Name", "StartDate", "isClosed", "Categories", "Locations", "Ages", "Hairs", "Genders", "Religions", "Educations", "Heights", "Weights" };
            var Cases = _repo.GetAllItems();
            string csv = string.Empty;
            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }
            csv += "\r\n";
            foreach (var idcase in Cases)
            {
                csv += idcase.Code.ToString().Replace(",", ";") + ',';
                csv += idcase.Name.ToString().Replace(",", ";") + ',';
                csv += idcase.StartDate.ToString("MM/dd/yyyy").Replace(",", ";") + ',';
                csv += idcase.isClosed.ToString().Replace(",", ";") + ',';
                csv += idcase.CategoryId.ToString().Replace(",", ";") + ',';
                csv += idcase.LocationId.ToString().Replace(",", ";") + ',';
                csv += idcase.AgeId.ToString().Replace(",", ";") + ',';
                csv += idcase.HairId.ToString().Replace(",", ";") + ',';
                csv += idcase.GenderId.ToString().Replace(",", ";") + ',';
                csv += idcase.ReligionId.ToString().Replace(",", ";") + ',';
                csv += idcase.EducationId.ToString().Replace(",", ";") + ',';
                csv += idcase.HeightId.ToString().Replace(",", ";") + ',';
                csv += idcase.WeightId.ToString().Replace(",", ";") + ',';

                csv += "\r\n";
            }
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "CasesNumeric.csv");
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
        private List<SelectListItem> GetGenders()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Gender> items = _genderrepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select distinguish gender"
            };
            IsUnits.Insert(0, defaultitem);
            return IsUnits;
        }
        private List<SelectListItem> GetReligions()
        {
            var IsUnits = new List<SelectListItem>();
            Pagination<Religion> items = _religionrepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            IsUnits = items.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            var defaultitem = new SelectListItem()
            {
                Value = "",
                Text = "Select religion"
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
