namespace TrikProjekt56.Repositories
{
    public class CorpseRepository : ICorpse
    {
        private readonly CaseContext _context;
        public CorpseRepository(CaseContext context)
        {
            _context = context;
        }

        public Corpse Create(Corpse Corpses)
        {
            _context.Corpses.Add(Corpses);
            _context.SaveChanges();
            return Corpses;
        }

        public Corpse Delete(Corpse Corpses)
        {
            _context.Corpses.Attach(Corpses);
            _context.Entry(Corpses).State = EntityState.Deleted;
            _context.SaveChanges();
            return Corpses;
        }

        public Corpse Edit(Corpse Corpses)
        {
            _context.Corpses.Attach(Corpses);
            _context.Entry(Corpses).State = EntityState.Modified;
            _context.SaveChanges();
            return Corpses;
        }
        public Corpse GetItem(int id)
        {
            Corpse item = _context.Corpses.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Corpse> DoSort(List<Corpse> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<Corpse> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Corpse> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Corpses.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Corpses.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Corpse> retItems = new Pagination<Corpse>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Corpses.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Corpses.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
