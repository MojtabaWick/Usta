# 🎉 **Usta – Service Marketplace Platform**  
A full‑stack service marketplace built with **.NET**, designed as the final project of the **Maktab Sharif Bootcamp**.

---

## 🚀 **Overview**
Usta is a service marketplace platform where users can request services, manage orders, and interact with experts.  
This project is built using **clean architecture**, **scalable backend patterns**, and **production‑grade tools**.

---

## 🛠️ **Technologies & Tools Used**

### **Backend**
- ⚙️ **C# (.NET 10)**
- 🧱 **Onion Architecture**
- 🌐 **ASP.NET Core Web API**
- 🧩 **Razor Pages**
- 🗄️ **SQL Server**
- 🔌 **Entity Framework Core**
- ⚡ **Dapper** (High‑performance queries)
- 🔐 **Microsoft Identity** (User & Role Management)
- 🧵 **TransactionScope** (Atomic operations)
- 🧰 **Dependency Injection**
- 📦 **Caching**
- 📊 **Result Pattern**
- 📄 **Paged Result (Pagination)**

### **Logging & Monitoring**
- 📝 **Serilog**
- 🛰️ **Custom Logging Middleware**

### **Frontend**
- 🎨 **HTML**
- 🎭 **CSS**
- 🅱️ **Bootstrap**

---

## 📦 **How to Run the Project**

### 1️⃣ **Configure Database Connection**
Update the SQL Server connection string inside `appsettings.json`:

```json
  "ConnectionStrings": {
    "SQL": "Server=.;Database=UstaDB;Trusted_Connection=True;"
  }
```

---

### 2️⃣ **Apply Migrations & Seed Initial Data**
The project includes **initial seed data**.  
To create the database and insert seed data:

In **Package Manager Console**:

- Set the **EF Core project** as the *Default Project*  
- Set the **Razor Pages project** as *Startup Project*  
- Run:

```powershell
Update-Database
```

This will:
- Create the database  
- Insert all initial seed data  

---

## 🔑 **Admin Panel Login**

Use the following credentials to access the admin panel:

```
Username: admin
Password: Admin@123
```

---

## 📁 **Project Structure**
```
Usta/
│── Usta.Web (Razor Pages UI)
│── Usta.API (REST API)
│── Usta.Domain (Entities, Interfaces, Services)
│── Usta.Infrastructure (EF Core, Dapper, Repositories)
│── Usta.Application (App Services, DTOs, Result Pattern)
│── Usta.Logging (Middleware, Serilog Config)
```

---

## 🌟 **Key Features**
- 🔐 Secure authentication & authorization with Microsoft Identity  
- 🧱 Clean, scalable **Onion Architecture**  
- ⚡ Hybrid data access (EF Core + Dapper)  
- 🧾 Structured logging with Serilog  
- 📄 Server-side pagination  
- 🧵 Transaction-safe operations  
- 🚀 High-performance caching  
- 🎨 Responsive UI with Bootstrap  

---

## 🤝 **Contributions**
Pull requests are welcome.  
For major changes, please open an issue first to discuss what you would like to change.

---

## 📜 **License**
This project is open-source and available under the MIT License.
