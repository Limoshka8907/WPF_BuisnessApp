using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace WPF_Practice2024.Models
{
    public class MainViewModel : ViewModelBase
    {
        //Fields

        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        //private IUserRepository userRepository;


        //Properties

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }
        public ICommand ShowAgentViewCommand { get; }
        public ICommand ShowRealEstateViewCommand { get; }
        public ICommand ShowRealtyViewCommand { get; }
        public ICommand ShowDemandsViewCommand { get; }


        public MainViewModel()
        {
            //userRepository = new UserRepository();
            //CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowAgentViewCommand = new ViewModelCommand(ExecuteShowAgentViewCommand);
            ShowRealEstateViewCommand = new ViewModelCommand(ExecuteShowRealEstateCommand);
            ShowRealtyViewCommand = new ViewModelCommand(ExecuteShowRealtyViewCommand);
            ShowDemandsViewCommand = new ViewModelCommand(ExecuteShowDemandsViewCommand);
            //Default view
            ExecuteShowHomeViewCommand(null);

            //LoadCurrentUserData();
        }
        private void ExecuteShowRealEstateCommand(object obj)
        {
            CurrentChildView = new RealEstateModel();
            Caption = "RealEstate";
            Icon = IconChar.HouseChimneyWindow;
        }
        private void ExecuteShowAgentViewCommand(object obj)
        {
            CurrentChildView = new AgentViewModel();
            Caption = "Agents";
            Icon = IconChar.PeopleRoof;
        }
        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Customers";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void ExecuteShowRealtyViewCommand(object obj)
        {
            CurrentChildView = new RealtyViewModel();
            Caption = "Realty";
            Icon = IconChar.City;
        }
        private void ExecuteShowDemandsViewCommand(object obj)
        {
            CurrentChildView = new DemandsViewModel();
            Caption = "Demands";
            Icon = IconChar.HandHolding;
        }
    }
}