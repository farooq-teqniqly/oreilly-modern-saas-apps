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
        public static TenantSettings TenantSettings => new TenantSettings
            {
                DefaultConnectionString = "Default Connection String",
                Tenants = new[]
                {
                    new Tenant { TenantName = "Tenant1", ConnectionString = "Connection String 1" },
                    new Tenant { TenantName = "Tenant2", ConnectionString = "Connection String 2" }
                }
            };
    }
    public class TenantServiceTests
    {
        [Fact]
        public void GetConnectionString_ReturnsCorrectConnectionString()
        {
            var options = Substitute.For<IOptions<TenantSettings>>();
            options.Value.Returns(FakeTenantSettings.TenantSettings);

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            httpContextAccessor.HttpContext.Returns(httpContext);
            httpContext.Request.Headers.Returns(new HeaderDictionary { { "x-tenant", "Tenant1" } });

            var tenantService = new TenantService(options, httpContextAccessor);

            tenantService.GetConnectionString().Should().Be(FakeTenantSettings.TenantSettings.Tenants[0].ConnectionString);
        }

        [Fact]
        public void GetTenant_ReturnsCorrectTenant()
        {
            var options = Substitute.For<IOptions<TenantSettings>>();
            options.Value.Returns(FakeTenantSettings.TenantSettings);

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            httpContextAccessor.HttpContext.Returns(httpContext);
            httpContext.Request.Headers.Returns(new HeaderDictionary { { "x-tenant", "Tenant2" } });

            var tenantService = new TenantService(options, httpContextAccessor);

            var tenant = tenantService.GetTenant();

            tenant.Should().BeEquivalentTo(FakeTenantSettings.TenantSettings.Tenants[1]);
        }
    }
}