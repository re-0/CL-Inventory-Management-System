using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace MgtSys
{
    public static partial class Methods
    {
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
                    Console.WriteLine($"Update failed!\n\n{e}");
                    // \n\n{query} ... Include this to check UPDATE query in case of error!
                }
            } // IF
            else
                Console.WriteLine("Warning! Empty UPDATE arguments! If you do not want to commit any updates press [n] next...");
        }
    }
}

