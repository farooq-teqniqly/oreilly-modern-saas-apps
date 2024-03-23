using System.Diagnostics;
using GoodHabits.Database.Entities;
using GoodHabits.Database.Interfaces;
using Microsoft.Extensions.Options;

namespace GoodHabits.HabitService.Services;

public class TenantService : ITenantService
{
    private readonly TenantSettings tenantSettings;
    private Tenant tenant = default!;

    public TenantService(IOptions<TenantSettings> options, IHttpContextAccessor accessor)
    {
        this.tenantSettings = options.Value;
        var httpContext = accessor.HttpContext!;

        if (httpContext is null)
        {
            return;
        }

        if (httpContext.Request.Headers.TryGetValue("x-tenant", out var tenantName))
        {
            if (string.IsNullOrEmpty(tenantName))
            {
                throw new InvalidOperationException("No tenant name was found in the request headers.");
            }

            var requestedTenant = this.tenantSettings.Tenants.SingleOrDefault(t => t.TenantName == tenantName)
            ?? throw new InvalidOperationException($"Invalid tenant - {this.tenant}");

            if (string.IsNullOrEmpty(requestedTenant.ConnectionString))
            {
                requestedTenant.ConnectionString = this.tenantSettings.DefaultConnectionString;
            }

            this.tenant = requestedTenant;
        }
        else
        {
            throw new InvalidOperationException($"Tenant request header is missing.");
        }
    }
    public string GetConnectionString() => this.tenant.ConnectionString;

    public Tenant GetTenant() => this.tenant;
}
