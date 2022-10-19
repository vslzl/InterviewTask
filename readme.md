# Build and run (w/ docker)

if `macos`
```
docker-compose -f docker-compose.yml -f docker-compose.override.macos.yml up
```
if `windows`
```
docker-compose -f docker-compose.yml -f docker-compose.override.windows.yml up
```
if `linux`
```
docker-compose -f docker-compose.yml -f docker-compose.override.linux.yml up
```

open 
> http://localhost:5100


# Build and run (w/out docker)

run backend first:

> From: ~/InterviewTask

```
cd src/Api/
dotnet run
```

run angular app:

> From: ~/InterviewTask

```
cd src/Frontend/
ng serve
```

navigate to `http://localhost:4200`

# Step 1
Interview task requires ne simple task.
## Create solution

```
mkdir InterviewTask
cd InterviewTask
dotnet new sln
```

## Create source folder and Api project
from ~/InterviewTask
```
mkdir src
cd src
dotnet new webapi -n InterviewTask.Api -o ./Api
```
from ~/InterviewTask
```
dotnet sln add src/Api/InterviewTask.Api.csproj
```

## Create Angular app
from ~/InterviewTask/src
```
ng new Frontend
```

develop a some SW...