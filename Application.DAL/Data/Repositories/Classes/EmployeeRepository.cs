namespace Application.DAL.Data.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext _dbContext) : GenericRepository<Employee>(_dbContext), IEmployeeRepository
    {
        //public IEnumerable<Employee> GetAll(bool WithTracking = false)
        //{
        //    if (WithTracking)
        //        return _dbContext.Employees.ToList();
        //    else
        //        return _dbContext.Employees.AsNoTracking().ToList();
        //}

        //public Employee? GetById(int id)
        //{
        //    return _dbContext.Employees.Find(id);
        //}

        //public int Add(Employee employee)
        //{
        //    _dbContext.Employees.Add(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _dbContext.Employees.Remove(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(Employee employee)
        //{
        //    _dbContext.Employees.Update(employee);
        //    return _dbContext.SaveChanges();
        //}
    }
}
