# 🚲 VeloMax database management

[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/powered-by-oxygen.svg)](https://forthebadge.com)

<img src=https://user-images.githubusercontent.com/63778269/138353604-f87af710-c382-45b5-a900-e69670b2e7a4.gif height=230/>

Implementation of a database for a bicycle company with a WPF application.

VeloMax is an imaginary company that sells bicycles and spare parts for bicycles. Our goal was to create a new database to make the clients and selling management system easier to use. Using the WPF application you are able to manage the creation, deletion and modification of the bicycles, spare parts, clients, orders and suppliers. At any time you have access to the stock of bicycles and spare parts.

<br/>

## Set up

To create the database you'll first need to download the 3 CSV files (**assemblages.csv, modele.csv,  programme.csv**). These are the last tuples of the database. If you are using MySQL Workbench you have to put these files in the _Uploads_ folder of MySQL Workbench. At the following path : _MySQL/MySQL Server 8.0/Uploads_

If you  don't know where your _MySQL_ folder is you can run this line on MySQL Workbench:

```sql
SHOW VARIABLES LIKE "secure_file_priv";
```

Once you have placed the csv files in the _Uploads_ folder you can run the **VeloMax.sql** file to create your database.

Be careful in the **VeloMax_Debes_Delemazure.sln** to change the _server_ and _localhost_ variables to the ones you have on MySQL Workbench, in the Utilities.cs class line 29.
You can now launch the C# code (**VeloMax_Debes_Delemazure.sln**) to manage your database. Use the ID/Password _bozo/bozo_ to be connected as a client, which means you won't be able to modify or delete tuples of the database. And use the ID/Password _root/root_ to be connected as the administrator and have access to every function.

<img src=https://user-images.githubusercontent.com/63778269/138357657-85fe841c-9e29-401f-ba04-7736e3bcba15.gif height=250/>

<br/>
<br/>

## SQL database and Entity-Relationship model

We were given a document containing all the different needs of the client. The idea was to build an application able to record all the transactions between the company and the customers. It includes a square parts and bicycle management system, and a record saver for the customer’s information. The client also needed to manage their different suppliers. In order to do so, we created an entity-relationship model before creating our SQL database.

Here is the model : 

<img src=https://user-images.githubusercontent.com/63778269/138358612-3d51e8eb-5359-4523-ab61-355355593c9f.png height=400/>

We populate our database with all the information transmitted by the client. We mainly use the function CHECK to make sure that the data were well completed. As for the suppression of useless data, we decided to use the CASCADE option.

<br/>
<br/>

## WPF application

One of the main ideas of our project was to use the interoperability between the SQL Database and the Visual Studio environment to create a convenient application.

<p align="left"> 
  <img src=https://user-images.githubusercontent.com/63778269/138359063-cf808d32-af25-408c-994b-5203cd1c5f6f.gif height=280/>
  <img src=https://user-images.githubusercontent.com/63778269/138359072-c07541b5-2cf2-4883-bb21-9f50cc1f5b86.gif height=230/>
</p>

The employee of the company can make an order for a bike or a square part at any time with a real-time validation of the available stock. If a client purchases a bicycle, the system will check whether the bicycle is available or if it can be assembled with square parts. If the bicycle or the spare parts are not available, the system will automatically order the missing parts or bicycle. It is possible to choose the supplier depending of the delay of delivery or the price.

<p align="center"> 
  <img src=https://user-images.githubusercontent.com/63778269/138360030-f8af284a-3780-4538-96de-ce9591b62d0d.gif height=350/>
</p>

To have a better understanding of the company’s results and flux, we created an easy-to-use statistical study uploaded in real-time in our final application. This includes an analysis of the orders (mean for a specific bike or the total price for example) and the previous clients. The demo mode is a small presentation of what our WPF application can do and display.

<p align="left"> 
  <img src=https://user-images.githubusercontent.com/63778269/138359041-3b4617f9-d43b-47a7-9bb7-9f4d68e5bd5e.gif height=250/>
  <img src=https://user-images.githubusercontent.com/63778269/138359058-0462c636-0d77-480e-b03b-00f253bdd9d4.gif height=250/>
</p>

Finally, at any time the administrator can export one of the tables in a JSON format or the pieces near sold out in an XML format.

<br/>
<br/>

## Note

The C# code is in french so do not hesitate to contact me if you want more details in English.

### Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

<br/>
<br/>

# 🧍‍♂️ Authors

* **Hugo Debes** _alias_ [@hugodebes](https://github.com/hugodebes)
* **Charles Delemazure** _alias_ [@Charlesdele](https://github.com/Charlesdele)
