#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FreeCourse.Web/FreeCourse.Web.csproj", "FreeCourse.Web/"]
COPY ["Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "Shared/FreeCourse.Shared/"]
RUN dotnet restore "FreeCourse.Web/FreeCourse.Web.csproj"
COPY . .
WORKDIR "/src/FreeCourse.Web"
RUN dotnet build "FreeCourse.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Web.dll"]