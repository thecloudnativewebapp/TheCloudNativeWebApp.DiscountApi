@description('Provide a location for the registry.')
param location string = resourceGroup().location

@minLength(5)
@maxLength(50)
@description('Provide a name of your Azure Container Apps')
param containerAppName string

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
@description('Provide the image to deploy')
param imageName string

@description('Provide Container App Environment Id')
param containerAppEnvironmentId string

@description('The administrator password of the SQL logical server.')
@secure()
param sqlConnectionString string

@description('Minimum number of replicas that will be deployed')
@minValue(0)
@maxValue(25)
param minReplica int = 1

@description('Maximum number of replicas that will be deployed')
@minValue(0)
@maxValue(25)
param maxReplica int = 1

resource containerApp 'Microsoft.App/containerApps@2022-06-01-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: containerAppEnvironmentId
    configuration: {
      ingress: {
        external: true
        targetPort: 8080
        allowInsecure: false
        traffic: [
          {
            latestRevision: true
            weight: 100
          }
        ]
      }
      registries: [
        {
          server: containerRegistryLoginServer
          username: containerRegistryUsername
          passwordSecretRef: 'container-registry-password'
        }
      ]
      secrets: [
        {
          name: 'container-registry-password'
          value: containerRegistryPassword
        }
      ]
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: imageName
          env: [
            {
              name: 'ConnectionString'
              value: sqlConnectionString
            }
          ]
          resources: {
            cpu: json('.25')
            memory: '.5Gi'
          }
        }
      ]
      scale: {
        minReplicas: minReplica
        maxReplicas: maxReplica
        rules: [
          {
            name: 'http-requests'
            http: {
              metadata: {
                concurrentRequests: '10'
              }
            }
          }
        ]
      }
    }
  }
}

output IpAddresses array = containerApp.properties.outboundIpAddresses
