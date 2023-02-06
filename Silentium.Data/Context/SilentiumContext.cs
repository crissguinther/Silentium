using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Silentium.Data.Context {
    public class SilentiumContext : DbContext {
        public SilentiumContext(DbContextOptions<SilentiumContext> options) : base(options) {
        }

        public DbSet<Silentium.Domain.SentFile> Files { get; set; }
    }
}
