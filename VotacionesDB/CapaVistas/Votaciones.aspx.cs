using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace VotacionesDB.CapaVistas
{
    public partial class Votaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar la lista de candidatos cuando la página se carga por primera vez
                CargarCandidatos();
            }
        }

        private void CargarCandidatos()
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection conexion = new SqlConnection(s))
            {
                string query = "SELECT CANDIDATO_ID, NOMBRE FROM CANDIDATOS";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        // Configurar el DropDownList con los candidatos
                        ddlCandidatos.DataSource = reader;
                        ddlCandidatos.DataTextField = "NOMBRE";
                        ddlCandidatos.DataValueField = "CANDIDATO_ID";
                        ddlCandidatos.DataBind();
                    }
                }
            }
        }

        protected void btnVotar_Click(object sender, EventArgs e)
        {
            int candidatoId = int.Parse(ddlCandidatos.SelectedValue);
            int votanteId = ObtenerVotanteId(); // Obtener el ID del votante actual
            DateTime fechaHora = DateTime.Now;

            string s = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection conexion = new SqlConnection(s))
            {
                string query = "SELECT COUNT(*) FROM VOTOS WHERE VOTANTE_ID = @VotanteId AND CANDIDATO_ID = @CandidatoId";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@VotanteId", votanteId);
                    comando.Parameters.AddWithValue("@CandidatoId", candidatoId);

                    conexion.Open();
                    int votoExistente = (int)comando.ExecuteScalar();

                    if (votoExistente > 0)
                    {
                        // Mostrar mensaje si el votante ya ha votado por este candidato
                        lblMensaje.Text = "Ya has votado por este candidato.";
                    }
                    else
                    {
                        // Insertar el nuevo voto
                        query = "INSERT INTO VOTOS (VOTANTE_ID, CANDIDATO_ID, FECHA, HORA) VALUES (@VotanteId, @CandidatoId, @Fecha, @Hora)";
                        using (SqlCommand insertComando = new SqlCommand(query, conexion))
                        {
                            insertComando.Parameters.AddWithValue("@VotanteId", votanteId);
                            insertComando.Parameters.AddWithValue("@CandidatoId", candidatoId);
                            insertComando.Parameters.AddWithValue("@Fecha", fechaHora.Date);
                            insertComando.Parameters.AddWithValue("@Hora", fechaHora.TimeOfDay);

                            try
                            {
                                insertComando.ExecuteNonQuery();
                                lblMensaje.Text = "¡Voto registrado con éxito!";

                                // Actualizar resultados después de registrar el voto
                                ActualizarResultados();
                            }
                            catch (Exception ex)
                            {
                                lblMensaje.Text = "Error al registrar el voto: " + ex.Message;
                            }
                        }
                    }
                }
            }
        }

        private void ActualizarResultados()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // Actualizar el conteo de votos totales para cada candidato
                    string updateVotosQuery = @"
                        UPDATE RESULTADOS
                        SET VOTOS_TOTALES = (
                            SELECT COUNT(*)
                            FROM VOTOS
                            WHERE CANDIDATO_ID = RESULTADOS.CANDIDATO_ID
                        )";

                    using (SqlCommand comando = new SqlCommand(updateVotosQuery, conexion))
                    {
                        int rowsAffected = comando.ExecuteNonQuery();
                        Console.WriteLine($"VOTOS_TOTALES actualizados para {rowsAffected} filas.");
                    }

                    // Calcular el porcentaje de votos para cada candidato
                    string updatePorcentajeQuery = @"
                        DECLARE @TotalVotos INT;
                        SELECT @TotalVotos = COUNT(*) FROM VOTOS;

                        UPDATE RESULTADOS
                        SET PORCENTAJE_TOTAL = 
                        CASE 
                            WHEN @TotalVotos > 0 THEN (VOTOS_TOTALES * 100.0 / @TotalVotos)
                            ELSE 0
                        END";

                    using (SqlCommand comando = new SqlCommand(updatePorcentajeQuery, conexion))
                    {
                        int rowsAffected = comando.ExecuteNonQuery();
                        Console.WriteLine($"PORCENTAJE_TOTAL actualizados para {rowsAffected} filas.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar los resultados: " + ex.Message);
                }
            }
        }

        private int ObtenerVotanteId()
        {
            // Obtener el ID del votante desde la sesión
            if (Session["VotanteID"] != null)
            {
                return (int)Session["VotanteID"];
            }
            else
            {
                // Lanzar excepción si el ID del votante no está en la sesión
                throw new InvalidOperationException("No se ha encontrado el ID del votante en la sesión.");
            }
        }
    }
}
