# dotnet-chat

This is a chat application built with .NET 6 and Angular. The backend is developed using .NET 6 and the frontend is developed using Angular. The application uses Pusher for real-time communication.

## Prerequisites

- .NET 6 SDK
- Node.js and npm
- Angular CLI
- MySQL Server

## Installation Steps

1. Clone the repository to your local machine.

    ```bash
    git clone https://github.com/Hlel-Mohamed/dotnet-chat.git
    ```

2. Navigate to the `dotnet-chat` directory.

    ```bash
    cd dotnet-chat
    ```

3. Install EF Core Tools if missing.

    ```bash
    dotnet tool install --global dotnet-ef
    ```

4. Apply Database Migrations (Make sure database is created beforehand).

    ```bash
    dotnet ef migrations add CreateUsersTable
    dotnet ef database update
    ```

5. Run the .NET application in the background.

    ```bash
    dotnet run
    ```

6. Open new terminal and navigate to the `dotnet-chat-angular` directory.

    ```bash
    cd dotnet-chat-angular
    ```

7. Install npm packages.

    ```bash
    npm install
    ```

8. Serve the Angular application.

    ```bash
    ng serve -o
    ```

## Configuration

The application's configuration is stored in the `appsettings.json` file. Here, you can change the log level, allowed hosts, and connection strings.

The application's launch settings are stored in the `launchSettings.json` file. Here, you can change the application's URLs and environment variables.

## License

[MIT](https://choosealicense.com/licenses/mit/)