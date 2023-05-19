using ECommerce_Demo_Core.Entities;

namespace Web_API_ECommerce_Demo.Helpers
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex,int pageCount,int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageCount = pageCount;
            Count = count;
            Data = data;
        }
    }
}
