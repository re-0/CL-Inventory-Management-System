using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace NewIn
{
    public static class Methods{
        public static void ListTheProducts(List<Product> someList)
        {
            Console.WriteLine("Product\t\t\tPrice\t\tQuantity");
            foreach(Product obj in someList)
                Console.WriteLine($"{obj.productName, -18}\t{(char)163}{obj.productPrice, -7}\t{obj.productQuantity}");
        }

        public static void removeItem(List<Product> someList, int theIndex)
        {
            try
            {
                someList.RemoveAt(theIndex);
                Console.WriteLine("Removing of item successful!\n");
                ListTheProducts(someList);
            }catch(System.Exception)
            {
                Console.WriteLine("Something went wrong!"); //throw; // Prints error to console
            } // end: try-catch
        }

        public static void editName(List<Product> someList, int theIndex)
        {
                Console.Write("Enter new name: ");
                var newVal_Name = Convert.ToString(Console.ReadLine());
                foreach(Product obj in someList.GetRange(theIndex, 1))
                { // to get 1 entry only. replace 0 with index needed.
                    Console.WriteLine($"\n{obj.productName}");
                    var name = obj.productName;
                    someList.Where(w => w.productName == name)
                    .ToList().ForEach(s => s.productName = newVal_Name);
                }    
        }

        public static void editBrand(List<Product> someList, int theIndex, string newVal = "")
        {
                Console.Write("Enter new brand: ");
                var newVal_Brand = Convert.ToString(Console.ReadLine());
                foreach(Product obj in someList.GetRange(theIndex, 1))
                {
                    Console.WriteLine($"\n{obj.brand}");
                    var brand = obj.brand;
                    someList.Where(w => w.brand == brand)
                    .ToList().ForEach(s => s.brand = newVal_Brand);
                }
        }

        public static void editPrice(List<Product> someList, int theIndex, int newVal = 0)
        {
                Console.Write("Enter new price: ");
                var newVal_Price = Convert.ToDecimal(Console.ReadLine());
                foreach(Product obj in someList.GetRange(theIndex, 1))
                {
                    Console.WriteLine($"\n{obj.productPrice}");
                    var price = obj.productPrice;
                    someList.Where(w => w.productPrice == price)
                    .ToList().ForEach(s => s.productPrice = newVal_Price);
                }
        }

        public static void editQuantity(List<Product> someList, int theIndex, int newVal = 0)
        {
                Console.Write("Enter new quantity: ");
                var newVal_Quantity = Convert.ToInt32(Console.ReadLine());
                foreach(Product obj in someList.GetRange(theIndex, 1))
                {
                        Console.WriteLine($"\n{obj.productQuantity}");
                        var quantity = obj.productQuantity;
                        someList.Where(w => w.productQuantity == quantity)
                        .ToList().ForEach(s => s.productQuantity = newVal_Quantity);
                }
        }

        public static void AddProduct(List<Product> someList)
        {
            using( var con = new SqliteConnection("DataSource = InventoryDB.db"))
            {
                con.Open();

                string query = "INSERT INTO Inventory ( Name, Brand, Price, Quantity) VALUES ( @Name, @Brand, @Price, @Quantity)";

                foreach(var product in someList){
                    using(var cmd = new SqliteCommand(query, con)){
                        //cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                        cmd.Parameters.AddWithValue("@Name", product.productName);
                        cmd.Parameters.AddWithValue("@Brand", product.brand);
                        cmd.Parameters.AddWithValue("@Price", product.productPrice);
                        cmd.Parameters.AddWithValue("@Quantity", product.productQuantity);
                        cmd.ExecuteNonQuery();
                    } // using cmd
                } // foreach
                con.Close();
            }
        }

        public static void ShowProduct(List<Product> someList) // Get current inventory stored in db
        {
            using( var con = new SqliteConnection("DataSource = InventoryDB.db"))
            {
                con.Open();
                
                string query = "INSERT INTO Inventory ( Name, Brand, Price, Quantity) VALUES ( @Name, @Brand, @Price, @Quantity)";

                foreach(var product in someList){
                    using(var cmd = new SqliteCommand(query, con)){
                        //cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                        cmd.Parameters.AddWithValue("@Name", product.productName);
                        cmd.Parameters.AddWithValue("@Brand", product.brand);
                        cmd.Parameters.AddWithValue("@Price", product.productPrice);
                        cmd.Parameters.AddWithValue("@Quantity", product.productQuantity);
                        cmd.ExecuteNonQuery();
                    } // using cmd
                } // foreach
                con.Close();
            }
        }

        public static void AddedInventoryValue(List<Product> someList)
        {
            var totalItems = someList.Sum(item => item.productQuantity);
            decimal sum = 0m;

            foreach(var obj in someList)
                sum += (obj.productPrice * obj.productQuantity);

                    Console.WriteLine($"Value of added inventory: {(char)163}{sum} (with {totalItems} items.)");
        }
    } // Method Class
}