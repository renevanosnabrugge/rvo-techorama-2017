$RESOURCEGROUP="demo"
$LOCATION="westeurope"
$DNSPREFIX="demo-k8-acs"
$CLUSTERNAME="demo-k8-acs-cluster"

$DOCKER_REGISTRY_SERVER ="registry.azurecr.io"
$DOCKER_USER="registry"
$DOCKER_PASSWORD="******************"
$DOCKER_EMAIL="email@email.com"

#az login 
az account set --subscription "Microsoft Azure Sub"

Write-Host "Create cluster"
az group create --name=$RESOURCEGROUP --location=$LOCATION
az acs create --orchestrator-type=kubernetes --resource-group $RESOURCEGROUP --name=$CLUSTERNAME --dns-prefix=$DNSPREFIX

Write-Host "Cluster created. Wait to spin up Nodes"
pause

Write-Host "Connect to Cluster"
az acs kubernetes get-credentials --resource-group=$RESOURCEGROUP --name=$CLUSTERNAME

pause

Write-Host "see nodes"
kubectl get nodes

pause

Write-Host "Configure private Azure Container Registry"
kubectl create secret docker-registry myregistrykey --docker-server=$DOCKER_REGISTRY_SERVER --docker-username=$DOCKER_USER --docker-password=$DOCKER_PASSWORD --docker-email=$DOCKER_EMAIL
pause

Start-Process  "http://localhost:8001/ui"
kubectl proxy

