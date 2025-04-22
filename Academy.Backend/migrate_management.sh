#!/bin/bash

echo "🚀 Создание и применение миграций для ManagementDbContext"

# Генерация миграции
dotnet ef migrations add Management_$(date +"%Y%m%d%H%M%S") \
    --context ManagementDbContext \
    --startup-project src/Academy.Web/ \
    --project src/Management/Academy.Management.Infrastructure

# Применение миграции
dotnet ef database update \
    --context ManagementDbContext \
    --startup-project src/Academy.Web/ \
    --project src/Management/Academy.Management.Infrastructure

echo "✅ Миграции применены успешно"

