<div dir="ltr">

# Delivery Management System

A sophisticated Full-stack system for logistics and delivery management. The system provides an end-to-end solution for managing packages, delivery personnel, and recipients, emphasizing Clean Architecture, performance, and a smooth user experience.

## 🛠 Technologies & Tools

### Server Side (Backend)
* **Framework:** .NET Core (ASP.NET Core Web API)[cite: 3]
* **Architecture:** Clean Architecture & Repository Pattern
* **Database:** SQL Server & Entity Framework Core
* **Security:** JWT (JSON Web Tokens) based authentication and authorization
* **Libraries:** AutoMapper for entity mapping, Dependency Injection

### Client Side (Frontend)
* **Library:** React.js
* **State Management:** React Hooks (useState, useEffect) and Custom Hooks for API management
* **Communication:** Axios for REST API interaction
* **Styling:** Custom responsive CSS3

## 🚀 Key Features
* **Dynamic Delivery Management:** Create, update, delete, and track package status (In Transit / Delivered).
* **Logistics Entity Management:** Systems for registering and managing delivery personnel (including work days and hours) and recipients.
* **Security & Permissions:** Secure Login system based on user roles (Recipient, Delivery Person).
* **Interactive UI:** Smart data tables, validated forms, and real-time data filtering.

## 📂 Project Structure (Architecture)
The project is built in a layered structure separating business logic from data infrastructure:
* **Core:** Definition of models, DTOs, and Interfaces.
* **Data:** Repository implementation and DB context (DataContext).
* **Service:** Business Logic layer.
* **API:** Controller layer exposing Endpoints to the client.

## ⚙️ Local Setup
1. Clone the repository: `git clone https://github.com/Efrat8277/SendsProject.git`
2. Update the `ConnectionString` in the `appsettings.json` file.
3. Run `Update-Database` in the Package Manager Console.
4. Run the Backend via Visual Studio.
5. In the Client folder: Run `npm install` and then `npm start`.

</div>
