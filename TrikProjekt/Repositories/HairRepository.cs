namespace TrikProjekt56.Repositories
{
    public class HairRepository : IHair
    {
        private readonly CaseContext _context;
        public HairRepository(CaseContext context)
        {
            _context = context;
        }

        public Hair Create(Hair Hairs)
        {
            _context.Hairs.Add(Hairs);
            _context.SaveChanges();
            return Hairs;
        }

        public Hair Delete(Hair Hairs)
        {
            _context.Hairs.Attach(Hairs);
            _context.Entry(Hairs).State = EntityState.Deleted;
            _context.SaveChanges();
            return Hairs;
        }

        public Hair Edit(Hair Hairs)
        {
            _context.Hairs.Attach(Hairs);
            _context.Entry(Hairs).State = EntityState.Modified;
            _context.SaveChanges();
            return Hairs;
        }
        public Hair GetItem(int id)
        {
            Hair item = _context.Hairs.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Hair> DoSort(List<Hair> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<Hair> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Hair> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Hairs.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Hairs.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Hair> retItems = new Pagination<Hair>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Hairs.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Hairs.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
