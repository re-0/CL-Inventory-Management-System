﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;
using static NewIn.Methods;

namespace NewIn
{
    class Program
    {
        static void Main(string[] args)
        {
        var prod = new List<Product>();
        Console.WriteLine("\n\tPress <ESC> when finished ...\n\tEnter 'exit' to abort.");
        
        Console.WriteLine("Your current inventory ...");
        ListTheProducts(ShowProducts());

            Console.ReadKey();
        //<!-- ADD NEW INVENTORY -->
        // Inventory will be stored in DB AFTER operations are done
        string input_1 = string.Empty;
        do {
            Console.Clear();
            var pr = new Product();
            var tempList = new List<Product>();
                Console.WriteLine("Add to inventory: ");
                input_1 = Console.ReadLine();
                    if(string.IsNullOrEmpty(input_1)) break;
                Console.Clear();
                if(input_1.ToLower() == "exit"){
                    break;}
                else{

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
                        prod.AddRange(tempList);

                        Console.WriteLine("Current inventory");
                        ListTheProducts(prod);
                    }
                    catch
                    {
                        Console.WriteLine("It appears you used an unknown command.");
                    }
                } // end: do-loop
            }while(Console.ReadKey(true).Key != ConsoleKey.Escape); // req windows?
        //<!-- END: ADD NEW INVENTORY -->

            Console.Clear();

            bool cmdOuter = false;
            do
            {
                if(input_1.ToLower() == "exit") break;
                Console.Write("Would you like to change anything in the inventory? [Y/n] ");
                string answ = Convert.ToString(Console.ReadLine()).ToLower();

                if(answ.ToLower() != "n" && answ.ToLower() != "y")
                {
                    Console.Clear();
                    Console.WriteLine($"{answ}\nUnknown command. Try again.");
                }

                if(answ.ToLower() == "n")
                    cmdOuter = true;
                
                if (answ.ToLower() == "y")
                {
                    bool cmdInner = false;
                    do
                    {
                        Console.WriteLine("\tRemove or edit? ");
                        string command = Console.ReadLine();
                        if(command.ToLower() != "remove" && command.ToLower() != "edit")
                        {
                            Console.Clear();
                            Console.WriteLine($"{command}\nUnknown command. Try again.");
                        }
                        
                        if (command.ToLower() == "remove")
                        {
                            Console.Write("Enter index of item to be removed: ");
                            int index = Convert.ToInt32(Console.ReadLine());
                            index--;
                            prod.RemoveAt(index);
                            cmdInner = true;
                        }

                        if (command.ToLower() == "edit")
                        {
                            Console.Clear();
                            bool cmdEdit = false;
                            do
                            {
                                Console.WriteLine("\tPlease type your desired action ... ");
                                // name, brand, price, quantity
                                string commandEdit = Console.ReadLine();
                                checkEditCmd(commandEdit);

                                if(commandEdit.ToLower() == "name")
                                {
                                    Console.Write("Enter index and new name: ");
                                    string input = Console.ReadLine();

                                    string[] temp = input.Split(",");
                                    int index = Convert.ToInt32(temp[0]);
                                    index--;
                                    string changeName = temp[1];
                                    try
                                    {
                                        editName(prod, index, changeName);
                                        cmdEdit = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Something went wrong...");
                                    }
                                } // editName
                                
                                if(commandEdit.ToLower() == "brand")
                                {
                                    Console.Write("Enter index and new brand name: ");
                                    string input = Console.ReadLine();

                                    string[] temp = input.Split(",");
                                    int index = Convert.ToInt32(temp[0]);
                                    index--;
                                    string changeBrand = temp[1];
                            
                            
                                    try
                                    {
                                        editBrand(prod, index, changeBrand);
                                        cmdEdit = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Something went wrong...");
                                    }
                                } // editBrand

                                if(commandEdit.ToLower() == "price")
                                {
                                    Console.Write("Enter index and new price: ");
                                    string input = Console.ReadLine();

                                    string[] temp = input.Split(",");
                                    int index = Convert.ToInt32(temp[0]);
                                    index--;
                                    decimal changePrice = Decimal.Parse(temp[1],
                                                    NumberStyles.Float, 
                                                    CultureInfo.InvariantCulture);
                                    try
                                    {
                                        editPrice(prod, index, changePrice);
                                        cmdEdit = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Something went wrong...");
                                    }
                                } // editPrice

                                if(commandEdit.ToLower() == "quantity")
                                {
                                    Console.Write("Enter index and new price: ");
                                    string input = Console.ReadLine();

                                    string[] temp = input.Split(",");
                                    int index = Convert.ToInt32(temp[0]);
                                    index--;
                                    int changeQuantity = Convert.ToInt32(temp[1]);
                                    try
                                    {
                                        editQuantity(prod, index, changeQuantity);
                                        cmdEdit = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Something went wrong...");
                                    }
                                } // editPrice

                                // Console.WriteLine("Works");
                            // cmdEdit = true;
                            } while (cmdEdit == false);

                            cmdInner = (cmdEdit == true) ? true : false;
                        }
                    } while (cmdInner == false);
                    
                    cmdOuter = (cmdInner == true) ? true : false;
                }
                
            } while (cmdOuter == false);
            
            Console.Clear();
            
            if ( (prod != null) & (prod.Count != 0 ))
            {
                try
                {
                    AddProduct(prod);
                    Console.WriteLine("Products successfully added to database!");
                    AddedInventoryValue(prod);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }else if ( (prod == null) || (prod.Count == 0 ))
                Console.WriteLine("No new entries to inventory");
            
            ListTheProducts(ShowProducts());
        // NEWLINE
        
        } // MAIN CLASS
    } // program class
} // namespace
