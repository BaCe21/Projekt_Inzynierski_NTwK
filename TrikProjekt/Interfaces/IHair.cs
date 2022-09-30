namespace TrikProjekt56.Interfaces
{
    public interface IHair
    {
        Pagination<Hair> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Hair GetItem(int id);
        Hair Create(Hair unit);
        Hair Edit(Hair unit);
        Hair Delete(Hair unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
