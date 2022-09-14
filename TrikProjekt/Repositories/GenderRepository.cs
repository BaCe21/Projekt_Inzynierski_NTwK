namespace TrikProjekt56.Repositories
{
    public class GenderRepository : IGender
    {
        private readonly CaseContext _context;
        public GenderRepository(CaseContext context)
        {
            _context = context;
        }

        public Gender Create(Gender Genders)
        {
            _context.Genders.Add(Genders);
            _context.SaveChanges();
            return Genders;
        }

        public Gender Delete(Gender Genders)
        {
            _context.Genders.Attach(Genders);
            _context.Entry(Genders).State = EntityState.Deleted;
            _context.SaveChanges();
            return Genders;
        }

        public Gender Edit(Gender Genders)
        {
            _context.Genders.Attach(Genders);
            _context.Entry(Genders).State = EntityState.Modified;
            _context.SaveChanges();
            return Genders;
        }
        public Gender GetItem(int id)
        {
            Gender item = _context.Genders.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Gender> DoSort(List<Gender> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<Gender> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Gender> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Genders.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Genders.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Gender> retItems = new Pagination<Gender>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Genders.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Genders.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
