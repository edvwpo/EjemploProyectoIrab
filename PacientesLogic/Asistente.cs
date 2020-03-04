using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PacientesLogic
{
    class Asistente
    {
        private List<string> listaPropiedades = new List<string>();
        private List<DateTime> listaFechas = new List<DateTime>();

        public Asistente()
        {

        }
        public Asistente(DataRow row) 
        {
            ArmarListasDesdeRow(row);
        }

        private void ArmarListasDesdeRow (DataRow row)
        {
            for(int x = 1; x< row.ItemArray.Length; x++)
            {
                if (x < (row.ItemArray.Length - 3))
                {
                    listaPropiedades.Add(row.ItemArray[x].ToString());
                }
                else
                {
                    if (row.ItemArray[x] != null)
                    {
                        listaFechas.Add((DateTime)row.ItemArray[x]);
                    }
                }
               
            }
        }

        public List<string> DevolverListaProp()
        {
            return listaPropiedades;
        }

        public List<DateTime> DevolverListaFechas()
        {
            return listaFechas;
        }
        
    }
}
