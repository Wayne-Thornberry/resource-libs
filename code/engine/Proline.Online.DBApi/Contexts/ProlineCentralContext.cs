using Proline.CentralEngine.DBApi.Models.Central;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proline.CentralEngine.DBApi.Contexts
{
    public class ProlineCentralContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ProlineCentralContext() : base("name=ProlineCentral")
        {
        }

        public DbSet<UserAccount> Users { get; set; }
        public DbSet<UserInstanceLicence> UserInstanceLicence { get; set; }
        public DbSet<UserAllow> UserAllow { get; set; }
        public DbSet<UserDeny> UserDeny { get; set; }
        public DbSet<Instance> Instance { get; set; }
        public DbSet<InstancePlayer> InstancePlayer { get; set; }
        public DbSet<InstanceUserDeny> InstanceUserDeny { get; set; }
        public DbSet<InstanceUserAllow> InstanceUserAllow { get; set; }
        public DbSet<PlayerAccount> PlayerRegistration { get; set; }
        public DbSet<PlayerIndentity> PlayerIndentity { get; set; }
        public DbSet<PlayerIndentityType> PlayerIdentityType { get; set; }
    }
}
