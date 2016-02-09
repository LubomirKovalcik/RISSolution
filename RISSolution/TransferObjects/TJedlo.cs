using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace DataHolder
{
    [DataContract]
    public class TJedlo : TransferEntity
    {
        int? id;
        String nazov;
        int id_typu;
        String nazov_typu;
        int? mnozstvo_kalorii;
        int? dlzka_pripravy;
        private ICollection<TSurovina> zoznam_surovin;
        private IDictionary<String, String> preklady; 

        public TJedlo(int id, string nazov, int idTypu, string nazovTypu)
        {
            this.id = id;
            this.nazov = nazov;
            id_typu = idTypu;
            nazov_typu = nazovTypu;
            zoznam_surovin = new List<TSurovina>();
        }

        

        public TJedlo(int id,string nazov, int idTypu, string nazovTypu,int? mnozstvoKalorii,int? dlzkaPripravy, ICollection<TSurovina> zoznamSurovin)
        {

            this.id = id;
            this.nazov = nazov;
            id_typu = idTypu;
            nazov_typu = nazovTypu;
            this.mnozstvo_kalorii = mnozstvoKalorii;
            this.dlzka_pripravy = dlzkaPripravy;
            this.zoznam_surovin = zoznamSurovin;
        }

        public TJedlo(int id, string nazov, int idTypu, string nazovTypu, int? mnozstvoKalorii, int? dlzkaPripravy, ICollection<TSurovina> zoznamSurovin, IDictionary<String, String> preklady)
        {

            this.id = id;
            this.nazov = nazov;
            id_typu = idTypu;
            nazov_typu = nazovTypu;
            this.mnozstvo_kalorii = mnozstvoKalorii;
            this.dlzka_pripravy = dlzkaPripravy;
            this.zoznam_surovin = zoznamSurovin;
            this.preklady = preklady;
        }


        public void PridajSurovinu(TSurovina surovina)
        {
            zoznam_surovin.Add(surovina);
        }

        public void PridajPreklad(String kodJazyka, String preklad)
        {
            preklady.Add(kodJazyka,preklad);
        }

        public TJedlo(int id)
        {
            this.id = id;
        }

        [DataMember]
        public int? Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Nazov
        {
            get { return nazov; }
            set { nazov = value; }
        }

        [DataMember]
        public int IdTypu
        {
            get { return id_typu; }
            set { id_typu = value; }
        }

        [DataMember]
        public string NazovTypu
        {
            get { return nazov_typu; }
            set { nazov_typu = value; }
        }

        [DataMember]
        public int? MnozstvoKalorii
        {
            get { return mnozstvo_kalorii; }
            set { mnozstvo_kalorii = value; }
        }

        [DataMember]
        public int? DlzkaPripravy
        {
            get { return dlzka_pripravy; }
            set { dlzka_pripravy = value; }
        }

        [DataMember]
        public ICollection<TSurovina> ZoznamSurovin
        {
            get { return zoznam_surovin; }
            set { zoznam_surovin = value; }
        }

        [DataMember]
        public IDictionary<string, string> Preklady
        {
            get { return preklady; }
            set { preklady = value; }
        }
    }
}
