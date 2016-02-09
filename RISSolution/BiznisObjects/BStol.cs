using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BStol
    {
        public int id_stola { get; set; }
        public int pocet_miest { get; set; }

        public ICollection<BObjednavka> objednavka { get; set; }

        public stol entityStol { get; set; }

        public BStol()
        {
            this.Reset();
        }

        public BStol(stol s)
        {
            id_stola = s.id_stola;
            pocet_miest = s.pocet_miest;
            objednavka = new List<BObjednavka>();
            foreach (var objednavka1 in s.objednavka)
            {
                BObjednavka pom = new BObjednavka(objednavka1);
                objednavka.Add(pom);
            }
            entityStol = s;
        }

        private void Reset()
        {
            id_stola = 0;
            pocet_miest = 0;
            objednavka = new List<BObjednavka>();
            entityStol = new stol();
        }

        private void FillBObject()
        {
            id_stola = entityStol.id_stola;
            pocet_miest = entityStol.pocet_miest;
            objednavka = new List<BObjednavka>();
            foreach (var objednavka1 in entityStol.objednavka)
            {
                BObjednavka pom = new BObjednavka(objednavka1);
                objednavka.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityStol.id_stola = id_stola;
            entityStol.pocet_miest = pocet_miest;
            foreach (var objednavka1 in objednavka)
            {
                entityStol.objednavka.Add(objednavka1.entityObjednavka);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_stola == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.stol.Add(entityStol);
                    risContext.SaveChanges();
                    id_stola = entityStol.id_stola;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.stol where a.id_stola == id_stola select a;
                    entityStol = temp.Single();
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
                var temp = risContext.stol.First(i => i.id_stola == id_stola);
                risContext.stol.Remove(temp);
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

        public bool Get(risTabulky risContext, int id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.stol where a.id_stola == id select a;
                entityStol = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BStolCol : Dictionary<int, BStol>
        {

            public BStolCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.stol select a;
                    List<stol> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_stola, new BStol(a));
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
