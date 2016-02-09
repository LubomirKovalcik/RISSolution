using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;
using DataHolder;

namespace BiznisObjects
{
    /// <summary>
    /// Reprezentuje surovinu z ktorej sa skladajú jedla a nápoje
    /// </summary>
    public class BSurovina : TransferTemplate
    {


        private ICollection<BSurovinaJedla> jedlo_surovina
        {
            get
            {
                ICollection<BSurovinaJedla> jedlo_surovina_temp = new List<BSurovinaJedla>();
                foreach (var jedloSurovina in entitySurovina.jedlo_surovina)
                {
                    BSurovinaJedla pom = new BSurovinaJedla(jedloSurovina);
                    jedlo_surovina_temp.Add(pom);
                }
                return jedlo_surovina_temp;
            }
            set
            {
                this.jedlo_surovina = value;
                
            }
        }

        /// <summary>
        /// Jedla obsahujúce túto surovinu
        /// </summary>
        /// <value>
        /// Navrácia kolekciu jedál obsahujúcich túto surovinu
        /// </value>
        public ICollection<BJedlo> jedla_obsahujuce_surovinu
        {
            get
            {
                ICollection<BJedlo> jedla = new List<BJedlo>();
                foreach (var bjedlo_surovina in jedlo_surovina)
                {
                    jedla.Add(bjedlo_surovina.jedlo);
                }
                return jedla;

            }
        }

        /// <summary>
        /// Nápoje obsahujúce túto surovinu
        /// </summary>
        /// <value>
        /// Navrácia kolekciu nápojov obsahujúcich túto surovinu
        /// </value>
        public ICollection<BNapoj> napoje_obsahujuce_surovinu
        {
            get
            {
                ICollection<BNapoj> napoje = new List<BNapoj>();
                foreach (var bnapoj_surovina in napoj_surovina)
                {
                    napoje.Add(bnapoj_surovina.napoj);
                }
                return napoje;

            }
        }


        private ICollection<BNapoj_surovina> napoj_surovina
        {
            get
            {
                List<BNapoj_surovina> napoj_surovina_temp = new List<BNapoj_surovina>();
                foreach (var napojSurovina in entitySurovina.napoj_surovina)
                {
                    BNapoj_surovina pom = new BNapoj_surovina(napojSurovina);
                    napoj_surovina_temp.Add(pom);
                }
                return napoj_surovina_temp;
            }
            set
            {
                this.napoj_surovina = value;
                
            }
        }

        /// <summary>
        /// Datbazová entita prisluchájuca tejto surovine
        /// </summary>
        public surovina entitySurovina;

        /// <summary>
        /// Názov suroviny
        /// </summary>
        /// <value>
        ///   navrácia názov suroviny
        /// </value>
        public BText nazov {
            get
            {
                return new BText(entitySurovina.text);
            }
         }

        /// <summary>
        /// Vrácia id suroviny
        /// </summary>
        /// <value>
        ///  id suroviny
        /// </value>
        public int ID
        {
            get
            {
                return entitySurovina.id_surovina;
            }

            

        }

        /// <summary>
        /// Merná jednotka suroviny
        /// </summary>
        public string jednotka
        {
            get
            {
                return entitySurovina.jednotka;
                
            }
            set
            {
                entitySurovina.jednotka = value;
            }
        }

        /// <summary>
        /// Je surovina alergénom?
        /// </summary>
        /// <value>Alergén</value>
        public bool alergen
        {
            get
            {
                return entitySurovina.alergen == 1;
                
            }
            set
            {
                if (value)
                {
                    entitySurovina.alergen = 1;
                }
                else
                {
                    entitySurovina.alergen = 0;
                }
            }
        }

        


        /// <summary>
        /// Vytvorí novú suroviny bez žiadných informácii
        /// </summary>
        public BSurovina()
        {
            this.Reset();
        }


        public BSurovina(TSurovina surovina,risTabulky risContext)
        {
            BSurovina temp = Zoznamy.dajSurovinu(surovina.Id, risContext);
            if (temp!=null) entitySurovina = temp.entitySurovina;
            
        }

        /// <summary>
        /// Vytovrí suorvinu na základe údajov z databázy 
        /// </summary>
        /// <param name="s">Informácie o surovine v databáze</param>
        public BSurovina(surovina s)
        {
           
            entitySurovina = s;
        }


