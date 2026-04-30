namespace Application.DAL.Data.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        public UnitOfWork(ApplicationDbContext _DbContext)
        {
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_DbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_DbContext));
            this._DbContext = _DbContext;
        }
        public IEmployeeRepository employeeRepository => _employeeRepository.Value;
        public IDepartmentRepository departmentRepository => _departmentRepository.Value;

        public async ValueTask DisposeAsync()
        {
            await _DbContext.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _DbContext.SaveChangesAsync();
        }
    }
}
