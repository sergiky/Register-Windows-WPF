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

namespace LibraryStateCheckbox
{
    /// <summary>
    /// Lógica de interacción para StateCheckbox.xaml
    /// </summary>
    public partial class StateCheckbox : UserControl
    {
        private double width = 0;
        private double height = 0;
        public StateCheckbox()
        {
            InitializeComponent();
            updateState();
        }


        private void updateState()
        {
            chkState.Width = this.width;
            chkState.Height = this.height;
        }

        public double Width1 { 
            get => width;
            set {
                width = value;
                updateState();
            } 
        }
        public double Height1 {
            get => height;
            set 
            {
                height = value;
                updateState();
            } 
        }


    }
}
