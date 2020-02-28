using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndyBooks.Models
{
    public class TotalValueService
    {
        public decimal calculatePageValue(IEnumerable<Book> collection)
        {
            decimal total = 0;
                
            foreach (Book b in collection)
            {
                total += b.Price;
            }
            return total;
        }
    }
}
