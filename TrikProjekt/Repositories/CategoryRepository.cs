namespace TrikProjekt56.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly CaseContext _context;
        public CategoryRepository(CaseContext context)
        {
            _context = context;
        }

        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(Category category)
        {
            _context.Categories.Attach(category);
            _context.Entry(category).State = EntityState.Deleted;
            _context.SaveChanges();
            return category;
        }

        public Category Edit(Category category)
        {
            _context.Categories.Attach(category);
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return category;
        }
        public Category GetCategory(int id)
        {
            Category category = _context.Categories.Where(c => c.Id == id).FirstOrDefault();
            return category;
        }
        private List<Category> DoSort(List<Category> categories, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    categories = categories.OrderBy(n => n.Name).ToList();
                }
                else
                {
                    categories = categories.OrderByDescending(n => n.Name).ToList();
                }
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    categories = categories.OrderBy(d => d.Description).ToList();
                }
                else
                {
                    categories = categories.OrderByDescending(d => d.Description).ToList();
                }
            }
            return categories;
        }
        public Pagination<Category> GetItems(string SortProperty, SortOrder sortOrder, string SearchText="", int pageIndex=1, int pageSize=5)
        {
            List<Category> categories;
            if (SearchText != "" && SearchText != null)
            {
                categories = _context.Categories.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                categories = _context.Categories.ToList();
            }
            categories = DoSort(categories, SortProperty, sortOrder);
            Pagination<Category> retCategories = new Pagination<Category>(categories, pageIndex, pageSize);
            return retCategories;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Categories.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Categories.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
