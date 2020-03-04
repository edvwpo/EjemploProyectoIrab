using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;


namespace DataAccess
{
    /// <summary>
    /// Clases que modifica y actualiza el DataSet a ser ingresado a la base de datos por Conectar.
    /// </summary>
    public class ModData
    {
        private DataSet data = new DataSet();
        private string sql { get; set; }

        /// <summary>
        /// Unico constructor de ModData.
        /// Carga el DataSet
        /// </summary>
        /// <param name="con">string para generar la SqlConnection</param>
        public ModData()
        {
            string directorio = AppDomain.CurrentDomain.BaseDirectory + "PacientesIrab.mdf";
            string sql = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = " + directorio + "; Integrated Security = True";
            SetearSqlString(sql);
            Conectar.setearSqlString(sql);
            SetearData();
        }
        private void SetearSqlString(string con)
        {
            if (con != null)
            {
                sql = con;
            }
        }
        /// <summary>
        /// Llena el DataSet data de ModData
        /// </summary>
        private void SetearData ()
        {
            Conectar.Llenar(data);
        }
        /// <summary>
        /// Borra el Row especificado de data
        /// </summary>
        /// <param name="nombre">Nombre de la tabla</param>
        /// <param name="indice">Indice de Row</param>
        public void BorrarRow (string nombre,int ID)
        {
            Conectar.Borrar(ID);
            Conectar.Llenar(data);
        }

        /// <summary>
        /// Inserta un row en la tabla especificada
        /// </summary>
        /// <param name="nombre">Nombre de la tabla</param>
        public void InsertarRow (List<string> propiedades, List<DateTime> fechas)
        {
            Conectar.Ingresar(propiedades, fechas);
        }
        /// <summary>
        /// Actualiza la Base de Datos con data
        /// </summary>
        public void ActualizarBD()
        {
            Conectar.Actualizar(data);
        }
        /// <summary>
        /// Retorna el dataset cargado
        /// </summary>
        /// <returns></returns>
        public  DataSet retornoData()
        {
            return data;
        }
    }
}
