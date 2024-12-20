﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int Speed { get; set; }

        public float Acceleration { get; set; }

        public float FuelConsumption { get; set; }

        public int Clearance {  get; set; }

        public decimal Price { get; set; }

        public byte[]? Avatar { get; set; }
    }
}
