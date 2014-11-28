using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Pfizer.Repository.History;
using System.Data.Entity.ModelConfiguration.Conventions;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Repository;
using Pfizer.Domain.Models;
using Wizardsgroup.Repository.AuditTrail;

namespace Pfizer.Repository.Context
{
    internal class MainContext : AbstractContext
    {
        internal static DatabaseInitializerMode DatabaseInitializerMode = DatabaseInitializerMode.DropThenCreate;
        private readonly AuditTrailHandler _auditLogger;

        // This is used for the TFS Auto Build and Deploy
        public void ForceDropThenCreate()
        {
            Database.SetInitializer(new ContextDropCreateDatabaseIfModelChangesInitializeSeeder());
        }

        // This is used for the CodedUI
        public void ForceDropThenCreate(IExternalSeeder instance)
        {
            Database.SetInitializer(new ContextDropCreateDatabaseIfModelChangesInitializeSeeder(instance));
        }

        // This is used for the Client Reseeding deployment
        public void ForceReseed()
        {
            Database.SetInitializer(new ContextInitializer());
        }

        static MainContext()
        {
            switch (DatabaseInitializerMode)
            {
                case DatabaseInitializerMode.DoNotChange:
                    Database.SetInitializer<MainContext>(null);
                    break;
                case DatabaseInitializerMode.DropThenCreate:
                    Database.SetInitializer(new ContextDropCreateDatabaseIfModelChangesInitializeSeeder());
                    break;
                case DatabaseInitializerMode.Reseed:
                    Database.SetInitializer(new ContextInitializer());
                    break;
            }
        }

        #region Constructor

        public MainContext()
            : base("name=Pfizer")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            _auditLogger = new AuditTrailHandler(GetTypesToExcludeInAudit(), this);
        }

        public MainContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            _auditLogger = new AuditTrailHandler(GetTypesToExcludeInAudit(), this);
        }

        #endregion

        #region Core

        public DbSet<ActivityNotification> ActivityNotifications { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<DataDictionary> DataDictionaries { get; set; }
        public DbSet<GridSetting> GridSettings { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<RuleDatastore> RuleDatastores { get; set; }
        public DbSet<SystemMessage> SystemMessages { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroupMap> UserGroupMaps { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupFunction> UserGroupFunctions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CompanyClassification> CompanyClassifications { get; set; }
        public DbSet<EmployeeClassification> EmployeeClassifications { get; set; }
        public DbSet<Employee> Employees { get; set; }
        #endregion

        #region Client
        public DbSet<CardPrefix> CardPrefixes { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ConversionFactor> ConversionFactors { get; set; }
        public DbSet<CustomerMapping> Customers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentProcessResult> DocumentProcessResults { get; set; }       
        public DbSet<Dosage> Dosages { get; set; }
        public DbSet<DumpData> DumpData { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InstitutionType> InstitutionTypes { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<ProgramProductMapping> ProgramProductMappings { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<SalesRetailPrice> SalesRetailPrices { get; set; }
        public DbSet<StoreArea> StoreAreas { get; set; }
        public DbSet<StoreBranch> StoreBranches { get; set; }        
        public DbSet<StoreMainType> StoreMainTypes { get; set; }
        public DbSet<StoreMainHandlingFee> StoreMainHandlingFees { get; set; }
        public DbSet<SulitMedMd> SulitMedMds { get; set; }
        public DbSet<SulitMedMdProduct> SulitMedMdProducts { get; set; }        
        public DbSet<SulitMedProduct> SulitMedProducts { get; set; }
        public DbSet<SulitMedTerritory> SulitMedTerritories { get; set; }        
        public DbSet<Target> Targets { get; set; }
        public DbSet<TargetType> TargetTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TerritoryProductCardMapping> TerritoryProductCardMappings { get; set; }
        public DbSet<TerritorySalesConfiguration> TerritorySalesConfigurations { get; set; }
        public DbSet<TerritorySalesConfigurationHistory> TerritorySalesConfigurationHistories { get; set; }
        public DbSet<TsstRaw> TsstRaws { get; set; }
        
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<AbstractUser>().Map(o => o.ToTable("User"));
            modelBuilder.Entity<AbstractUserGroupMap>().Map(o => o.ToTable("UserGroupMap"));
            modelBuilder.Entity<AbstractUserGroup>().Map(o => o.ToTable("UserGroup"));
            modelBuilder.Entity<AbstractCompany>().Map(o => o.ToTable("Company"));
            modelBuilder.Entity<AbstractEmployee>().Map(o => o.ToTable("Employee"));

            modelBuilder.Entity<TerritoryProductCardMapping>()
                .HasRequired(o => o.OldTeam)
                .WithMany(o => o.OldTerritoryProductCardMappings)
                .HasForeignKey(o => o.OldTeamId);

            modelBuilder.Entity<TerritoryProductCardMapping>()
                .HasRequired(o => o.NewTeam)
                .WithMany(o => o.NewTerritoryProductCardMappings)
                .HasForeignKey(o => o.NewTeamId);

            //modelBuilder.Entity<ModuleRegister>()
            //            .HasOptional(t => t.Parent)
            //            .WithMany(t => t.Children)
            //            .HasForeignKey(t => t.ParentId);

            //Cascade delete
            //modelBuilder.Entity<Model>()
            //    .HasMany(o => o.Property)
            //    .WithRequired(o => o.PropertyModel)
            //    .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            LogToHistory();
            AuditChanges();
            return base.SaveChanges();
        }
        
        private void LogToHistory()
        {

            var handler = new HistoryInsertHandler(GetTrackedEntities(), this);
            handler.HandleInsertHistory();
        }

        private void AuditChanges()
        {
            if (DatabaseInitializerMode != DatabaseInitializerMode.Migration)
                _auditLogger.HandleAuditTrail(GetTrackedEntities());
        }

        private List<DbEntityEntry> GetTrackedEntities()
        {
            var changeTrackedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Added ||
                                                                 p.State == EntityState.Deleted ||
                                                                 p.State == EntityState.Modified).ToList();
            return changeTrackedEntities;
        }

        private static List<Type> GetTypesToExcludeInAudit()
        {
            return new List<Type>
            {
                typeof(GridSetting),typeof(DataDictionary),typeof(AuditLog)
            };
        }
    }
}