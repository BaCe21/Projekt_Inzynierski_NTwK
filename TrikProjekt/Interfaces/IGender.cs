namespace TrikProjekt56.Interfaces
{
    public interface IGender
    {
        Pagination<Gender> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Gender GetItem(int id);
        Gender Create(Gender unit);
        Gender Edit(Gender unit);
        Gender Delete(Gender unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
