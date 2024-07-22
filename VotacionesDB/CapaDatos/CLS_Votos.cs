using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotacionesDB.CapaDatos
{
    public class CLS_Votos
    {
        // Variables de VOTOS
        public static int VotoId { get; set; }
        public static int VotanteId { get; set; }
        public static int CandidatoId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
    }
}