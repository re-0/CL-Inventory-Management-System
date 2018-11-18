using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;
using static MgtSys.Methods;

namespace MgtSys
{
    public static class TheUpdateFunction
    {
        public static void Update_Inventory()
        {
            Console.WriteLine("Your current inventory ...");
            ListTheProducts(ShowProducts(), "SQL");
            Console.WriteLine("\n");

            bool isTrue = false;
            do
            {
                // Console.WriteLine("What would you like to update?");
                Console.Write("Please enter your desired changes: ");
                string input = Convert.ToString(Console.ReadLine());

                UpdateProducts(input);
                // checkEditCmd(input);
                Console.WriteLine("Would you like to update additional items? [Y/n]");
                string command = Console.ReadLine().ToLower();

                bool chk = false;

                do
                {
                    if(command != "y" && command != "n")
                        Console.WriteLine($"<{command}>\nUnknown command!");
                    if(command == "y")
                    {
                        Console.Clear();
                        chk = true;
                    }
                    if(command == "n")
                    {
                        chk = true;
                        isTrue = true;
                    }
                } while (chk == false);

            } while (isTrue == false);
        }
    }
}