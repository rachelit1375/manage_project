using System.Collections.ObjectModel;
using System.Windows;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>


    public partial class EngineerListWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience level { get; set; } = BO.EngineerExperience.None;
        public EngineerListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp);

        }

        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().Show();
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Cmb_LevelChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EngineerList = level == BO.EngineerExperience.None ?
            new(s_bl?.Engineer.ReadAll()!) : new(s_bl?.Engineer.ReadAll(item => item.Level == level)!);

        }
    }
}
