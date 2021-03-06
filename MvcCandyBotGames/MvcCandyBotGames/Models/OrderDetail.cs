//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcCandyBotGames.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class OrderDetail
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

        [Required]
        public int OrderDetailID { get; set; }

        [Required]
        public int OrderID { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
