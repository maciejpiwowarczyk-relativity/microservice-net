minikube docker-env | Invoke-Expression
dotnet publish AspNetCoreMicroservice -c release -o dockerize\publish -r linux-x64 --no-self-contained
cd dockerize
docker build -t aspmicroservice:dev .
docker image prune -f
