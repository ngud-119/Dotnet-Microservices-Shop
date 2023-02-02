FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OcelotApi.Gateway/OcelotApi.Gateway.csproj", "src/OcelotApi.Gateway/"]
RUN dotnet restore "src/OcelotApi.Gateway/OcelotApi.Gateway.csproj"
COPY . .
WORKDIR "/src/src/OcelotApi.Gateway/"
RUN dotnet build "OcelotApi.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotApi.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApi.Gateway.dll"]
