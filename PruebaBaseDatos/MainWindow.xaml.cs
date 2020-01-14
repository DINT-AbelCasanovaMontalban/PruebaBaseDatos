using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace PruebaBaseDatos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseAbelCasanovaEntities contexto;
        private CollectionViewSource vista;

        public MainWindow()
        {
            InitializeComponent();

            CLIENTE nuevo = new CLIENTE();
            InsertarStackPanel.DataContext = nuevo;

            contexto = new DatabaseAbelCasanovaEntities();
            contexto.CLIENTES.Load();
            contexto.PEDIDOS.Load();

            ClientesListBox.DataContext = contexto.CLIENTES.Local;
            IdComboBox.DataContext = contexto.CLIENTES.Local;
            IdModificarComboBox.DataContext = contexto.CLIENTES.Local;
            DatosDataGrid.DataContext = contexto.CLIENTES.Local;
             

            vista = new CollectionViewSource();
            vista.Source= contexto.CLIENTES.Local;
            PedidosDataGrid.DataContext = vista;

            vista.Filter += Vista_Filter;

        }

        private void Vista_Filter(object sender, FilterEventArgs e)
        {
            CLIENTE item = (CLIENTE)e.Item;

            if (FiltrarTextBox.Text=="")
            {
                e.Accepted = true;
            } else
            {
                String nombreCompleto = item.nombre + " " + item.apellido;
                if (nombreCompleto.Contains(FiltrarTextBox.Text))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            contexto.CLIENTES.Remove((CLIENTE)IdComboBox.SelectedItem);
        }

        private void InsertarButton_Click(object sender, RoutedEventArgs e)
        {

            contexto.CLIENTES.Add((CLIENTE)InsertarStackPanel.DataContext);

            contexto.SaveChanges();

            CLIENTE nuevo = new CLIENTE();
            InsertarStackPanel.DataContext = nuevo;
            

        }

        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            contexto.SaveChanges();
        }

        private void ActualizarButton_Click(object sender, RoutedEventArgs e)
        {
            contexto.SaveChanges();
        }



        private void FiltrarButton_Click(object sender, RoutedEventArgs e)
        {
            vista.View.Refresh();
        }
    }
}
