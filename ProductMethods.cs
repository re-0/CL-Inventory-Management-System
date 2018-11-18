using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace MgtSys
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
                Console.WriteLine("It appears you used an unknown input method.");
            }
        }

        public static void ListTheProducts(List<Product> someList, string someCommand = "")
        {

            if( (string.IsNullOrEmpty(someCommand)) )
            {
                try
                {
                    Console.WriteLine("Product\t\t\tPrice\t\tQuantity");
                    foreach(Product obj in someList)
                        Console.WriteLine($"{obj.productName, -18}\t{(char)163}{obj.productPrice, -7}\t{obj.productQuantity}");
                }
                catch (System.Exception)
                {
                    throw;
                }
                
            } // if statement

            if( !(string.IsNullOrEmpty(someCommand)) && someCommand.ToLower() != "sql")
                Console.WriteLine("Method cannot return data. Unknown command.");

            if(someCommand.ToLower() == "sql"){
                try
                {
                    int i = 1;
                    Console.WriteLine("Db ID\tProduct\t\t\t\tBrand\t\tPrice\t\tQuantity");
                    foreach(Product obj in someList)
                    {
                        Console.WriteLine($"{obj.Id}.\t{obj.productName, -25}\t{obj.brand, -10}\t{(char)163}{obj.productPrice, -7}\t{obj.productQuantity}");
                        i++;
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            
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
                
                string query = "SELECT ID, Name, Brand, Price, Quantity FROM Inventory";

                    using(var cmd = new SqliteCommand(query, con))
                    {
                        using(var reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                var tempProduct = new Product();
                                tempProduct.Id              = Convert.ToInt32(reader["ID"]);
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

        public static void UpdateProducts(string input)
        {
            string name         = string.Empty;
            string brand        = string.Empty;
            string price        = string.Empty;
            string quantity     = string.Empty;
            string updateQuery  = string.Empty;
            
            int? Id = null;
            string tmpName = string.Empty;
            string tmpBrand = string.Empty;
            decimal? tmpPrice = null;
            int? tmpQuantity = null;
            
            string[] temp = input.Split(',');
        
        
            if(temp.ElementAtOrDefault(0) != null)
                Id = Int32.Parse(temp[0]);

            if(temp.ElementAtOrDefault(1) != null)
            {
                tmpName = temp[1];
                if(!string.IsNullOrWhiteSpace(tmpName))
                {
                    name = "Name = @Name";
                    updateQuery += name;
                }

            }

            if(temp.ElementAtOrDefault(2) != null)
            {
                tmpBrand = temp[2];
                if(!string.IsNullOrWhiteSpace(tmpBrand))
                {
                    brand = "Brand = @Brand";
                    if(!string.IsNullOrWhiteSpace(updateQuery))
                        updateQuery += ", "+ brand;
                    else
                        updateQuery += brand;
                }
            }

            if(temp.ElementAtOrDefault(3) != null)
            {
                tmpPrice = Decimal.Parse(temp[3], NumberStyles.Float, CultureInfo.InvariantCulture);
                if(tmpPrice.HasValue)
                {
                    price = "Price = @Price";
                    if(!string.IsNullOrWhiteSpace(updateQuery))
                        updateQuery += ", "+ price;
                    else
                        updateQuery += price;
                }
            }

            if(temp.ElementAtOrDefault(4) != null)
            {
                tmpQuantity = Int32.Parse(temp[4]);
                if(tmpQuantity.HasValue)
                {
                    quantity = "Quantity = @Quantity";
                    if(!string.IsNullOrWhiteSpace(updateQuery))
                        updateQuery += ", "+ quantity;
                    else
                        updateQuery += quantity;
                }
            }

            string query = string.Empty;

            if(!string.IsNullOrWhiteSpace(updateQuery))
            {
                    try
                {
                    using( var con = new SqliteConnection("DataSource = InventoryDB.db"))
                    {
                        con.Open();

                        query = "UPDATE Inventory SET " + updateQuery + " WHERE ID = @ID";

                            using(var cmd = new SqliteCommand(query, con)){
                                if(Id != null && Id.HasValue)
                                cmd.Parameters.AddWithValue("@ID",        Id);
                                if(tmpName != null && !string.IsNullOrWhiteSpace(tmpName))
                                    cmd.Parameters.AddWithValue("@Name", tmpName);
                                if(tmpBrand != null && !string.IsNullOrWhiteSpace(tmpBrand))
                                    cmd.Parameters.AddWithValue("@Brand", tmpBrand);
                                if(tmpPrice != null && tmpPrice.HasValue)
                                    cmd.Parameters.AddWithValue("@Price", tmpPrice);
                                if(tmpQuantity != null && tmpQuantity.HasValue)
                                    cmd.Parameters.AddWithValue("@Quantity", tmpQuantity);
                                cmd.ExecuteNonQuery();
                            } // using cmd
                        
                        con.Close();
                    }
                    Console.WriteLine("Update successful!");
                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"Update failed!\n\n\n{query}\n\n{e}");
                    // \n\n{query} ... Include this to check UPDATE query in case of error!
                }
            } // IF
            else
                Console.WriteLine("Warning! Empty UPDATE arguments! If you do not want to commit any updates press [n] next...");
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