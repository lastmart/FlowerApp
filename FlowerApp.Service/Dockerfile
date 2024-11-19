# Устанавливаем базовый образ .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# Копируем файлы решения и проектов
COPY FlowerApp.sln ./
COPY FlowerApp.Domain/FlowerApp.Domain.csproj ./FlowerApp.Domain/
COPY FlowerApp.Data/FlowerApp.Data.csproj ./FlowerApp.Data/
COPY FlowerApp.Service/FlowerApp.Service.csproj ./FlowerApp.Service/

# Восстанавливаем зависимости
RUN dotnet restore FlowerApp.Service/FlowerApp.Service.csproj

# Копируем все файлы проекта
COPY . .

# Сборка приложения
WORKDIR /app/FlowerApp.Service
RUN dotnet publish -c Release -o out

# Используем легковесный runtime образ
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/FlowerApp.Service/out ./

# Устанавливаем порт для работы приложения
#ENV ASPNETCORE_URLS=http://+:8888
EXPOSE 8888

ENV ASPNETCORE_ENVIRONMENT=Development
# Запуск приложения
ENTRYPOINT ["dotnet", "FlowerApp.Service.dll"]
