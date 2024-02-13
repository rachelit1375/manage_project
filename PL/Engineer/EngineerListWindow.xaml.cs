using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>


    public partial class EngineerListWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.None;//set as a default value
        public EngineerListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp);//Fills the collection with engineers- if no engineers exist the collection is empty

        }

        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));


        private void Btn_AddClick(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();//Displaying a screen for adding an engineer
        }

        private void Cmb_LevelChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EngineerList = Level == BO.EngineerExperience.None ?
            new(s_bl?.Engineer.ReadAll()!) : new(s_bl?.Engineer.ReadAll(item => item.Level == Level)!);//If the choice changes then sort the engineers by choice

        }

        private void Lv_UpdateClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            int id = engineerInList!.Id;
            new EngineerWindow(id).ShowDialog();//Displaying a screen for updating the choosen engineer
        }
        private void Window_activity(object sender, EventArgs e)
        {
            EngineerList = new(s_bl.Engineer.ReadAll()!);
        }
    }
}
