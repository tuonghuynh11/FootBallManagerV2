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
    
    public partial class Notification
    {
        public int ID { get; set; }
        public Nullable<int> IDHLV { get; set; }
        public string NOTIFY { get; set; }
        public string CHECKED { get; set; }
    
        public virtual HUANLUYENVIEN HUANLUYENVIEN { get; set; }
    }
}
