namespace TrikProjekt56.Repositories
{
    public class EducationRepository : IEducation
    {
        private readonly CaseContext _context;
        public EducationRepository(CaseContext context)
        {
            _context = context;
        }

        public Education Create(Education Educations)
        {
            _context.Educations.Add(Educations);
            _context.SaveChanges();
            return Educations;
        }

        public Education Delete(Education Educations)
        {
            _context.Educations.Attach(Educations);
            _context.Entry(Educations).State = EntityState.Deleted;
            _context.SaveChanges();
            return Educations;
        }

        public Education Edit(Education Educations)
        {
            _context.Educations.Attach(Educations);
            _context.Entry(Educations).State = EntityState.Modified;
            _context.SaveChanges();
            return Educations;
        }
        public Education GetItem(int id)
        {
            Education item = _context.Educations.Where(c => c.Id == id).FirstOrDefault();
            return item;
        }
        private List<Education> DoSort(List<Education> items, string SortProperty, SortOrder sortOrder)
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
        public Pagination<Education> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Education> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Educations.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Educations.ToList();
            }
            items = DoSort(items, SortProperty, sortOrder);
            Pagination<Education> retItems = new Pagination<Education>(items, pageIndex, pageSize);
            return retItems;
        }
        public bool IsExisting(string name)
        {
            int ct = _context.Educations.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsExisting(string name, int Id)
        {
            int ct = _context.Educations.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
