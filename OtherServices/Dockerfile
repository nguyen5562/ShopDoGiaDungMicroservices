FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OtherServices.csproj", "./"]
RUN dotnet restore "OtherServices.csproj"
COPY . .
RUN dotnet build "OtherServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OtherServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OtherServices.dll"] 