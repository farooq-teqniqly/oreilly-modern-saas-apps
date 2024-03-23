using Xunit;
using FluentAssertions;
using GoodHabits.HabitService.Services;
using GoodHabits.Database.Tests.Fakes;

namespace GoodHabits.Database.Tests
{
        
    public class TenantServiceTests
    {
        private readonly FakeHttpContext fakeHttpContext;
        private readonly FakeTenantSettings fakeTenantSettings;

        public TenantServiceTests()
        {
            this.fakeHttpContext = new FakeHttpContext();
            this.fakeTenantSettings = new FakeTenantSettings();
        }

        [Fact]
        public void GetConnectionString_ReturnsCorrectConnectionString()
        {
            this.fakeHttpContext.AddTenantHeader("Tenant1");

            var tenantService = new TenantService(this.fakeTenantSettings.GetOptions(), this.fakeHttpContext.GetHttpContextAccessor());

            tenantService.GetConnectionString().Should().Be(this.fakeTenantSettings.GetTenant("Tenant1").ConnectionString);
        }

        [Fact]
        public void GetTenant_ReturnsCorrectTenant()
        {
            this.fakeHttpContext.AddTenantHeader("Tenant2");

            var fakeTenantSettings = new FakeTenantSettings();

            var tenantService = new TenantService(this.fakeTenantSettings.GetOptions(), this.fakeHttpContext.GetHttpContextAccessor());

            var tenant = tenantService.GetTenant();

            tenant.Should().BeEquivalentTo(this.fakeTenantSettings.GetTenant("Tenant2"));
        }
    }
}