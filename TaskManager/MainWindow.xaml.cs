using System;
using System.Windows;
using System.Windows.Controls;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProcessListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            if (listBox.SelectedItems.Count > 0)
            {
                var viewModel = (ViewModel)DataContext;
                viewModel.SelectedProcess = ((ProcessListItem)listBox.SelectedItems[0]).Process;
            }
        }

        private void UpdateProcesses(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.Update(sender, e);
        }
        private void KillProcess(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;     
            
            try
            {
                viewModel.Kill();
                MessageBox.Show("Процесс закрыт успешно!");
            }
            catch(Exception)
            {
                MessageBox.Show("Ошибка закрытия процесса!");
            }
        }

        private void ShowDetails(object sender, RoutedEventArgs e)
        {
            showDetailsButton.Visibility = Visibility.Collapsed;
            detailsGrid.Visibility = Visibility.Visible;
            hideDetailsButton.Visibility = Visibility.Visible;
        }

        private void HideDetails(object sender, RoutedEventArgs e)
        {
            hideDetailsButton.Visibility = Visibility.Collapsed;
            showDetailsButton.Visibility = Visibility.Visible;
            detailsGrid.Visibility = Visibility.Hidden;
        }
    }
}
