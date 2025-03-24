# GymFitness - Membership Management System

## Overview
GymFitness is a comprehensive membership management system designed to streamline gym operations. It helps gym owners and administrators efficiently manage member registrations, subscriptions, payments, and attendance tracking.

## Features
- **Member Registration:** Add and manage gym members with personal details.
- **Subscription Management:** Handle different membership plans and renewals.
- **Payment Tracking:** Record and track membership payments.
- **Attendance Monitoring:** Log member check-ins and check-outs.
- **Reports & Analytics:** Generate reports for member activity and revenue insights.
- **Admin Panel:** Role-based access control for managing system users.

## Installation
### Prerequisites
- .NET Core 6.0 or later
- SQL Server 2019 or later
- Visual Studio 2022
- Node.js (for frontend dependencies if applicable)

### Steps
1. Clone the repository:
   ```sh
   git clone https://github.com/kvenky102/GymFitness.git
   cd GymFitness
   ```
2. Configure the database connection in `appsettings.json`.
3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```
5. Open a browser and navigate to `http://localhost:5000`.

## Technologies Used
- **Frontend:** HTML, CSS, JavaScript, Bootstrap
- **Backend:** ASP.NET Core Web API
- **Database:** SQL Server
- **Authentication:** JWT-based authentication

## Contributing
We welcome contributions! Please follow these steps:
1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Submit a pull request.

## License
This project is licensed under the MIT License.

## Contact
For any inquiries or support, contact **venky1002@gmail.com** or open an issue in the repository.

