# 📌 Cat Records API

![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![Vue](https://img.shields.io/badge/Vue-3.x-green)
![Tests](https://img.shields.io/badge/tests-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-lightgrey)
![Dockerized](https://img.shields.io/badge/docker-ready-blue)
![CI](https://github.com/h-albaNatali/cat-records-api/actions/workflows/ci.yml/badge.svg)
![codecov](https://codecov.io/gh/h-albaNatali/cat-records-api/branch/master/graph/badge.svg)



## 📖 Overview

The Cat Records API is a RESTful web service built to manage and query user-defined records, including the ability to fetch and store external cat facts. The project is developed in **.NET 9** using **Clean Architecture** and follows best practices like **DDD**, **Dependency Injection**, and **Separation of Concerns**. It includes a Vue 3 + TypeScript frontend and background processing via **Hangfire**.

This document includes all necessary instructions to install, configure, run, and test the application locally or via Docker Compose.

---

## 🔹 Technologies Used

- ASP.NET Core 9 (Web API)
- Vue 3 + TypeScript (Frontend)
- MSSQL Server (via RDS or Docker)
- Entity Framework Core (ORM)
- Hangfire (Background jobs)
- Serilog (Logging)
- Polly (Retry policies)
- Swagger (API Documentation)
- Docker & Docker Compose

---

## 🛠️ Setup & Configuration

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/h-albaNatali/cat-records-api.git
cd cat-records-api
```

### 2️⃣ Set Up Environment (Optional)

Update `appsettings.json` or `appsettings.Development.json` with your SQL Server connection string. Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your-server;Database=CatRecordsDb;User Id=your-user;Password=your-password;"
}
```

### 3️⃣ Run with .NET (Locally)

```bash
dotnet build
dotnet run --project src/MyProject.Api
```

Frontend:

```bash
cd frontend
npm install
npm run dev
```

### 4️⃣ Run with Docker Compose

```bash
docker-compose up --build
```

---

## 🔹 API Endpoints (Backend)

### 🔹 Records

**GET** `/api/data` – Fetch all records\
**POST** `/api/data` – Add new record\
**PUT** `/api/data/{id}` – Update record\
**DELETE** `/api/data/{id}` – Delete record

### 🔹 External API Integration

**POST** `/api/data/fetch-external` – Fetch and store cat facts (from `https://catfact.ninja/facts?limit=20`)

---

## 🚀 Testing the API with cURL

### Create a Record

```bash
curl -X POST http://localhost:5257/api/data \
 -H "Content-Type: application/json" \
 -d '{"title": "Cat Fact", "description": "Cats sleep 70% of their lives."}'
```

### Fetch All Records

```bash
curl http://localhost:5257/api/data
```

### Fetch External Data

```bash
curl -X POST http://localhost:5257/api/data/fetch-external
```

---

## ❌ Possible Errors & Solutions

| Error                           | Cause                                     | Solution                                |
| ------------------------------- | ----------------------------------------- | --------------------------------------- |
| `500 Internal Server Error`     | DB connection or external API unreachable | Check connection string or API endpoint |
| `404 Not Found`                 | Endpoint typo or not implemented          | Verify Swagger docs                     |
| `Failed to fetch external data` | Public API down or changed                | Use another API or fix request          |

---

## 📄 Project Structure (Clean Architecture)

```
MyProject
├── Domain
├── Application
├── Infrastructure
├── Api
├── Worker
├── BackgroundJobs
├── Tests
└── Frontend (Vue)
```

---

## 🚀 Bonus Features

- Global error middleware
- Retry policies with Polly
- Docker Compose ready
- Logging via Serilog

---

## 💼 Author

**Henrique Alba**\
📧 [natalihenrique6@gmail.com](mailto\:natalihenrique6@gmail.com)\
🔗 [GitHub - h-albaNatali](https://github.com/h-albaNatali)

---
