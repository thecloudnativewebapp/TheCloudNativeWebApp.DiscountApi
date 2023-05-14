@description('Provide a location for the registry.')
param location string = resourceGroup().location

module containerRegistry 'container-registry.module.bicep' = {
  name: 'create-container-registry.module'
  scope: resourceGroup()
  params: {
    location: location
  }
}
