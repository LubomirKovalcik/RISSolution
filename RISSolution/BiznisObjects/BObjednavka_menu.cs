using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BObjednavka_menu
    {
        public int id_polozky { get; set; }
        public int id_menu { get; set; }
        public int id_objednavky { get; set; }
        public int id_podniku { get; set; }

        public BMenu menu { get; set; }
        public BObjednavka objednavka { get; set; }

        public objednavka_menu entityObjednavkaMenu { get; set; }

        public BObjednavka_menu()
        {
            this.Reset();
        }

        public BObjednavka_menu(objednavka_menu om)
        {
            id_polozky = om.id_polozky;
            id_menu = om.id_menu;
            id_objednavky = om.id_objednavky;
            id_podniku = om.id_podniku;

            menu = new BMenu(om.menu);
            objednavka = new BObjednavka(om.objednavka);

            entityObjednavkaMenu = om;
        }

        private void Reset()
        {
            id_polozky = 0;
            id_menu = 0;
            id_objednavky = 0;
            id_podniku = 0;

            menu = new BMenu();
            objednavka = new BObjednavka();

            entityObjednavkaMenu = new objednavka_menu();
        }

        private void FillBObject()
        {
            id_polozky = entityObjednavkaMenu.id_polozky;
            id_menu = entityObjednavkaMenu.id_menu;
            id_objednavky = entityObjednavkaMenu.id_objednavky;
            id_podniku = entityObjednavkaMenu.id_podniku;

            menu = new BMenu(entityObjednavkaMenu.menu);
            objednavka = new BObjednavka(entityObjednavkaMenu.objednavka);
        }

        private void FillEntity()
        {
            entityObjednavkaMenu.id_polozky = id_polozky;
            entityObjednavkaMenu.id_menu = id_menu;
            entityObjednavkaMenu.id_objednavky = id_objednavky;
            entityObjednavkaMenu.id_podniku = id_podniku;

            entityObjednavkaMenu.menu = menu.entityMenu;
            entityObjednavkaMenu.objednavka = objednavka.entityObjednavka;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = from a in risContext.objednavka_menu where a.id_objednavky == id_objednavky &&
                               a.id_menu == id_menu select a;

                if (!temp.Any()) // INSERT
                {
                    this.FillEntity();
                    risContext.objednavka_menu.Add(entityObjednavkaMenu);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    entityObjednavkaMenu = temp.Single();
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
                var temp = risContext.objednavka_menu.First(i => i.id_objednavky == id_objednavky && i.id_menu == id_menu);
                risContext.objednavka_menu.Remove(temp);
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

        public bool Get(risTabulky risContext, int idObjednavka, int idMenu)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.objednavka_menu where a.id_objednavky == idObjednavka &&
                           a.id_menu == idMenu select a;
                entityObjednavkaMenu = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BObjednavka_menuCol : Dictionary<string, BObjednavka_menu>
        {

            public BObjednavka_menuCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.objednavka_menu select a;
                    List<objednavka_menu> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_menu + "," + a.id_objednavky, new BObjednavka_menu(a));
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
