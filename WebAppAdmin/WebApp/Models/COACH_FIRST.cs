//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class COACH_FIRST
    {
        public decimal TRAINID { get; set; }
        public decimal COACHNO { get; set; }
        public decimal SEATING { get; set; }
    
        public virtual TRAIN TRAIN { get; set; }
    }
}