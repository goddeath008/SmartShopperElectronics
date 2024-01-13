using System;
namespace XTL.Helpers
{
    public class PagingModel
    {
        public int currentPage {  get; set; }
        public int countPage{ get; set; }
        public Func<int?,string > generaUrl { get; set; }
    }
}
