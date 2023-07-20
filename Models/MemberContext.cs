using Microsoft.EntityFrameworkCore;

namespace CrudnetCoreMember.Models
{
    public class MemberContext : DbContext
    {
        public MemberContext(DbContextOptions<MemberContext> options) : base(options)
        {
            
        }
        public DbSet<Member> Members { get; set; }
    }
}

