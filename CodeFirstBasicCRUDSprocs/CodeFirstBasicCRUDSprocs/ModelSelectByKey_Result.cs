using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstBasicCRUDSprocs
{
    //this class was created "manually".  It is basically a copy of what the edmx add sproc process does
    class ModelSelectByKey_Result
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public string ManufacturerCode { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public string StatusName { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public Nullable<decimal> ListPrice { get; set; }
        public string ImageCollection { get; set; }
        public string CategoryCustomData { get; set; }
        public string ManufacturerCustomData { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}
