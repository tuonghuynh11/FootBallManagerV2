//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FootBallProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LEAGUESUPPLIER
    {
        public int idLeague { get; set; }
        public int idSupplier { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public Nullable<int> duration { get; set; }
        public Nullable<int> status { get; set; }
    
        public virtual LEAGUE LEAGUE { get; set; }
        public virtual SUPPLIER SUPPLIER { get; set; }
    }
}