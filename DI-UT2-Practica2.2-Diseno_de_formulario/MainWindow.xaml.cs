using DI_UT2_Practica2._2_Diseno_de_formulario.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DI_UT2_Practica2._2_Diseno_de_formulario
{
    public partial class MainWindow : Window
    {

        private readonly IDictionary<string,string> hintsDictionary = new Dictionary<string, string>()
        {
            { "nif", "71034925K" },
            { "name", "Bart" },
            { "surname", "Simpsons" },
            { "address", "C/ Simpsons" },
            { "phoneNumber", "683543582" },
            { "email", "bart@iesgregorioprieto.com" },
            { "comments", "Negative point: don't like Fornite" }
        };



        private IDictionary<string, bool> requiredFields = new Dictionary<string, bool>();

        public MainWindow()
        {
            InitializeComponent();

            lblNIFerror.Content = "";
            lblNameError.Content = "";
            lblSurnameError.Content = "";
            lblDateBirthError.Content = "";
            lblPhoneNumberError.Content = "";
            lblEmailError.Content = "";
            lblStateError.Content = "";
            lblLevelError.Content = "";
            lblCommentsError.Content = "";

            cmbLevel.SelectedIndex = 0;

            requiredFields.Add("nif", true);
            requiredFields.Add("name", true);
            requiredFields.Add("surname", true);
            requiredFields.Add("birthDate", true);
            requiredFields.Add("phoneNombre", true);
            requiredFields.Add("level", true);


        }

        /*
         * When you remove the title bar with WindowStyle="None" you can't move the window
         */
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // If the user press the left button that can move the window (yes in all parts of the window)
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // WindowState = WindowState.Maximized;

            hintsTextBox();


            loadDatePickerConfiguration();
        }

        private void hintsTextBox()
        {
            SolidColorBrush brushGray = new SolidColorBrush(Colors.DarkGray);


            txbNIF.Text = hintsDictionary["nif"];
            txbNIF.Foreground = brushGray;

            txbNIF.Focus();

            txbName.Text = hintsDictionary["name"];
            txbName.Foreground = brushGray;

            txbSurname.Text = hintsDictionary["surname"];
            txbSurname.Foreground = brushGray;

            txbAddress.Text = hintsDictionary["address"];
            txbAddress.Foreground = brushGray;

            txbPhoneNumber.Text = hintsDictionary["phoneNumber"];
            txbPhoneNumber.Foreground = brushGray;

            txbEmail.Text = hintsDictionary["email"];
            txbEmail.Foreground = brushGray;

            txbComments.Text = hintsDictionary["comments"];
            txbComments.Foreground = brushGray;

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Checkers checkers = new Checkers();

            // REQUIRED FIELDS
            requiredFields["nif"] = true;
            requiredFields["name"] = true;
            requiredFields["surname"] = true;
            requiredFields["birthDate"] = true;
            requiredFields["phoneNumber"] = true;
            requiredFields["level"] = true;



            // If it is the letters are in darkGray meaning that the user has not write yet
            if (txbNIF.Foreground.ToString() == Brushes.DarkGray.ToString())
            {
                txbNIF.BorderBrush = Brushes.Red;
                lblNIFerror.Content = "*Required NIF";
                requiredFields["nif"] = false;
            }

            // NAME

            if (txbName.Foreground.ToString() == Brushes.DarkGray.ToString())
            {
                txbName.BorderBrush = Brushes.Red;
                lblNameError.Content = "*Required name";
                requiredFields["name"] = false;
            }


            // SURNAME

            if (txbSurname.Foreground.ToString() == Brushes.DarkGray.ToString())
            {
                txbSurname.BorderBrush = Brushes.Red;
                lblSurnameError.Content = "*Required surname";
                requiredFields["surname"] = false;
            }

            // BIRTHDAY DATE

            int resultBirthDate = checkers.checkBirthDate(dtpBirthDate.Text);

            if (resultBirthDate == 0) 
            {
                lblDateBirthError.Content = "*Required birth date";
                requiredFields["birthDate"] = false;
            }

            // PHONE NUMBER
            if (txbPhoneNumber.Foreground.ToString() == Brushes.DarkGray.ToString())
            {
                lblPhoneNumberError.Content = "*Required phone number";
                requiredFields["phoneNumber"] = false;
            }

            // COMBOBOX LEVEL 
            if (cmbLevel.SelectedIndex == 0)
            {
                lblLevelError.Content = "*Required level";
                requiredFields["level"] = false;
            }
            else
            {
                lblLevelError.Content = "";
            }

            bool isError = false;
            foreach(Boolean state in requiredFields.Values)
            {
                if (!state) { isError = true; }
            }

            if (!isError)
            {
                MessageBox.Show("All fields correct");
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loadDatePickerConfiguration()
        {
            dtpBirthDate.FontSize = 15;
            dtpBirthDate.Foreground = Brushes.DarkGray;
            dtpBirthDate.BorderBrush = Brushes.Transparent;
        }

        // -------------------------------------------------- To check that the written NIF is well formed ------------------------------------------

        private void txbNIF_KeyUp(object sender, KeyEventArgs e)
        {
            if (txbNIF.Text.Length <= 9 && txbNIF.Text.Length > 0)
            {
                Checkers checkers = new Checkers();
                bool nifValid = checkers.checkNif(txbNIF.Text);

                if (!nifValid)
                {
                    txbNIF.BorderBrush = Brushes.Red;
                    lblNIFerror.Content = "*Invalid format";
                    requiredFields["nif"] = false;
                }
                else
                {
                    // NIF OK
                    txbNIF.BorderBrush = Brushes.DarkGray;
                    lblNIFerror.Content = "";
                }

            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------


        // -------------------------------------------------- To check that the written NAME is well formed ------------------------------------------

        private void txbName_KeyDown(object sender, KeyEventArgs e)
        {
            Checkers checkers = new Checkers();

            // If the key pressed is a number, put the error in the label
            if (checkers.containNumber(e.Key.ToString()))
            {
                lblNameError.Content = "Numbers not allowed";
                txbName.BorderBrush = Brushes.Red;
                requiredFields["name"] = false;
            }
        }

        private void txbName_KeyUp(object sender, KeyEventArgs e)
        {
            Checkers checkers = new Checkers();

            // If the string doesn't containt a number, put to the correct color
            if (!checkers.containNumber(txbName.Text))
            {
                lblNameError.Content = "";
                txbName.BorderBrush = Brushes.DarkGray;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------


        // -------------------------------------------------- To check that the written SURNAME is well formed ------------------------------------------

        private void txbSurname_KeyDown(object sender, KeyEventArgs e)
        {
            Checkers checkers = new Checkers();
            if (checkers.containNumber(e.Key.ToString()))
            {
                lblSurnameError.Content = "Numbers not allowed";
                txbSurname.BorderBrush = Brushes.Red;
                requiredFields["surname"] = false;
            }
        }

        private void txbSurname_KeyUp(object sender, KeyEventArgs e)
        {
            Checkers checkers = new Checkers();

            if (!checkers.containNumber(txbSurname.Text))
            {
                lblSurnameError.Content = "";
                txbSurname.BorderBrush = Brushes.DarkGray;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------

        // -------------------------------------------------- To check that the written DATE BIRTH is well formed and calendar handlers ------------------------------------------



        // I know that this function is active when a key is pressed, but I don't found the handler that works only when one key is pressed
        private void dtpBirthDate_KeyDown(object sender, KeyEventArgs e)
        {

            SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
            dtpBirthDate.Foreground = brushWhite;
        }

        private void dtpBirthDate_KeyUp(object sender, KeyEventArgs e)
        {
            Checkers checkers = new Checkers();
            int resultBirthDate = checkers.checkBirthDate(dtpBirthDate.Text);

            if (resultBirthDate == -1)
            {
                lblDateBirthError.Content = $"*Invalid date (1900-{DateTime.Now.Year})";
                requiredFields["birthDate"] = false;
            }
            else
            {
                lblDateBirthError.Content = "";
            }

        }

        private void dtpBirthDate_CalendarOpened(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
            dtpBirthDate.Foreground = brushWhite;
        }


        // -------------------------------------------------------------------------------------------------------------------------------------------

        // -------------------------------------------------- To check that the written PHONE NUMBER is well formed ------------------------------------------

        private void txbPhoneNumber_KeyUp(object sender, KeyEventArgs e)
        {
            Checkers checkers = new Checkers();
            if (! checkers.checkPhoneNumber(txbPhoneNumber.Text) && txbPhoneNumber.Text != "")
            {
                lblPhoneNumberError.Content = "*Invalid format";
                txbPhoneNumber.BorderBrush = Brushes.Red;
                requiredFields["phoneNumber"] = false;
            } else
            {
                lblPhoneNumberError.Content = "";
                txbPhoneNumber.BorderBrush = Brushes.White;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------

        // -------------------------------------------------- To check that the written PHONE NUMBER is well formed ------------------------------------------

        private void txbEmail_KeyUp(object sender, KeyEventArgs e)
        {
            Checkers checkers = new Checkers();
            if (checkers.checkEmail(txbEmail.Text))
            {
                lblEmailError.Content = "";
                txbEmail.BorderBrush = Brushes.White;
            }
            else
            {
                lblEmailError.Content = "*Invalid format";
                txbEmail.BorderBrush = Brushes.Red;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------

        // -------------------------------------------------- State (Active or inactive) -----------------------------------------------------------


        private void chkState_Click(object sender, RoutedEventArgs e)
        {
            if (chkState.IsChecked == true)
            {
                lblSateStatus.Content = "Active";
            }
            else
            {
                lblSateStatus.Content = "Inactive";
            }
        }

        private void lblSateStatus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (chkState.IsChecked == false)
            {
                chkState.IsChecked = true;
                lblSateStatus.Content = "Active";
            } else
            {
                chkState.IsChecked = false;
                lblSateStatus.Content = "Inactive";
            }

        }

        // -------------------------------------------------------------------------------------------------------------------------------------------

        // -------------------------------------------------- Comments section -----------------------------------------------------------------------

        private void txbComments_KeyUp(object sender, KeyEventArgs e)
        {
            if (txbComments.Text.Length == 30)
            {
                lblCommentsError.Content = "*max characters, open the text editor";
                lblCommentsError.Foreground = Brushes.LightGray;
            }
            else
            {
                lblCommentsError.Content = "";
                lblCommentsError.Foreground = Brushes.Red;
            }
        }

        private void btnWriteMore_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"/res/Images/pencil_white.png", UriKind.RelativeOrAbsolute));

            imgPencil.Source = image.Source;
        }

        private void btnWriteMore_MouseLeave(object sender, MouseEventArgs e)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"/res/Images/pencil_purple.png", UriKind.RelativeOrAbsolute));

            imgPencil.Source = image.Source;
        }

        private void btnWriteMore_Click(object sender, RoutedEventArgs e)
        {
            CommentsWindow commentsWindow = null;

            // Send the text throught the window
            commentsWindow = new CommentsWindow();

            // if user click okay in the comments windows, put the text 
            if (txbComments.Text != hintsDictionary["comments"])
            {
                commentsWindow.Text = txbComments.Text;
                commentsWindow.AvailableCharacters = txbComments.Text.Length;
            }

            // Execute the window and enter in the if when user click in ok button
            if (commentsWindow.ShowDialog() == true)
            {
                // If it's in blank, put the hint
                if (commentsWindow.Text == "")
                {
                    txbComments.Text = hintsDictionary["comments"];
                    txbComments.Foreground = new SolidColorBrush(Colors.DarkGray);
                } else
                {
                    txbComments.Text = commentsWindow.Text;
                }

            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------


        /*

            foreach (StackPanel stackPanel in gridStacks.Children)
    {
        foreach (var element in stackPanel.Children)
        {
            if (element is TextBox)
            {
                TextBox textBox = (TextBox)element;
                if (textBox.IsFocused)
                {
                    buttonHasFocus();

                }
            }
        }
    }

*/

        private void txbNIF_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbNIF.Text == "" || txbNIF.Text == hintsDictionary["nif"])
            {
                SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
                txbNIF.Foreground = brushWhite;
                txbNIF.Text = "";
            }
        }

        private void txbNIF_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbNIF.Text == "")
            {
                SolidColorBrush brushGray = new SolidColorBrush(Colors.DarkGray);
                txbNIF.Foreground = brushGray;
                txbNIF.Text = hintsDictionary["nif"];
            }
        }

        private void txbName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbName.Text == "" || txbName.Text == hintsDictionary["name"])
            {
                SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
                txbName.Foreground = brushWhite;
                txbName.Text = "";
            }
        }

        private void txbName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbName.Text == "")
            {
                SolidColorBrush brushGray = new SolidColorBrush(Colors.DarkGray);
                txbName.Foreground = brushGray;
                txbName.Text = hintsDictionary["name"];
            }
        }

        private void txbSurname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbSurname.Text == "" || txbSurname.Text == hintsDictionary["surname"])
            {
                SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
                txbSurname.Foreground = brushWhite;
                txbSurname.Text = "";
            }
        }

        private void txbSurname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbSurname.Text == "")
            {
                SolidColorBrush brushGray = new SolidColorBrush(Colors.DarkGray);
                txbSurname.Foreground = brushGray;
                txbSurname.Text = hintsDictionary["surname"];
            }
        }

        private void txbAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbAddress.Text == "" || txbAddress.Text == hintsDictionary["address"])
            {
                SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
                txbAddress.Foreground = brushWhite;
                txbAddress.Text = "";
            }
        }

        private void txbAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbAddress.Text == "")
            {
                SolidColorBrush brushGray = new SolidColorBrush(Colors.DarkGray);
                txbAddress.Foreground = brushGray;
                txbAddress.Text = hintsDictionary["address"];
            }
        }

        private void txbPhoneNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbPhoneNumber.Text == "" || txbPhoneNumber.Text == hintsDictionary["phoneNumber"])
            {
                SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
                txbPhoneNumber.Foreground = brushWhite;
                txbPhoneNumber.Text = "";
            }
        }

        private void txbPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbPhoneNumber.Text == "")
            {
                SolidColorBrush brushGray = new SolidColorBrush(Colors.DarkGray);
                txbPhoneNumber.Foreground = brushGray;
                txbPhoneNumber.Text = hintsDictionary["phoneNumber"];
            }
        }

        private void txbEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbEmail.Text == "" || txbEmail.Text == hintsDictionary["email"])
            {
                SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
                txbEmail.Foreground = brushWhite;
                txbEmail.Text = "";
            }
        }

        private void txbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbEmail.Text == "")
            {
                SolidColorBrush brushGray = new SolidColorBrush(Colors.DarkGray);
                txbEmail.Foreground = brushGray;
                txbEmail.Text = hintsDictionary["email"];
            }
        }

        private void txbComments_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbComments.Text == "" || txbComments.Text == hintsDictionary["comments"])
            {
                SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
                txbComments.Foreground = brushWhite;
                txbComments.Text = "";
            }
        }

        private void txbComments_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbComments.Text == "")
            {
                SolidColorBrush brushGray = new SolidColorBrush(Colors.DarkGray);
                txbComments.Foreground = brushGray;
                txbComments.Text = hintsDictionary["comments"];
            }
        }

        private void cmbLevel_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbLevel.SelectedIndex != 0)
            {
                lblLevelError.Content = "";
            }
            else
            {
                lblLevelError.Content = "*Required level";
            }
        }
    }
}
