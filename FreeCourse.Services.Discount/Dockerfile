#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FreeCourse.Services.Discount/FreeCourse.Services.Discount.csproj", "FreeCourse.Services.Discount/"]
COPY ["Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "Shared/FreeCourse.Shared/"]
RUN dotnet restore "FreeCourse.Services.Discount/FreeCourse.Services.Discount.csproj"
COPY . .
WORKDIR "/src/FreeCourse.Services.Discount"
RUN dotnet build "FreeCourse.Services.Discount.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.Discount.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.Discount.dll"]