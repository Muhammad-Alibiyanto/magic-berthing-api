#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-strectch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-strectch AS build
WORKDIR /src
COPY ["magicBerthing/magicBerthing.csproj", "magicBerthing/"]
RUN dotnet restore "magicBerthing/magicBerthing.csproj"
COPY . .
WORKDIR "/src/magicBerthing"
RUN dotnet build "magicBerthing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "magicBerthing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "magicBerthing.dll"]