using System.Data.Entity;

namespace MedicalWeb.Models
{
    public class MedicalContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MedicalWeb.Models.MedicalContext>());

        public MedicalContext() : base("name=MedicalContext")
        {
        }

        public DbSet<MedicalType> MedicalTypes { get; set; }

        public DbSet<Medical> Medicals { get; set; }

        public DbSet<Pharmci> Pharmcis { get; set; }

        public DbSet<InvoiceClass> InvoiceClasses { get; set; }

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}
