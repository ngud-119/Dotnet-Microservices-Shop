FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Shopping.WebApp/Shopping.WebApp.csproj", "src/Shopping.WebApp/"]
RUN dotnet restore "src/Shopping.WebApp/Shopping.WebApp.csproj"
COPY . .
WORKDIR "/src/src/Shopping.WebApp/"
RUN dotnet build "Shopping.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.WebApp.dll"]
