FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 6165
ENV ASPNETCORE_URLS=http://+:6165

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["KayraExport.API.csproj", "./"]
RUN dotnet restore "KayraExport.API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "KayraExport.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KayraExport.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KayraExport.API.dll"]
