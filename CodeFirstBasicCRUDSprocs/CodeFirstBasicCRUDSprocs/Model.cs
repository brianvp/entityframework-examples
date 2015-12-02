namespace CodeFirstBasicCRUDSprocs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product.Model")]
    public partial class Model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            PartNumbers = new HashSet<PartNumber>();
        }

        public int ModelId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(25)]
        public string ManufacturerCode { get; set; }

        public int? CategoryId { get; set; }

        public string Description { get; set; }

        public string Features { get; set; }

        public int? StatusId { get; set; }

        public int? ManufacturerId { get; set; }

        [Column(TypeName = "money")]
        public decimal? ListPrice { get; set; }

        public string ImageCollection { get; set; }

        public string CategoryCustomData { get; set; }

        public string ManufacturerCustomData { get; set; }

        // The following properties can be set as 
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        // however, this will require the sproc product.modelInsert to add these columns
        // to the identity select:
        /*
            select ModelId, DateModified, DateCreated
	        from product.Model
	        where @@ROWCOUNT > 0 and ModelId = SCOPE_IDENTITY()
        */
        // if this isn't present, the following error is thrown:
        //  A function mapping specifies a result column 'DateModified' that the result set does not contain.
        // You can verify this pattern by setting the columns to computed and turning off sproc mapping.  The EF
        // generated SQL will add DateModified, DateCreated to the select list.  

        // Ulitmately, since We are not querying Model directly, we do not need to have these properties loaded
        // so leaving as [NotMapped]

        [NotMapped]
        [Column(TypeName = "datetime2")]
        public  DateTime? DateModified { get; set; }

        [NotMapped]
        [Column(TypeName = "datetime2")]
        public  DateTime? DateCreated { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Status Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartNumber> PartNumbers { get; set; }

        
    }
}
