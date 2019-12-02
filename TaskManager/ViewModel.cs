using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace TaskManager
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Process _selectedProcess;
        public ObservableCollection<ProcessListItem> Processes { get; } = new ObservableCollection<ProcessListItem>();
        public Process SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                _selectedProcess = value;
                OnPropertyChanged();
            }
        }

        public ViewModel()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Tick += Update;
            timer.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            var currentIds = Processes.Select(process => process.Id).ToList();

            foreach (var process in Process.GetProcesses())
            {
                if (!currentIds.Remove(process.Id)) 
                {
                    Processes.Add(new ProcessListItem(process));
                }
            }

            foreach (var id in currentIds) 
            {
                var process = Processes.First(process => process.Id == id);
                if (process.IsAlive)
                {
                    Process.Start(process.ProcessName);
                }
                Processes.Remove(process);
            }
        }

        public void Kill()
        {
            SelectedProcess.Kill();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
