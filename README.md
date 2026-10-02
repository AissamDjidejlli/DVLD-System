# 🚗 Driver & Vehicle Licensing Department (DVLD) System

![C#](https://img.shields.io/badge/C%23-.NET%20Framework-blue)
![Architecture](https://img.shields.io/badge/Architecture-3--Tier%20%2F%20DAL--BLL--Presentation-green)
![Database](https://img.shields.io/badge/Database-SQL%20Server-red)
![IDE](https://img.shields.io/badge/IDE-Visual%20Studio-purple)

DVLD is a comprehensive, enterprise-grade desktop application designed to manage driver licensing operations, application workflows, driving tests, international licenses, and license detention/release procedures. 

Built using C#, ADO.NET, and SQL Server, the application adheres strictly to N-Tier Architecture (DAL, BLL, Presentation) to ensure high scalability, strict separation of concerns, and robust data integrity.

---

## 🔑 Key Features

### 👤 Management Modules
- **People & Users Management:** Complete CRUD operations with personal details, profile picture integration, secure credentials handling, and role-based permissions.
- **Drivers Management:** Tracking driver history, active driving licenses, and linked person records.

### 📝 Applications & Licensing Workflows
- **Local Driving License Applications:** Issue, renew, replace (lost/damaged), and track multi-step license applications.
- **Test Appointment Scheduling:** Multi-stage test scheduling system (Vision Test, Written Test, Street Test) with appointment locking and pass/fail criteria validation.
- **International License Issuance:** Automated issuance of international driving permits linked to active local licenses.

### 🛑 Detain & Release System
- **License Detention:** Fine calculation, custom detention grounds, and constraint validation preventing duplicate detentions.
- **License Release Workflow:** Automated application creation for license release, fee processing, and real-time status updates.

---

## 🏗️ Software Architecture

The application is engineered using a decoupled **3-Tier Architecture**:

### Architectural Highlights
- **Encapsulated Data Access:** Parameterized ADO.NET queries preventing SQL Injection attacks.
- **Clean NULL Handling:** Nullable types (`int?`, `DateTime?`) combined with defensive `DBNull.Value` parameter mappings across database boundaries.
- **Custom UI Controls:** Modularized reusable UI controls (`UserControl`) to minimize code duplication across search, person details, and license verification cards.
- **Relational Integrity:** Foreign Key constraint management, transactional logic integrity, and SQL views for efficient multi-table read operations.

---

## 🛠️ Tech Stack & Prerequisites

- **Language:** C#
- **Framework:** .NET Framework (Windows Forms)
- **Database:** Microsoft SQL Server
- **Data Access:** ADO.NET (Raw SQL with Parameterized Commands)
- **Development Environment:** Visual Studio

---

## 🚀 Getting Started

### 1. Database Setup
1. Open **SQL Server Management Studio (SSMS)**.
2. Execute the provided database creation script located in `Database/DVLD_DatabaseScript.sql`.
3. Verify table schema creation and constraint setup.

### 2. Application Configuration
1. Clone this repository:
   ```bash
   git clone [https://github.com/AissamDji/DVLD-System.git](https://github.com/AissamDji/DVLD-System.git)

Open DVLD.sln in Visual Studio.

Update the Connection String inside clsDataAccessSettings.cs to point to your local SQL Server instance:

C#
public static string ConnectionString = "Server=YOUR_SERVER_NAME;Database=DVLD;Integrated Security=True;";
Build and run the project (F5).

👨‍💻 Author
Aissam

Computer Science & Software Engineering Student

LinkedIn: [(https://www.linkedin.com/in/aissam-djidjelli-400304379/)]

GitHub: [(https://github.com/AissamDji)]
   
