namespace TrikProjekt56.Models
{
    public class Pagination<T> : List<T>
    {
        public int TotalRecords { get; set; }
        public Pagination(List<T> source, int pageIndex, int pageSize)
        {
            TotalRecords = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            this.AddRange(items);
        }
    }
}
