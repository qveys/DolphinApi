//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DolphinApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class piscine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public piscine()
        {
            this.dolphinmatch = new HashSet<dolphinmatch>();
        }
    
        public int ID_PISCINE { get; set; }
        public string NOM_PISCINE { get; set; }
        public decimal ADR_LATITUDE { get; set; }
        public decimal ADR_LONGITUDE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dolphinmatch> dolphinmatch { get; set; }
    }
}
