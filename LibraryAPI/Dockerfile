#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN mkdir Images
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LibraryApp.csproj", "LibraryApp/"]
RUN dotnet restore "LibraryApp/LibraryApp.csproj"
WORKDIR "/src/LibraryApp"
COPY . .
RUN dotnet build "LibraryApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryApp.dll"]
