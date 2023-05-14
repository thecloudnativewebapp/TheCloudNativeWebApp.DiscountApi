@description('Provide a location for the registry.')
param location string = resourceGroup().location

@minLength(5)
@maxLength(100)
@description('Provide the login server of your Azure Container Registry')
param containerRegistryLoginServer string

@minLength(5)
@maxLength(100)
@description('Provide the username of your Azure Container Registry')
@secure()
param containerRegistryUsername string

@minLength(5)
@maxLength(100)
@description('Provide the password of your Azure Container Registry')
@secure()
param containerRegistryPassword string

@minLength(20)
@maxLength(100)
@description('Provide the name of the Docker image to deploy')
@secure()
param imageName string

@minLength(5)
@maxLength(100)
@description('Provide the username of your Sql Server')
@secure()
param sqlLogin string

@minLength(5)
@maxLength(100)
@description('Provide the password of your Sql Server')
@secure()
param sqlPassword string

@description('Provide the connectionstring of your Sql Server')
@secure()
param sqlConnectionString string

var sqlServerName = 'sqlserver-vouchers-westeu'

module containerAppsEnvironment 'container-apps-environment.module.bicep' = {
  name: 'create-vouchers-containerapps-environement'
  scope: resourceGroup()
  params: {
    location: location
  }
}

module containerApp 'container-app.module.bicep' = {
  name: 'create-vouchers-containerapp'
  scope: resourceGroup()
  params: {
    location: location
    containerAppName: 'ca-vouchers-westeu'
    imageName: imageName
    containerRegistryLoginServer: containerRegistryLoginServer
    containerAppEnvironmentId: containerAppsEnvironment.outputs.containerAppEnvironmentId
    sqlConnectionString: sqlConnectionString
    containerRegistryUsername: containerRegistryUsername
    containerRegistryPassword: containerRegistryPassword
  }
}

module sqlServer 'sql-server.module.bicep' = {
  name: 'create-sql-server'
  scope: resourceGroup()
  params: {
    ipAddressWhitelist: containerApp.outputs.IpAddresses
    location: location
    sqlServerName: sqlServerName
    sqlDatabaseName: 'vouchers'
    sqlLogin: sqlLogin
    sqlPassword: sqlPassword
  }
}
