using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataHolder
{
    /// <summary>
    /// Prenosová entita surovina
    /// </summary>
    [DataContract]
    public class TSurovina:TransferEntity
    {
        int id;
        String nazov;
        bool alergen;
        String jednotka;

        double mnozstvo;

        /// <summary>
        /// Vytvorí novú prenosovú entitu surovinu
        /// </summary>
        /// <param name="id">id suroviny</param>
        /// <param name="nazov">názov suroviny vo vybranom jazyku</param>
        /// <param name="alergen">je surovina alergen</param>
        /// <param name="jednotka">merná jednotka suroviny</param>
        public TSurovina(int id, string nazov, bool alergen, string jednotka)
        {
            this.id = id;
            this.nazov = nazov;
            this.alergen = alergen;
            this.jednotka = jednotka;
        }


        public TSurovina(int id,double mnozstvo)
        {
            this.id = id;
            this.mnozstvo = mnozstvo;
        }

        [DataMember]
        public int Id
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
        public bool Alergen
        {
            get { return alergen; }
            set { alergen = value; }
        }

        [DataMember]
        public string Jednotka
        {
            get { return jednotka; }
            set { jednotka = value; }
        }


        public double Mnozstvo
        {
            get { return mnozstvo; }
            set { mnozstvo = value; }
        }
    }
}
