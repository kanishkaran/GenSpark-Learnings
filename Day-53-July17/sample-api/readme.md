
## Azure Container Registry, Replication - Learnings

    `az login`

### Create registry
    `az acr create --resource-group appdev-training --name containerregistrykanish --sku Premium `


### create a dockerfile

    `az acr build --registry containerregistrykanish --image acrtask:v1 .`


### listing the docker

    kanishkaran@FVFG90G8Q05F- sample-api % az acr repository list --name containerregistrykanish --output table                                        
    Result
    --------
    acrtask

### Enable admin
    az acr update -n containerregistrykanish --admin-enabled true

### View Credentials

    az acr credential show --name containerregistrykanish

    
### Create container

    az container create --resource-group appdev-training --name acr-task --image containerregistrykanish.azurecr.io/acrtask:v1 --registry-login-server containerregistrykanish.azurecr.io --ip-address Public --location eastus2 --registry-username containerregistrykanish --registry-password  --os-type Linux --cpu 1 --memory 1


### View Container

    az container show --resource-group appdev-training --name acr-task --query ipAddress.ip --output table

### Create Replication

    az acr replication create --registry containerregistrykanish --location eastus2

### View Replication

    az acr replication list --registry containerregistrykanish --output table