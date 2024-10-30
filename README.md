# FlowerApp
Серверная часть приложения "Помощник по уходу за растениями"

## Настройка окружения  

### Развёртка PostgreSql
В appsettings.json в поле DefaultConnection укажите информацию о машине, на которой будет развёрнут postgres.   
Пример заполнения:
```
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;database=database;Username=username;Password=password"
}
```
Затем нужно накатить последнюю миграцию в проекте. Файл со всеми миграциями в проекте __FlowerApp.Data__ 
в директории __Migrations__. Удобнее всего это сделать при помощи плагина __Database Tools and SQL__. 
После установки в верхней панели Rider:  
Tools > Entity Framework Core > Update Database  
Параметры будут проставлены автоматически, нажимем Ок и база сконфигурирована.

