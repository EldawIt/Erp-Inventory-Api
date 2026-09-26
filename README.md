# ErpSystem

A full Enterprise Resource Planning (ERP) system built with **.NET 9** and **ASP.NET Core Web API**, following Clean Architecture and the Result Pattern. The system covers Sales, Purchasing, Inventory, and General Accounting with full integration between modules through automated posting and double-entry journal entries.

---

## 🛠 Tech Stack

| Technology | Purpose |
|------------|---------|
| **.NET 9** | Framework |
| **ASP.NET Core Web API** | REST API |
| **Entity Framework Core** | ORM |
| **SQL Server** | Database |
| **Result Pattern** | Error handling without exceptions |
| **Unit of Work** | Transaction management |
| **Clean Architecture** | Separation of concerns |
| **Serilog** | Structured logging |
| **Mapster** | Object mapping |
| **FluentValidation** | Input validation |
| **JWT** | Authentication |

---

## 🏗 Architecture

Clean Architecture with strict separation:


---

## 📦 Modules

### 💰 Sales
- Create, get, list, and cancel sales invoices
- Post invoices: discharge stock, create `InventoryTransaction`, `SalesTransaction`, and two journal entries (Revenue + COGS)
- Reports: summary, by customer, by product

### 📥 Purchasing
- Create, get, list, and cancel purchase orders
- Receive orders: add stock, create `InventoryTransaction`, `PurchaseTransaction`, and a journal entry (Inventory + Input VAT / Supplier)
- Reports: summary, by supplier, by product

### 📦 Inventory
- Stock balances per warehouse with real-time quantity and average cost
- Inventory transactions (Addition / Discharge) with cost snapshots
- FIFO batch queue (planned)

### 🧾 Accounting
- Hierarchical Chart of Accounts (Asset, Liability, Equity, Revenue, Expense, Cost)
- Journal entries (manual + automatic) with balanced debit/credit validation
- Posting and reversing entries
- Account balances per fiscal period for fast reporting
- Financial reports: Trial Balance, Income Statement, Balance Sheet

---

## 🔐 Roles & Permissions

| Role | Code | Responsibilities |
|------|------|------------------|
| Admin | `Admin` | Full system access |
| Finance Manager | `FinanceManager` | Approvals, posting, financial reports |
| Accountant | `Accountant` | Chart of accounts, journal entries, reports |
| Warehouse Keeper | `Warehouse` | Stock management, posting invoices, receiving orders |
| Sales | `Sales` | Sales invoices, customers |
| Purchasing | `Purchasing` | Purchase orders, suppliers |

### Permission Matrix

| Feature | Admin | Finance Manager | Accountant | Warehouse | Sales | Purchasing |
|---------|-------|-----------------|------------|-----------|-------|------------|
| Create Sales Invoice | ✅ | ❌ | ❌ | ❌ | ✅ | ❌ |
| Post Sales Invoice | ✅ | ✅ | ❌ | ✅ | ❌ | ❌ |
| Cancel Sales Invoice | ✅ | ❌ | ❌ | ❌ | ✅ | ❌ |
| Create Purchase Order | ✅ | ❌ | ❌ | ❌ | ❌ | ✅ |
| Receive Purchase Order | ✅ | ✅ | ❌ | ✅ | ❌ | ❌ |
| Cancel Purchase Order | ✅ | ❌ | ❌ | ❌ | ❌ | ✅ |
| Create Journal Entry | ✅ | ❌ | ✅ | ❌ | ❌ | ❌ |
| Post Journal Entry | ✅ | ✅ | ✅ | ❌ | ❌ | ❌ |
| Reverse Journal Entry | ✅ | ❌ | ✅ | ❌ | ❌ | ❌ |
| Manage Accounts | ✅ | ❌ | ✅ | ❌ | ❌ | ❌ |
| View Financial Reports | ✅ | ✅ | ✅ | ❌ | ❌ | ❌ |
| Manage Users | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ |

---

## 🗄 Database Design

**Key decisions:**
- Soft Delete via `AuditableEntity`
- Snapshot costing (`UnitCost` stored on transactions)
- Denormalized invoice totals for fast reporting
- Normalized transactions referencing detail lines

---

## 🧠 Business Logic

### Sales Invoice Lifecycle


