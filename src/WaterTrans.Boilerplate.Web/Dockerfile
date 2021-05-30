#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["WaterTrans.Boilerplate.Web/WaterTrans.Boilerplate.Web.csproj", "WaterTrans.Boilerplate.Web/"]
RUN dotnet restore "WaterTrans.Boilerplate.Web/WaterTrans.Boilerplate.Web.csproj"
COPY . .
WORKDIR "/src/WaterTrans.Boilerplate.Web"
RUN dotnet build "WaterTrans.Boilerplate.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WaterTrans.Boilerplate.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WaterTrans.Boilerplate.Web.dll"]