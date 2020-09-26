using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.Common.Models
{
    public class PagedList<T>
    {
        public PagedList(List<T> t, int pageSize, int pageNumber)
        {
            Items = t;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Count = t != null ? t.Count() : 0;
        }
        public IList<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
    }
}
