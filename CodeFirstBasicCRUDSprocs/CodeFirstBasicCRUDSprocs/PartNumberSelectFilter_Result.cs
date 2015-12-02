using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstBasicCRUDSprocs
{
    class PartNumberSelectFilter_Result
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string PartNumberName { get; set; }
        public string ManufacturerCode { get; set; }
        public Nullable<int> PartNumberId { get; set; }
        public string InventoryPartNumber { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string InvoiceDescription { get; set; }
        public string UPC { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public string ModelStatusName { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public Nullable<decimal> ListPrice { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string ImageCollection { get; set; }
        public string CategoryCustomData { get; set; }
        public string ManufacturerCustomData { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}
