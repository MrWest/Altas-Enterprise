using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Infrastructure.Data.Db4O
{
    [TestClass, ExcludeFromCodeCoverage]
    public class WorkForceRepositoryBaseTests : MockedTestBase<WorkForceRepository>
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");
        private Mock<IDb4ODatabaseContext> _db;
        private Mock<IWageScaleRepository> _wageScaleRepository;


        [TestInitialize]
        public override void Initialize()
        {
            _db = new Mock<IDb4ODatabaseContext>();
            _wageScaleRepository = new Mock<IWageScaleRepository>();

            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkForce>()).Returns(() => new WorkForceStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IWageScale>()).Returns(() => new WageScaleStub());
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
        }

        protected override void CreateMock()
        {
            TestMock = new Mock<WorkForceRepository>(_db.Object) { CallBase = true };
        }

        protected override void CreateInstance()
        {
            ServiceLocatorMock.Setup(x => x.GetInstance<IWageScaleRepository>()).Returns(_wageScaleRepository.Object);

            base.CreateInstance();
        }


        [TestMethod]
        public void RelevantProperties_ReturnsCorrectProperties()
        {
            // Act
            var actualProperties = GetProperty<string[]>(TestObject, "RelevantProperties");

            // Assert
            string[] expectedProperties =
            {
                ExtractPropertyName<IWorkForce, object>(x => x.Id),
                ExtractPropertyName<IWorkForce, string>(x => x.Name),
                ExtractPropertyName<IWorkForce, string>(x => x.Code)
            };
            CollectionAssert.AreEquivalent(expectedProperties, actualProperties);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullWorkForce_Throws()
        {
            // Act
            TestObject.Add(null);
        }

        [TestMethod]
        public void Add_GivenWorkForceWithNullWageScale_IncludesItInTheDatabaseWithANullWageScale()
        {
            // Arrange
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var workForce = new WorkForceStub
                {
                    Name = "WF",
                    Code = "1"
                };

                var repository = new WorkForceRepository(db);

                // Act
                repository.Add(workForce);

                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IWorkForce dbWorkForce = db.Query<IWorkForce>().Single();

                Assert.IsNotNull(dbWorkForce.Id);
                Assert.AreNotEqual(default(Guid), dbWorkForce.Id);
                Assert.AreEqual("WF", dbWorkForce.Name);
                Assert.AreEqual("1", dbWorkForce.Code);
                Assert.IsNull(dbWorkForce.WageScale);
            }
        }

        [TestMethod]
        public void Add_GivenWorkForceWithWageScale_IncludesItInTheDatabaseWithThatWageScale()
        {
            // Arrange
            var wageScale = new WageScaleStub { Id = Guid.NewGuid() };
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(wageScale);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var workForce = new WorkForceStub { WageScale = wageScale };
                var repository = new WorkForceRepository(db);
                repository.Add(workForce);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IWageScale dbWageScale = db.Query<IWageScale>().Single();
                IWorkForce dbWorkForce = db.Query<IWorkForce>().Single();

                Assert.AreSame(dbWageScale, dbWorkForce.WageScale);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Update_GivenNullWorkForce_Throws()
        {
            TestObject.Update(null);
        }

        [TestMethod]
        public void Update_GivenWorkForceWithWageScale_AndDatabaseVersionDoesNotHaveAOne_SetsTheGivenWageScaleToTheDatabaseVersion()
        {
            // Arrange
            var wageScale = new WageScaleStub { Id = Guid.NewGuid() };
            var workForce = new WorkForceStub { Id = Guid.NewGuid() };
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(wageScale);
                db.Store(workForce);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                workForce.WageScale = wageScale;
                var repository = new WorkForceRepository(db);
                repository.Update(workForce);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IWageScale dbWageScale = db.Query<IWageScale>().Single();
                IWorkForce dbWorkForce = db.Query<IWorkForce>().Single();

                Assert.AreSame(dbWageScale, dbWorkForce.WageScale);
            }
        }

        [TestMethod]
        public void Update_GivenWorkForceWithWageScale_AndDatabaseVersionHaveAOne_SetsTheNewGivenWageScaleToTheDatabaseVersion()
        {
            // Arrange
            var wageScale1 = new WageScaleStub { Id = Guid.NewGuid() };
            var wageScale2 = new WageScaleStub { Id = Guid.NewGuid() };
            var workForce = new WorkForceStub { Id = Guid.NewGuid(), WageScale = wageScale1 };
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(wageScale1);
                db.Store(wageScale2);
                db.Store(workForce);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                workForce.WageScale = wageScale2;
                var repository = new WorkForceRepository(db);
                repository.Update(workForce);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IWageScale dbWageScale1 = db.Query<IWageScale>().Single(x => Equals(x.Id, wageScale1.Id));
                IWageScale dbWageScale2 = db.Query<IWageScale>().Single(x => Equals(x.Id, wageScale2.Id));
                IWorkForce dbWorkForce = db.Query<IWorkForce>().Single();

                Assert.AreSame(dbWageScale2, dbWorkForce.WageScale);
                Assert.AreNotSame(dbWageScale1, dbWorkForce.WageScale);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullWorkForce_Throws()
        {
            TestObject.Relate(null, Mock.Of<IWageScale>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullWageScale_Throws()
        {
            TestObject.Relate(Mock.Of<IWorkForce>(), null);
        }

        [TestMethod]
        public void Relate_GivenBothCorrectArguments_RelatesThemCorrectly()
        {
            // Arrange
            var wageScale = Mock.Of<IWageScale>();
            var workForce = new Mock<IWorkForce>();

            // Act
            TestObject.Relate(workForce.Object, wageScale);

            // Assert
            workForce.VerifySet(x => x.WageScale = wageScale);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullWorkForce_Throws()
        {
            TestObject.Unrelate(null, Mock.Of<IWageScale>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullWageScale_Throws()
        {
            TestObject.Unrelate(Mock.Of<IWorkForce>(), null);
        }

        [TestMethod]
        public void Unrelate_GivenBothCorrectArguments_UnrelatesThemCorrectly()
        {
            // Arrange
            var wageScale = Mock.Of<IWageScale>();
            var workForce = new Mock<IWorkForce>();
            workForce.SetupProperty(x => x.WageScale);
            workForce.Object.WageScale = wageScale;

            // Act
            TestObject.Unrelate(workForce.Object, wageScale);

            // Assert
            workForce.VerifySet(x => x.WageScale = null);
            Assert.IsNull(workForce.Object.WageScale);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SaveReference_GivenNullWorkForce_Throws()
        {
            TestObject.SaveReference((IWorkForce)null);
        }

        [TestMethod]
        public void SaveReference_GivenWorkForce_DatabaseContextStoresIt()
        {
            // Arrange
            var workForce = Mock.Of<IWorkForce>();

            // Act
            TestObject.SaveReference(workForce);

            // Assert
            _db.Verify(x => x.Store(workForce));
        }


        [TestMethod]
        public void Clone_ReturnsAWorkForceCloneWithAlsoACloneOfItsWageScale()
        {
            // Arrange
            var wageScale = new WageScaleStub { Retribution = 56.0m, Name = "A", Id = Guid.NewGuid() };

            var wageScaleClone = new WageScaleStub { Retribution = 56.0m, Name = "A", Id = Guid.NewGuid() };
            var workForce = new WorkForceStub { WageScale = wageScale, Name = "B", Code = "C", Id = Guid.NewGuid() };
            
            _wageScaleRepository.Setup(x => x.Find(wageScale.Id)).Returns(wageScaleClone);

            // Act
            var workForceClone = (IWorkForce)TestObjectInternals.Invoke("Clone", workForce);

            // Assert
            Assert.AreNotSame(workForce, workForceClone);
            Assert.AreEqual(workForce.Id, workForceClone.Id);
            Assert.AreEqual(workForce.Name, workForceClone.Name);
            Assert.AreEqual(workForce.Code, workForceClone.Code);

            Assert.AreSame(wageScaleClone, workForceClone.WageScale);
        }
    }
}
