//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace demogsoft1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class trnSalesItem
    {
        public int SalesItemId { get; set; }
        [Required]
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal SGSTPrc { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal CGSTPrc { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal IGSTPrc { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal MRP { get; set; }
        public decimal SalesRate { get; set; }
        public int SalesId { get; set; }
        public int ItemId { get; set; }
        public int TaxId { get; set; }
        public decimal DIscPrc { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal NetAmt { get; set; }
        public System.DateTime VoucherDate { get; set; }
    
        public virtual mstItem mstItem { get; set; }
        public virtual mstTax mstTax { get; set; }
        public virtual trnSale trnSale { get; set; }
    }
  

}
