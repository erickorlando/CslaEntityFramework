using Ucp.Entidades;

namespace Ucp.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Colegio : DbContext
    {
        // Your context has been configured to use a 'Colegio' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Ucp.Data.Colegio' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Colegio' 
        // connection string in the application configuration file.
        public Colegio()
            : base("name=Colegio")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Alumno> Alumnos { get; set; }  
    }

    
}