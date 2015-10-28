using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstBasicCRUD
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

                var query = from m in db.Models
                            orderby m.Name
                            select m;

                Console.WriteLine("All Models in the Database");

                foreach (var model in query)
                {
                    Console.WriteLine(model.ModelId + " | " + model.Name);
                }
            }
        }

        static void SelectSingleModel(int modelId)
        {
            using (var db = new BikeStoreContext())
            {
                var model = db.Models.SingleOrDefault(b => b.ModelId == modelId);

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
                var query = from md in db.Models
                            join pn in db.PartNumbers
                            on md.ModelId equals pn.ModelId
                            join ct in db.Categories
                                on md.CategoryId equals ct.CategoryId
                            where md.Name.Contains("tape")
                            select new { md.ModelId, ModelName = md.Name, PartNumberName = pn.Name, pn.InventoryPartNumber, pn.ListPrice, CategoryName = ct.Name };

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

            using (var db = new BikeStoreContext())
            {
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
            using (var db = new BikeStoreContext())
            {
                var model = db.Models.SingleOrDefault(b => b.ModelId == modelId);

                if (model != null)
                {
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
