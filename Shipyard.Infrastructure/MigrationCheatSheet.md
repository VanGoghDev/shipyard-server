# Cheat sheet for fast ctrl+c, ctrl+v migrations

Example for UserDbContext

### Creating migration named "User" 

```shell
dotnet ef migrations add User --project Shipyard.Infrastructure --startup-project Shipyard.Api       
```

### Run migration

```shell
 dotnet ef database update --project Shipyard.Infrastructure --startup-project Shipyard.Api --context UserDbContext
```

### Remove migrations

```shell
 dotnet ef migrations remove --project Shipyard.Infrastructure --startup-project Shipyard.Api --context UserDbContext
```
