namespace GoodHabits.Database.Entities;

public class Tenant
{
    public string TenantName { get; set; } = default!;
    public string ConnectionString { get; set; } = default!;
}
