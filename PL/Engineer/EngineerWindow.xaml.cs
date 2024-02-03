using System.Windows;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();//יש כבר את אותה שורה בEngineerListWindow
        public BO.EngineerExperience level { get; set; } = BO.EngineerExperience.None;//יש כבר את אותה שורה בEngineerListWindow

        public EngineerWindow(int id = 0)
        {
            InitializeComponent();
            try
            {
                Engineer = id == 0 ? new BO.Engineer() : s_bl.Engineer.Read(id)!;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());//
            }
        }

        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public static readonly DependencyProperty EngineerProperty =
                    DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerListWindow), new PropertyMetadata(null));

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
