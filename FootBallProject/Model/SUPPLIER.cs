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
    
    public partial class SUPPLIER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUPPLIER()
        {
            this.DOIBONGSUPPLIERs = new HashSet<DOIBONGSUPPLIER>();
            this.LEAGUESUPPLIERs = new HashSet<LEAGUESUPPLIER>();
            this.SUPPLIERSERVICEs = new HashSet<SUPPLIERSERVICE>();
        }
    
        public int idSupplier { get; set; }
        public string supplierName { get; set; }
        public string addresss { get; set; }
        public string phoneNumber { get; set; }
        public string representativeName { get; set; }
        public Nullable<System.DateTime> establishDate { get; set; }
        public string images { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOIBONGSUPPLIER> DOIBONGSUPPLIERs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LEAGUESUPPLIER> LEAGUESUPPLIERs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPPLIERSERVICE> SUPPLIERSERVICEs { get; set; }
    }
}
