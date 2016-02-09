using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BTyp_napoja
    {
        public int id_typu { get; set; }
        public int text_id { get; set; }

        public BText text { get; set; }
        public ICollection<BNapoj> napoj { get; set; }

        public typ_napoja entityTypNapoja { get; set; }

        public BTyp_napoja()
        {
            this.Reset();
        }

        public BTyp_napoja(typ_napoja tn)
        {
            id_typu = tn.id_typu;
            text_id = tn.text_id;
            text = new BText(tn.text);

            napoj = new List<BNapoj>();
            foreach (var napoj1 in tn.napoj)
            {
                BNapoj pom = new BNapoj(napoj1);
                napoj.Add(pom);
            }

            entityTypNapoja = tn;
        }

        private void Reset()
        {
            id_typu = 0;
            text_id = 0;
            text = new BText();

            napoj = new List<BNapoj>();

            entityTypNapoja = new typ_napoja();
        }

        private void FillBObject()
        {
            id_typu = entityTypNapoja.id_typu;
            text_id = entityTypNapoja.text_id;
            text = new BText(entityTypNapoja.text);

            napoj = new List<BNapoj>();
            foreach (var napoj1 in entityTypNapoja.napoj)
            {
                BNapoj pom = new BNapoj(napoj1);
                napoj.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityTypNapoja.id_typu = id_typu;
            entityTypNapoja.text_id = text_id;
            entityTypNapoja.text = text.entityText;

            foreach (var napoj1 in napoj)
            {
                entityTypNapoja.napoj.Add(napoj1.entityNapoj);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_typu == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.typ_napoja.Add(entityTypNapoja);
                    risContext.SaveChanges();
                    id_typu = entityTypNapoja.id_typu;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.typ_napoja where a.id_typu == id_typu select a;
                    entityTypNapoja = temp.Single();
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
                var temp = risContext.typ_napoja.First(i => i.id_typu == id_typu);
                risContext.typ_napoja.Remove(temp);
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
                var temp = from a in risContext.typ_napoja where a.id_typu == id select a;
                entityTypNapoja = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BTyp_napojaCol : Dictionary<int, BTyp_napoja>
        {

            public BTyp_napojaCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.typ_napoja select a;
                    List<typ_napoja> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_typu, new BTyp_napoja(a));
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
