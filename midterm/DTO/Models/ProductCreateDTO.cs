﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class ProductCreateDTO : ProductDTO
    {
        public int CategoryId { get; set; }
    }
}
