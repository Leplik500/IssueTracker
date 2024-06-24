FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IssueTracker/IssueTracker.csproj", "IssueTracker/"]
COPY ["IssueTracker.DAL/IssueTracker.DAL.csproj", "IssueTracker.DAL/"]
COPY ["IssueTracker.Domain/IssueTracker.Domain.csproj", "IssueTracker.Domain/"]
COPY ["IssueTracker.Service/IssueTracker.Service.csproj", "IssueTracker.Service/"]
RUN dotnet restore "IssueTracker/IssueTracker.csproj"
COPY . .
WORKDIR "/src/IssueTracker"
RUN dotnet build "IssueTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IssueTracker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IssueTracker.dll"]
