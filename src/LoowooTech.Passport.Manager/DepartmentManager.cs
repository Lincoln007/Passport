using LoowooTech.Passport.Dao;
using LoowooTech.Passport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Manager
{
    public class DepartmentManager : ManagerBase
    {
        private DepartmentDao dao = new DepartmentDao();


        public List<Department> GetList(int clientId)
        {
            return dao.GetList(clientId);
        }

        private List<Department> GetChildren(List<Department> list, Department parent)
        {
            var children = list.Where(e => e.ParentID == parent.ID).ToList();
            if (children.Count == 0) return children;
            foreach (var item in children)
            {
                item.Children = GetChildren(list, item);
            }
            return children;
        }

        public List<Department> GetTree(int clientId)
        {
            var list = GetList(clientId);

            var roots = list.Where(e => e.ParentID == 0).ToList();

            foreach (var root in roots)
            {
                root.Children = GetChildren(list, root);
            }
            return roots;
        }

        public Department GetModel(int id)
        {
            if (id == 0) return null;
            return dao.GetModel(id);
        }

        public void Delete(int id)
        {
            if (id == 0) return;

            dao.Delete(id);
        }

        public void Save(Department model)
        {
            if (model == null)
            {
                throw new ArgumentException("department is null");
            }

            if (model.ID > 0)
            {
                dao.Update(model);
            }
            else
            {
                dao.Add(model);
            }
        }
    }
}
