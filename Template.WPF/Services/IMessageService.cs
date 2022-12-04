using System.Windows;

namespace Template.WPF.Services
{
    public interface IMessageService
    {
        void ShowDialog(string message);

        MessageBoxResult Question(string message);
      
    }
}
