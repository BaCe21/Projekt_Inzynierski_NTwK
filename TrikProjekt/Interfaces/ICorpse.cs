namespace TrikProjekt56.Interfaces
{
    public interface ICorpse
    {
        Pagination<Corpse> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Corpse GetItem(int id);
        Corpse Create(Corpse unit);
        Corpse Edit(Corpse unit);
        Corpse Delete(Corpse unit);
        public bool IsExisting(string name);
        public bool IsExisting(string name, int Id);
    }
}
