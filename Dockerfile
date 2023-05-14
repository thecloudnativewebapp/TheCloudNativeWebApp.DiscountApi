FROM mcr.microsoft.com/dotnet/sdk:7.0 as builder
WORKDIR /src
COPY . .

RUN dotnet restore

RUN dotnet test

WORKDIR /src/Discounts.Api

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as final
WORKDIR /publish
COPY --from=builder /publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Discounts.Api.dll"]