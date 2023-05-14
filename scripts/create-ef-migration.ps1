Write-Host "You are about to create a new EntityFramework migration"
Write-Host "Type the name of the migration"
$name = Read-Host

dotnet ef migrations add $name --project ../WebshopOnDdd.Discounts.Infrastructure.Sql/WebshopOnDdd.Discounts.Infrastructure.Sql.csproj --startup-project ../WebshopOnDdd.Discounts.Api/WebshopOnDdd.Discounts.Api.csproj 