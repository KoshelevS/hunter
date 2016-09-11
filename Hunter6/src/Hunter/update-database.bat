dotnet ef --project ../Hunter.Security --startup-project . database update --context SecurityDbContext
dotnet ef --project ../Hunter.Infrastructure.Data --startup-project . database update --context DomainContext
