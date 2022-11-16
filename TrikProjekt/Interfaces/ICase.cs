namespace TrikProjekt56.Interfaces
{
    public interface ICase
    {
        Pagination<Case> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        public List<Case> GetAllItems();
        public List<Case> GetCategiores();
        public List<Case> GetLocations();
        public List<Case> GetAges();
        public List<Case> GetHairs();
        public List<Case> GetGenders();
        public List<Case> GetReligions();
        public List<Case> GetEducations();
        public List<Case> GetHeights();
        public List<Case> GetWeights();
        Case GetItem(string Code);
        Case Create(Case unit);
        Case Edit(Case unit);
        Case Delete(Case unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, string Code);
        public bool IsCodeExisting(string itemCode);
    }
}
