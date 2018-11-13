using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace NewIn
{
    public static class Methods{

        public static void AddNew(List<Product> someList, string input_1) // this methods handles adding new items to inventory in-memory
        {
            Product pr = new Product();
            List<Product> tempList = new List<Product>();
            try
            {
                string[] temp = input_1.Split(",");
                pr.productName = temp[0];
                pr.brand = temp[1];
                pr.productPrice = Decimal.Parse(temp[2],
                                          NumberStyles.Float, 
                                          CultureInfo.InvariantCulture);
                pr.productQuantity = Int32.Parse(temp[3]);
                        tempList.Add(pr);
                        someList.AddRange(tempList);

                        Console.WriteLine("Current inventory");
                        ListTheProducts(someList);
            }
            catch
            {
                Console.WriteLine("It appears you used an unknown command.");
            }
        }

        public static void ListTheProducts(List<Product> someList)
        {
            Console.WriteLine("Product\t\t\tPrice\t\tQuantity");
            foreach(Product obj in someList)
                Console.WriteLine($"{obj.productName, -18}\t{(char)163}{obj.productPrice, -7}\t{obj.productQuantity}");
        }

        public static void checkEditCmd(string someCommand)
        {
            string[] commands = {"name", "brand", "price", "quantity"};
            if(!commands.Contains(someCommand.ToLower()))
             {
                            Console.Clear();
                            Console.WriteLine($"<{someCommand}>\nUnknown command. Try again.");
            }
        }

        public static List<Product> editName(List<Product> someList, int theIndex, string new_name){
            string tmp = string.Empty;
            foreach (Product item in someList.GetRange(theIndex, 1))
            {
                tmp = Convert.ToString(item.productName);
            }

            someList.FirstOrDefault(c => c.productName == tmp).productName = new_name;

            return someList.ToList();
        }

        public static List<Product> editBrand(List<Product> someList, int theIndex, string new_brand){
            string tmp = string.Empty;
            foreach (Product item in someList.GetRange(theIndex, 1))
            {
                tmp = Convert.ToString(item.brand);
            }

            someList.FirstOrDefault(c => c.brand == tmp).brand = new_brand;

            return someList.ToList();
        }

        public static List<Product> editPrice(List<Product> someList, int theIndex, decimal new_price){
            decimal tmp = 0m;
            foreach (Product item in someList.GetRange(theIndex, 1))
            {
                tmp = Convert.ToDecimal(item.productPrice);
            }

            someList.FirstOrDefault(c => c.productPrice == tmp).productPrice = new_price;

            return someList.ToList();
        }

        public static List<Product> editQuantity(List<Product> someList, int theIndex, int new_quantity){
            int tmp = 0;
            foreach (Product item in someList.GetRange(theIndex, 1))
            {
                tmp = Convert.ToInt32(item.productQuantity);
            }

            someList.FirstOrDefault(c => c.productQuantity == tmp).productQuantity = new_quantity;

            return someList.ToList();
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

        public static List<Product> ShowProducts() // Not tried, yet
        {
            var tempList = new List<Product>();
            using( var con = new SqliteConnection("DataSource = InventoryDB.db"))
            {
                con.Open();
                
                string query = "SELECT Name, Brand, Price, Quantity FROM Inventory";

                    using(var cmd = new SqliteCommand(query, con))
                    {
                        using(var reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                var tempProduct = new Product();
                                tempProduct.productName     = Convert.ToString(reader["Name"]);
                                tempProduct.brand           = Convert.ToString(reader["Brand"]);
                                tempProduct.productPrice    = Convert.ToDecimal(reader["Price"]);
                                tempProduct.productQuantity = Convert.ToInt32(reader["Quantity"]);
                                tempList.Add(tempProduct);
                            } 
                        } // reader
                    }
                
                con.Close();
            } // connection
            return tempList.ToList();
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