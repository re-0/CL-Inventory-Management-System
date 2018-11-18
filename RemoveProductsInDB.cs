using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;
using static MgtSys.Methods;

namespace MgtSys
{
    public static class TheRemoveFromDBFunction
    {
        public static void Remove_Inventory()
        {
            bool isTrue = false;
            do
            {
                Console.WriteLine("\"CSV\" mode or \"newline\" mode?");
                string command = Console.ReadLine().ToLower();
                if(command != "csv" && command != "nl")
                {
                    Console.Clear();
                    Console.WriteLine($"<{command}>\nUnknown command. Please try again!");
                }

                ListTheProducts(ShowProducts(), "SQL");
                
                if(command == "csv")
                {
                    RemoveFromDB("csv");
                    isTrue = true;
                }

                if(command == "nl")
                {
                    RemoveFromDB("nl");
                    isTrue = true;
                }
            } while (isTrue == false);
        }
    }
}