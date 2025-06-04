FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProductServices/ProductServices.csproj", "ProductServices/"]
COPY ["OrderServices/OrderServices.csproj", "OrderServices/"]
COPY ["AccountServices/AccountServices.csproj", "AccountServices/"]
COPY ["RoleServices/RoleServices.csproj", "RoleServices/"]
COPY ["AuthServices/AuthServices.csproj", "AuthServices/"]
COPY ["ApiGateway/ApiGateway.csproj", "ApiGateway/"]
RUN dotnet restore "ProductServices/ProductServices.csproj"
COPY . .
WORKDIR "/src/ProductServices"
RUN dotnet build "ProductServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductServices.dll"] 