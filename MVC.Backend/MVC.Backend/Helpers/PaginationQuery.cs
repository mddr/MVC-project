using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Helpers
{
    public class PaginationQuery<T>
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public IEnumerable<T> Paginate(IEnumerable<T> items) => items.Skip(Page * Size).Take(Size);        
    }
}
