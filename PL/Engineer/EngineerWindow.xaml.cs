using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();// for the contact with BL

        public EngineerWindow(int idEngineer = 0)
        {
            InitializeComponent();
            if (idEngineer == 0)
            {
                Engineer = new BO.Engineer();
            }
            else
            {
                Engineer = s_bl.Engineer.Read(idEngineer)!;// if we are in update state find the engineer  
            }
        }

        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public static readonly DependencyProperty EngineerProperty =
                    DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)!.Content.ToString() == "Add")//Checks the name of the button and accordingly saves the data
            {
                try
                {
                    int? id = s_bl.Engineer.Create(Engineer!);//call the func from the BL- create the engineer
                    MessageBox.Show($"Engineer {id} added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show(ex.Message, "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                try
                {
                    s_bl.Engineer.Update(Engineer);//call the func from the BL- update the engineer
                    MessageBox.Show($"Engineer {Engineer.Id} updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (BO.BlDoesNotExistException ex)
                {
                    MessageBox.Show(ex.Message, "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
        }
    }
}
