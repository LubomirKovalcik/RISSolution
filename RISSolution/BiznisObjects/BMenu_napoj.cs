using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BMenu_napoj
    {
        public int id_napoja { get; set; }
        public int id_menu { get; set; }
        public double cena { get; set; }
        public double mnozstvo { get; set; }
        public int id_podniku { get; set; }

        public BMenu menu { get; set; }
        public BNapoj napoj { get; set; }

        public menu_napoj entityMenuNapoj { get; set; }

        public BMenu_napoj(menu_napoj mn)
        {
            id_napoja = mn.id_napoja;
            id_menu = mn.id_menu;
            cena = mn.cena;
            mnozstvo = mn.mnozstvo;
            id_podniku = mn.id_podniku;

            napoj = new BNapoj(mn.napoj);
            menu = new BMenu(mn.menu);

            entityMenuNapoj = mn;
        }

        private void Reset()
        {
            id_napoja = 0;
            id_menu = 0;
            cena = 0;
            mnozstvo = 0; 
            id_podniku = 0;

            napoj = new BNapoj(new napoj());
            menu = new BMenu();

            entityMenuNapoj = new menu_napoj();
        }

        private void FillBObject()
        {
            id_napoja = entityMenuNapoj.id_napoja;
            id_menu = entityMenuNapoj.id_menu;
            cena = entityMenuNapoj.cena;
            mnozstvo = entityMenuNapoj.mnozstvo;
            id_podniku = entityMenuNapoj.id_podniku;

            napoj = new BNapoj(entityMenuNapoj.napoj);
            menu = new BMenu(entityMenuNapoj.menu);
        }

        private void FillEntity()
        {
            entityMenuNapoj.id_napoja = id_napoja;
            entityMenuNapoj.id_menu = id_menu;
            entityMenuNapoj.cena = cena;
            entityMenuNapoj.mnozstvo = mnozstvo;
            entityMenuNapoj.id_podniku = id_podniku;

            entityMenuNapoj.napoj = napoj.entityNapoj;
            entityMenuNapoj.menu = menu.entityMenu;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = from a in risContext.menu_napoj where a.id_menu == id_menu &&
                               a.id_napoja == id_napoja select a;

                if (!temp.Any()) // INSERT
                {
                    this.FillEntity();
                    risContext.menu_napoj.Add(entityMenuNapoj);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    entityMenuNapoj = temp.Single();
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
                var temp = risContext.menu_napoj.First(i => i.id_menu == id_menu && i.id_napoja == id_napoja);
                risContext.menu_napoj.Remove(temp);
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

        public bool Get(risTabulky risContext, int idMenu, int idNapoj)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.menu_napoj where a.id_menu == idMenu &&
                           a.id_napoja == idNapoj select a;
                entityMenuNapoj = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BMenu_napojCol : Dictionary<string, BMenu_napoj>
        {

            public BMenu_napojCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.menu_napoj select a;
                    List<menu_napoj> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_menu + "," + a.id_napoja, new BMenu_napoj(a));
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
