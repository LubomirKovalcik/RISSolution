//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class den_v_tyzdni
    {
        public den_v_tyzdni()
        {
            this.otvaracie_hodiny = new HashSet<otvaracie_hodiny>();
            this.zmena_otvaracich_hodin = new HashSet<zmena_otvaracich_hodin>();
        }
    
        public int cislo_dna { get; set; }
        public int text_id { get; set; }
    
        public virtual ICollection<otvaracie_hodiny> otvaracie_hodiny { get; set; }
        public virtual ICollection<zmena_otvaracich_hodin> zmena_otvaracich_hodin { get; set; }
        public virtual text text { get; set; }
    }
}
