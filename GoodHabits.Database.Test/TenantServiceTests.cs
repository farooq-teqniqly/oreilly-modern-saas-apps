using Xunit;
using NSubstitute;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using GoodHabits.HabitService.Services;
using GoodHabits.Database.Entities;

namespace GoodHabits.HabitService.Tests
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

    public class FakeHttpContext
    {
        private IHttpContextAccessor httpContextAccessor;
        private HttpContext httpContext;

        public FakeHttpContext()
        {
            this.httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            this.httpContext = Substitute.For<HttpContext>();
            this.httpContextAccessor.HttpContext.Returns(this.httpContext);
        }

        public void AddTenantHeader(string tenantName)
        {
            this.httpContext.Request.Headers.Returns(new HeaderDictionary { { "x-tenant", tenantName } });
        }

        public IHttpContextAccessor GetHttpContextAccessor()
        {
            return this.httpContextAccessor;
        }
    }
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