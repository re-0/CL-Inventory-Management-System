using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;
using static NewIn.LargeVoidMethods;

namespace NewIn
{
    class Program
    {
        static void Main(string[] args)
        {   
            string[] firstActionCommands = {"add", "rem", "upd", "exit"};


            Console.WriteLine("Welcome!\n\t\tThis project is developed by Reginald Okonkwo, 3rd year Econ student at Uni of Manchester");

            
            bool firstBool = false;
            do
            {
                Console.WriteLine("What would you like to do?");

                string firstAction = Convert.ToString(Console.ReadLine());
                if(!firstActionCommands.Contains(firstAction.ToLower()))
                {
                    Console.Clear();
                    Console.WriteLine("Uh oh! Seems like you entered an unknown command");
                }

                if(firstAction.ToLower() == "add")
                {
                    Add_Inventory();
                    firstBool = true;
                }

                if(firstAction.ToLower() == "rem")
                {
                    Console.WriteLine("Removing inventory will be added soon ...");
                    firstBool = true;
                }                

                if(firstAction.ToLower() == "upd")
                {
                    Console.WriteLine("Updating inventory will be added soon ...");
                    firstBool = true;
                }

                if(firstAction.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting the program ...");
                    firstBool = true;
                }
                
            } while (firstBool == false);
        } // MAIN
    } // program class
} // namespace
