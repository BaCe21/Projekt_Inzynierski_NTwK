namespace TrikProjekt56.Interfaces
{
    public interface ILocation
    {
        Pagination<Location> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Location GetItem(int id);
        Location Create(Location unit);
        Location Edit(Location unit);
        Location Delete(Location unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
