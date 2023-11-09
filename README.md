<img src="https://github.com/jil1710/readmedemo/assets/125335932/bba44243-71fb-4263-ab3b-0fddcb115467" width="360px" height="200px"/>

# OData Asp.Net Core

- The OData(Open Data Protocol) is an application-level protocol for interacting with data via the restful interface. OData supports the description of data models, editing, and querying of data according to those models.

-  OData helps you focus on your business logic while building RESTful APIs without having to worry about the various approaches to define request and response headers, status codes, HTTP methods, URL conventions, media types, payload formats, query options, etc. OData also provides guidance for tracking changes, defining functions/actions for reusable procedures, and sending asynchronous/batch requests.

- OData RESTful APIs are easy to consume. The OData metadata, a machine-readable description of the data model of the APIs, enables the creation of powerful generic client proxies and tools.

- OData query features are:
   **$select,
   $orderBy,
   $filter,
   $skip,
   $count,
   $expand**

## Let's Implement OData in ASP.NET Core APIs

- **Create .NET Web API Application:** To create .NET application we use IDE's(editors) either Visual Studio 2022 or Visual Studio Code(Using .NET CLI command). In this demo, we are going to use Visual Studio Code(using .NET CLI command)..NET CLI command to create Web API project.

    ```csharp
       dotnet new webapi -o name_of_your_project
    ```

- **Install Entity Framework Core NuGet Package && Entity Framework Core SQL Server Package from Package Manager Console:**

  ```csharp
    Install-Package Microsoft.EntityFrameworkCore
  ```

  ```csharp
     Install-Package Microsoft.EntityFrameworkCore.SqlServer
  ```

