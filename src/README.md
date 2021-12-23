dotnet publish -c release -o publish -r linux-x64 --no-self-contained
docker build -t aspmicroservice:dev -f .\Dockerfile-standalone .
docker image prune -f
docker run -d -p 8080:80 --name aspmicroservice:dev aspmicroservice
http://localhost:8080/swagger/index.html
