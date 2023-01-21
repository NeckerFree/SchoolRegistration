<a name="readme-top"></a>

<!-- TABLE OF CONTENTS -->

# 📗 Table of Contents

- [📖 About the Project](#about-project)
  - [🛠 Built With](#built-with)
    - [Tech Stack](#tech-stack)
    - [Key Features](#key-features)
- [💻 Getting Started](#getting-started)
  - [Setup](#setup)
  - [Prerequisites](#prerequisites)
  - [Install](#install)
  - [Usage](#usage)
- [👥 Authors](#authors)
- [⭐️ Show your support](#support)
- [🙏 Acknowledgements](#acknowledgements)
- [❓ FAQ (OPTIONAL)](#faq)
- [📝 License](#license)

<!-- PROJECT DESCRIPTION -->

# 📖 [School Registration] <a name="about-project"></a>

**[School Registration]** is an application that allows the students rate the courses they are doing and perform CRUD operations to evaluate a course.

## 🛠 Built With <a name="built-with"></a>

### Tech Stack <a name="tech-stack"></a>

<details>
  <summary>Client</summary>
  <ul>
    <li><a href="https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-7.0">ASP.NET MVC</a></li>
  </ul>
</details>

<details>
  <summary>Server</summary>
  <ul>
    <li><a href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0">.NET Core Minimal API</a></li>
  </ul>
</details>

<details>
<summary>Database</summary>
  <ul>
    <li><a href="https://www.microsoft.com/en-US/download/details.aspx?id=101064">SQL Server</a></li>
  </ul>
</details>

<!-- Features -->

### Key Features <a name="key-features"></a>

- **EF Core Database First**
- **Data Annotations**
- **Extension Methods**
- **Dependency Injection**
- **GIT Flow**

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- GETTING STARTED -->

## 💻 Getting Started <a name="getting-started"></a>

To get a local copy up and running, follow these steps:

### Prerequisites

In order to run this project you need:

- Visual Studio .NET 2022 updated to use NET Core 7
- SQL Server Database 

### Setup

1. Clone this repository to your desired folder:

```sh
  cd my-folder
  git clone https://github.com/NeckerFree/SchoolRegistration
```

2. Create the Application database in SQl Server

3. Create a new User as db_owner of this batabase

4. Modify the connection string (SchoolConnection) to point your database in the  files:
```
    \SR.WebApp\appsettings.json
    \SR.MinApi\appsettings.json
```
### Install

Install this project with:
1. Build the solution and assure that doesn't have errors

2. Set the desired project as default 
3. If require run backend and frontend at same time: in Solution Explorer, right-click the solution name and select Set Startup Project. 
   Change the startup project from Single startup project to Multiple startup projects. Select Start for each project’s action.

### Usage

To run the project, 

Start the application (F5), the /swagger/index.html page is displayed

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- AUTHORS -->

<!-- AUTHORS -->

## 👥 Authors <a name="authors"></a>

👤 **Elio Cortés**

- GitHub: [@NeckerFree](https://github.com/NeckerFree)
- Twitter: [@ElioCortesM](https://twitter.com/ElioCortesM)
- LinkedIn: [elionelsoncortes](https://www.linkedin.com/in/elionelsoncortes/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->

## 🤝 Contributing <a name="contributing"></a>

Contributions, issues, and feature requests are welcome!

Feel free to check the [issues page](../../issues/).

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- SUPPORT -->

## ⭐️ Show your support <a name="support"></a>

> Write a message to encourage readers to support your project

If you like this project please start my project

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- FAQ (optional) -->

## ❓ FAQ (OPTIONAL) <a name="faq"></a>

> - **What command are required to Scaffold from Scratch a DB First?**

  - Run next commands:
  ```
    dotnet add SR.DataAccess package Microsoft.EntityFrameworkCore.Design
    dotnet add SR.DataAccess package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add SR.DataAccess package Microsoft.EntityFrameworkCore.Tools
    dotnet tool update --global dotnet-ef
    dotnet ef dbcontext scaffold "Data Source=localhost\ELIO_SQL;Initial Catalog=[your database]; User Id=[your user];Password=[your password];Encrypt=False" 
        Microsoft.EntityFrameworkCore.SqlServer --project SR.DataAccess --output-dir "SR.Models\Models" --context-dir "SR.DataAccess\Data" --namespace SR.Models 
        --context-namespace SR.DataAccess --context SchoolContext -f --no-onconfiguring --data-annotations
    dotnet tool install --global dotnet-ef
    dotnet tool update --global dotnet-ef 
  ```
 
<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->

## 📝 License <a name="license"></a>

This project is [MIT](./LICENSE) licensed.

_NOTE: we recommend using the [MIT license](https://choosealicense.com/licenses/mit/) - you can set it up quickly by [using templates available on GitHub](https://docs.github.com/en/communities/setting-up-your-project-for-healthy-contributions/adding-a-license-to-a-repository). You can also use [any other license](https://choosealicense.com/licenses/) if you wish._

<p align="right">(<a href="#readme-top">back to top</a>)</p>
