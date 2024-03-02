using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksIdentityDbContext: IdentityDbContext
    {
        public NZWalksIdentityDbContext(DbContextOptions<NZWalksIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "f84257b3-39db-4fa9-be29-34dff53ee938";
            var writerRoleId = "fd3d925e-b3e9-4c25-8f86-f3281becdf99";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },

                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            // Seeds roles to identity db
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
