﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EF.Models
{
    public class Cart
    {
        [ForeignKey("User")]
        public int ID { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
        public virtual List<CartProduct> CartProduct { get; set; }
    }
}