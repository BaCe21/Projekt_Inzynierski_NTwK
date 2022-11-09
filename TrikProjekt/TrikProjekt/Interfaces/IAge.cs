namespace TrikProjekt56.Interfaces
{
    public interface IAge
    {
        Pagination<Age> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Age GetItem(int id);
        Age Create(Age unit);
        Age Edit(Age unit);
        Age Delete(Age unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
