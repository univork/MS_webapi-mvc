using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleMVCApp.DTO
{
    public class ProductReadDTO : ProductDTO
    {
        public int Id { get; set; }
        public virtual CategoryDTO? Category { get; set; }
    }
}
