using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace VotacionesDB.CapaVistas
{
    public partial class Candidatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar datos en el GridView si es la primera carga de la página
                LlenarGrid();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Recuperar datos del formulario y guardar el candidato
            string nombre = txtNombre.Text;
            string partido = txtPartido.Text;
            string propuesta = txtPropuesta.Text;

            GuardarCandidato(nombre, partido, propuesta);
        }

        private void GuardarCandidato(string nombre, string partido, string propuesta)
        {
            try
            {
                // Obtener cadena de conexión y abrir la conexión
                string s = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(s))
                {
                    conexion.Open();
                    string query = "INSERT INTO candidatos (nombre, partido, propuesta) VALUES (@Nombre, @Partido, @Propuesta)";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        // Añadir parámetros y ejecutar el comando
                        comando.Parameters.AddWithValue("@Nombre", nombre);
                        comando.Parameters.AddWithValue("@Partido", partido);
                        comando.Parameters.AddWithValue("@Propuesta", propuesta);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        // Mostrar mensaje de éxito o error
                        Response.Write(filasAfectadas > 0 ? "Candidato guardado con éxito." : "Error al guardar el candidato.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Response.Write("Ocurrió un error: " + ex.Message);
            }
        }

        protected void LlenarGrid()
        {
            // Obtener datos de la base de datos y llenar el GridView
            string constr = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM candidatos"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
}
