//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace di.proyecto.clase.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class tipossw
    {
        public short idTipos { get; set; }
        public string Nom_Programa { get; set; }
        public string Version { get; set; }
        public short incidencias_idincidencias { get; set; }
    
        public virtual incidencias incidencias { get; set; }
    }
}
