FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY Policies.Api/*.csproj ./Policies.Api/
COPY Policies.Core/*.csproj ./Policies.Core/
COPY Policies.Repositories/*.csproj ./Policies.Repositories/
COPY Policies.Services/*.csproj ./Policies.Services/
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Policies.Api.dll"]