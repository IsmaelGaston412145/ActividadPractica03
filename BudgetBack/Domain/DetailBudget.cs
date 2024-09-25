using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBack.Domain
{
    public class DetailBudget
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }

        public float SubTotal()
        {
            return Price * Count;
        }

    }
}
