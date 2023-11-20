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
    
    public partial class Table_CustomerRegistartion
    {
        public int CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Unit { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string AddressThree { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string GSTIN { get; set; }
        public string Cluster { get; set; }
        public string RouteNumber { get; set; }
        public Nullable<int> RouteId { get; set; }
        public Nullable<int> ClusterId { get; set; }
        public string Region { get; set; }
        public Nullable<int> RegionId { get; set; }
        public string Zone { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public string WeeklyOff { get; set; }
        public string WorkingStart { get; set; }
        public string WorkingEnd { get; set; }
        public string SecurityFormalities { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CompanyOldName { get; set; }
    }
}