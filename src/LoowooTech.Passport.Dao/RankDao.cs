using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class RankDao : DaoBase
    {
        public List<Rank> GetList()
        {
            using (var db = GetDataContext())
            {
                return db.Rank.Where(e => e.Deleted == 0).ToList();
            }
        }

        public void Add(Rank model)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Rank.FirstOrDefault(e => e.Name.ToLower() == model.Name.ToLower());
                if (entity != null)
                {
                    if (entity.Deleted == 1)
                    {
                        entity.Deleted = 0;
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.Rank.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public void Update(Rank model)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Rank.FirstOrDefault(e => e.ID == model.ID);
                if (entity == null)
                {
                    return;
                }
                db.Entry(entity).CurrentValues.SetValues(model);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Rank.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    entity.Deleted = 1;
                    db.SaveChanges();
                }
            }
        }

        public Rank GetModel(int id)
        {
            using (var db = GetDataContext())
            {
                return db.Rank.FirstOrDefault(e => e.ID == id);
            }
        }
    }
}
