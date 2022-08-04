namespace TrikProjekt56.Repositories
{
    public class CaseRepository : ICase
    {
        private readonly CaseContext _context;
        public CaseRepository(CaseContext context)
        {
            _context = context;
        }

        public Case Create(Case Cases)
        {
            _context.Cases.Add(Cases);
            _context.SaveChanges();
            return Cases;
        }

        public Case Delete(Case Case)
        {
            Case = pGetItem(Case.Code);
            _context.Cases.Attach(Case);
            _context.Entry(Case).State = EntityState.Deleted;
            _context.SaveChanges();
            return Case;
        }

        public Case Edit(Case Cases)
        {
            _context.Cases.Attach(Cases);
            _context.Entry(Cases).State = EntityState.Modified;
            _context.SaveChanges();
            return Cases;
        }
        public Case GetItem(string Code)
        {
            Case item = _context.Cases.Where(c => c.Code == Code).Include(u => u.Categories).FirstOrDefault();
            return item;
        }
        public Case pGetItem(string Code)
        {
            Case item = _context.Cases.Where(c => c.Code == Code).FirstOrDefault();
            return item;
        }
        private List<Case> DoSort(List<Case> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    items = items.OrderBy(n => n.Name).ToList();
                }
                else
                {
                    items = items.OrderByDescending(n => n.Name).ToList();
                }
            }
            else if (SortProperty.ToLower() == "code")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.Code).ToList();
                else
                    items = items.OrderByDescending(n => n.Code).ToList();
            }
            else if (SortProperty.ToLower() == "startdate")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.StartDate).ToList();
                else
                    items = items.OrderByDescending(n => n.StartDate).ToList();
            }
            else if (SortProperty.ToLower() == "isclosed")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.isClosed).ToList();
                else
                    items = items.OrderByDescending(n => n.isClosed).ToList();
            }
            else if (SortProperty.ToLower() == "categoryid")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.CategoryId).ToList();
                else
                    items = items.OrderByDescending(n => n.CategoryId).ToList();
            }
            return items;
        }
        public Pagination<Case> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Case> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Cases.Where(n => n.Name.Contains(SearchText) || n.Code.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Cases.Include(u => u.Categories).ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Case> retItems = new Pagination<Case>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Cases.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, string Code)
        {
            int ct = _context.Cases.Where(n => n.Name.ToLower() == name.ToLower() && n.Code != Code).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsCodeExisting(string itemCode)
        {
            int ct = _context.Cases.Where(n => n.Code.ToLower() == itemCode.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
