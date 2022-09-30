namespace TrikProjekt56.Interfaces
{
    public interface IWeight
    {
        Pagination<Weight> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Weight GetItem(int id);
        Weight Create(Weight unit);
        Weight Edit(Weight unit);
        Weight Delete(Weight unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
