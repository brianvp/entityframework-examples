namespace CodeFirstBasicCRUDSprocs
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BikeStoreContext : DbContext
    {
        public BikeStoreContext()
            : base("name=BikeStoreContext")
        {
            Database.SetInitializer<BikeStoreContext>(null);
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<PartNumber> PartNumbers { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>()
                .Property(e => e.ListPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PartNumber>()
                .Property(e => e.ListPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PartNumber>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PartNumber>()
                .Property(e => e.UPC)
                .IsFixedLength();

            
            modelBuilder
            .Entity<Model>()
            .MapToStoredProcedures(s =>
                s.Update(u => u.HasName("product.ModelUpdate")
                           )
                .Delete( d => d.HasName("product.ModelDelete"))
                
                // Can't use ModelID as a parameter here EF either doesn't support output params properly
                // or can't pass an identity col/property in an insert
                .Insert( i => i.HasName("product.ModelInsert"))
                                //.Parameter(m => m.ModelId, "ModelID")
                                /*
                                .Parameter(m => m.Name, "Name")
                                .Parameter(m => m.ManufacturerCode, "ManufacturerCode")
                                .Parameter(m => m.CategoryId, "CategoryId")
                                .Parameter(m => m.Description, "Description")
                                .Parameter(m => m.Features, "Features")
                                .Parameter(m => m.StatusId, "StatusId")
                                .Parameter(m => m.ManufacturerId, "ManufacturerId")
                                .Parameter(m => m.ListPrice, "ListPrice")
                                .Parameter(m => m.ImageCollection, "ImageCollection")
                                .Parameter(m => m.CategoryCustomData, "CategoryCustomData")
                                .Parameter(m => m.ManufacturerCustomData, "ManufacturerCustomData"))*/
                );
       
        }
    }
}
