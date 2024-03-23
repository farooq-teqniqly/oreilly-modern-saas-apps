using NSubstitute;
using Microsoft.AspNetCore.Http;

namespace GoodHabits.Database.Tests.Fakes
{
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
}