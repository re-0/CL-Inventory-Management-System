# Inventory Management System (C#, Command-line based)
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
Following soon!
