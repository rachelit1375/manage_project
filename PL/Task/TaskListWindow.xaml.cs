using PL.Task;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Task
{
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

        private void Lv_UpdateClick(object sender, MouseButtonEventArgs e)
        {
            BO.Task? taskInList = (sender as ListView)?.SelectedItem as BO.Task;
            int id = taskInList!.Id;
            new TaskWindow(id).ShowDialog();//Displaying a screen for updating the choosen task
        }

        private void Btn_AddClick(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();//Displaying a screen for adding an task
        }

        private void Cmb_StatusChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = Status == BO.Status.None ?
           new(s_bl?.Task.ReadAll()!) : new(s_bl?.Task.ReadAll(item => item.StatusTask == Status)!);//If the choice changes then sort the tasks by choice
        }
    }
}
