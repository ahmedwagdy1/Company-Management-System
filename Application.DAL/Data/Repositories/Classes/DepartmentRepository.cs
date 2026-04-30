namespace Application.DAL.Data.Repositories.Classes
{
    // C# 12.0 .NET 8.0 Feature ==> Primary Constructor
    public class DepartmentRepository(ApplicationDbContext dbContext) : GenericRepository<Department>(dbContext), IDepartmentRepository
    {
        // Controller[PL]  ==> Service[BLL]  ==> Repository[DAL]  ==> DbContext[DAL] ==> Database[SQL Server]

        // 5 CRUD Operations
        // GetAll
        //public IEnumerable<Department> GetAll(bool WithTracking = false)
        //{
        //    if (WithTracking)
        //        return dbContext.Departments.ToList();
        //    else
        //        return dbContext.Departments.AsNoTracking().ToList();
        //}

        //// GetById
        //public Department? GetById(int id) => dbContext.Departments.Find(id);

        //// Add
        //public int Add(Department department)
        //{
        //    dbContext.Departments.Add(department);
        //    return dbContext.SaveChanges();
        //}

        //// Update
        //public int Update(Department department)
        //{
        //    dbContext.Departments.Update(department);
        //    return dbContext.SaveChanges();
        //}

        //// Delete
        //public int Delete(Department department)
        //{
        //    dbContext.Departments.Remove(department);
        //    return dbContext.SaveChanges();
        //}
    }
}
