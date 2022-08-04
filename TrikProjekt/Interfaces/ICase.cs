namespace TrikProjekt56.Interfaces
{
    public interface ICase
    {
        Pagination<Case> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Case GetItem(string Code);
        Case Create(Case unit);
        Case Edit(Case unit);
        Case Delete(Case unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, string Code);
        public bool IsCodeExisting(string itemCode);
    }
}
