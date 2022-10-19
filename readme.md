# Step 1
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

## Create Angular app
from ~/InterviewTask/src
```
ng new Frontend
```