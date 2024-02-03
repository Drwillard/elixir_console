# Elixir Console App

This is a simple console app to demonstrate .NET 8, EntityFramework, Docker and Sqlite.  The application runs in a docker container based on Microsoft .NET Core 8 image (`mcr.microsoft.com/dotnet/runtime:8.0`)

The application is a simple console app that asks for a name. For new names, it prompts the user to store answers to 3 security questions.  For existing users, the app prompts the user to provide the answers for previously-stored security questions.

## Prerequisites
Docker Compose

## To Run
1. Clone repository
2. Open a terminal and type `docker compose  run --rm app `
3. By design it will run forever; type `Ctrl+C` to exit and stop the container.

## Notes
- **This app is not currently persisting data outside the container!** Once the container is brought down, the ephemeral storage goes away. The sqlite db needs to be stored in a docker volume mounted on the host's filesystem, but I'm having trouble getting the permissions set up and I've already sunk too much time into for a demo app. Alternatively, the .NET code CAN be built and run directly (outside a container environment); in this case, the data **does persist**.
- This project uses Entity Framework, so sqlite can easily be swapped out for a more robust persistence layer.  Update the appsettings.json file, install a nuget package for the RDBMS connector of choice, and go to town.
- EntityFramework also sets up and seeds the db with questions automatically at runtime.  The raw SQL DDL and seed data can be viewed in `setup.sql` but this does NOT need to be run manually.
- Because this is a small console app, it isn't very testable. The repositories could have unit tests written for them, as there is some business logic inherent in the queries. But for better test coverage, write integration tests or end-to-end tests to cover all intended use cases.
- When answering security questions, we could also mask the input using a solution like this (`https://stackoverflow.com/a/40869537/2175593`).
