namespace WaterTrans.Boilerplate.Domain
{
    public class PagingQuery
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 20;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
    }
}
