//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FPL.Dal.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Table_V4030
    {
        public int ID { get; set; }
        public Nullable<int> RefID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string BasicQty { get; set; }
        public string BasicPrice { get; set; }
        public string AuxQty { get; set; }
        public string AuxPrice { get; set; }
        public Nullable<int> TemplateID { get; set; }
        public string TemplateName { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string YourEnquiry { get; set; }
        public string KindAttention { get; set; }
        public string BillingAddress { get; set; }
        public Nullable<int> TotalAmount { get; set; }
    }
}
