# PDF Printing Service

This repository hosts a PDF printing service, designed to convert HTML content into PDF files. Built with C# and utilizing ASP.NET 8.0, the service leverages PuppeteerSharp, a .NET wrapper for Puppeteer, to perform web page rendering and PDF generation. It's developed to be easily deployed either as a standalone Web API service or within a Docker container, catering to diverse deployment environments.

## Features

- HTML to PDF conversion using PuppeteerSharp.
- Standalone Web API service built with ASP.NET 8.0.
- Docker support for easy deployment and scalability.
  - Fully encapsulated environment with all necessary dependencies, including Puppeteer.

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Docker (for Docker deployment)

### Installation

Clone the repository to your local machine:

```bash
git clone https://github.com/yourusername/pdf-print-service.git
cd pdf-print-service
```

### Running Locally
To run the service locally, navigate to the project directory and execute:

```bash
dotnet restore
dotnet build
dotnet run
```

This will start the Web API service on your local machine on http://localhost:5000.

### Running with Docker

#### Building the Docker Image

A Dockerfile is provided in the repository, containing all the required dependencies, including Puppeteer. To build the Docker image, run the following command in the project root directory:

```bash
docker build -t pdf-print-service .
```

#### Running the Docker Container

Once the image is built, you can run the service in a Docker container:

```bash
docker run -d -p 5000:8080 --name pdf-print-service pdf-print-service
```

This command will start the service and bind it to port 8080 on your host machine.

## How to Use

Once the service is running, you can convert HTML to PDF by sending a POST request to the service endpoint with your HTML content.
Here is an example using curl:

```bash
curl \
  -X POST \
  -H "Content-Type: application/json" \
  -d '{"fileName":"example.pdf", "content":"<!DOCTYPE html><html><head><title>Test PDF</title></head><body><h1>Hello, World!</h1><p>This is a simple HTML document for PDF conversion.</p></body></html>"}' \
  http://localhost:5000/api/html-to-pdf
```

Also you can convert a URL to PDF by sending a POST request to the service endpoint with the URL.
Here is an example using curl:

```bash
curl \
  -X POST \
  -H "Content-Type: application/json" \
  -d '{"fileName":"example.pdf", "url":"https://www.google.com"}' \
  http://localhost:5000/api/url-to-pdf
```

## Contributing

Contributions are welcome! Please feel free to submit a pull request or create an issue for bugs, features, or suggestions.
