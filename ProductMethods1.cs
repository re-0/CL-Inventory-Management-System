using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace MgtSys
{
    public static partial class Methods
    {
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

    } // Method Class
}