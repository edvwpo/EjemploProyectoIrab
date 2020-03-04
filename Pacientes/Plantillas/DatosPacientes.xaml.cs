using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pacientes.Plantillas
{
    /// <summary>
    /// Lógica de interacción para DatosPacientes.xaml
    /// </summary>
    public partial class DatosPacientes : Window
    {
        private List<string> info = new List<string>();
        private List<DateTime> fechas = new List<DateTime>();
        private List<TextBox> textBoxList = new List<TextBox>();
        private List<DatePicker> datePickersList = new List<DatePicker>();
        private DateTime fechaDefault = new DateTime(1990,01,01);

        private bool ingresado { get; set; }
        public DatosPacientes()
        {
            
            InitializeComponent();
            generarListas();

        }
        /// <summary>
        /// Asigna la lista de string "info"
        /// </summary>
        private void listaAString()
        {
            for (int x = 0; x < (textBoxList.Count); x++)
            {
                if (x != 5)
                {
                    info.Add(textBoxList[x].Text);
                    
                }
                else
                {
                    info.Add(SexoCombo.Text);
                }
            }
        }

        /// <summary>
        /// Asigna la lista de DateTime "fechas"
        /// </summary>
        private void listaDateTime()
        {
            for (int x = 0; x < datePickersList.Count; x++)
            {
                fechas.Add(datePickersList[x].SelectedDate.GetValueOrDefault(fechaDefault));
            }
        }

        /// <summary>
        /// Generan las listas con los elementos para rellenar
        /// </summary>
        private void generarListas()
        {
            textBoxList.Add(NumeroTxt);
            textBoxList.Add(NombreTxt);
            textBoxList.Add(ApellidoTxt);
            textBoxList.Add(DniTxt);
            textBoxList.Add(EdadTxt);
            textBoxList.Add(new TextBox());
            textBoxList[textBoxList.Count - 1].Text = "asd";
            textBoxList.Add(NombreMadreTxt);
            textBoxList.Add(ApellidoMadreTxt);
            textBoxList.Add(DniMadreTxt);
            textBoxList.Add(TelefonoTxt);
            textBoxList.Add(CelularTxt);
            textBoxList.Add(DomicilioTxt);
            textBoxList.Add(LocalidadTxt);
            textBoxList.Add(CamaTxt);
            textBoxList.Add(DiagnosticoTxt);

            datePickersList.Add(NacimientoDate);
            datePickersList.Add(IngresoDate);
            datePickersList.Add(AltaDate);
            listaAString();
            listaDateTime();
        }

        /// <summary>
        /// Ingresa los datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IngresarBtn_Click(object sender, RoutedEventArgs e)
        {
            bool validado = validarDatos();
            DateADate();
            PacientesLogic.Admin admin = new PacientesLogic.Admin();
            try
            {
                if (validado)
                {
                    admin.insertar(info, fechas);
                    MessageBox.Show("Paciente Ingresado Correctamente", "Paciente Ingresado", MessageBoxButton.OK, MessageBoxImage.Information);
                    ingresado = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Falta ingresar datos", "Incompleto", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar al paciente", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex.Message);
                
            }

        }

        private void CancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            ingresado = false;
            Close();
        }

        public void CargarPLantilla(PacientesLogic.Pacientes paciente)
        {
            int x = 0;
            List<string> listaProp = paciente.ObtenerListaPropiedades();
            List<DateTime> listaFechas = paciente.ObtenerListaFechas();
            foreach(TextBox box in textBoxList)
            {
                if(x != 5)
                {
                    box.Text = listaProp[x];
                }
                else
                {
                    if ((box.Text == SexoCombo.Text))
                    {
                        SexoCombo.SelectedIndex = 1;
                    }
                    else
                    {
                        SexoCombo.SelectedIndex = 0;
                    }
                    
                }
                x++;
            }
            x = 0;
            foreach(DatePicker time in datePickersList)
            {
                time.SelectedDate = listaFechas[x];
                x++;
            }
        }
        public bool retornoIngresado()
        {
            return ingresado;
        }

        private bool validarDatos()
        {
            int x = 0;

            foreach(var cadena in textBoxList)
            {
                if (cadena.Text == "" || cadena.Text == " "|| cadena.Text == "  " || cadena.Text == "   ")
                {
                    return false;
                }
                else
                {
                    if (x != 5)
                    {
                        info[x] = cadena.Text.ToUpper();
                    }
                    else
                    {
                        info[x] = SexoCombo.Text;
                    }
                }
                x++;
                
            }
            return true;
        }
        private void DateADate()
        {
            int x = 0;
            foreach (DatePicker time in datePickersList)
            {
                fechas[x] = time.SelectedDate.GetValueOrDefault(fechaDefault);
                x++;
            }
        }

        
    }
}
