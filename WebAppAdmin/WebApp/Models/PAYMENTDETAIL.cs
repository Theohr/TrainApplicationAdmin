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
    
    public partial class PAYMENTDETAIL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAYMENTDETAIL()
        {
            this.TRANSACTIONS = new HashSet<TRANSACTION>();
        }
    
        public decimal USERID { get; set; }
        public string NAMEONCARD { get; set; }
        public string CARDNUMBER { get; set; }
        public System.DateTime EXPIRYDATE { get; set; }
        public string STREETNUMBER { get; set; }
        public string POSTCODE { get; set; }
    
        public virtual USERACCOUNT USERACCOUNT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACTION> TRANSACTIONS { get; set; }
    }
}
