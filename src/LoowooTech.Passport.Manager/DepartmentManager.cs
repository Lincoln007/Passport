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


        public List<Department> GetList()
        {
            return dao.GetList();
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

        public List<Department> GetTree(int parentId = 0)
        {
            var list = GetList();

            var roots = list.Where(e => e.ParentID == parentId).ToList();

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

        public IEnumerable<Department> GetAccountDepartments(Account account)
        {
            if (account.DepartmentId > 0)
            {
                yield return GetModel(account.DepartmentId);
            }
            if (account.AgentId > 0)
            {
                if (account.Agent.DepartmentId > 0)
                {
                    yield return GetModel(account.DepartmentId);
                }
            }
        }
    }
}
