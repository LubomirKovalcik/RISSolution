using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BPlatnost_zaznamu
    {
        public int typ_platnosti { get; set; }

        public ICollection<BMenu> menu { get; set; }

        public platnost_zaznamu entityPlatnostZaznamu { get; set; }

        public BPlatnost_zaznamu()
        {
            this.Reset();
        }

        public BPlatnost_zaznamu(platnost_zaznamu pz)
        {
            typ_platnosti = pz.typ_platnosti;
            menu = new List<BMenu>();
            foreach (var menu1 in pz.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }

            entityPlatnostZaznamu = pz;
        }

        private void Reset()
        {
            typ_platnosti = 0;
            menu = new List<BMenu>();

            entityPlatnostZaznamu = new platnost_zaznamu();
        }

        private void FillBObject()
        {
            typ_platnosti = entityPlatnostZaznamu.typ_platnosti;
            menu = new List<BMenu>();
            foreach (var menu1 in entityPlatnostZaznamu.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityPlatnostZaznamu.typ_platnosti = typ_platnosti;
            foreach (var menu1 in menu)
            {
                entityPlatnostZaznamu.menu.Add(menu1.entityMenu);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (typ_platnosti == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.platnost_zaznamu.Add(entityPlatnostZaznamu);
                    risContext.SaveChanges();
                    typ_platnosti = entityPlatnostZaznamu.typ_platnosti;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.platnost_zaznamu where a.typ_platnosti == typ_platnosti select a;
                    entityPlatnostZaznamu = temp.Single();
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
                var temp = risContext.platnost_zaznamu.First(i => i.typ_platnosti == typ_platnosti);
                risContext.platnost_zaznamu.Remove(temp);
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

        public bool Get(risTabulky risContext, int typ)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.platnost_zaznamu where a.typ_platnosti == typ select a;
                entityPlatnostZaznamu = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BPlatnost_zaznamuCol : Dictionary<int, BPlatnost_zaznamu>
        {

            public BPlatnost_zaznamuCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.platnost_zaznamu select a;
                    List<platnost_zaznamu> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.typ_platnosti, new BPlatnost_zaznamu(a));
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
