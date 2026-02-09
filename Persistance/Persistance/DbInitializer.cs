using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class DbInitializer
    {
        public static async Task Initialize(ProductsDbContext context, CancellationToken cancellation)
        {
            await context.Database.EnsureCreatedAsync(cancellation);
        }
    }
}
