﻿using System;

namespace FridgeApi.Models
{
    public class FridgeModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public short Year { get; set; }
    }
}