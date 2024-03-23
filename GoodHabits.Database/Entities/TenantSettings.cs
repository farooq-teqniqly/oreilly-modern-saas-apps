namespace GoodHabits.Database.Entities;

public class TenantSettings
{
    public string DefaultConnectionString { get; set; } = default!;
    public IList<Tenant> Tenants { get; set; } = default!;
}
