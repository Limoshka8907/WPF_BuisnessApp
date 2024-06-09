using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_Practice2024.Models
{
    public class ViewModelCommand : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        //Constructors
        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        //Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Methods
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
    public class HomeViewModel : ViewModelBase
    {

    }
    public class CustomerViewModel : ViewModelBase
    {

    }
    public class AgentViewModel : ViewModelBase
    {

    }
    public class RealEstateModel : ViewModelBase
    {

    }
    public class RealtyViewModel : ViewModelBase
    {

    }
    public class DemandsViewModel : ViewModelBase
    {

    }
    public class SupplyViewModel : ViewModelBase
    {
    }
}
