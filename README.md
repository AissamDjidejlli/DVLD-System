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
