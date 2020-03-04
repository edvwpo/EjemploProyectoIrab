using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DataAccess
{
    /// <summary>
    /// Clase para Conectar a la base de datos especificada y obtener informacion
    /// </summary>
    class Conectar
    {
        private static string sql { get; set; }
        private static string tabla = "Pacientes2020";
        private static List<string> columnas = new List<string>();
        public static void setearSqlString (string con)
        {
            if (con != null)
                sql = con;
        }

        /// <summary>
        /// Llena el dataset ingresado por parametro con la base de datos seteada en ModData
        /// </summary>
        /// <param name="data">DataSet el cual va a llenar</param>
        public static void Llenar(DataSet data)
        {
            SqlConnection sqlCon = new SqlConnection(sql);
            SqlCommand cmd = new SqlCommand("SelectPacientes2020", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            SqlCommandBuilder scb = new SqlCommandBuilder(adap);
            try
            {
                sqlCon.Open();
                adap.FillSchema(data,SchemaType.Mapped, tabla);
                adap.Fill(data.Tables[tabla]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al rellenar DataSet!" + e.Message);
                
                throw;
            }
            finally
            {
                sqlCon.Close();
            }
        }
        /// <summary>
        /// Conecta a la base de datos y utiliza DataAdapter.Update()
        /// </summary>
        /// <param name="data">DataSet para el update</param>
        public static void Actualizar(DataSet data)
        {
            SqlConnection sqlCon = new SqlConnection(sql);
            SqlCommand cmd = new SqlCommand("", sqlCon);
            
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            SqlCommandBuilder scb = new SqlCommandBuilder(adap);

            try
            {
                
                sqlCon.Open();
                adap.Update(data.Tables[tabla]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al Actualizar DB" + e.Message);
                throw;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        /// <summary>
        /// Insertar paciente en base de datos
        /// </summary>
        /// <param name="propiedades">Lista de propiedades obtenidas de la clase Pacientes</param>
        /// <param name="fechas">Lista de fechas obtenidas de la clase Pacientes</param>
        public static void Ingresar(List<string> propiedades, List<DateTime> fechas )
        {
            SqlConnection sqlCon = new SqlConnection(sql);
            SqlCommand cmd = new SqlCommand("InsertarPaciente", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            listarColumnas();
            int propiedad = 0;
            int fecha = 0;
            for (int x = 0; x < (propiedades.Count + fechas.Count); x++)
            {
                if (x < propiedades.Count)
                {

                        cmd.Parameters.AddWithValue(columnas[x], propiedades[propiedad]);
                        propiedad++;
                }
                else
                {
                    cmd.Parameters.AddWithValue(columnas[x], fechas[fecha]);
                    fecha++;
                }
            }
            try
            {
                sqlCon.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al ejecutar cmd!!" + e.Message);
                throw;
            }
            finally
            {
                sqlCon.Close();
            }
            
        }
        public static void Borrar(int ID)
        {
            SqlConnection sqlCon = new SqlConnection(sql);
            SqlCommand cmd = new SqlCommand($"DELETE FROM {tabla} where ID = {ID}", sqlCon);
            sqlCon.Open();
            cmd.ExecuteNonQuery();
            sqlCon.Close();

        }
        /// <summary>
        /// Genera la lista de parametros para hacer Insert a la base de datos con el Paciente
        /// </summary>
        private static void listarColumnas()
        {
            string[] col = { "@Numero","@Nombre", "@Apellido", "@Dni", "@Edad", "@Sexo", "@NombreMadre", "@ApellidoMadre", "@DniMadre", "@Telefono", "@Celular", "@Domicilio", "@Localidad", "@Cama", "@Diagnostico", "@FechaNacimiento", "@FechaIngreso", "@FechaAlta" };
            foreach (string colu in col)
            {
                columnas.Add(colu);
            }
        }
    }
}
