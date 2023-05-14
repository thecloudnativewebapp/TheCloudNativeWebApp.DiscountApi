@description('Provide a location for the registry.')
param location string = resourceGroup().location

@description('Provide a name for the SQL server.')
@secure()
param sqlServerName string

@description('Provide a name for the SQL database.')
param sqlDatabaseName string

param ipAddressWhitelist array

@allowed([
  'Basic'
  'Standard'
])
param databaseEdition string = 'Basic'

@allowed([
  'Basic'
  'S0'
  'S1'
  'S2'
  'S3'
  'S4'
  'S6'
  'S7'
  'S9'
  'S12'
])
param databaseRequestedServiceObject string = 'Basic'

@description('The administrator username of the SQL logical server.')
@secure()
param sqlLogin string

@description('The administrator password of the SQL logical server.')
@secure()
param sqlPassword string

resource sqlServer 'Microsoft.Sql/servers@2022-05-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: sqlLogin
    administratorLoginPassword: sqlPassword
  }
}

resource database 'Microsoft.Sql/servers/databases@2021-11-01-preview' = {
  parent: sqlServer
  name: sqlDatabaseName
  location: location
  sku: {
    tier: databaseEdition
    name: databaseRequestedServiceObject
  }
}

resource allowedIPAddresses 'Microsoft.Sql/servers/firewallRules@2021-11-01-preview' = [for i in range(0, length(ipAddressWhitelist)): {
  parent: sqlServer
  name: 'AllowedIPAddress ${i + 1}'
  properties: {
    startIpAddress: ipAddressWhitelist[i]
    endIpAddress: ipAddressWhitelist[i]
  }
}]
