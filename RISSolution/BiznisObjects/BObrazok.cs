using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BObrazok
    {
        public int id_obrazka { get; set; }
        public string metadata { get; set; }

        public ICollection<BAkcia> akcia { get; set; }
        public ICollection<BDenne_menu> denne_menu { get; set; }
        public ICollection<BMenu> menu { get; set; }

        public obrazok entityObrazok { get; set; }

        public BObrazok()
        {
            this.Reset();
        }

        public BObrazok(obrazok o)
        {
            id_obrazka = o.id_obrazka;
            metadata = o.metadata;

            akcia = new List<BAkcia>();
            foreach (var akcia1 in o.akcia)
            {
                BAkcia pom = new BAkcia(akcia1);
                akcia.Add(pom);
            }
            denne_menu = new List<BDenne_menu>();
            foreach (var denneMenu in o.denne_menu)
            {
                BDenne_menu pom = new BDenne_menu(denneMenu);
                denne_menu.Add(pom);
            }
            menu = new List<BMenu>();
            foreach (var menu1 in o.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }
            entityObrazok = o;
        }

        private void Reset()
        {
            id_obrazka = 0;
            metadata = "";

            akcia = new List<BAkcia>();
            denne_menu = new List<BDenne_menu>();
            menu = new List<BMenu>();

            entityObrazok = new obrazok();
        }

        private void FillBObject()
        {
            id_obrazka = entityObrazok.id_obrazka;
            metadata = entityObrazok.metadata;

            akcia = new List<BAkcia>();
            foreach (var akcia1 in entityObrazok.akcia)
            {
                BAkcia pom = new BAkcia(akcia1);
                akcia.Add(pom);
            }
            denne_menu = new List<BDenne_menu>();
            foreach (var denneMenu in entityObrazok.denne_menu)
            {
                BDenne_menu pom = new BDenne_menu(denneMenu);
                denne_menu.Add(pom);
            }
            menu = new List<BMenu>();
            foreach (var menu1 in entityObrazok.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityObrazok.id_obrazka = id_obrazka;
            entityObrazok.metadata = metadata;

            foreach (var akcia1 in akcia)
            {
                entityObrazok.akcia.Add(akcia1.entityAkcia);
            }
            foreach (var denneMenu in denne_menu)
            {
                entityObrazok.denne_menu.Add(denneMenu.entityDenneMenu);
            }
            foreach (var menu1 in menu)
            {
                entityObrazok.menu.Add(menu1.entityMenu);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_obrazka == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.obrazok.Add(entityObrazok);
                    risContext.SaveChanges();
                    id_obrazka = entityObrazok.id_obrazka;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.obrazok where a.id_obrazka == id_obrazka select a;
                    entityObrazok = temp.Single();
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
                var temp = risContext.obrazok.First(i => i.id_obrazka == id_obrazka);
                risContext.obrazok.Remove(temp);
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
                var temp = from a in risContext.obrazok where a.id_obrazka == id select a;
                entityObrazok = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BObrazokCol : Dictionary<int, BObrazok>
        {

            public BObrazokCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.obrazok select a;
                    List<obrazok> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_obrazka, new BObrazok(a));
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
