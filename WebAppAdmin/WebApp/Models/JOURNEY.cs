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
    
    public partial class JOURNEY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JOURNEY()
        {
            this.TICKETs = new HashSet<TICKET>();
        }
    
        public decimal JOURNEYID { get; set; }
        public decimal DEPARTURE { get; set; }
        public decimal ARRIVAL { get; set; }
        public System.DateTime DEPARTURETIME { get; set; }
        public System.DateTime ARRIVALTIME { get; set; }
    
        public virtual STATION STATION { get; set; }
        public virtual STATION STATION1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TICKET> TICKETs { get; set; }
    }
}
