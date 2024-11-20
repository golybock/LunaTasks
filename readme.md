Start rabbitmq in docker

```cmd
docker run --rm -p 5672:5672 rabbitmq:3.10.7-management
```

Start postgresql in docker

```cmd
docker pull postgres:latest
 
docker run --name luna-pg -p 4444:5432 -e POSTGRES_PASSWORD=admin -d postgres:latest
```

Database scripts in Solution Folder "DatabaseFiles"

db needs to create all databases

start project - run docker compose file 