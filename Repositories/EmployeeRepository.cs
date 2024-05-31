using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repositories
{
    public class EmployeeRepository:IRepository<Employee,int>
    {
        EmployeeDbContext dbContext = new EmployeeDbContext();

        public void AddNew(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Employee FindById(int id)
        {
            return dbContext.Employees
                //.AsNoTracking()
                .FirstOrDefault(c=>c.EmployeeId == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return dbContext.Employees
                //.AsNoTracking()
                .ToList();
        }
        public IEnumerable<Employee> GetByCriteria(string filtercriteria)
        {
            return null;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            var Employees = GetAll();
            foreach(var Employee in Employees)
            {
                if (Employee.EmployeeId == id)
                {
                    //dbContext.Remove(Employee);
                    //dbContext.SaveChanges();
                    dbContext.Employees
                        .Where(c => c.EmployeeId == id)
                        .ExecuteDelete();
                    
                }
            }
            Console.WriteLine("Employee Not Found");
            return;
        }

        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Upsert(Employee entity)
        {
            var emp = dbContext.Employees.Find(entity.EmployeeId);
            dbContext.ChangeTracker.Clear();
            dbContext.ChangeTracker.DetectChanges();
            if (emp is null)
            {
                dbContext.Employees.Add(entity); //dbContext.Entity(entity).State = EntityState.Added;
            }
            else
            {
                //dbContext.Entry(entity).State = EntityState.Modified;
                //dbContext.Employees.Update(entity);

                //Directly execute the UPDATE Statement, does not involve the ChangeTracking Mechanism
                dbContext.Employees
                    .Where(c => c.EmployeeId == entity.EmployeeId)
                    .ExecuteUpdate(setters =>
                        setters.SetProperty(p => p.FirstName, entity.FirstName)
                        .SetProperty(p => p.LastName, entity.LastName)
                        .SetProperty(p => p.HireDate, entity.HireDate)
                    );
            }
            dbContext.SaveChanges();
        }
    }
}
