using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Dao;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Manager
{
    public class RankManager : ManagerBase
    {
        private RankDao Dao = new RankDao();
        public List<Rank> GetList()
        {
            return Dao.GetList();
        }

        public void Add(string name)
        {
            Dao.Add(new Rank { Name = name });
        }

        public void Update(Rank model)
        {
            Dao.Update(model);
        }

        public void Delete(int id)
        {
            Dao.Delete(id);
        }

        internal Rank GetModel(int id)
        {
            return Dao.GetModel(id);
        }
    }
}
