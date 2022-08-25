#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FreeCourse.Services.Order.API/FreeCourse.Services.Order.API.csproj", "FreeCourse.Services.Order.API/"]
COPY ["FreeCourse.Services.Order.Application/FreeCourse.Services.Order.Application.csproj", "FreeCourse.Services.Order.Application/"]
COPY ["FreeCourse.Services.Order.Infrastructure/FreeCourse.Services.Order.Infrastructure.csproj", "FreeCourse.Services.Order.Infrastructure/"]
COPY ["FreeCourse.Services.Order.Domain/FreeCourse.Services.Order.Domain.csproj", "FreeCourse.Services.Order.Domain/"]
COPY ["FreeCourse.Services.Order.Domain.Core/FreeCourse.Services.Order.Domain.Core.csproj", "FreeCourse.Services.Order.Domain.Core/"]
COPY ["Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "Shared/FreeCourse.Shared/"]
RUN dotnet restore "FreeCourse.Services.Order.API/FreeCourse.Services.Order.API.csproj"
COPY . .
WORKDIR "/src/FreeCourse.Services.Order.API"
RUN dotnet build "FreeCourse.Services.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.Order.API.dll"]