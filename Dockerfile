FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["MoveIT/MoveIT.csproj", "MoveIT/"]
RUN dotnet restore "MoveIT/MoveIT.csproj"
COPY . .
WORKDIR "/src/MoveIT"
RUN dotnet publish "MoveIT.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MoveIT.dll"]
