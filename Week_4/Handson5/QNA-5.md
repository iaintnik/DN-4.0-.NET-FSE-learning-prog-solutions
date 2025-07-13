🔍 Overview
This hands-on project demonstrates how to implement secure access to a Web API using JWT (JSON Web Tokens) and Bearer Authentication in ASP.NET Core. It also includes how to configure CORS, how to protect endpoints using [Authorize], and how to test everything using Postman.

🔒 What is JWT and Bearer Authentication?
JWT (JSON Web Token) is a compact, self-contained way of securely transmitting information between parties as a JSON object. It contains claims such as the user ID, role, and expiry time. These tokens are digitally signed, so they cannot be tampered with.

Bearer Authentication is a method of sending the JWT to the server in the Authorization header of an HTTP request. The format looks like this:

makefile
Copy
Edit
Authorization: Bearer <your-token>
The server validates the token before allowing access to protected resources.

🚀 Hands-On Implementation Flow
Enable CORS to allow local apps to access the API.

Configure JWT Authentication in Program.cs using AddAuthentication() and AddJwtBearer().

Create an AuthController to generate a JWT.

Add [Authorize] to EmployeeController to protect endpoints.

Use Postman to test protected and public endpoints.

🔧 Program.cs: JWT Configuration Explained
We begin by creating a secret key:

csharp
Copy
Edit
string securityKey = "mysuperdupersecret";
var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
This securityKey is shared between token creation and validation and must remain secret.

Next, we configure authentication:

csharp
Copy
Edit
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "mySystem",
        ValidAudience = "myUsers",
        IssuerSigningKey = symmetricSecurityKey
    };
});
This sets up JWT Bearer as the default authentication scheme and specifies the parameters for token validation (issuer, audience, signing key, and expiration).

Finally, add this to the middleware pipeline:

csharp
Copy
Edit
app.UseAuthentication();
app.UseAuthorization();
🎯 AuthController: Generate JSON Web Token
csharp
Copy
Edit
[AllowAnonymous]
[HttpGet("token")]
public IActionResult GetToken()
{
    var token = GenerateJSONWebToken(123, "Admin");
    return Ok(token);
}
AllowAnonymous: lets users access this endpoint without authentication.

GenerateJSONWebToken(): builds and signs a token that includes user role and ID in claims.

Claims example:

csharp
Copy
Edit
var claims = new List<Claim>
{
    new Claim(ClaimTypes.Role, userRole),
    new Claim("UserId", userId.ToString())
};
Token setup with expiry:

csharp
Copy
Edit
var token = new JwtSecurityToken(
    issuer: "mySystem",
    audience: "myUsers",
    claims: claims,
    expires: DateTime.Now.AddMinutes(10),
    signingCredentials: credentials
);
👮‍♀️ EmployeeController: Protected Endpoint
csharp
Copy
Edit
[Authorize(Roles = "Admin,POC")]
[HttpGet("data")]
public IActionResult Get()
{
    return Ok("This is protected Employee data.");
}
Only users with Admin or POC roles can access this endpoint.

Requests without a valid token or with incorrect roles receive 401 Unauthorized.

📬 Testing with Postman
Step-by-step:
Run your project (Ctrl + F5)

Go to:

bash
Copy
Edit
GET https://localhost:{port}/api/auth/token
→ You receive a JWT token.

In Postman:

Go to GET https://localhost:{port}/api/employee/data

In the Authorization tab:

Type: Bearer Token

Token: Paste the copied JWT

If valid → Response 200 OK

If invalid/expired → Response 401 Unauthorized

🧪 Token Expiration and Invalid Testing
In AuthController, reduce token expiry to 2 minutes:

csharp
Copy
Edit
expires: DateTime.Now.AddMinutes(2),
Wait for 2 minutes and send request → Should return 401 Unauthorized

Edit a single character in the JWT token → Signature becomes invalid → 401 Unauthorized

🔐 Role-Based Authorization
Token contains role claim:

csharp
Copy
Edit
new Claim(ClaimTypes.Role, "Admin")
Endpoint restricts access:

csharp
Copy
Edit
[Authorize(Roles = "Admin,POC")]
If token doesn't include required role → user gets denied access.

📌 Key Points and Differences
Topic	Explanation
JWT vs Session	JWT is stateless; session is server-stored
JWT Expiration	Controlled via expires: property
Bearer Token Location	Passed in the Authorization header
Role-based Access	Implemented using [Authorize(Roles = "...")]
AllowAnonymous	Allows public access without token
CORS	Required for frontend-backend interaction on different ports

❓ Questions & Answers (For Interview/Understanding)
Q1: What is JWT used for?
A: Secure, stateless authentication by encoding user claims in a signed token.

Q2: Where is the JWT stored on the client side?
A: Typically stored in localStorage or sessionStorage in frontend applications.

Q3: How does Bearer authentication work?
A: The token is passed in the Authorization header and validated on the server.

Q4: What happens if the token is expired or invalid?
A: ASP.NET Core returns a 401 Unauthorized response automatically.

Q5: What is the purpose of Claims in JWT?
A: Claims store user info like Role, UserId, etc., and are used to apply authorization rules.

Q6: What is the role of TokenValidationParameters?
A: It controls how JWT tokens are validated (issuer, audience, key, expiration, etc.)

Q7: How can you revoke or blacklist a token?
A: JWT is stateless, so you can’t revoke it unless you store and check a blacklist on the server manually.

