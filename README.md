# Power Production Plan Challenge

## About

This project is a solution to the Power Production Plan Challenge. The goal of the challenge is to develop an API that computes the optimal power production plan given a specific load.

## Repository

You can find the project hosted on GitHub at the following link:
[Power Production Plan Challenge Repository](https://github.com/massiouiAccount/PowerplantCodingChallenge)

## Requirements

To run this project, you'll need the following:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started) (for running in a container)
- Any IDE or text editor that supports C# (e.g., Visual Studio, Visual Studio Code)

## How to Run the Project

1. **Clone the repository:**

   ```bash
   git clone https://github.com/massiouiAccount/PowerplantCodingChallenge.git
   cd PowerplantCodingChallenge
   ```

2. **Restore the dependencies:**

   ```bash
   dotnet restore
   ```

3. **Run the project:**

   ```bash
   dotnet run --project ./src/PowerplantCodingChallenge.WebApi
   ```

4. **Access the API:**

   The API should now be running locally. You can test the API by sending a POST request to the `/productionplans/productionplan` endpoint.

   Example using `curl`:

   ```bash
   curl -X POST https://localhost:8888/productionplans/productionplan \
   -H "Content-Type: application/json" \
   -d @payload3.json
   ```

   Replace `payload3.json` with the path to your JSON request file.

## How to Run Tests

1. **Navigate to the test project directory:**

   ```bash
   cd tests/PowerplantCodingChallenge.WebApi.UnitTests
   ```

2. **Run the tests:**

   ```bash
   dotnet test
   ```

   This will execute all the unit tests in the project and provide a summary of the results.

## Running the Project with Docker

1. **Build the Docker image:**

   ```bash
   docker build -t power-production-plan .
   ```

2. **Run the Docker container:**

   ```bash
   docker run -d -p 8888:80 --name power-production-plan-container power-production-plan
   ```

3. **Access the API:**

   The API will be running inside the Docker container, accessible via `http://localhost:8888/productionplans`.

