#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Academy.Web/Academy.Web.csproj", "src/Academy.Web/"]
COPY ["src/CourseManagement/Academy.CourseManagement.Presentation/Academy.CourseManagement.Presentation.csproj", "src/CourseManagement/Academy.CourseManagement.Presentation/"]
COPY ["src/Shared/Academy.Framework/Academy.Framework.csproj", "src/Shared/Academy.Framework/"]
COPY ["src/Shared/Academy.Core/Academy.Core.csproj", "src/Shared/Academy.Core/"]
COPY ["src/Shared/Academy.SharedKernel/Academy.SharedKernel.csproj", "src/Shared/Academy.SharedKernel/"]
RUN dotnet restore "./src/Academy.Web/Academy.Web.csproj"
COPY . .
WORKDIR "/src/src/Academy.Web"
RUN dotnet build "./Academy.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Academy.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Academy.Web.dll"]