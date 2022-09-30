namespace TrikProjekt56.Repositories
{
    public class HeightRepository : IHeight
    {
        private readonly CaseContext _context;
        public HeightRepository(CaseContext context)
        {
            _context = context;
        }

        public Height Create(Height Heights)
        {
            _context.Heights.Add(Heights);
            _context.SaveChanges();
            return Heights;
        }

        public Height Delete(Height Heights)
        {
            _context.Heights.Attach(Heights);
            _context.Entry(Heights).State = EntityState.Deleted;
            _context.SaveChanges();
            return Heights;
        }

        public Height Edit(Height Heights)
        {
            _context.Heights.Attach(Heights);
            _context.Entry(Heights).State = EntityState.Modified;
            _context.SaveChanges();
            return Heights;
        }
        public Height GetItem(int id)
        {
            Height item = _context.Heights.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Height> DoSort(List<Height> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<Height> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Height> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Heights.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Heights.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Height> retItems = new Pagination<Height>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Heights.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Heights.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
