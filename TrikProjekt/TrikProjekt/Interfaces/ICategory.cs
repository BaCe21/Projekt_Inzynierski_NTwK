namespace TrikProjekt56.Interfaces
{
    public interface ICategory
    {
        Pagination<Category> GetItems(string SortProperty, SortOrder sortOrder, string SearchText="", int pageIndex = 1, int pageSize = 5);
        Category GetCategory(int id);
        Category Create(Category category);
        Category Edit(Category category);
        Category Delete(Category category);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
