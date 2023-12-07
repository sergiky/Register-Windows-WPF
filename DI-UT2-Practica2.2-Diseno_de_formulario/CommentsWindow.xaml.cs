using DI_UT2_Practica2._2_Diseno_de_formulario.Properties;
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
using System.Windows.Shapes;

namespace DI_UT2_Practica2._2_Diseno_de_formulario
{
    /// <summary>
    /// Lógica de interacción para CommentsWindow.xaml
    /// </summary>
    public partial class CommentsWindow : Window
    {
        public string Text { get => txbComments.Text; set => txbComments.Text = value; }
        public int AvailableCharacters { set => txbAvailableCharacters.Text = $"{value}/500"; }

        public CommentsWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // If the user press the left button that can move the window (yes in all parts of the window)
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txbComments_KeyUp(object sender, KeyEventArgs e)
        {
            int MAXCHARACTERS = 500;
            int availableCharacters = 0;
            do
            {
                availableCharacters = txbComments.Text.Length;
                txbAvailableCharacters.Text = $"{availableCharacters}/{MAXCHARACTERS}";
            } while (IsFocused == true);
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
