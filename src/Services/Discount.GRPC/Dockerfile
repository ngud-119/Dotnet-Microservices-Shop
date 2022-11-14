FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Discount.GRPC/Discount.GRPC.csproj", "src/Services/Discount.GRPC/"]
RUN dotnet restore "src/Services/Discount.GRPC/Discount.GRPC.csproj"
COPY . .
WORKDIR "/src/src/Services/Discount.GRPC/"
RUN dotnet build "Discount.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.GRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.GRPC.dll"]
