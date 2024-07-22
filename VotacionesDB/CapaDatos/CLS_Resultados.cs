using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotacionesDB.CapaDatos
{
    public class CLS_Resultados
    {
        // Variables de RESULTADOS
        public static int ResultadoId { get; set; }
        public static int candidatoId { get; set; }
        public static int VotosTotales { get; set; }
        public static decimal PorcentajeTotal { get; set; }
    }
}