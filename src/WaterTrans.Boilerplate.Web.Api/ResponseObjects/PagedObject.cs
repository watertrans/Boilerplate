using System.Collections.Generic;

namespace WaterTrans.Boilerplate.Web.Api.ResponseObjects
{
    public class PagedObject<TObject>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long Total { get; set; }
        public List<TObject> Items { get; set; } = new List<TObject>();
    }
}
