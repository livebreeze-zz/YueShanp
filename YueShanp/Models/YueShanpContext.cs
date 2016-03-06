namespace YueShanp.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class YueShanpContext : DbContext
    {
        // Your context has been configured to use a 'QuotedContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'YueShanp.Models.QuotedContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'QuotedContext' 
        // connection string in the application configuration file.
        public YueShanpContext()
            : base("name=YueShanpContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Quoted> Quoteds { get; set; }
        //public DbSet<CostQuoted> CostQuoted { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}