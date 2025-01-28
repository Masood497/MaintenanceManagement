# Service Management System

This is a Service Management System that allows users to manage services, tasks, and their associated actions. This document will guide you on how to set up the project, run migrations, and interact with the API.

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Setup Instructions](#setup-instructions)
3. [Running Migrations](#running-migrations)

-Run these commands in package manager console
-dotnet ef migrations add InitialCreate
-dotnet ef database update

   - [Create a Migration](#create-a-migration)
   - [Apply Migrations to the Database](#apply-migrations-to-the-database)
   - [Rollback a Migration](#rollback-a-migration)
   - [View All Migrations](#view-all-migrations)
4. [API Endpoints](#api-endpoints)
   - [Login API](#login-api)
   - [Add a Service](#add-a-service)
   - [Update a Service](#update-a-service)
   - [Delete a Service](#delete-a-service)
5. [Running the Application](#running-the-application)

---

## Prerequisites

Before you start, make sure you have the following installed:

- **.NET SDK** (version 6.0 or later)
- **SQL Server** (or any compatible database system)
- **Visual Studio** or any IDE with support for .NET
- **Entity Framework Core CLI Tools** for running migrations

---

## Setup Instructions

### 1. **Clone the Repository**

Clone the repository to your local machine:

```bash
git clone https://github.com/Masood497/MaintenanceManagement.git

