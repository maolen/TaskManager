using System.Diagnostics;

namespace TaskManager
{
    public class ProcessListItem
    {
        public Process Process { get; }
        public int Id { get => Process.Id; }
        public string ProcessName { get => Process.ProcessName; }
        public bool IsAlive { get; set; }

        public ProcessListItem(Process process)
        {
            Process = process;
        }
    }
}
