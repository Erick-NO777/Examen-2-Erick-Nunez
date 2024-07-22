using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace VotacionesDB.CapaVistas
{
    public partial class Resultados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar los resultados cuando la página se carga por primera vez
                CargarResultados();
            }
        }

        private void CargarResultados()
        {
            try
            {
                // Obtener cadena de conexión desde el archivo de configuración
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Consulta para obtener los resultados de los candidatos
                    string resultadosQuery = @"
                    SELECT C.CANDIDATO_ID, C.NOMBRE AS Nombre, C.PARTIDO, R.VOTOS_TOTALES AS Votos, R.PORCENTAJE_TOTAL AS Porcentaje
                    FROM RESULTADOS R
                    JOIN CANDIDATOS C ON R.CANDIDATO_ID = C.CANDIDATO_ID
                    ORDER BY R.VOTOS_TOTALES DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(resultadosQuery, conexion);
                    DataTable resultadosTable = new DataTable();
                    adapter.Fill(resultadosTable);

                    // Mostrar los resultados en el GridView
                    gvResultados.DataSource = resultadosTable;
                    gvResultados.DataBind();

                    // Determinar y mostrar el ganador
                    if (resultadosTable.Rows.Count > 0)
                    {
                        DataRow ganador = resultadosTable.Rows[0];
                        lganador.Text = $"{ganador["Nombre"]} ({ganador["Partido"]}) con {ganador["Votos"]} votos ({ganador["Porcentaje"]}%)";
                    }
                    else
                    {
                        lganador.Text = "No hay votos registrados.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje de error en la página
                lganador.Text = "Ocurrió un error al cargar los resultados: " + ex.Message;
            }
        }
    }
}
