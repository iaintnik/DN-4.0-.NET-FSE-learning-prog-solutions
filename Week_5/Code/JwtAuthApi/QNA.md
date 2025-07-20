# 🔐 JWT Authentication Labs – From Basics to Advanced Authorization

This guide explains the progression of authentication and authorization features implemented across Lab 1 to Lab 4. Each lab adds a real-world capability, making the microservice more secure and production-ready.

---

## 🧩 Lab 1 vs Lab 2 – From Token Creation to Secured Endpoints

| 🔍 Feature              | **Lab 1: JWT Issuance**                             | **Lab 2: Securing Endpoints with JWT**             |
|------------------------|------------------------------------------------------|----------------------------------------------------|
| 🎯 Objective           | Generate JWT on successful login                    | Enforce `[Authorize]` on API routes                |
| 🧠 Key Component       | `AuthController.cs`                                  | `SecureController.cs`                              |
| 🔑 Token Handling      | Creates JWT using secret key                        | Verifies token before processing request           |
| 📬 API Testing         | `POST /api/Auth/login`                              | `GET /api/Secure/data`                             |
| 🚫 Without Token       | No restriction, open access                         | Access denied → `401 Unauthorized`                 |
| ✅ With Valid Token    | Returns a signed JWT                                | Grants access → `200 OK`                           |

---

## 🔐 Lab 3 vs Lab 4 – Adding Roles & Expiry Handling

| 🔍 Feature              | **Lab 3: Role-Based Access**                        | **Lab 4: Token Expiry Awareness**                  |
|------------------------|------------------------------------------------------|----------------------------------------------------|
| 👥 Role Usage          | Adds `"Admin"` role into JWT                        | No roles added, focuses on expiry behavior         |
| 🔐 Access Control      | Uses `[Authorize(Roles = "Admin")]`                | Adds custom logic for token expiration             |
| 📍 Main Code Focus     | `AdminController.cs`                                | `Program.cs` with JWT event handling               |
| ⏱️ Expiry Detection    | Not implemented                                     | Detects expired token & appends response header    |
| 🧪 API Test            | `GET /api/Admin/dashboard`                          | Test with expired token → check `"Token-Expired"` header |
| ❌ Error Feedback      | Generic `401` on any auth failure                   | Custom handling for token expiration               |

---

## 🧾 Summary Table – Features Across Labs

| Feature/Capability                 | Lab 1 | Lab 2 | Lab 3 | Lab 4 |
|-----------------------------------|:-----:|:-----:|:-----:|:-----:|
| 🔑 JWT Token Creation             |  ✔️   |  ✔️   |  ✔️   |  ✔️   |
| 🔐 `[Authorize]` Usage            |  ❌   |  ✔️   |  ✔️   |  ✔️   |
| 👤 Role-Specific Authorization    |  ❌   |  ❌   |  ✔️   |  ✔️   |
| ⏳ Expired Token Awareness        |  ❌   |  ❌   |  ❌   |  ✔️   |

---

## 🚀 Conclusion

As you progressed through each lab, the system evolved to support:

- ✅ Basic authentication with JWT token generation
- 🔒 Endpoint security using `[Authorize]`
- 🛡️ Role-based access for granular control
- ⏰ Graceful handling of expired tokens with custom responses

Each step brings you closer to a **secure, scalable, and real-world-ready authentication system**.

---

> ℹ️ Want a visual diagram or flowchart to complement this README? Let me know!
