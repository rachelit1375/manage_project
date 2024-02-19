using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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
            if (idTask == 0)
            {
                Task = new BO.Task()
                {
                    Id = 0,
                    Description = "",
                    Alias = "",
                    MileStone = null,
                    StatusTask = null,
                    DependenceList = new List<TaskInList>(),
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
                if (Task.DependenceList != null)
                {
                    Dependence =  Task.DependenceList;
                }              
            }
            InitializeComponent();
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

        public List<BO.TaskInList>? Dependence//dependency feature 
        {
            get { return (List<BO.TaskInList>)GetValue(DependenceProperty); }
            set { SetValue(DependenceProperty, value); }
        }

        public static readonly DependencyProperty DependenceProperty =
                  DependencyProperty.Register("Dependence", typeof(List<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));

        private void Cmb_DependenceChanged(object sender, SelectionChangedEventArgs e)
        {          
            if ((sender as ComboBox)?.SelectedValue != null)//If a new dependency is selected
            {//We will add the selected dependency to the task's dependency list
                var newDependence = (from task in s_bl.Task.ReadAll()
                         where task.Alias == (sender as ComboBox)!.SelectedValue.ToString()
                         select new TaskInList()//
                         {
                             Id = task.Id,
                             Description = task.Description,
                             Alias = task.Alias,
                             Status = task.StatusTask
                         }).FirstOrDefault();
                Task.DependenceList?.Add(newDependence!);
            }
        }
    }
}
