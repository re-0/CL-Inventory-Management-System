using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace MgtSys
{
    public static partial class Methods
    {
        
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

        

        public static void AddedInventoryValue(List<Product> someList)
        {
            var totalItems = someList.Sum(item => item.productQuantity);
            decimal sum = 0m;

            foreach(var obj in someList)
                sum += (obj.productPrice * obj.productQuantity);

                    Console.WriteLine($"Value of added inventory: {(char)163}{sum} (with {totalItems} items.)");
        }
    }

}