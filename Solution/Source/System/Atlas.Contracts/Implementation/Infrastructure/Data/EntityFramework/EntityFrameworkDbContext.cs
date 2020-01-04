using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Security;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework
{
    public class NewDbContext : DbContext
    {
        public NewDbContext() : base(GetConnectionString()) {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewDbContext, Migrations.Configuration>(GetConnectionString()));
        }

        public static string GetConnectionString()
        {

            string cxn = "AtlasDb";
            try
            {
                cxn = ((AtlasDataAccess)ServiceLocator.Current.GetInstance(typeof(AtlasDataAccess))).ConnectionString;
            }
            catch (System.Exception ex)
            {

                cxn = "Data Source=WILDWEST\\WILDWEST;Initial Catalog=Atlas50;User ID=sa;Password=kidwest12!";

            }




            return cxn;
        }
    }
    public class EntityFrameworkDbContext<T> : DbContext, IEntityFrameworkDbContext<T>
         where T : EntityBase
    {
        //   private readonly IObjectContainer _database;


        /// <summary>
        /// Initializes a new instance of a database context to handle a DB4O database. //old: AppDomain.CurrentDomain.BaseDirectory
        /// : base(Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Atlas Enterprise\\Atlas Suite\\EntityFramework", "atlas.mdf"))
        /// </summary>
        /// "Data Source=WILDWEST\\WILDWEST;Initial Catalog=Atlas50;User ID=sa;Password=kidwest12!"
        public EntityFrameworkDbContext()
            : base(GetConnectionString())
        {
            Database.CommandTimeout = 60;
         
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewDbContext, Migrations.Configuration>(GetConnectionString()));
            //   if(Database.CompatibleWithModel(false))
            //    Database.Initialize(false);

        }

        //private static DbConnection GetDbConnection()
        //{
        //    DbConnection cxn = new SqlConnection();
        //    try
        //    {
        //        cxn = ((AtlasDataAccess)ServiceLocator.Current.GetInstance(typeof(AtlasDataAccess))).DbConnection;
        //    }
        //    catch (System.Exception ex)
        //    {

        //        cxn = new SqlConnection();

        //    }
        //    return cxn;
        //}
        public static string GetConnectionString()
        {
           
            string cxn = "AtlasDb";
            try
            {
                cxn = ((AtlasDataAccess)ServiceLocator.Current.GetInstance(typeof(AtlasDataAccess))).ConnectionString;
            }
            catch (System.Exception ex)
            {

                cxn = "AtlasDb";

            }


            

            return cxn;
        }


       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<T>().HasKey(x=> x.Id);

            modelBuilder.Properties<DateTime>()
            .Configure(c => c.HasColumnType("datetime2"));

            // modelBuilder.Entity<AtlasModuleInfo>().ToTable("AtlasModuleInfo");
            // modelBuilder.Entity<AtlasModuleInfo>().HasKey(x => x.ModuleName);
            //  modelBuilder.Entity<AtlasUser>().HasMany(au => au.AllowedModules);
        }

        public virtual DbSet<T> Entities { get; set; }

        public IQueryable<T1> GetAll<T1>()
        {
            return Entities.Cast<T1>();
        }

        public string GenerateKey()
        {
            return Guid.NewGuid().ToString();
        }

        public void Add(T entity) 
        {
          if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            entity.Id = GenerateKey();
            Entities.Add(entity);
        }

        public void Delete(T entity)
        {
            Entities.Remove(Entities.Find(entity.Id));
        }

        public void Save()
        {
           //SaveChangesAsync();
           SaveChanges();
         
        }

        public void DropChanges()
        {
          
            

            
        }

        public void Update(T item)
        {
            Save();
        }

        public T Find(object id)
        {
            if (id == null)
                return null;
             return Entities.SingleOrDefault(x => x.Id == id.ToString());
        }

        public T Find(ISpecification<T> specification)
        {
          return Entities.Find(specification);
        }

        //public IEnumerable<T> Where(Expression<Func<T, bool>> specification)
        //{

        //    return Entities.Where(specification);
        //}

        public IEnumerable<T> Where(IQueryable<T> queryable)
        {
           
            return queryable;
        }

        public IEnumerable<T> Where(IQueryable<T> queryable, string Parameter)
        {
            //Database.CommandTimeout = 60;
            //var sqlCommand = queryable.ToString();
            return Entities.SqlQuery(queryable.ToString(), new SqlParameter("@p__linq__0", Parameter)).ToList();//@p__linq__0.SqlQuery(queryable.ToString(), new sqlpara Parameter);
        }

        public IEnumerable<T> RoughSQL(string sql)
        {
           
            return Database.SqlQuery<T>(sql).ToList();//@p__linq__0.SqlQuery(queryable.ToString(), new sqlpara Parameter);
        }
        public void Add<T1>(T1 entity) where T1 : IEntity
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            string newId = DateTime.Now.Ticks.ToString();
            newId = newId + GenerateKey();
            entity.Id = newId;
            Entities.Add(entity as T);
        }

        public void Delete<T1>(T1 entity) where T1 : IEntity
        {
            Entities.Remove(Entities.Find(entity.Id));
        }

        public void Update<T1>(T1 item) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 Find<T1>(object id) where T1 :  IEntity
        {
            throw new NotImplementedException();
        }

        public T1 Find<T1>(ISpecification<T1> specification) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T1> Where<T1>(ISpecification<T1> specification) where T1 : IEntity
        {
            return Entities.Cast<T1>();
        }

        public IEnumerable<T1> GetSorted<T1>(Comparison<T1> comparison) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public void Merge<TEntity>(IEnumerable<TEntity> formerItems, IEnumerable<TEntity> currentItems, Action<TEntity> addAction, Action<TEntity> updateAction,
            Action<TEntity> deleteAction) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public string DbConnectionString { get { return Database?.Connection.ConnectionString; } }


        //public IEnumerable<object> GetSorted(Comparison<object> comparison)
        //{
        //    throw new NotImplementedException();
        //}
    }
}