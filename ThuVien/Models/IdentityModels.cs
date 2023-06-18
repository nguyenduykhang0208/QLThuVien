using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace ThuVien.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string masv { get; set; }
        public string gioitinh { get; set; }
        public string diachi { get; set; }
        public int matsach { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<chitietmuon> chitietmuons { get; set; }
        public virtual DbSet<nhaxuatban> nhaxuatbans { get; set; }
        public virtual DbSet<phieumuon> phieumuons { get; set; }
        public virtual DbSet<sach> saches { get; set; }
        public virtual DbSet<tacgia> tacgias { get; set; }
        public virtual DbSet<theloai> theloais { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}