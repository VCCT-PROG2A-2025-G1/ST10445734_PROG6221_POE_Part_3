using ST10445734_PROG6221_POE_Part_3.Services;
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

namespace ST10445734_PROG6221_POE_Part_3.Views
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private ObservableCollection<Task> tasks;
        private TaskService taskService;

        public TaskWindow()
        {
            InitializeComponent();
            taskService = new TaskService();
            tasks = new ObservableCollection<Task>(taskService.GetTasks());
            TaskListView.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleInput.Text.Trim();
            string description = DescriptionInput.Text.Trim();
            DateTime? reminderDate = ReminderDateInput.SelectedDate;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Title and Description cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task newTask = new Task(title, description, reminderDate);

            taskService.AddTask(newTask);
            tasks.Add(newTask);

            TitleInput.Clear();
            DescriptionInput.Clear();
            ReminderDateInput.SelectedDate = null;

        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is Task selectedTask)
            {
                taskService.DeleteTask(selectedTask);
                tasks.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MarkCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is Task selectedTask)
            {
                selectedTask.IsCompleted = true;
                taskService.MarkTaskAsCompleted(selectedTask);
                TaskListView.Items.Refresh();
            }
            else 
            {
                MessageBox.Show("Please select a task to mark as completed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
