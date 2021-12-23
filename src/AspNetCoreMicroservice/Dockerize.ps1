minikube docker-env | Invoke-Expression
dotnet publish -c release -o publish -r linux-x64 --no-self-contained
docker build -t aspmicroservice:dev -f .\Dockerfile-standalone .
docker image prune -f
