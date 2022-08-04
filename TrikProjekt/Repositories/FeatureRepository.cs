namespace TrikProjekt56.Repositories
{
    public class FeatureRepository : IFeature
    {
        private readonly CaseContext _context;
        public FeatureRepository(CaseContext context)
        {
            _context = context;
        }

        public DistFeature Create(DistFeature Features)
        {
            _context.DistFeatures.Add(Features);
            _context.SaveChanges();
            return Features;
        }

        public DistFeature Delete(DistFeature Features)
        {
            _context.DistFeatures.Attach(Features);
            _context.Entry(Features).State = EntityState.Deleted;
            _context.SaveChanges();
            return Features;
        }

        public DistFeature Edit(DistFeature Features)
        {
            _context.DistFeatures.Attach(Features);
            _context.Entry(Features).State = EntityState.Modified;
            _context.SaveChanges();
            return Features;
        }
        public DistFeature GetItem(int id)
        {
            DistFeature item = _context.DistFeatures.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<DistFeature> DoSort(List<DistFeature> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<DistFeature> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<DistFeature> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.DistFeatures.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.DistFeatures.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<DistFeature> retItems = new Pagination<DistFeature>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.DistFeatures.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.DistFeatures.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
