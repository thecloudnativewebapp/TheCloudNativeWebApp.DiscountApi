@description('Provide a location for the registry.')
param location string = resourceGroup().location

@minLength(5)
@maxLength(50)
@description('Provide a name of your Azure Operational Insights Workspace')
param logAnalyticsName string = 'oiw-vouchers-westeu'

@minLength(5)
@maxLength(50)
@description('Provide a name of your Azure Container Apps Environment')
param containerNameEnvironmentName string = 'acae-vouchers-westeu'

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: logAnalyticsName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource containerAppEnvironment 'Microsoft.App/managedEnvironments@2022-06-01-preview' = {
  name: containerNameEnvironmentName
  location: location
  sku: {
    name: 'Consumption'
  }
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalytics.properties.customerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
      }
    }
  }
}

output containerAppEnvironmentId string = containerAppEnvironment.id

