using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EDMXBasicCRUDSprocs
{
    class Program
    {
        // The database for this project located at: https://github.com/brianvp/bikestore-database
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
            using (var db = new BikeStoreEntities())
            {
                //#1 Call sproc as method of context class - this would be the preferred way when using EDMX I believe...
                // var is of type ModelSelectFilter_Result
                var models = db.ModelSelectFilter(null,null,null,null,null,null,null,null,null);

                foreach (var model in models)
                {
                    Console.WriteLine(model.ModelId + " | " + model.Name);
                }
                
                //#2 directly call sproc - not recommended
                /*
                var name = new SqlParameter("@name", DBNull.Value);
                var manufacturerCode = new SqlParameter("@manufacturercode", DBNull.Value);
                var categoryName = new SqlParameter("@categoryname", DBNull.Value);
                var description = new SqlParameter("@description", DBNull.Value);
                var features = new SqlParameter("@features", DBNull.Value);
                var minListPrice = new SqlParameter("@minListPrice", DBNull.Value);
                var maxListPrice = new SqlParameter("@maxListPrice", DBNull.Value);
                var statusName = new SqlParameter("@statusName", DBNull.Value);
                var manufacturerName = new SqlParameter("@manufacturerName", DBNull.Value);

                //var models = db.Database.SqlQuery<ModelSelectFilter_Result>("product.ModelSelectFilter @name", { name, manufacturercode});

                var models = db.Database.SqlQuery<ModelSelectFilter_Result>("product.ModelSelectFilter @name, @manufacturercode, @categoryname,@description,@features,@minListPrice,@maxListPrice,@statusName, @manufacturerName", 
                                        name,manufacturerCode, categoryName, description, features, minListPrice,maxListPrice,statusName,manufacturerName).ToList();

                foreach (var model in models)
                {
                    Console.WriteLine(model.ModelId + " | " + model.Name);
                }
                */
            }
        }

        static void SelectSingleModel(int modelId)
        {
            using (var db = new BikeStoreEntities())
            {
                // option #1 Call db context method linked to sproc
                var model = db.ModelSelectByKey(modelId).FirstOrDefault();

                //Option #2 - Directly call sproc
                /*
                var modelIdParam = new SqlParameter("@modelid", modelId);
                var model = db.Database.SqlQuery<ModelSelectByKey_Result>("product.ModelSelectByKey @modelID", modelIdParam).FirstOrDefault();
                */

                Console.WriteLine("Selected Model:");

                if (model != null)
                {
                    Console.WriteLine(model.ModelId + " | " + model.Name);
                }
            }
        }
        static void FilteredQuery()
        {
            using (var db = new BikeStoreEntities())
            {
                var query = db.PartNumberSelectFilter(null, null, null, null, null, "tape", null, null, null, null, null, null);

                Console.WriteLine("All Bar Tape Products in the Database");

                foreach (var item in query)
                {
                    Console.WriteLine(item.ModelId + " | " + item.ModelName + " | " + item.PartNumberName + " | " + item.InventoryPartNumber + " | " + item.ListPrice + " | " + item.CategoryName);
                }

            }
        }

        static int CreateModel()
        {
            int modelId = 0;

            
            using (var db = new BikeStoreEntities())
            {
                //approach #1 sproc through the function import
                /*
                var modelIdParam = new System.Data.Entity.Core.Objects.ObjectParameter("modelid", -1);
                var model = new Model { Name = "Domane 5.2", ListPrice = 3499.99m };

                //modeid is an output param
                var result = db.ModelInsert(modelIdParam, model.Name, null, null, null, null, null, null, model.ListPrice,null,null,null);
                modelId = (int)modelIdParam.Value;
                 */

                //approach 2 Used Stored procedure mapping on the Model entity
                //Need to have a select @ the end of the inset sproc                
                var model = new Model { Name = "Domane 5.2", ListPrice = 3499.99m };

                db.Models.Add(model);
                db.SaveChanges();

                modelId = model.ModelId;

            }

            Console.WriteLine("New Model: " + modelId);

            return modelId;
        }

        static void UpdateModel(int modelId)
        {
            using (var db = new BikeStoreEntities())
            {
                //ModelSelectByKey mapped to entity "Model"
                var model = db.ModelSelectByKey(modelId).FirstOrDefault();

                if (model != null)
                {
                    //the following is necessary if the return type of ModelSelectByKey is complex type ModelSelectByKey_Result
                    // ModelSelectByKey uses the default
                    /*
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

                    db.Models.Attach(m);*/

                    // make the change - needs to happen after the attach, otherwise the change
                    // will not be registered

                    //m.Features = "500 Series OCLV Frame";

                    model.Features = "500 Series OCLV Frame";

                    db.SaveChanges();
                } 
            }
        }

        static void DeleteModel(int modelId)
        {
            using (var db = new BikeStoreEntities())
            {
                // uses sprocs when mapping set in model
                var model = new Model { ModelId = modelId };

                db.Models.Attach(model);
                db.Models.Remove(model);
                db.SaveChanges();
            }
        }
    }
}
