using GoodHabits.Database.Entities;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace GoodHabits.Database.Tests.Fakes
{
public class FakeTenantSettings
    {
        private readonly TenantSettings tenantSettings = new TenantSettings
            {
                DefaultConnectionString = "Default Connection String",
                Tenants = new[]
                {
                    new Tenant { TenantName = "Tenant1", ConnectionString = "Connection String 1" },
                    new Tenant { TenantName = "Tenant2", ConnectionString = "Connection String 2" }
                }
            };

        private readonly IOptions<TenantSettings> options;
        public FakeTenantSettings()
        {
            this.options = Substitute.For<IOptions<TenantSettings>>();
            this.options.Value.Returns(this.tenantSettings);
        }

        public IOptions<TenantSettings> GetOptions() => this.options;

        public Tenant GetTenant(string tenantName)
        {
            return this.tenantSettings.Tenants.Single(t => t.TenantName == tenantName);
        }
    }
}