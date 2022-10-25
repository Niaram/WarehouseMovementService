using System.Globalization;
using WarehouseMovementService.Infrastructure.Persistence;

namespace WarehouseMovementService.Startup
{
    public class DDDUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;

        public DDDUnitOfWorkMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDBContext applicationDBContext)
        {
            await _next(context);

            await applicationDBContext.SaveChangesAsync();
        }
    }
}
