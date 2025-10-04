# AbySalto Junior Project

## Pokretanje projekta

### 1. Postavi connection string

Ažuriraj connection string u `appsettings.Development.json` datoteci da pokazuje na Vašu lokalnu SQL instancu:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(LocalDb)\\MSSQLLocalDB;Database=AbySalto;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 2. Pokreni migracije

```bash
dotnet ef database update
```

## Migracije

### Nova migracija

```bash
dotnet ef migrations add [NazivMigracije]
```

### Ažuriraj bazu

```bash
dotnet ef database update
```
