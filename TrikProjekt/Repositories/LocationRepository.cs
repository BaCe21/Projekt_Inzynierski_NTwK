namespace TrikProjekt56.Repositories
{
    public class LocationRepository : ILocation
    {
        private readonly CaseContext _context;
        public LocationRepository(CaseContext context)
        {
            _context = context;
        }

        public Location Create(Location locations)
        {
            _context.Locations.Add(locations);
            _context.SaveChanges();
            return locations;
        }

        public Location Delete(Location locations)
        {
            _context.Locations.Attach(locations);
            _context.Entry(locations).State = EntityState.Deleted;
            _context.SaveChanges();
            return locations;
        }

        public Location Edit(Location locations)
        {
            _context.Locations.Attach(locations);
            _context.Entry(locations).State = EntityState.Modified;
            _context.SaveChanges();
            return locations;
        }
        public Location GetItem(int id)
        {
            Location item = _context.Locations.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Location> DoSort(List<Location> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<Location> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Location> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Locations.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Locations.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Location> retItems = new Pagination<Location>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Locations.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Locations.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
