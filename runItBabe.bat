cd src

dotnet restore
dotnet test
start dotnet run --project ./webapi/webapi.csproj

cd angularapp

call npm install
call ng test --watch=false
start ng serve

cd..
cd..