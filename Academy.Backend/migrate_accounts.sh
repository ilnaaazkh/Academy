#!/bin/bash

echo "🚀 Создание и применение миграций для AccountsDbContext"

timestamp=$(date +"%Y%m%d%H%M%S")

# Генерация миграции
dotnet ef migrations add Accounts_$timestamp \
    --context AccountsDbContext \
    --startup-project src/Academy.Web/ \
    --project src/Accounts/Academy.Accounts.Infrastructure \

# Применение миграции
dotnet ef database update \
    --context AccountsDbContext \
    --startup-project src/Academy.Web/ \
    --project src/Accounts/Academy.Accounts.Infrastructure

echo "✅ Миграции применены успешно"

