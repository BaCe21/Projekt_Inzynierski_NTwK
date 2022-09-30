namespace TrikProjekt56.Interfaces
{
    public interface IEducation
    {
        Pagination<Education> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Education GetItem(int id);
        Education Create(Education unit);
        Education Edit(Education unit);
        Education Delete(Education unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
