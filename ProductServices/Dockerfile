FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProductServices.csproj", "./"]
RUN dotnet restore "ProductServices.csproj"
COPY . .
RUN dotnet build "ProductServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductServices.dll"] 