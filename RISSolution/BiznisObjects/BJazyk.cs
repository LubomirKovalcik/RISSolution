using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BJazyk
    {
        public string kod_jazyka { get; set; }
        public string nazov { get; set; }

        public ICollection<BPreklad> preklad { get; set; }

        public jazyk entityJazyk { get; set; }

        public BJazyk()
        {
            this.Reset();
        }

        public BJazyk(jazyk j)
        {
            kod_jazyka = j.kod_jazyka;
            nazov = j.nazov;

            preklad = new List<BPreklad>();
     

            entityJazyk = j;
        }

        private void Reset()
        {
            kod_jazyka = "";
            nazov = "";
            preklad = new List<BPreklad>();

            entityJazyk = new jazyk();
        }

        private void FillBObject()
        {
            kod_jazyka = entityJazyk.kod_jazyka;
            nazov = entityJazyk.nazov;

            preklad = new List<BPreklad>();
            foreach (var p in entityJazyk.preklad)
            {
                BPreklad pom = new BPreklad(p);
                preklad.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityJazyk.kod_jazyka = kod_jazyka;
            entityJazyk.nazov = nazov;
            foreach (var p in preklad)
            {
                entityJazyk.preklad.Add(p.entityPreklad);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (kod_jazyka == "") // INSERT
                {
                    this.FillEntity();
                    risContext.jazyk.Add(entityJazyk);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.jazyk where a.kod_jazyka == kod_jazyka select a;
                    entityJazyk = temp.Single();
                    this.FillEntity();
                    risContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Save()"), ex);
            }

            return success;
        }

        public bool Del(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = risContext.jazyk.First(i => i.kod_jazyka == kod_jazyka);
                risContext.jazyk.Remove(temp);
                risContext.SaveChanges();
                Reset();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Del()"), ex);
            }

            return success;
        }

        public bool Get(risTabulky risContext, string kod)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.jazyk where a.kod_jazyka == kod select a;
                entityJazyk = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BJazykCol : Dictionary<string, BJazyk>
        {

            public BJazykCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.jazyk select a;
                    List<jazyk> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.kod_jazyka, new BJazyk(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }
    }
}
