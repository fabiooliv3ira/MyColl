# MyColl - Multi-platform Collectibles Store

**MyColl** is an integrated solution developed for the management and trade of collectible items (coins, stamps, jerseys, etc.). The project leverages the **.NET 8** ecosystem to deliver a consistent experience across both Web and mobile devices, ensuring high security and scalability.

---

## System Architecture

The project is built on a **decoupled model**, divided into three main components:

1.  **Multi-platform Frontend (RCL + Web + MAUI):**
    *   **RCL (Razor Class Library):** Centralizes the entire User Interface (UI) and client-side logic.
    *   **Blazor Web:** Host for browser-based access.
    *   **Blazor Hybrid (.NET MAUI):** Native application for Windows, Android, macOS, and more.
2.  **Backend (RESTful API):**
    *   Built with **ASP.NET Core Web API**, acting as the intermediary between clients and the database.
    *   Implements security via **JWT (JSON Web Tokens)** with Role-based Claims.
3.  **Database (SQL Server):**
    *   Data persistence using **Entity Framework Core 8** with a *Code-First* approach.

---

## Key Features

### Audiences and Profiles
*   **Anonymous User:** Catalog browsing, advanced search, and viewing featured products.
*   **Customer:** Shopping cart management and simulated checkout process.
*   **Provider:** Management of their own inventory.
*   **Administrator:** Full user control (account approval), category management, profit margin definition, and order logistics.

### Technical Highlights
*   **Cascading Search:** Reactive filters that dynamically update subcategories based on the selected parent category.
*   **Chunked Image Upload:** A robust binary file upload system via SignalR, processing data in 512KB blocks to ensure connection stability.
*   **Automated Business Rules:** Automatic calculation of final prices (Admin Fee) and real-time stock reduction upon shipping.
*   **Hybrid Security:** Centralized identity management through JWT tokens, providing a secure and consistent authorization flow between the Multi-platform Frontend and the RESTful API.

---

## 🛠️ Running the Project
For correct operation, Visual Studio must start the following projects simultaneously (Multi-project Startup):
*   **RESTfulAPIMYCOLL** (Backend API)
*   **MYCOLL.Web** (Frontend Web)
*   **MYCOLL.MAUI** (Frontend App)
*   **MYCOLL** (Backoffice/Management)

---

## Authors
- [Fábio Oliveira](https://github.com/fabiooliv3ira)
- [Sebastian Gonçalves](https://github.com/sebie12)
