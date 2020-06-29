FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY src/BaseCleanArchitecture.API/BaseCleanArchitecture.API.csproj src/BaseCleanArchitecture.API/
COPY src/BaseCleanArchitecture.Infra/BaseCleanArchitecture.Infra.csproj src/BaseCleanArchitecture.Infra/
COPY src/BaseCleanArchitecture.Core/BaseCleanArchitecture.Core.csproj src/BaseCleanArchitecture.Core/
COPY src/BaseCleanArchitecture.UseCases/BaseCleanArchitecture.UseCases.csproj src/BaseCleanArchitecture.UseCases/
RUN dotnet restore "src/BaseCleanArchitecture.API/BaseCleanArchitecture.API.csproj"
COPY . .
WORKDIR "/src/src/BaseCleanArchitecture.API"
RUN dotnet build "BaseCleanArchitecture.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseCleanArchitecture.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*5000
ENTRYPOINT ["dotnet", "BaseCleanArchitecture.API.dll"]
