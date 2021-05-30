namespace WaterTrans.Boilerplate.Domain.DataTransferObjects
{
    public class ForecastQueryDto : PagingQuery
    {
        public string Query { get; set; }
        public SortOrder Sort { get; set; }
    }
}
