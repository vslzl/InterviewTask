FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src

COPY ["src/Api/InterviewTask.Api.csproj", "src/Api/"]
COPY ["NuGet.config", "./"]
COPY ["InterviewTask.sln", "./"]
RUN dotnet restore "InterviewTask.sln"

COPY . .
WORKDIR "/src/src/Api/"

FROM build AS publish
RUN dotnet publish --no-restore "InterviewTask.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InterviewTask.Api.dll"]
