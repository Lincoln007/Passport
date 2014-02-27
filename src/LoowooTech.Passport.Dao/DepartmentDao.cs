using LoowooTech.Passport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Dao
{
    public class DepartmentDao : DaoBase
    {
        public List<Department> GetList()
        {
            using (var db = GetDataContext())
            {
                return db.Department.Where(e => e.Deleted == 0).ToList();
            }
        }

        public void Add(Department entity)
        {
            using (var db = GetDataContext())
            {
                db.Department.Add(entity);
                db.SaveChanges();
            }
        }
        public void Update(Department entity)
        {
            using (var db = GetDataContext())
            {
                var toUpdae = db.Department.FirstOrDefault(e => e.ID == entity.ID);
                if (toUpdae == null)
                {
                    throw new ArgumentException("departmentId参数错误");
                }
                db.Entry(toUpdae).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Department.FirstOrDefault(e => e.ID == id);
                if (entity == null)
                {
                    throw new ArgumentException("departmentId参数错误");
                }
                entity.Deleted = 1;
                db.SaveChanges();
            }
        }

        public Department GetModel(int id)
        {
            using (var db = GetDataContext())
            {
                return db.Department.FirstOrDefault(e => e.ID == id);
            }
        }
    }
}