        /// <summary>
        /// Vytvorí surovinu s daným ID, ak sa taká nachádza uložená v databáze ,tak jej data naplní tými z databázy
        /// </summary>
        /// <param name="id_suroviny">ID suroviny</param>
        /// <param name="risContext">kontext databázy</param>
        public BSurovina(int id_suroviny, risTabulky risContext)
        {
        
            try
            {
                var temp = from a in risContext.surovina where a.id_surovina == id_suroviny select a;
                if (temp.Count() > 0)
                {
                    entitySurovina = temp.First();

                }
                else
                {
                    throw new ItemNotExistsExcpetion();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

           
        }

        private void Reset()
        {
            entitySurovina = null;

        }


        /// <summary>
        /// Metóda uloží aktuálny stav biznis objektu surovina do databázy
        /// </summary>
        /// <param name="risContext">Kontext databázy</param>
        /// <returns>
        ///    <c>TRUE</c> , ak bola zmena úspešne uložena
        ///    <c>FALSE</c> , ak nebola zmena úspešne uložená
        /// </returns>
        public bool Save(risTabulky risContext)
        {
            bool success = false;
            try
            {
                risContext.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Save()"), ex);
            }

            return success;
        }


        /// <summary>
        /// Metóda zmaže aktuálnu surovinu z databázy
        /// </summary>
        /// <param name="risContext">Kontext databázy</param>
        /// <returns>
        ///    <c>TRUE</c> , ak došlo k úspešnému vymazaniu
        ///    <c>FALSE</c> , ak nedošlo k úspešenému vymazaniu
        /// </returns>
        public bool Del(risTabulky risContext)
        {
            bool result = false;
            if (jedlo_surovina.Count > 0 || napoj_surovina.Count > 0)
            {
                entitySurovina.platna = 0;
                Save(risContext);
                result = true;
            }
            else
            {
                risContext.surovina.Remove(entitySurovina);
                risContext.SaveChanges();
                Reset();
                result = true;
            }
            return result;
        }

        public TransferEntity toTransferObject(string id_jazyka)
        {
            TSurovina transferSurovina=new TSurovina(ID,nazov.getPreklad(id_jazyka),alergen,jednotka);
            transferSurovina.Id_jazyka = id_jazyka;
            return transferSurovina;
        }

        public void updatefromTransferObject(TransferEntity transferEntity, risTabulky risContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Zoznam surovín
        /// </summary>
        public class BSurovinaCollection : Dictionary<int, BSurovina>, TransferTemplateList
        {
            /// <summary>
            /// Vytvorí nový zoznám surovín
            /// </summary>
            public BSurovinaCollection()
            {
            }

            /// <summary>
            /// Naplní zoznám surovín všetkými surovinamy v databáze
            /// </summary>
            /// <param name="risContext"></param>
            /// <returns>
            ///    <c>TRUE</c> , ak došlo k úspešnému načitaniu
            ///    <c>FALSE</c> , ak nedošlo k úspešenému načitaniu
            /// </returns>
            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.surovina select a;
                    List<surovina> tempList = temp.ToList();
                    this.Clear();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_surovina, new BSurovina(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// Naplní zoznam surovín všetkými surovinamy v databáze ,ktorých názvy začínajú na text ,ktorý je parametrom
            /// </summary>
            /// <param name="startingString">text, na ktorý majú začínať nazvy , aspoň 3 písmena</param>
            /// <param name="risContext">kontext databázy</param>
            /// <returns>
            ///    <c>TRUE</c> , ak došlo k úspešnému načitaniu
            ///    <c>FALSE</c> , ak nedošlo k úspešenému načitaniu
            /// </returns>
            public bool GetNameStartingWith(String startingString,risTabulky risContext)
            {
                try
                {
                    var temp = risContext.surovina.Where(x => x.platna == 1).Select(x => x.id_surovina);
                    var tempPreklady = from a in risContext.preklad where temp.Contains(a.text_id) && a.preklad1.StartsWith(startingString)  select a.text_id;
                    var suroviny = from a in risContext.surovina where tempPreklady.Contains(a.text.text_id) select a;


                    List < surovina > tempList = suroviny.ToList();
                    this.Clear();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_surovina, new BSurovina(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            ///    Konverzia do zoznamu prenosových entít
            /// </summary>
            /// <param name="id_jazyka">id_jazyka pre text v prenosovej entite</param>
            /// <returns>zoznam surovin ako prenosových entít</returns>
           public IList<TransferEntity> toTransferList(string id_jazyka)
            {
                List<TransferEntity> result=new List<TransferEntity>();
                foreach (var surovina in this)
                {
                    result.Add(surovina.Value.toTransferObject(id_jazyka));
                }
               return result;
            }
        }

    }
}
