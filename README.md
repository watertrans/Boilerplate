# Clean Architecture Boilerplate
This is a Boilerplate Code, with implementation of Clean Architecture, using Microsoft .Net Core 3.1.

## Getting Started

### Prerequisites

- [Visual Studio 2019 Community](https://visualstudio.microsoft.com/vs/community/)
- [Docker Desktop for Windows](https://hub.docker.com/editions/community/docker-ce-desktop-windows)

### Initialize Database

- MariaDB
  - Execute: `\db\docker-compose.yml`
  - Execute: `\src\WaterTrans.Boilerplate.Persistence\SqlResources\CreateDatabase.sql`
  - Execute: `\src\WaterTrans.Boilerplate.Persistence\SqlResources\CreateTestDatabase.sql`
- Visual Studio
  - Open: `WaterTrans.Boilerplate.sln`
  - Set as Startup Project: `WaterTrans.Boilerplate.Web`
  - Start Debugging
  - Execute: `/api/v1/debug/database/initialize`
  - Execute: `​/api​/v1​/debug​/database​/loadInitialData`
