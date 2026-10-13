\# RaceDay - Event Management System



\### A RESTful API for South African road running, walking and cycling events



\---



\## 1. System Overview



\*\*RaceDay\*\* is a full-stack event management platform for the South African road running, walking, and cycling community. South Africa has a rich road events culture — from the Comrades Marathon to the Cape Town Cycle Tour — but many events are still managed using paper-based registration, spreadsheets, and disconnected communication channels.



RaceDay solves this by providing a central platform where \*\*Event Organisers\*\* create and manage events and \*\*Participants\*\* browse, register, and track their performance.



This repository contains \*\*Part 2\*\* of the project: the RESTful API built with ASP.NET Core Web API and Entity Framework Core.



\---



\## 2. User Roles



The system supports \*\*two distinct roles\*\*:



\### Organiser

\- Register and log in

\- Create, edit, and delete events

\- Manage event categories (distance, fee, capacity)

\- View all enrolments for their own events

\- Update enrolment statuses (Registered → Confirmed → Completed → Cancelled)

\- Upload race results (finish times and positions)



\### Participant

\- Register and log in

\- Browse upcoming events

\- Enrol in an event by selecting a category

\- View personal enrolment history

\- View personal results

\- Update their own profile



\*\*Role-based access is enforced at the API level\*\* using JWT authentication and `\[Authorize(Roles = "...")]` attributes on controllers.



\---



\## 3. Technologies Used



| Layer | Technology |

|-------|-----------|

| Language | C# (.NET 8) |

| Framework | ASP.NET Core Web API |

| ORM | Entity Framework Core (Code-First) |

| Database | SQL Server / LocalDB |

| Authentication | JWT Bearer Tokens |

| Password Hashing | BCrypt.Net-Next |

| API Documentation | Swagger (Swashbuckle) |

| Unit Testing | xUnit + EF Core InMemory |

| CI/CD | GitHub Actions |

| IDE | Visual Studio |



\---



\## 4. Repository Structure



```

RaceDay/

├── .github/

│   └── workflows/

│       └── dotnet-ci.yml              # GitHub Actions workflow

├── RaceDay.API/                       # Web API (controllers, Program.cs)

│   ├── Controllers/

│   ├── appsettings.json

│   └── Program.cs

├── RaceDay.Core/                      # Domain layer

│   ├── Entities/                      # Database entities

│   ├── DTOs/                          # Data Transfer Objects

│   └── Interfaces/                    # Service interfaces

├── RaceDay.Infrastructure/            # Infrastructure layer

│   ├── Data/                          # DbContext

│   ├── Migrations/                    # EF Core migrations

│   └── Services/                      # Business logic

├── RaceDay.Tests/                     # Unit tests (27 tests)

└── README.md

```



\---



\## 5. Setup Instructions



\### Prerequisites

\- \*\*Visual Studio 2022\*\* (with ASP.NET and .NET 8 workloads)

\- \*\*SQL Server\*\* or \*\*SQL Server LocalDB\*\* (comes with Visual Studio)

\- \*\*.NET 8 SDK\*\*

\- \*\*SQL Server Management Studio (SSMS)\*\* – for viewing the database



\### Step 1: Clone or Open the Solution



```bash

git clone https://github.com/\[your-username]/\[repository-name].git

```



Or open `RaceDay.sln` directly in Visual Studio.



\### Step 2: Check the Connection String



Open `RaceDay.API/appsettings.json` and confirm the connection string matches your SQL Server instance:



```json

"ConnectionStrings": {

&#x20; "DefaultConnection": "Server=(localdb)\\\\MSSQLLocalDB;Database=RaceDayDb;Trusted\_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"

}

```



\### Step 3: Apply the Database Migration



Open \*\*Package Manager Console\*\* (`Tools → NuGet Package Manager → Package Manager Console`).



\- Set \*\*Default project\*\* to `RaceDay.Infrastructure`

\- Run:



```powershell

Update-Database -StartupProject RaceDay.API

```



This creates the `RaceDayDb` database with all tables.



\### Step 4: Run the API



1\. Set `RaceDay.API` as the \*\*Startup Project\*\*

2\. Press \*\*F5\*\*

3\. A browser opens with \*\*Swagger UI\*\* at `https://localhost:XXXX/swagger`



\### Step 5: Test the Endpoints in Swagger



1\. Register an Organiser via `POST /api/auth/register`

2\. Register a Participant via the same endpoint

3\. Log in via `POST /api/auth/login` and \*\*copy the JWT token\*\*

4\. Click the \*\*Authorize\*\* button at the top of Swagger and paste the token

5\. Test protected endpoints (create event, enrol, etc.)



