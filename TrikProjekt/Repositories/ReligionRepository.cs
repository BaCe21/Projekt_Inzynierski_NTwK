namespace TrikProjekt56.Repositories
{
    public class ReligionRepository : IReligion
    {
        private readonly CaseContext _context;
        public ReligionRepository(CaseContext context)
        {
            _context = context;
        }

        public Religion Create(Religion Religions)
        {
            _context.Religions.Add(Religions);
            _context.SaveChanges();
            return Religions;
        }

        public Religion Delete(Religion Religions)
        {
            _context.Religions.Attach(Religions);
            _context.Entry(Religions).State = EntityState.Deleted;
            _context.SaveChanges();
            return Religions;
        }

        public Religion Edit(Religion Religions)
        {
            _context.Religions.Attach(Religions);
            _context.Entry(Religions).State = EntityState.Modified;
            _context.SaveChanges();
            return Religions;
        }
        public Religion GetItem(int id)
        {
            Religion item = _context.Religions.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Religion> DoSort(List<Religion> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<Religion> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Religion> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Religions.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Religions.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Religion> retItems = new Pagination<Religion>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Religions.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Religions.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
