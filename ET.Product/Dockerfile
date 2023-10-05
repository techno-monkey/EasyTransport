#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ET.Product/ET.Product.csproj", "ET.Product/"]
COPY ["ET.JwtAuthManager/ET.JwtAuthManager.csproj", "ET.JwtAuthManager/"]
COPY ["ET.Models/ET.Models.csproj", "ET.Models/"]
RUN dotnet restore "ET.Product/ET.Product.csproj"
COPY . .
WORKDIR "/src/ET.Product"
RUN dotnet build "ET.Product.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ET.Product.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ET.Product.dll"]