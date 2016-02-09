using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BNapoj_surovina
    {
        public int id_surovina { get; set; }
        public int id_napoja { get; set; }
        public int mnozstvo { get; set; }

        public BNapoj napoj { get; set; }
        public BSurovina surovina { get; set; }

        public napoj_surovina entityNapojSurovina;

        public BNapoj_surovina(napoj_surovina ns)
        {
            id_surovina = ns.id_surovina;
            id_napoja = ns.id_napoja;
            mnozstvo = ns.mnozstvo;

            napoj = new BNapoj(ns.napoj);
            surovina = new BSurovina(ns.surovina);

            entityNapojSurovina = ns;
        }
    }
}
