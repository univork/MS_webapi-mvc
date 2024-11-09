using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class ProductReadDTO : ProductDTO
    {
        public int Id { get; set; }
        public virtual CategoryDTO? Category { get; set; }
    }
}
