using BlApi;
using BO;
using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience ComplexityLevel { get; set; }// for the combox
        public DateTime MinDate { get; set; } = DateTime.Now;// for the combox

        public TaskWindow(int idTask = 0)//the id has a default value
        {
            InitializeComponent();

            if (idTask == 0)
            {
                Task = new BO.Task()
                {
                    Id = 0,
                    Description = "",
                    Alias = "",
                    MileStone = null,
                    StatusTask = null,
                    DependenceList = null,
                    CreateAt = DateTime.Now,
                    Start = null,
                    ScheduledDate = null,
                    Deadline = DateTime.Now,
                    Complete = null,
                    Deliverables = "",
                    Remarks = "",
                    Engineer = new EngineerInTask()
                    {
                        Id = 0,
                        Name = ""
                    },
                    ComplexityLevel = null
                };
            }
            else
            {
                Task = s_bl.Task.Read(idTask)!;// if we are in update state find the Task  
            }
        }

        public BO.Task Task//dependency feature 
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
                  DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

        private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            //if ( != null)
            //{
            //  BO.EngineerInTask engineerInTask = new BO.EngineerInTask() {Id= 0, Name = "ggg" };
            //    Task.Engineer = new BO.EngineerInTask() { engineerInTask.Id, engineerInTask.Name };
            //}
            
            if ((sender as Button)!.Content.ToString() == "Add")//Checks the name of the button and accordingly saves the data
            {
                try
                {
                    int? id = s_bl.Task.Create(Task!);//call the func from the BL- create the task
                    MessageBox.Show($"Task {id} added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                try
                {
                    s_bl.Task.Update(Task);//call the func from the BL- update the task
                    MessageBox.Show($"Task {Task.Id} updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
        }
    }
}
