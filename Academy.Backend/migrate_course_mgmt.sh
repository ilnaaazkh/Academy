#!/bin/bash

echo "🚀 Создание и применение миграций для Course_Management"

timestamp=$(date +"%Y%m%d%H%M%S")

# Генерация миграции
dotnet ef migrations add Course_mgmt_$(date +"%Y%m%d%H%M%S")\
    --context CourseManagementWriteDbContext \
    --startup-project src/Academy.Web/ \
    --project src/CourseManagement/Academy.CourseManagement.Infrastructure

# Применение миграции
dotnet ef database update \
    --context CourseManagementWriteDbContext \
    --startup-project src/Academy.Web/ \
    --project src/CourseManagement/Academy.CourseManagement.Infrastructure

echo "✅ Миграции применены успешно"

