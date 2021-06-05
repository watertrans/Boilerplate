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

### Get Access Tokens

- Visual Studio
  - Open: `WaterTrans.Boilerplate.sln`
  - Set as Startup Project: `WaterTrans.Boilerplate.Web.Api`
  - Start Debugging
  - Visit: `/api/v1/token`
  - Enter the following values
    - grant_type: `client_credentials`
    - client_id: `owner`
    - client_secret: `owner-secret`

## MariaDB default user / password

- MariaDB root user
  - root / root
- MariaDB Boilerplate database owner
  - user / user
- MariaDB BoilerplateTest database owner
  - test / test
