# Inventory Management System (C#, Command-line based)

### Intro
This is a command-line based **inventory management system** that will become part of a Point of Sales (PoS) system.

The PoS, too, will be command-line based at first, however, as I progress in my software development journey, I will start building a GUI (probably using WPF).

There are two main reasons as to why I decided to go with SQLite as a database. Firstly, I wanted to use a lightweight database that runs on both my Windows Desktop PC, as well as on my laptop (w/ Linux Mint) without having to do much installation. Secondly, using SQLite allows everyone who tries to run the code to do so without having to download and install unecessary software.

### Build project files
```
$ dotnet new console -o Your_project_name
$ cd Your_project_name
$ dotnet add package Microsoft.Data.SQLite
$ dotnet restore
```
Next, just create and copy & paste the required source files. Don't forget the .db file!

### Run
`$ dotnet run`

### Usage
Right now, at the start of the program, only the command `add` is supported.

The program accepts entries in the form of `name, brand, price, quantity`. I decided to go with a way that resembles *.csv-files*, as this is an easy and efficient way to work with terminal inputs.

**Ex.:** `C# 7.0 in a Nutshell, O'Reilly, 34.23, 1`

If you want to leave some info out you can write something like this: `Some name, , 10, 1`
While this would not throw any errors and the product would still be added to the database, I highly recommend using something like `N/A` instead of leaving a blank.

After you added your last item to the inventory (at least one), you will be given the choice of doing any adjustments to the items you added during the current session. You can decide between removing an item, or doing some editing. Both ways use the item's index in the list and assume you start counting at *1*.

When you decide to edit an item, you can use the following commands for further action: `name`, `brand`, `price`, and `quantity`. Data is accepted in the form of `index,change`. When `change` is a string, every whitespace will be kept!

### What happens next?
Next, I'll create a method for updating an item in the database, or deleting one. 
