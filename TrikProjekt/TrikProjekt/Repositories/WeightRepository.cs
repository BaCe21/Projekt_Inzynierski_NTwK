namespace TrikProjekt56.Repositories
{
    public class WeightRepository : IWeight
    {
        private readonly CaseContext _context;
        public WeightRepository(CaseContext context)
        {
            _context = context;
        }

        public Weight Create(Weight Weights)
        {
            _context.Weights.Add(Weights);
            _context.SaveChanges();
            return Weights;
        }

        public Weight Delete(Weight Weights)
        {
            _context.Weights.Attach(Weights);
            _context.Entry(Weights).State = EntityState.Deleted;
            _context.SaveChanges();
            return Weights;
        }

        public Weight Edit(Weight Weights)
        {
            _context.Weights.Attach(Weights);
            _context.Entry(Weights).State = EntityState.Modified;
            _context.SaveChanges();
            return Weights;
        }
        public Weight GetItem(int id)
        {
            Weight item = _context.Weights.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Weight> DoSort(List<Weight> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<Weight> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Weight> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Weights.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Weights.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Weight> retItems = new Pagination<Weight>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Weights.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Weights.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
