using Microsoft.EntityFrameworkCore;

namespace Silentium.Data.Context {
    public class SilentiumContext : DbContext {
        public SilentiumContext(DbContextOptions options) : base(options) {         
        }
    }
}
