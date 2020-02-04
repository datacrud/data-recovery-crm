using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using Project.Security.Extensions;
using Project.Security.Helpers;
using Project.Security.Models;

namespace Project.Model
{
    public class BusinessDbContext : DbContext
    {

        public BusinessDbContext() : base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }



        //add you models to DbSet here
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HddInfo> HddInfos { get; set; }
        public DbSet<CaseLog> CaseLogs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Audit> Audits { get; set; }







        #region MyRegion

        public override int SaveChanges()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return PrivateSaveChangesAsync(userId, CancellationToken.None).Result;
        }

        public override Task<int> SaveChangesAsync()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return PrivateSaveChangesAsync(userId, CancellationToken.None);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return PrivateSaveChangesAsync(userId, cancellationToken);
        }

        private async Task<int> PrivateSaveChangesAsync(string userId, CancellationToken cancellationToken)
        {
            // If you want to have audits in transaction with records you must handle
            // transactions manually
            //using (var scope =
            //    new TransactionScope(TransactionScopeOption.Required))
            //{
                ObjectContext context = ((IObjectContextAdapter) this).ObjectContext;
                await context.SaveChangesAsync(SaveOptions.DetectChangesBeforeSave, cancellationToken)
                    .ConfigureAwait(false);

                // Now you must call your audit code but instead of adding audits to context
                // you must add them to list. 

                var audits = new List<Audit>();

                //ApplicationUser currentUser = OwinContextHelper.CurrentApplicationUser;
                foreach (var entry in ChangeTracker.Entries()
                    .Where(o => o.State != EntityState.Unchanged && o.State != EntityState.Detached).ToList())
                {
                    var changeType = entry.State.ToString();
                    Type entityType = AuditHelper.GetEntityType(entry);

                    string tableName = AuditHelper.GetTableName(context, entityType);

                    string identityJson = new AuditHelper().GetIdentityJson(entry, entityType);

                    var audit = new Audit
                    {
                        Id = Guid.NewGuid(),
                        AuditUserId = userId,
                        ChangeType = changeType,
                        ObjectType = entityType.ToString(),
                        FromJson = (entry.State == EntityState.Added
                            ? "{  }"
                            : AuditHelper.GetAsJson(entry.OriginalValues)),
                        ToJson = (entry.State == EntityState.Deleted
                            ? "{  }"
                            : AuditHelper.GetAsJson(entry.CurrentValues)),
                        TableName = tableName,
                        IdentityJson = identityJson,
                        DateCreated = DateTime.UtcNow,
                    };

                    //audit.AuditUserId = currentUser != null
                    //    ? currentUser.Id
                    //    : null; //HttpContext.Current.User.Identity.GetUserId();
#if DEBUG
                    //if (audit.FromJson == audit.ToJson)
                    //{
                    //    throw new Exception(
                    //        $"Something went wrong because this {audit.ChangeType} Audit shows no changes!");
                    //}
#endif
                    audits.Add(audit);
                }

                // This is the reason why you must not add changes to context. You must accept
                // old changes prior to adding your new audit records otherwise EF will perform
                // changes again. If you add your entities to context and call accept before 
                // saving them your changes will be lost
                context.AcceptAllChanges();

                // Now add all audits from list to context
                Audits.AddRange(audits);

                int result = await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                // Complete the transaction
                //scope.Complete();

                return result;
            //}

            #endregion
        }
    }
}
