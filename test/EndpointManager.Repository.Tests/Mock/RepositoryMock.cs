using EndpointManager.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace EndpointManager.Repository.Tests.Mock
{
    public class RepositoryMock<TRepository, TFixture> : BaseMock where TRepository : class
    {
        private readonly TRepository _repository;
        protected readonly TFixture Fixture;

        public TRepository GetRepository() => _repository;
        public Mock<IDbContext> GetContext() => AutoMocker.GetMock<IDbContext>();

        protected RepositoryMock(TFixture fixture)
        {
            _repository = AutoMocker.CreateInstance<TRepository>();
            Fixture = fixture;
        }
        public Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return dbSetMock;

        }
    }
}