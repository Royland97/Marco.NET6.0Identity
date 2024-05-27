#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserInterface.Web/UserInterface.Web.csproj", "UserInterface.Web/"]
COPY ["Infrastructure.Services/Infrastructure.Services.csproj", "Infrastructure.Services/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["Infrastructure.DataAccess/Infrastructure.DataAccess.csproj", "Infrastructure.DataAccess/"]
COPY ["Core.DataAccess/Core.DataAccess.csproj", "Core.DataAccess/"]
RUN dotnet restore "UserInterface.Web/UserInterface.Web.csproj"
COPY . .
WORKDIR "/src/UserInterface.Web"
RUN dotnet build "UserInterface.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserInterface.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserInterface.Web.dll"]