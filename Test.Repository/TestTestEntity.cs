using System;
using FluentAssertions;
using Gaapp;
using Gaapp.Models;
using Gaapp.Repository;
using NUnit.Framework;

namespace Test.Repository
{
    [TestFixture]
    public class TestTestEntity
    {
        [SetUp]
        public void Setup()
        {
            GaapRepository.ConnectionString = "data source=(local);Integrated Security=True;initial catalog=Gaapp_Test;";
        }

        [Test]
        public void InsertANewEntity()
        {
            var entity = new TestEntity {Name = "Name", Test1 = "Test1", Test2 = "Test2"};
            var id = TestRepository.SaveEntity( entity );
            id.Should().BeGreaterThan(0, "We expect a new Id value");
        }

        [Test]
        public void DeleteANewEntity()
        {
            var entity = new TestEntity { Name = "Name", Test1 = "Test1", Test2 = "Test2" };
            var id = TestRepository.SaveEntity(entity);
            id.Should().BeGreaterThan(0, "We expect a new Id value");

            var save = TestRepository.GetEntity(id);
            save.Id.Should().Be(entity.Id);
            save.Version.Should().Be(entity.Version);
            save.Name.Should().Be(entity.Name);
            save.Test1.Should().Be(entity.Test1);
            save.Test2.Should().Be(entity.Test2);

            TestRepository.DeleteEntity( save );

            // It may only succeed 1 time
            save.Invoking(s => TestRepository.DeleteEntity(save)).ShouldThrow<GaappDoesNotExistException>();

            // We may not retreive it from the database anymore
            save.Invoking(s => TestRepository.GetEntity(save.Id)).ShouldThrow<GaappDoesNotExistException>();
        }

        [Test]
        public void UpdateANewEntity()
        {
            var entity = new TestEntity { Name = "Name", Test1 = "Test1", Test2 = "Test2" };
            var id = TestRepository.SaveEntity(entity);
            id.Should().BeGreaterThan(0, "We expect a new Id value");

            var save = TestRepository.GetEntity(id);
            save.Id.Should().Be(entity.Id);
            save.Version.Should().Be(entity.Version);
            save.Name.Should().Be(entity.Name);
            save.Test1.Should().Be(entity.Test1);
            save.Test2.Should().Be(entity.Test2);

            var version = save.Version;
            save.Test1 = save.Test1 + ", " + save.Test2;
            TestRepository.SaveEntity( save );
            save.Version.Should().Be(version + 1);
            var save2 = TestRepository.GetEntity(id);
            save2.Id.Should().Be(save.Id);
            save2.Version.Should().Be(version+1);
            save2.Name.Should().Be(save.Name);
            save2.Test1.Should().Be(save.Test1);
            save2.Test2.Should().Be(save.Test2);

            // We must have a correct version number
            save2.Version += 10;
            save2.Invoking(s => TestRepository.SaveEntity(s)).ShouldThrow<GaappDoesNotExistException>();
        }

    }
}