- **Create Models**

  ![image](https://github.com/jil1710/readmedemo/assets/125335932/29740725-0ca6-4569-9f87-da908957e29e)

  ![image](https://github.com/jil1710/readmedemo/assets/125335932/56f179b2-1175-4f54-99a8-96be9d27649a)

- **Create DBContext in order to communicate with database**

  ![image](https://github.com/jil1710/readmedemo/assets/125335932/4fe3894f-1704-44f9-81e0-40794f4653e2)

  ![image](https://github.com/jil1710/readmedemo/assets/125335932/47c5a17b-a408-4f5d-9454-a4b3d6fe1905)


- **We also create a simplified service class that will handle the interactions with the database:**

  ![image](https://github.com/jil1710/readmedemo/assets/125335932/5400e338-6f42-4b83-a2fe-25fb3bace5a8)

- **Now Install OData NuGet Package:**

  ```csharp
    Install-Package Microsoft.AspNetCore.OData
  ```

- **Register OData Service:** Now we have to extend the 'AddControllers()' service to register our OData service. Also, we have to register our OData query types like 'Select', 'filter', 'count', 'expand', etc. And also our return type is 'Employee', so we have to register the our 'Employee' type with OData EDM(Entity Data Model). The Entity Data Model is used to map our response type so that the OData engine can analyze its queries. So in 'Program.cs' let's crate method that will return the ODATA EDM.

  ![image](https://github.com/jil1710/readmedemo/assets/125335932/f382ba4f-57f0-4445-9b36-7aeeff812b5c)

  ![image](https://github.com/jil1710/readmedemo/assets/125335932/b4c4bbe6-f663-4b99-90e4-2e0a4fe4b199)

  - So here `EmployeeDept` is controller name and return `Employee` type. Similarlly If you have more controller with return type you can add like above in GEtEdmModel() method.
 
 - Let’s consider the case where we are building a web service that provides information on Employees and their Department. In order to request a resource using OData we can send a simple GET request to the web service:

   ```
    GET /odata/employeedept
   ```

   - This will get us all the available entries. We can also query for a specific entry, by providing its ID:

     ```
      GET /odata/employeedept(1)
     ```
     
   - Here we request the top 5 results, after skipping the first 10 entries. Note that all options in OData start with the dollar sign ($) and that they can be combined in the same query.

     ```
      GET /odata/employeedept?$top=5&$skip=10
     ```

   - **Here is a list of the query options available in OData:**

     - /odata/employeedept?$top=5  –   Get the top 5 entries
       
     - /odata/employeedept?$count=true   –   Get the total entry count (along with all the entries)
       
     - /odata/employeedept?$filter=name eq ‘jhon’   –   Get entries where name=’jhon’
       
     - /odata/employeedept?$filter=contains(name, 'patel')   –   Get entries where the name contains ‘patel’
       
     - /odata/employeedept?$orderby=name   –   Order entries by employee name
       
     - /odata/employeedept?$skip=5   –   Skip the five first entries and return the rest
       
     - /odata/employeedept?$select=ID,Name   –   Return only the ID and Name of all employeedept
       
     - /odata/employeedept?$expand=department   –   Include also the department details corresponding to employees
       
     - /odata/employeedept?$select=id,name&$expand=department($select=name) - Select Id,Name of Employees also include department details but not all only name of the department.
    
   - **Let's Execute all the option in Postman** :
  
     1. **$expand:** Using $expand we can query the internal or navigating property object. So we have to assign the navigation property name to the '$expand' then we can apply all other operations like '$select', '$filter', '$skip' on the navigation property type.

        ![image](https://github.com/jil1710/readmedemo/assets/125335932/2c230426-db2e-46c9-831b-bc612a17a775)
        
        ```json
           {
               "@odata.context": "https://localhost:7097/odata/$metadata#EmployeeDept(*,Department(Name))",
               "value": [
                   {
                       "EmployeeId": 1,
                       "FirstName": "Jil",
                       "LastName": "pat",
                       "DepartmentId": 1,
                       "Department": {
                           "Name": "Engineering"
                       }
                   }
               ]
           }
        ```

     2. **$count:** It count the number of records produce by query after all the filter or skips return count of records along with all records.Here we select all the records whose employeeId is greater than 3 and also expand department details of the employees.
    
        ![image](https://github.com/jil1710/readmedemo/assets/125335932/839f1cf6-78b9-48a7-8f7a-414769799171)

        ```json
           {
                "@odata.context": "https://localhost:7097/odata/$metadata#EmployeeDept(*,Department(Name))",
                "@odata.count": 3,
                "value": [
                    {
                        "EmployeeId": 4,
                        "FirstName": "Janet",
                        "LastName": "Gates",
                        "DepartmentId": 3,
                        "Department": {
                            "Name": "Sales"
                        }
                    },
                    {
                        "EmployeeId": 6,
                        "FirstName": "fgh",
                        "LastName": "fghfg",
                        "DepartmentId": 2,
                        "Department": {
                            "Name": "Administration"
                        }
                    },
                    {
                        "EmployeeId": 45,
                        "FirstName": "fgh",
                        "LastName": "fghfg",
                        "DepartmentId": 1,
                        "Department": {
                            "Name": "Engineering"
                        }
                    }
                ]
            }
        ```

     3. **$select:** The $select system query option allows clients to request a specific set of properties for each entity or complex type. The set of properties will be comma separated while requesting.

        ![image](https://github.com/jil1710/readmedemo/assets/125335932/4f9fb07c-900e-45db-b7ed-d4bf3d7a34b8)

        ```json
           {
               "@odata.context": "https://localhost:7097/odata/$metadata#EmployeeDept(FirstName,LastName)",
               "value": [
                   {
                       "FirstName": "Jil",
                       "LastName": "pat"
                   },
                   {
                       "FirstName": "Keith",
                       "LastName": "Harris"
                   },
                   {
                       "FirstName": "Donna",
                       "LastName": "Carreras"
                   },
                   {
                       "FirstName": "Janet",
                       "LastName": "Gates"
                   },
                   {
                       "FirstName": "fgh",
                       "LastName": "fghfg"
                   },
                   {
                       "FirstName": "fgh",
                       "LastName": "fghfg"
                   }
               ]
           }
        ```

     4. **$filter:** The $filter filters data based on a boolean condition. The following are conditional operators that have to be used in 'URLs':

         - eq- equals to
         - ne - not equals to
         - gt -greater than
         - ge - greater than or equal
         - lt - less than
         - le - less than or equal
       
        ![image](https://github.com/jil1710/readmedemo/assets/125335932/25977c9d-ebb2-4f86-bfe0-c88a140f4f82)

        ```json
           {
               "@odata.context": "https://localhost:7097/odata/$metadata#EmployeeDept(FirstName)",
               "value": [
                   {
                       "FirstName": "Jil"
                   },
                   {
                       "FirstName": "Keith"
                   },
                   {
                       "FirstName": "Janet"
                   },
                   {
                       "FirstName": "fgh"
                   }
               ]
           }
        ```

     5. **$orderby:** The $orderby sorts the data using 'asc' and 'desc' keywords. We can do sorting on multiple properties using comma separation.
    
        ![image](https://github.com/jil1710/readmedemo/assets/125335932/23a6eb9f-90a6-4f74-8e12-cc26ab694214)

        ```json
          {
              "@odata.context": "https://localhost:7097/odata/$metadata#EmployeeDept(FirstName)",
              "value": [
                  {
                      "FirstName": "fgh"
                  },
                  {
                      "FirstName": "Janet"
                  },
                  {
                      "FirstName": "Jil"
                  },
                  {
                      "FirstName": "Keith"
                  }
              ]
          }
        ```

     6. **$skip:** The $skip skips the specified number of records and fetches the remaining data
    
        ![image](https://github.com/jil1710/readmedemo/assets/125335932/5a12fea8-9258-4247-a47a-a238852e926d)

        ```json
           {
               "@odata.context": "https://localhost:7097/odata/$metadata#EmployeeDept(FirstName)",
               "value": [
                   {
                       "FirstName": "Jil"
                   },
                   {
                       "FirstName": "Keith"
                   }
               ]
           }
        ```

     7. **$top:** The $top fetches specified the count of top records in the collection. so to work this operator, we must specify an extension method like 'SetMaxTo(specify_max_number)'.

        ![image](https://github.com/jil1710/readmedemo/assets/125335932/18ad5a21-7e03-4c4f-a67f-a4a98c88626a)

        ![image](https://github.com/jil1710/readmedemo/assets/125335932/467f12fe-ef55-4d11-9737-f25afe1c0916)

        ```json
        {
             "@odata.context": "https://localhost:7097/odata/$metadata#EmployeeDept(FirstName)",
             "value": [
                 {
                     "FirstName": "Janet"
                 },
                 {
                     "FirstName": "fgh"
                 }
             ]
         }
        ```



        



    
        


        







  
