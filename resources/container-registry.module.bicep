@description('Provide a location for the registry.')
param location string = resourceGroup().location

@minLength(5)
@maxLength(50)
@description('Provide a globally unique name of your Azure Container Registry')
param containerRegistryName string = 'acrxscwesteu'

@description('Provide a tier of your Azure Container Registry.')
param containerRegistrySku string = 'Basic'

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: containerRegistryName
  location: location
  sku: {
    name: containerRegistrySku
  }
  properties: {
    adminUserEnabled: true
  }
}
