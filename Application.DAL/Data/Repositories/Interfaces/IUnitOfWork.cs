namespace Application.DAL.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository employeeRepository { get; }
        public IDepartmentRepository departmentRepository { get; }
        public Task<int> SaveChangesAsync();
    }
}
