//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDMXBasicCRUDSprocs
{
    using System;
    using System.Collections.Generic;
    
    public partial class Model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            this.PartNumbers = new HashSet<PartNumber>();
        }
    
        public int ModelId { get; set; }
        public string Name { get; set; }
        public string ManufacturerCode { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public Nullable<decimal> ListPrice { get; set; }
        public string ImageCollection { get; set; }
        public string CategoryCustomData { get; set; }
        public string ManufacturerCustomData { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartNumber> PartNumbers { get; set; }
    }
}