\### Step 6: Run the Unit Tests



\- In Visual Studio: \*\*Test → Run All Tests\*\*

\- Or from Package Manager Console:



```powershell

dotnet test

```



All 27 tests should pass.



\---



\## 6. API Endpoints



\### Auth (public)

| Method | Route | Description |

|--------|-------|-------------|

| POST | `/api/auth/register` | Register as Organiser or Participant |

| POST | `/api/auth/login` | Log in and get a JWT token |



\### Users (authenticated)

| Method | Route | Description |

|--------|-------|-------------|

| GET | `/api/users/profile` | Get current user's profile |

| PUT | `/api/users/profile` | Update current user's profile |



\### Events

| Method | Route | Role | Description |

|--------|-------|------|-------------|

| GET | `/api/events` | Public | List all events |

| GET | `/api/events/{id}` | Public | Get one event |

| POST | `/api/events` | Organiser | Create event |

| PUT | `/api/events/{id}` | Organiser (owner) | Update event |

| DELETE | `/api/events/{id}` | Organiser (owner) | Delete event |



\### Enrolments

| Method | Route | Role | Description |

|--------|-------|------|-------------|

| POST | `/api/enrolments` | Participant | Enrol in a category |

| GET | `/api/enrolments/my` | Participant | View own enrolments |

| GET | `/api/enrolments/event/{eventId}` | Organiser (owner) | View event enrolments |

| PUT | `/api/enrolments/{id}/status` | Organiser (owner) | Update enrolment status |



\### Results

| Method | Route | Role | Description |

|--------|-------|------|-------------|

| POST | `/api/results` | Organiser (owner) | Upload a result |

| GET | `/api/results/my` | Participant | View own results |

| GET | `/api/results/event/{eventId}` | Public | View event results |



\---



\## 7. Authentication \& Security



\- Passwords are hashed using \*\*BCrypt\*\* before being stored. Plain-text passwords are never saved.

\- Authentication uses \*\*JWT tokens\*\*. The token contains the user's ID, email, name, and role.

\- Role-based access is enforced by `\[Authorize(Roles = "Organiser")]` and `\[Authorize(Roles = "Participant")]` attributes.

\- Ownership checks ensure that organisers can only modify their own events.



\---



\## 8. Unit Tests



The `RaceDay.Tests` project contains \*\*27 unit tests\*\* covering:



| Test File | Tests | What They Cover |

|-----------|-------|-----------------|

| `AuthServiceTests.cs` | 8 | Registration, login, duplicate email, wrong password, invalid role, password hashing |

| `EventServiceTests.cs` | 8 | Event creation, date validation, ownership checks |

| `EnrolmentServiceTests.cs` | 11 | Enrolment, capacity limits, duplicate enrolments, role-based access |



All tests run against an \*\*in-memory database\*\* so no SQL Server is needed for testing.



\---



\## 9. CI/CD — GitHub Actions



A GitHub Actions workflow runs on every push and pull request. It:



1\. Checks out the code

2\. Sets up .NET 8

3\. Restores dependencies

4\. Builds the solution

5\. Runs all 27 tests



\*\*Workflow file:\*\* `.github/workflows/dotnet-ci.yml`



\### CI/CD Screenshot



!\[CI/CD Success](ci-success.png)



\*Screenshot taken after a successful workflow run in the GitHub Actions tab.\*



\---



\## 10. The Video Walkthrough



\[!\[

The video covers:

1\. Project structure (Core / Infrastructure / API / Tests)

2\. The ERD and how it maps to the code

3\. Running the API and testing endpoints in Swagger

4\. Demonstrating role-based access (403 responses)

5\. Running the unit tests

6\. The GitHub Actions workflow and green build



\---



\## 11. Author



\- \*\*Name:\*\* \[Sefako siphosethu Mongalo]

\- \*\*Student Number:\*\* \[ST10181363]

\- \*\*Course:\*\* \[Your Course]

\- \*\*Institution:\*\* Rosebank College Braamfontien (Pty) Ltd

\- \*\*Project:\*\* RaceDay – POE, Part 2



\---



\## 12. References



Microsoft (2024) \*ASP.NET Core Web API documentation\*. Available at: https://learn.microsoft.com/en-us/aspnet/core/web-api/ (Accessed: 13 October 2026).



Microsoft (2024) \*Entity Framework Core documentation\*. Available at: https://learn.microsoft.com/en-us/ef/core/ (Accessed: 13 October 2026).



Pears, R. and Shields, G. (2022) \*Cite them right: the essential referencing guide\*. 12th edn. London: Bloomsbury Academic.

