Write-Host "Hi! And welcome aboard. Great you're joining our team!"
Write-Host "Did you install Docker? (Y/N)"
$yesNo = Read-Host
if($yesNo -eq "n" || $yesNo -eq "N") {
    Write-Error "Please install Docker first!"
    exit
}

Write-Host "Awesome, I'm going to start SQL server now. I'll use the default password: yourStrong(!)Password"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

Write-Host "I'll configure the API to use it too"
cd ../WebshopOnDdd.Discounts.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionString" "Server=127.0.0.1;Database=Discounts;User Id=sa;Password=yourStrong(!)Password;Encrypt=True;TrustServerCertificate=True"

cd ../scripts

Write-Host "All set. You're good to go."

Write-Host "Happy programming!"