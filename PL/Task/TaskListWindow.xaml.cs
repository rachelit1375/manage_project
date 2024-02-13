using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status Status { get; set; } = BO.Status.None;//set as a default value
        public TaskListWindow()
        {
            InitializeComponent();
        }

        public ObservableCollection<BO.Task> TaskList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));

        private void Window_activity(object sender, EventArgs e)
        {
            TaskList = new(s_bl.Task.ReadAll()!);
        }

        private void Cmb_StatusChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Lv_UpdateClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_AddClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
