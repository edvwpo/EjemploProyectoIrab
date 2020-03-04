using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace Pacientes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet source = new DataSet();
        PacientesLogic.Admin admin = new PacientesLogic.Admin();
        public MainWindow()
        {
            InitializeComponent();
            
            source = admin.Datos();
            PacientesGrid.ItemsSource = source.Tables["Pacientes2020"].DefaultView;
            

            
        }

        private void IngresarBtn_Click(object sender, RoutedEventArgs e)
        {
            Plantillas.DatosPacientes datosPacientes = new Plantillas.DatosPacientes();
            datosPacientes.CancelarBtn.IsEnabled = true;
            datosPacientes.Show();
        }

        private void ModificarBtn_Click(object sender, RoutedEventArgs e)
        {
            PacientesLogic.Admin admin = new PacientesLogic.Admin();

            if(PacientesGrid.SelectedCells.Count > 0)
            {
                PacientesLogic.Pacientes pacienteAModificar = admin.Modificar(PacientesGrid.SelectedItems.Cast<DataRowView>().ToArray());
                Plantillas.DatosPacientes datosPaciente = new Plantillas.DatosPacientes();
                datosPaciente.CargarPLantilla(pacienteAModificar);
                datosPaciente.CancelarBtn.IsEnabled = false;
                PacientesGrid.SelectedItems.Remove(PacientesGrid.SelectedValue);
                datosPaciente.Show();
            }
            else
            {
                MessageBox.Show("Seleccione una celda", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
            

        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            PacientesGrid.ItemsSource = null;
            source = admin.Datos();
            PacientesGrid.ItemsSource = source.Tables["Pacientes2020"].DefaultView;

        }

        private void BorrarBtn_Click(object sender, RoutedEventArgs e)
        {
            if(PacientesGrid.SelectedCells.Count > 0)
            {
                DataRowView[] drv = PacientesGrid.SelectedItems.Cast<DataRowView>().ToArray();
                DataRow[] rows = (from dr in drv select dr.Row).ToArray();
                PacientesLogic.Admin admin = new PacientesLogic.Admin();
                admin.BorrarRow(Int32.Parse(rows[0].ItemArray[0].ToString()));
                RefreshBtn_Click(BorrarBtn, e);
            }
            else
            {
                MessageBox.Show("Seleccione una celda", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void DarAltaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PacientesGrid.SelectedCells.Count > 0)
            {
                DataRowView[] drv = PacientesGrid.SelectedItems.Cast<DataRowView>().ToArray();
                PacientesLogic.Admin admin = new PacientesLogic.Admin();
                admin.DarAlta(drv, FechaAltaDate.SelectedDate.GetValueOrDefault());
                RefreshBtn_Click(DarAltaBtn, e);
            }
            else
            {
                MessageBox.Show("Seleccione una celda", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        private void BuscarBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BuscarTxt.Text != "" || BuscarTxt.Text!= " ")
            {
                BuscarTxt.Text = BuscarTxt.Text.ToUpper();
                var query = from fila in source.Tables["Pacientes2020"].AsEnumerable()
                            where fila.Field<string>($"{BuscarCombo.Text}") == $"{BuscarTxt.Text}"
                            select fila;
                
                
                PacientesGrid.ItemsSource = query.AsDataView();
            }
        }
    }
}
