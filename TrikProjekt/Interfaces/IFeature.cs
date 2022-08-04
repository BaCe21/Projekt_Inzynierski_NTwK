namespace TrikProjekt56.Interfaces
{
    public interface IFeature
    {
        Pagination<DistFeature> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        DistFeature GetItem(int id);
        DistFeature Create(DistFeature unit);
        DistFeature Edit(DistFeature unit);
        DistFeature Delete(DistFeature unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
