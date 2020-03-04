using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace PacientesLogic
{
    public class Admin
    {
        Pacientes paciente = new Pacientes();

        public Admin()
        {
            
        }

        public DataSet Datos()
        {
            DataAccess.ModData data = new DataAccess.ModData();
            var datos = data.retornoData();

            return datos;
        }
        /// <summary>
        /// Inicia el proceso de insercion a la base de datos
        /// </summary>
        /// <param name="paciente">Paciente a ingresar</param>
        public void insertar(List<string> propiedades, List<DateTime> fechas)
        {
            cargarPaciente(propiedades, fechas);

            DataAccess.ModData data = new DataAccess.ModData();
            data.InsertarRow(paciente.ObtenerListaPropiedades(), paciente.ObtenerListaFechas());
            
        }

        private void cargarPaciente(List<string> propiedades, List<DateTime> fechas)
        {

            paciente.cargarPaciente(propiedades, fechas);
            
        }

        public Pacientes Modificar (DataRowView[] rowViews)
        {
            DataRow[] rows = (from dr in rowViews select dr.Row).ToArray();
            Asistente asistente = new Asistente(rows[0]);
            paciente.cargarPaciente(asistente.DevolverListaProp(), asistente.DevolverListaFechas());
            DataAccess.ModData mod = new DataAccess.ModData();
            mod.BorrarRow("Pacientes2020", Int32.Parse(rows[0].ItemArray[0].ToString()));
            return paciente;
        }

        public void BorrarRow(int ID)
        {
            DataAccess.ModData mod = new DataAccess.ModData();
            mod.BorrarRow("Pacientes2020", ID);
        }

        public void DarAlta(DataRowView[] rowViews, DateTime alta)
        {
            DataRow[] rows = (from dr in rowViews select dr.Row).ToArray();
            rows[0][rows[0].ItemArray.Length - 1] = alta;
            Asistente asistente = new Asistente(rows[0]);
            var lista1 = asistente.DevolverListaProp();
            var lista2 = asistente.DevolverListaFechas();
            paciente.cargarPaciente(asistente.DevolverListaProp(), asistente.DevolverListaFechas());
            DataAccess.ModData data = new DataAccess.ModData();
            data.InsertarRow(paciente.ObtenerListaPropiedades(), paciente.ObtenerListaFechas());
            data.BorrarRow("Pacientes2020", Int32.Parse(rows[0].ItemArray[0].ToString()));

        }
        
    }
}
