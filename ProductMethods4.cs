using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace MgtSys
{
    public static partial class Methods
    {
        public static void RemoveFromDB(string someCommand)
        {
            if(someCommand == "csv")
            {
                Console.WriteLine("Enter IDs (type exit to abort):");
                bool csvIsTrue = false;
                do
                {
                    string input_csv = Console.ReadLine();
                    if(input_csv.ToLower() == "exit")
                    {
                        csvIsTrue = true;
                    }
                            
                    string[] csv_temp = input_csv.Split(",");
                    int csv_length = csv_temp.Length;

                    if(csv_length < 1)
                        Console.WriteLine("You did not enter an ID! Please try again!");
                    else
                        {
                            try
                            {
                                using( var con = new SqliteConnection("DataSource = InventoryDB.db"))
                                {
                                    con.Open();

                                    string query = "DELETE FROM Inventory WHERE ID = @ID";
                                        for (int i = 0; i < csv_length; i++)
                                        {
                                            using(var cmd = new SqliteCommand(query, con))
                                            {
                                                cmd.Parameters.AddWithValue("@ID", Int32.Parse(csv_temp[i]));
                                                cmd.ExecuteNonQuery();
                                            } // using cmd  
                                        }
                                        
                                    con.Close();
                                }
                                    Console.WriteLine("Removing successful!");
                                    csvIsTrue = true;
                            }
                            catch (System.Exception e)
                            {
                                Console.WriteLine($"Removing failed!\n\n{e}");
                                    // \n\n{query} ... Include this to check DELETE query in case of error!
                            }
                        }
                } while (csvIsTrue == false);
            }

            if(someCommand == "nl")
            {
                var tempList = new List<int>();
                bool nlIsTrue = false;
                Console.WriteLine("Enter IDs (type non-integer value to abort):");
                do
                {
                    string input_nl = Console.ReadLine();
                    try
                    {
                        tempList.Add(Int32.Parse(input_nl));
                    }
                    catch
                    {
                        Console.WriteLine($"Exiting program!");
                        nlIsTrue = true;
                    }
                } while (nlIsTrue == false);

                tempList.ForEach(x => {Console.Write("Removed items:"); Console.Write($" {x} ");});
                try
                {
                    using( var con = new SqliteConnection("DataSource = InventoryDB.db"))
                    {
                        con.Open();
                        
                        string query = "DELETE FROM Inventory WHERE ID = @ID";
                        
                            foreach(int obj in tempList)
                            {
                                
                                using(var cmd = new SqliteCommand(query, con))
                                {
                                    cmd.Parameters.AddWithValue("@ID", obj);
                                    cmd.ExecuteNonQuery();
                                } // using cmd  
                            }
                            
                                            
                        con.Close();
                    }
                    
                    Console.WriteLine("Removing successful!");
                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"Removing failed!\n\n{e}");
                    // \n\n{query} ... Include this to check DELETE query in case of error!
                }                
                // TRY CATCH WORKS
                // DELETE QUERY BELOW
            }
        } // REM DB
    }
}