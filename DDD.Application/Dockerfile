FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5010

ENV ASPNETCORE_URLS=http://+:5010

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DDD.Application/DDD.Application.csproj", "DDD.Application/"]
RUN dotnet restore "DDD.Application\DDD.Application.csproj"
COPY . .
WORKDIR "/src/DDD.Application"
RUN dotnet build "DDD.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDD.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDD.Application.dll"]
