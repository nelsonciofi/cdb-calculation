
# CDB Calculation

This application allows users to simulate CDB investments and find out the gross and net income after a given period of months.

## Prerequisites
- .NET Core SDK 7.0
- Node.js v18.17.0
- npm 9.6.7
- Angular CLI 16.1.6
- OS: win32 x64
- Google Chrome Web Browser (because Angular is from Google and it's too annoying to make the tests run in another browsers.)

## Installation

Clone the repository to your local machine:

```
git clone https://github.com/nelsonciofi/cdb-calculation.git
```

## Backend

### Usage

1. Navigate to /src  
2. To build and run the project, execute the following commands:

```
dotnet restore
dotnet build
dotnet run --project ./webapi/webapi.csproj
```

### Testing
We use xUnit for unit testing. Follow these steps to run the unit tests.

1. Navigate to /src
2. Run the tests using the following command: 

```
1. dotnet test
```

If you are looking for code coverage, run the following command:

```
dotnet test --collect:"XPlat Code Coverage"
```
A coverage.cobertura.xml file will be created in B3.Tests project with lots of statitics about code coverage.


## Frontend

### Usage

1. Install the required dependencies using npm:

```
npm install
``` 

2. Run the Angular development server:
```
ng serve
``` 

3. Open your web browser and navigate to http://localhost:4200 to access the application.
4. Fill in the initial investment and redeem term months, then click the "Submit" button to simulate the CDB calculation.

### Testing
This project uses Jasmine and Karma for testing. To run the tests, follow these steps:

1. Ensure you have the required dependencies installed:
```
npm install
``` 
2. Run the tests:
```
ng test
``` 

## Optional

Optionally, you can run the [RunitBabe](runItBabe.bat) and it will run the frontend and backend.   
Then navigate to http://localhost:4200 on your web browser.

## License

This project is licensed under the [MIT License](LICENSE).
