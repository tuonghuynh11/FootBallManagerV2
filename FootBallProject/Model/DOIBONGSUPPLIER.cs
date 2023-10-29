﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FootBallProject.Model
{
    using DevExpress.ClipboardSource.SpreadsheetML;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Media;

    public partial class DOIBONGSUPPLIER
    {
        public string idDoiBong { get; set; }
        public int idSupplier { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public Nullable<int> duration { get; set; }
        public Nullable<int> status { get; set; }
        public string statusString { 
            get{
                return DateTime.Now > endDate ? "Hết hạn" : "Còn hạn";
            } set
            {

            } }
        public string durationString
        {
            get
            {
                return this.duration + " năm";
            }
            set
            {

            }
        }
        public System.Windows.Media.Brush statusColor
        {
            get
            {
                Random r = new Random();

                SolidColorBrush brushExpired = (SolidColorBrush)new BrushConverter().ConvertFrom("#EE878A");
                SolidColorBrush brushWanring = (SolidColorBrush)new BrushConverter().ConvertFrom("#F5F881");
                SolidColorBrush brushRemain = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");
                TimeSpan difference = (TimeSpan)( this.endDate - DateTime.Now);
                if (difference.Days<0)
                {
                    return brushExpired;
                }
                if(difference.Days < 10)
                {
                    return brushWanring;
                }
               return brushRemain;
            }                                                                                        

            set
            {
            }
        }
        public virtual DOIBONG DOIBONG { get; set; }
        public virtual SUPPLIER SUPPLIER { get; set; }
    }
}
