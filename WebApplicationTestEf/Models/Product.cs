using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplicationTestEf.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
    }
}
