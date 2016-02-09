using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;


namespace BiznisObjects
{
    public class BDenne_menu
    {
        public DateTime datum_platnosti { get; set; }
        public int id_menu { get; set; }
        public int id_podniku { get; set; }
        public int id_obrazka { get; set; }
        public int cena { get; set; }

        public BMenu menu { get; set; }
        public BObrazok obrazok { get; set; }

        public denne_menu entityDenneMenu { get; set; }

        public BDenne_menu()
        {
            this.Reset();
        }

        public BDenne_menu(denne_menu dm)
        {
            datum_platnosti = dm.datum_platnosti;
            id_menu = dm.id_menu;
            id_podniku = dm.id_podniku;
            if (dm.id_obrazka != null) id_obrazka = (int) dm.id_obrazka;
            cena = dm.cena;
            menu = new BMenu(dm.menu);
            obrazok = new BObrazok(dm.obrazok);
            entityDenneMenu = dm;
        }

        private void Reset()
        {
            datum_platnosti = DateTime.MinValue;
            id_menu = 0;
            id_podniku = 0;
            id_obrazka = 0;
            cena = 0;
            menu = new BMenu();
            obrazok = new BObrazok();
            entityDenneMenu = new denne_menu();
        }

        private void FillBObject()
        {
            datum_platnosti = entityDenneMenu.datum_platnosti;
            id_menu = entityDenneMenu.id_menu;
            id_podniku = entityDenneMenu.id_podniku;
            if (entityDenneMenu.id_obrazka != null) id_obrazka = (int)entityDenneMenu.id_obrazka;
            cena = entityDenneMenu.cena;
            menu = new BMenu(entityDenneMenu.menu);
            obrazok = new BObrazok(entityDenneMenu.obrazok);
            entityDenneMenu = entityDenneMenu;
        }

        private void FillEntity()
        {
            entityDenneMenu.datum_platnosti = datum_platnosti;
            entityDenneMenu.id_menu = id_menu;
            entityDenneMenu.id_podniku = id_podniku;
            entityDenneMenu.id_obrazka = id_obrazka;
            entityDenneMenu.cena = cena;
            entityDenneMenu.menu = menu.entityMenu;
            entityDenneMenu.obrazok = obrazok.entityObrazok;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_menu == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.denne_menu.Add(entityDenneMenu);
                    risContext.SaveChanges();
                    id_menu = entityDenneMenu.id_menu;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.denne_menu where a.id_menu == id_menu select a;
                    entityDenneMenu = temp.Single();
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
                var temp = risContext.denne_menu.First(i => i.id_menu == id_menu);
                risContext.denne_menu.Remove(temp);
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
                var temp = from a in risContext.denne_menu where a.id_menu == id select a;
                entityDenneMenu = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BDenne_menuCol : Dictionary<int, BDenne_menu>
        {

            public BDenne_menuCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.denne_menu select a;
                    List<denne_menu> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_menu, new BDenne_menu(a));
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
