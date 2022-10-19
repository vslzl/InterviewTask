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

# Development Step 1
Interview task requires one simple task.
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

> used angular material for styling and templating.

develop some SW...

# Notes
1. Docker's localhost can be challenging between platforms. I used macos to develop this sample so I use the `docker-compose.override.macos.yml`file when building and running image.
1. To dynamically change angular apiUrl environment variable, I've added some tweaks to app which can be seen at `index.html` and `environment.ts` files (included script file at the end of the head tag)
1. If you set `CreateMockData` to `Y` (the env variable) at relevant docker-compose.override file, database will be seeded with random todo data.
