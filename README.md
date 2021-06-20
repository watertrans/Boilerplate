# Clean Architecture Boilerplate
This is a Boilerplate Code, with implementation of Clean Architecture, using Microsoft .Net Core 3.1.

## Getting Started

### Prerequisites

- [Visual Studio 2019 Community](https://visualstudio.microsoft.com/vs/community/)
- [Docker Desktop for Windows](https://hub.docker.com/editions/community/docker-ce-desktop-windows)

### Initialize Database

- MariaDB on Docker
  - `cd .\db`
  - `docker compose up -d`
  - `dbadmin init server -s`
  - `dbadmin init database -s`

### Get Client Credentials Flow Access Tokens

With machine-to-machine applications, such as CLIs, daemons, or services running on your back-end.

- Visual Studio
  - Open: `WaterTrans.Boilerplate.sln`
  - Set as Startup Project: `WaterTrans.Boilerplate.Web.Api`
  - Start Debugging
  - Visit: `/api/v1/token`
  - Enter the following values
    - grant_type: `client_credentials`
    - client_id: `owner`
    - client_secret: `owner-secret`

### Get Authorization Code Flow Access Tokens

With public clients applications, such as native or single-page applications.

- Visual Studio
  - Open: `WaterTrans.Boilerplate.sln`
  - Set as Startup Project: `WaterTrans.Boilerplate.Web.Server`
  - Start Debugging
  - Enter the following values
    - LoginId: `admin`
    - Password: `admin-secret`
  - Copy the Authorization Code from url: `/Dashboard?token={Authorization Code}`
  - Set as Startup Project: `WaterTrans.Boilerplate.Web.Api`
  - Start Debugging
  - Visit: `/api/v1/token`
  - Enter the following values
    - grant_type: `authorization_code`
    - client_id: `clientapp`
    - code: `{Authorization Code}`

## Database Maintenance

- Create tables, indexes, stored procedures and others.
  - `cd .\db`
  - `dbadmin apply database`

## Default settings

- MariaDB
  - Root user: `root` / `root`
  - Boilerplate database owner: `user` / `user`
  - BoilerplateTest database owner: `test` / `test`
  - Data: `C:\Users\{USERNAME}\AppData\Local\MariaDB\data`
  - Backup: `C:\Users\{USERNAME}\AppData\Local\MariaDB\backup`