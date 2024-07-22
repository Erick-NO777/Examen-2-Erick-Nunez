using System;
using System.Web.UI;

namespace VotacionesDB.CapaVistas
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si la página se está cargando por primera vez y no como resultado de un postback
            if (!IsPostBack)
            {
                // Leer el nombre del usuario desde la sesión
                string nombreUsuario = Session["VotanteNombre"] as string;

                // Verificar si el nombre del usuario no es nulo ni vacío
                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    // Si el nombre del usuario está presente, mostrar un mensaje de bienvenida personalizado
                    lbienvenido.Text = "Bienvenido/a al sistema de votaciones " + nombreUsuario;
                }
                else
                {
                    // Si el nombre del usuario no está presente, mostrar un mensaje de bienvenida genérico
                    lbienvenido.Text = "Bienvenido/a al sistema de votaciones";
                }
            }
        }
    }
}
