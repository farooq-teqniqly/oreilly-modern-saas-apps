using GoodHabits.Database.Entities;

namespace GoodHabits.Database.Interfaces;

public interface ITenantService
{
    public string GetConnectionString();
    public Tenant GetTenant();
}
