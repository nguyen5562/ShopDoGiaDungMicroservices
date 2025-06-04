FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrderServices.csproj", "./"]
RUN dotnet restore "OrderServices.csproj"
COPY . .
RUN dotnet build "OrderServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderServices.dll"] 