namespace TrikProjekt56.Repositories
{
    public class AgeRepository : IAge
    {
        private readonly CaseContext _context;
        public AgeRepository(CaseContext context)
        {
            _context = context;
        }

        public Age Create(Age Ages)
        {
            _context.Ages.Add(Ages);
            _context.SaveChanges();
            return Ages;
        }

        public Age Delete(Age Ages)
        {
            _context.Ages.Attach(Ages);
            _context.Entry(Ages).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ages;
        }

        public Age Edit(Age Ages)
        {
            _context.Ages.Attach(Ages);
            _context.Entry(Ages).State = EntityState.Modified;
            _context.SaveChanges();
            return Ages;
        }
        public Age GetItem(int id)
        {
            Age item = _context.Ages.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Age> DoSort(List<Age> items, string SortProperty, SortOrder sortOrder)
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
            else
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    items = items.OrderBy(d => d.Description).ToList();
                }
                else
                {
                    items = items.OrderByDescending(d => d.Description).ToList();
                }
            }
            return items;
        }
        public Pagination<Age> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Age> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Ages.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Ages.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Age> retItems = new Pagination<Age>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Ages.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Ages.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
