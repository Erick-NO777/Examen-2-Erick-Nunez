using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VotacionesDB.CapaDatos;

namespace VotacionesDB.CapaVistas
{
    public partial class Login : System.Web.UI.Page
    {
        // Método que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Maneja el evento de clic del botón de ingreso
        protected void bingresar_Click(object sender, EventArgs e)
        {
            int votanteId = Validarusuario(tnombre.Text, tcedula.Text); // Llama al método Validarusuario para obtener el ID

            if (votanteId != -1) // Verifica si el ID es válido
            {
                // Establecer el ID del votante en la sesión
                Session["VotanteID"] = votanteId;

                // Establecer el nombre del votante en la sesión
                Session["VotanteNombre"] = tnombre.Text; // O el nombre que obtengas de la base de datos

                // Redirigir a la página de inicio
                Response.Redirect("Inicio.aspx");
            }
            else
            {
                // Si no se encuentra el usuario, muestra un mensaje de error
                lerror.Text = "Nombre o Cedula incorrecta";
            }
        }

        // Método para validar el usuario en la base de datos
        protected int Validarusuario(string Nombre, string Cedula)
        {
            try
            {
                // Obtiene la cadena de conexión desde el archivo de configuración
                String s = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                // Usa la instrucción using para asegurar que la conexión se cierre automáticamente
                using (SqlConnection conexion = new SqlConnection(s))
                {
                    conexion.Open(); // Abre la conexión a la base de datos

                    // Usa la instrucción using para asegurar que el comando se libere automáticamente
                    using (SqlCommand comando = new SqlCommand("SELECT VOTANTE_ID FROM VOTANTES WHERE nombre = @Nombre AND cedula = @Cedula", conexion))
                    {
                        // Agrega los parámetros a la consulta para evitar la inyección SQL
                        comando.Parameters.AddWithValue("@Nombre", Nombre);
                        comando.Parameters.AddWithValue("@Cedula", Cedula);

                        // Usa la instrucción using para asegurar que el lector de datos se cierre automáticamente
                        using (SqlDataReader registro = comando.ExecuteReader())
                        {
                            // Verifica si se encontró algún registro que coincida con los parámetros
                            if (registro.Read())
                            {
                                // Retorna el ID del votante si se encuentra
                                return Convert.ToInt32(registro["VOTANTE_ID"]);
                            }
                            else
                            {
                                // Retorna -1 si el usuario no es encontrado
                                return -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Captura y muestra cualquier excepción que ocurra durante el proceso
                lerror.Text = "Ocurrió un error: " + ex.Message;
                return -1;
            }
        }

    }
}
