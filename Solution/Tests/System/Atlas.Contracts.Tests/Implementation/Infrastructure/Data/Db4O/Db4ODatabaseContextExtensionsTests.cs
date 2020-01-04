using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.Data.Db4O.Stubs;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.Data.Db4O
{
    [TestClass, ExcludeFromCodeCoverage]
    public class Db4ODatabaseContextExtensionsTests
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.adb");


        [TestInitialize]
        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(_dbPath);
        }

        [TestMethod]
        public void Merge_MergesTwoGivenListsAndUpdatesTheChangesDetectedInTheProcess()
        {
            // Arrange
            EntityStub entity1 = new EntityStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "E1"
            },
            entity2 = new EntityStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "E2"
            };

            // Act
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(entity1);
                db.Store(entity2);
                db.Commit();
            }

            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                entity2.Name = "EE2";
                var entity3 = new EntityStub
                {
                    Name = "E3"
                };
                var list1 = new[] { entity1, entity2 };
                var list2 = new[] { entity2, entity3 };

                db.MergeLists(list1, list2, db.Add, x =>
                {
                    var dbEntity = db.Find<EntityStub>(x.Id);
                    if (dbEntity == null)
                        return;

                    x.UpdateProperties(dbEntity, "Id", "Name");
                    db.Update(dbEntity);
                }, db.Delete);
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                EntityStub[] entities = db.Query<EntityStub>().ToArray();
                Assert.AreEqual(2, entities.Length);

                Assert.AreEqual(1, entities.Count(x => x.Name == "EE2"));
                Assert.AreEqual(1, entities.Count(x => x.Name == "E3"));
            }
        }
    }
}
