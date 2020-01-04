using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class Db4ORelatedRepositoryExtensionsTests : TestBase
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.adb");


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullRepository_Throws()
        {
            RelatedRepositoryExtensions.Relate(null, Mock.Of<SecondEntity>(), Mock.Of<FirstEntity>(), Mock.Of<IDb4ODatabaseContext>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullFirstEntity_Throws()
        {
            var repository = new RepositoryStub(Mock.Of<IDb4ODatabaseContext>());
            repository.Relate(null, Mock.Of<FirstEntity>(), Mock.Of<IDb4ODatabaseContext>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullSecondEntity_Throws()
        {
            var repository = new RepositoryStub(Mock.Of<IDb4ODatabaseContext>());
            repository.Relate(Mock.Of<SecondEntity>(), null, Mock.Of<IDb4ODatabaseContext>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullContext_Throws()
        {
            var repository = new RepositoryStub(Mock.Of<IDb4ODatabaseContext>());
            repository.Relate(Mock.Of<SecondEntity>(), Mock.Of<FirstEntity>(), (IDb4ODatabaseContext)null);
        }

        [TestMethod]
        public void Relate_GivenBothEntities_RelatesSecondWithFirst()
        {
            // Arrange
            var many = new SecondEntity
            {
                Id = Guid.NewGuid().ToString()
            };
            var one = new FirstEntity
            {
                Id = Guid.NewGuid().ToString()
            };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(many);
                db.Store(one);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db);
                repository.Relate(many, one, db);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreSame(one, many.One);
                CollectionAssert.Contains(one.Seconds.ToArray(), many);

                many = db.Query<SecondEntity>().Single();
                one = db.Query<FirstEntity>().Single();

                Assert.AreSame(one, many.One);
                CollectionAssert.Contains(one.Seconds.ToArray(), many);
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Relate_IfFirstEntityDoesNotExist_Throws()
        {
            // Arrange
            var db = new Mock<IDb4ODatabaseContext>();
            db.Setup(x => x.Find<SecondEntity>(It.IsAny<object>())).Returns(new SecondEntity());
            var repository = new RepositoryStub(db.Object);

            // Act
            repository.Relate(new SecondEntity(), new FirstEntity(), db.Object);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Relate_IfSecondEntityDoesNotExist_Throws()
        {
            // Arrange
            var db = new Mock<IDb4ODatabaseContext>();
            db.Setup(x => x.Find<FirstEntity>(It.IsAny<object>())).Returns(new FirstEntity());
            var repository = new RepositoryStub(db.Object);

            // Act
            repository.Relate(new SecondEntity(), new FirstEntity(), db.Object);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullRepository_Throws()
        {
            RelatedRepositoryExtensions.Unrelate(null, Mock.Of<SecondEntity>(), Mock.Of<FirstEntity>(), Mock.Of<IDb4ODatabaseContext>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullFirstEntity_Throws()
        {
            var repository = new RepositoryStub(Mock.Of<IDb4ODatabaseContext>());
            repository.Unrelate(null, Mock.Of<FirstEntity>(), Mock.Of<IDb4ODatabaseContext>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullSecondEntity_Throws()
        {
            var repository = new RepositoryStub(Mock.Of<IDb4ODatabaseContext>());
            repository.Unrelate(Mock.Of<SecondEntity>(), null, Mock.Of<IDb4ODatabaseContext>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullContext_Throws()
        {
            var repository = new RepositoryStub(Mock.Of<IDb4ODatabaseContext>());
            repository.Unrelate(Mock.Of<SecondEntity>(), Mock.Of<FirstEntity>(), (IDb4ODatabaseContext)null);
        }

        [TestMethod]
        public void Unrelate_GivenBothEntities_UnrelatesSecondWithFirst()
        {
            // Arrange
            var one = new FirstEntity { Id = Guid.NewGuid().ToString() };
            var many = new SecondEntity
            {
                One = one
            };
            one.Seconds.Add(many);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(one);
                db.Store(many);
                db.Commit();
            }


            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db);
                repository.Unrelate(many, one, db);

                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                one = db.Query<FirstEntity>().Single();
                many = db.Query<SecondEntity>().Single();

                Assert.IsNull(many.One);
                CollectionAssert.DoesNotContain(one.Seconds.ToArray(), many);
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Unrelate_IfFirstEntityDoesNotExist_Throws()
        {
            // Arrange
            var db = new Mock<IDb4ODatabaseContext>();
            db.Setup(x => x.Find<SecondEntity>(It.IsAny<object>())).Returns(new SecondEntity());
            var repository = new RepositoryStub(db.Object);

            // Act
            repository.Unrelate(new SecondEntity(), new FirstEntity(), db.Object);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Unrelate_IfSecondEntityDoesNotExist_Throws()
        {
            // Arrange
            var db = new Mock<IDb4ODatabaseContext>();
            db.Setup(x => x.Find<FirstEntity>(It.IsAny<object>())).Returns(new FirstEntity());
            var repository = new RepositoryStub(db.Object);

            // Act
            repository.Unrelate(new SecondEntity(), new FirstEntity(), db.Object);
        }


        public class FirstEntity : IEntity
        {
            public FirstEntity()
            {
                Seconds = new List<SecondEntity>();
            }


            public IList<SecondEntity> Seconds { get; set; }
        
            public string Id { get; set; }

            public string FullName { get; set; }
        }


        public class SecondEntity : IEntity
        {
            public FirstEntity One { get; set; }
        
            public string Id { get; set; }

            public string FullName { get; set; }
        }


        public class RepositoryStub : Db4ORepositoryBase<SecondEntity>, IRelatedRepository<SecondEntity, FirstEntity>
        {
            public RepositoryStub(IDb4ODatabaseContext context)
                : base(context)
            {
            }


            public void Relate(SecondEntity current, FirstEntity other)
            {
                current.One = other;
                other.Seconds.Add(current);
            }

            public void Unrelate(SecondEntity current, FirstEntity other)
            {
                current.One = null;
                other.Seconds.Remove(current);
            }

            public void SaveReference(SecondEntity current)
            {
                DatabaseContext.Store(current);
            }

            public void SaveReference(FirstEntity other)
            {
                DatabaseContext.Store(other.Seconds);
            }
        }
    }
}
