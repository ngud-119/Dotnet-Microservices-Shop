FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ordering.Domain/Ordering.Domain.csproj", "src/Discount.Domain/"]
COPY ["src/Ordering.Application/Ordering.Application.csproj", "src/Ordering.Application/"]
COPY ["src/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "src/Ordering.Infrastructure/"]
COPY ["src/Ordering.API/Ordering.API.csproj", "src/Ordering.API/"]
RUN dotnet restore "src/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/src/Ordering.API/"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
