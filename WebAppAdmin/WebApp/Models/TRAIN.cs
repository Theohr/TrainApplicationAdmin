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
    
    public partial class TRAIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRAIN()
        {
            this.COACH_FIRST1 = new HashSet<COACH_FIRST>();
            this.COACH_STANDARD1 = new HashSet<COACH_STANDARD>();
            this.SCHEDULEs = new HashSet<SCHEDULE>();
        }
    
        public decimal TRAINID { get; set; }
        public decimal COACH_FIRST { get; set; }
        public decimal COACH_STANDARD { get; set; }
        public string STATUS { get; set; }
        public decimal TOTALSEATS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COACH_FIRST> COACH_FIRST1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COACH_STANDARD> COACH_STANDARD1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCHEDULE> SCHEDULEs { get; set; }
    }
}
