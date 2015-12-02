using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CodeFirstBasicCRUDSprocs
{
    class Program
    {
        static void Main(string[] args)
        {
            SelectAllModels();

            FilteredQuery();

            int modelId = CreateModel();

            UpdateModel(modelId);

            SelectSingleModel(modelId);

            DeleteModel(modelId);

            SelectSingleModel(modelId);

            Console.ReadKey();
        }
        static void SelectAllModels()
        {
            using (var db = new BikeStoreContext())
            {
                var name = new SqlParameter("@name", DBNull.Value);
                var manufacturerCode = new SqlParameter("@manufacturercode", DBNull.Value);
                var categoryName = new SqlParameter("@categoryname", DBNull.Value);
                var description = new SqlParameter("@description", DBNull.Value);
                var features = new SqlParameter("@features", DBNull.Value);
                var minListPrice = new SqlParameter("@minListPrice", DBNull.Value);
                var maxListPrice = new SqlParameter("@maxListPrice", DBNull.Value);
                var statusName = new SqlParameter("@statusName", DBNull.Value);
                var manufacturerName = new SqlParameter("@manufacturerName", DBNull.Value);

                var models = db.Database.SqlQuery<ModelSelectFilter_Result>("product.ModelSelectFilter @name, @manufacturercode, @categoryname,@description,@features,@minListPrice,@maxListPrice,@statusName, @manufacturerName", 
                                        name,manufacturerCode, categoryName, description, features, minListPrice,maxListPrice,statusName,manufacturerName).ToList();

                foreach (var model in models)
                {
                    Console.WriteLine(model.ModelId + " | " + model.Name);
                }
            }
        }

        static void SelectSingleModel(int modelId)
        {
            using (var db = new BikeStoreContext())
            {
                var modelIdParam = new SqlParameter("@modelid", modelId);

                var model = db.Database.SqlQuery<ModelSelectByKey_Result>("product.ModelSelectByKey @modelid", modelIdParam).SingleOrDefault();

                Console.WriteLine("Selected Model:");

                if (model != null)
                {
                    Console.WriteLine(model.ModelId + " | " + model.Name);
                }

            }
        }
        static void FilteredQuery()
        {
            using (var db = new BikeStoreContext())
            {

                var modelName = new SqlParameter("@ModelName", "tape");
                var partNumberName = new SqlParameter("@PartNumberName", DBNull.Value);
                var manufacturerCode = new SqlParameter("@manufacturercode", DBNull.Value);
                var inventoryPartNumber = new SqlParameter("@InventoryPartNumber", DBNull.Value);
                var manufacturerPartNumber = new SqlParameter("@ManufacturerPartNumber", DBNull.Value);
                var categoryName = new SqlParameter("@categoryname", DBNull.Value);
                var description = new SqlParameter("@description", DBNull.Value);
                var features = new SqlParameter("@features", DBNull.Value);
                var minListPrice = new SqlParameter("@minListPrice", DBNull.Value);
                var maxListPrice = new SqlParameter("@maxListPrice", DBNull.Value);
                var statusName = new SqlParameter("@statusName", DBNull.Value);
                var manufacturerName = new SqlParameter("@manufacturerName", DBNull.Value);

                var models = db.Database.SqlQuery<PartNumberSelectFilter_Result>("product.PartNumberSelectFilter @ModelName,@PartNumberName, @manufacturercode,@InventoryPartNumber,@ManufacturerPartNumber, @categoryname,@description,@features,@minListPrice,@maxListPrice,@statusName, @manufacturerName",
                                     modelName, partNumberName, manufacturerCode, inventoryPartNumber,manufacturerPartNumber, categoryName, description, features, minListPrice, maxListPrice, statusName, manufacturerName).ToList();

                Console.WriteLine("All Bar Tape Products in the Database");

                foreach (var item in models)
                {
                    Console.WriteLine(item.ModelId + " | " + item.ModelName + " | " + item.PartNumberName + " | " + item.InventoryPartNumber + " | " + item.ListPrice + " | " + item.CategoryName);
                }

            }
        }

        static int CreateModel()
        {
            int modelId = 0;

            using (var db = new BikeStoreContext())
            {
                // Use stored procedure mapping
                var model = new Model { Name = "Domane 5.2", ListPrice = 3499.99m };

                db.Models.Add(model);
                db.SaveChanges();

                modelId = model.ModelId;
                
                // Call sproc directly - not recommended ...
                // to get the following to work, need to 
                // A) remove @Modelid output param from product.modelInsert
                // B) change the identity select after insert to select all fields for loading into model
                /*
                var name = new SqlParameter("@name","Domane 5.2");
                var manufacturerCode = new SqlParameter("@manufacturercode", DBNull.Value);
                var categoryid = new SqlParameter("@categoryid", System.Data.SqlDbType.Int);
                categoryid.Value = DBNull.Value;
                var description = new SqlParameter("@description",DBNull.Value);
                var features = new SqlParameter("@features", DBNull.Value);
                var statusid = new SqlParameter("@statusid", System.Data.SqlDbType.Int);
                statusid.Value = DBNull.Value;
                var manufacturerid = new SqlParameter("@manufacturerid", System.Data.SqlDbType.Int);
                manufacturerid.Value = DBNull.Value;
                var listprice = new SqlParameter("@listprice", 3499.99m);
                var imagecollection = new SqlParameter("@imagecollection", DBNull.Value);
                var CategoryCustomData = new SqlParameter("@CategoryCustomData", DBNull.Value);
                var ManufacturerCustomData = new SqlParameter("@ManufacturerCustomData", DBNull.Value);

                var result = db.Database.SqlQuery<Model>("product.modelInsert @name, @manufacturercode, @categoryid, @Description, @features,@statusid, @manufacturerid, @listprice,@imagecollection,@CategoryCustomData, @ManufacturerCustomDAta",
                    name, manufacturerCode, categoryid, description, features, statusid, manufacturerid, listprice, imagecollection, CategoryCustomData, ManufacturerCustomData).SingleOrDefault();

                 modelId = result.ModelId;
                 */
            }

            Console.WriteLine("New Model: " + modelId);

            return modelId;
        }

        static void UpdateModel(int modelId)
        {
            using (var db = new BikeStoreContext())
            {
                var modelIdParam = new SqlParameter("@modelid", modelId);

                // ModelSelectByKey_Result
                //var model = db.Database.SqlQuery<ModelSelectByKey_Result>("product.ModelSelectByKey @modelid", modelIdParam).SingleOrDefault();
                var model = db.Database.SqlQuery<Model>("product.ModelSelectByKey @modelid", modelIdParam).SingleOrDefault();

                if (model != null)
                {
                    /*
                    //Load Model
                    // note that model is of type ModelSelectByKey_Result, so we can't
                    // attach this to db.Models
                    // additionally, we can't load
                    Model m = new Model();

                    m.ModelId = model.ModelId;
                    m.Name = model.Name;
                    m.ManufacturerCode = model.ManufacturerCode;
                    m.CategoryId = model.CategoryId;
                    m.Description = model.Description;
                    m.Features = model.Features;
                    m.StatusId = model.StatusId;
                    m.ManufacturerId = model.ManufacturerId;
                    m.ListPrice = model.ListPrice;
                    m.ImageCollection = model.ImageCollection;
                    m.CategoryCustomData = model.CategoryCustomData;
                    m.ManufacturerCustomData = model.ManufacturerCustomData;

                    db.Models.Attach(m);

                    // make the change - needs to happen after the attach, otherwise the change
                    // will not be registered

                    m.Features = "500 Series OCLV Frame";
                    */

                    //unlike edmx, need to attach the model entity to the db context, as 
                    //SqlQuery() does not do this

                    db.Models.Attach(model);
                    model.Features = "500 Series OCLV Frame";

                    db.SaveChanges();
                }
            }
        }

        static void DeleteModel(int modelId)
        {
            using (var db = new BikeStoreContext())
            {
                var model = new Model { ModelId = modelId };

                db.Models.Attach(model);
                db.Models.Remove(model);
                db.SaveChanges();
            }
        }
    }
}
