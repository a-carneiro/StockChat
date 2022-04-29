namespace EndpointManager.Application.Tests.Mock
{
    public class ApplicationMock<TRepository, TFixture> : BaseMock where TRepository : class
    {
        private readonly TRepository _repository;
        protected readonly TFixture Fixture;

        public TRepository GetRepository() => _repository;
        protected ApplicationMock(TFixture fixture)
        {
            _repository = AutoMocker.CreateInstance<TRepository>();
            Fixture = fixture;
        }
    }
}
