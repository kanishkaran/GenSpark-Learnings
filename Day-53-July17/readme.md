

## Azure DNL scopes - Task 2 forenoon

Task 1 : [azure-container-resgistry](./sample-api/readme.md)


### Create Azure Container Registry

    az acr create --name kanishacr --resource-group appdev-training --sku Standard --dnl-scope TenantReuse

### Pulling sample docker image

    docker pull mcr.microsoft.com/hello-world

### Tagging the image

    docker tag mcr.microsoft.com/hello-world kanishacr-ghb0bcc9gbg3cacy.azurecr.io/hello-world:v1

### Docker login

    docker login kanishacr-ghb0bcc9gbg3cacy.azurecr.io  

### Docker Push

    docker push kanishacr-ghb0bcc9gbg3cacy.azurecr.io/hello-world:v1 

### Docker rmi - remove local image

    docker rmi kanishacr-ghb0bcc9gbg3cacy.azurecr.io/hello-world:v1 

### Docker run

    docker run kanishacr-ghb0bcc9gbg3cacy.azurecr.io/hello-world:v1 