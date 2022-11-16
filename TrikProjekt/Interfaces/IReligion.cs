namespace TrikProjekt56.Interfaces
{
    public interface IReligion
    {
        Pagination<Religion> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Religion GetItem(int id);
        Religion Create(Religion unit);
        Religion Edit(Religion unit);
        Religion Delete(Religion unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
