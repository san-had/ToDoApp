#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ToDo.UI/ToDo.UI.csproj", "ToDo.UI/"]
COPY ["ToDo.Module/ToDo.Module.csproj", "ToDo.Module/"]
COPY ["ToDo.Domain/ToDo.Domain.csproj", "ToDo.Domain/"]
COPY ["ToDo.Extensibility/ToDo.Extensibility.csproj", "ToDo.Extensibility/"]
COPY ["ToDo.Service/ToDo.Service.csproj", "ToDo.Service/"]
RUN dotnet restore "ToDo.UI/ToDo.UI.csproj"
COPY . .
WORKDIR "/src/ToDo.UI"
RUN dotnet build "ToDo.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.UI.dll"]