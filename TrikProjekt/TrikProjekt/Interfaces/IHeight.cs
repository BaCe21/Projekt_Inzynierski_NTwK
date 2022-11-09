namespace TrikProjekt56.Interfaces
{
    public interface IHeight
    {
        Pagination<Height> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Height GetItem(int id);
        Height Create(Height unit);
        Height Edit(Height unit);
        Height Delete(Height unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
