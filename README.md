# MeleeAramBackend

### Setup Docker

1. pull docker image:
   docker pull postgres:latest
2. Start a postgres container instace
   docker run --name my-postgres -e POSTGRES_USER=myuser -e POSTGRES_PASSWORD=mypassword -e POSTGRES_DB=mydb -p 5432:5432 -d postgres:latest
3. You can access the container with  
   docker exec -it my-postgres psql -U myuser -d mydb
4. Setup default connection in appsettings.json
   {
   "ConnectionStrings": {
   "DefaultConnection": "Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword"
   }
   }
5. Migrate db with dotnet cli
   dotnet ef migrations add InitialCreate
6. Update database with dotnet cli
   dotnet ef database update
