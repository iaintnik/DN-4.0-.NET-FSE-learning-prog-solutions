# 🔐 ASP.NET Core Web API - JWT Authentication and Authorization Microservice

## 📌 What is a Microservice?

A **microservice** is a small, independently deployable service that does one specific task. In modern web architectures, microservices communicate via HTTP APIs and are loosely coupled to ensure scalability, maintainability, and resilience.

In this project, we implement **authentication and authorization** as a microservice using **JWT (JSON Web Tokens)** in **ASP.NET Core Web API**.

---

## 🔐 JWT Authentication & Authorization Overview

JWT (JSON Web Token) is an open standard for securely transmitting information between parties as a JSON object.

- ✅ Stateless (doesn’t require server storage)
- ✅ Self-contained (includes user identity & claims)
- ✅ Used for authentication and authorization

---

## ✅ What We Built

⬇️ Step-by-step Implementation:

1. **Created ASP.NET Core Web API Project**
2. **Defined Models:**
   - `User.cs` ➝ Represents system users
   - `LoginModel.cs` ➝ Used for capturing login request
3. **Built Controllers:**
   - `AuthController.cs` ➝ Issues JWT on valid login
   - `SecureController.cs` ➝ Protected route (uses `[Authorize]`)
   - `AdminController.cs` ➝ Role-based protected route
4. **Added JWT Configuration in `appsettings.json`**
5. **Setup Authentication Middleware in `Program.cs`**
6. **Enabled Swagger for testing**
7. **Handled Expired Token Scenarios**

---

## ⚙️ JWT Structure Breakdown

A JWT token consists of three parts:

1. **Header** (Algorithm, Token Type)
2. **Payload** (Claims like `username`, `role`)
3. **Signature** (Signed using secret key)

Example Claims:

```json
{
  "name": "admin",
  "role": "Admin",
  "exp": 1625822399
}
