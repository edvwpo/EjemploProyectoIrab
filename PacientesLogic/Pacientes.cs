using System;
using System.Collections.Generic;

namespace PacientesLogic
{
    public class Pacientes
    {
        private string numero { get; set; }
        private string nombre { get; set; }
        private string apellido { get; set; }
        private string dni { get; set; }
        private string sexo { get; set; }
        private string edad { get; set; }
        private string nombreMadre { get; set; }
        private string apellidoMadre { get; set; }
        private string dniMadre { get; set; }
        private string telefono { get; set; }
        private string celular { get; set; }
        private string domicilio { get; set; }
        private string localidad { get; set; }
        private string cama { get; set; }
        private string diagnostico { get; set; }
        private DateTime fechaIngreso { get; set; }
        private DateTime fechaNacimiento { get; set; }
        private DateTime fechaAlta { get; set; }

        private List<string> propiedades = new List<string>();
        private List<DateTime> fechas = new List<DateTime>();


        /// <summary>
        /// CTOR que llena las listas de propiedades
        /// </summary>
        public Pacientes()
        {
            propiedades = ListarPropiedades();
            fechas = ListarFechas();
            
        }

        /// <summary>
        /// Metodo para cargar las propiedades
        /// </summary>
        /// <param name="prop">Lista de propiedades ya cargadas</param>
        /// <param name="fechasL">Lista de fechas ya cargadas</param>
        public void cargarPaciente(List<string> prop, List<DateTime> fechasL)
        {
            AsignarListaFechas(fechasL);
            AsignarListaPropiedades(prop);

        }

        /// <summary>
        /// Lista de propiedades string
        /// </summary>
        /// <returns>retorna la lista</returns>
        private List<string> ListarPropiedades()
        {
            List<string> lista = new List<string>();
            lista.Add(numero);
            lista.Add(nombre);
            lista.Add(apellido);
            lista.Add(dni);
            lista.Add(edad);
            lista.Add(sexo);
            lista.Add(nombreMadre);
            lista.Add(apellidoMadre);
            lista.Add(dniMadre);
            lista.Add(telefono);
            lista.Add(celular);
            lista.Add(domicilio);
            lista.Add(localidad);
            lista.Add(cama);
            lista.Add(diagnostico);
            return lista;
        }
        /// <summary>
        /// Lista de propiedades DateTime
        /// </summary>
        /// <returns>retorna la lista</returns>
        private List<DateTime> ListarFechas()
        {
            List<DateTime> fechas = new List<DateTime>();
            fechas.Add(fechaNacimiento);
            fechas.Add(fechaIngreso);
            fechas.Add(fechaAlta);
            return fechas;
        }
        
        /// <summary>
        /// Asigna los valores a la lista 
        /// </summary>
        /// <param name="lista">Lista de string para llenar los parametros del paciente</param>
        private void AsignarListaPropiedades(List<string> lista)
        {
            for (int x = 0; x < propiedades.Count; x++)
            {
                propiedades[x] = lista[x];
            }
        }
        /// <summary>
        /// Asigna los valores a la lista
        /// </summary>
        /// <param name="lista">Lista de DateTime para llenar los parametros del paciente</param>
        private void AsignarListaFechas(List<DateTime> lista)
        {
            for (int x = 0; x < fechas.Count; x++)
            {
                fechas[x] = lista[x];
            }
        }

        /// <summary>
        /// Devuelve la lista de propiedades
        /// </summary>
        /// <returns>Retorno string</returns>
        public List<string> ObtenerListaPropiedades()
        {
            return propiedades;
        }
        /// <summary>
        /// devuelve la lista de fechas
        /// </summary>
        /// <returns>Retorno DateTime</returns>
        public List<DateTime> ObtenerListaFechas()
        {
            return fechas;
        }
    }
}
