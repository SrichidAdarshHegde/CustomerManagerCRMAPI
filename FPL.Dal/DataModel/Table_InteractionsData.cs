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
    
    public partial class Table_InteractionsData
    {
        public int Id { get; set; }
        public Nullable<int> CutomerId { get; set; }
        public string CutomerName { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<int> MachineNumber { get; set; }
        public Nullable<int> ModelId { get; set; }
        public string ModelName { get; set; }
        public Nullable<int> RegionId { get; set; }
        public string RegionName { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> AttendedByUserId { get; set; }
        public string AttendedByUserName { get; set; }
        public Nullable<int> AttendedHowId { get; set; }
        public string AttendedHowName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> RequestId { get; set; }
        public Nullable<System.DateTime> DateOfInteraction { get; set; }
        public Nullable<int> TicketNo { get; set; }
    }
}
