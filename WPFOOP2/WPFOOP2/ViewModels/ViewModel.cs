using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFOOP2.Repos;

namespace WPFOOP2
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Model model;

        public event PropertyChangedEventHandler PropertyChanged;

        private Repo repository;

        public static ObservableCollection<Model> page;

        public ICommand Increment { get; set; }
        public ICommand Flip { get; set; }
        
        public ICommand SetRandomNums { get; set; }

        public ICommand Save { get; set; }

        public ICommand Load { get; set; }

        public ObservableCollection<Model> Page
        {
            get
            {
                return page;
            }
            set
            {
                if (value != page)
                {
                    page = value;
                }
            }
        }

        public int timesClicked
        { 
            
            get {return model.timesClicked;}
            set {
                model.timesClicked = value;
                RaisePropertyChanged("timesClicked");
            }
        }

        public string headsTailsString
        {
            get { return model.headsTailsString; }
            set {
                model.headsTailsString = value;
                 }
        }

        public int timesFlipped
        {
            get { return model.timesFlipped; }
            set {
                model.timesFlipped = value;
                RaisePropertyChanged("timesFlipped"); }
        }

        public string dateString
        {
            get { return model.dateString; }
            set {
                model.dateString = value;
                RaisePropertyChanged(); }
        }

        public string nameString
        {
            get { return model.nameString; }
            set {
                model.nameString = value;
                RaisePropertyChanged(); }
        }

        public int firstNumber
        {
            get { return model.firstNumber; }
            set {
                model.firstNumber = value;
                RaisePropertyChanged(); }
        }

        public int secondNumber
        {
            get { return model.secondNumber; }
            set {
                model.secondNumber = value;
                RaisePropertyChanged(); }
        }

        public string resultString
        {
            get { return model.resultString; }
            set {
                model.resultString = value;
                RaisePropertyChanged(); }
        }

        public ViewModel(Model m)
        {
            model = m;
            repository = new Repo();
            this.Increment = new ViewModelCommand(IncrementTimesClicked, CanIncrement);
            this.Flip = new ViewModelCommand(GetHeadsOrTails, CanFlip);
            this.SetRandomNums = new ViewModelCommand(SetRandomNumbers, CanSetRandom);
            this.Save = new ViewModelCommand(SavePage, CanSave);
            this.Load = new ViewModelCommand(LoadPage, CanLoad);

            Page = new ObservableCollection<Model>();
            Page.Add(model);
        }

        private bool CanIncrement(object parameter)
        {
            return true;
        }

        public void IncrementTimesClicked(object parameter)
        {
            model.IncrementTimesClicked();
            RaisePropertyChanged("timesClicked");
        }

        private bool CanFlip(object parameter)
        {
            return true;
        }

        public void GetHeadsOrTails(object parameter)
        {
            model.SetHeadsOrTails();
            RaisePropertyChanged("headsTailsString");           
        }

        public void SetDate()
        {
            model.SetDate();
        }

        private bool CanSetRandom(object parameter)
        {
            return true;
        }

        public void SetRandomNumbers()
        {
            model.SetRandomNumbers();
        }

        public void SetRandomNumbers(object parameter)
        {
            model.SetRandomNumbers();
            RaisePropertyChanged("firstNumber");
            RaisePropertyChanged("secondNumber");
        }

        private bool CanSetResult(object parameter)
        {
            return true;
        }

        public void SetResultString(string answer)
        {
            model.SetResultString(answer);
            RaisePropertyChanged("resultString");
        }

        private bool CanSave(object parameter)
        {
            return true;
        }

        public void SavePage(object parameter)
        {
            Page.RemoveAt(0);
            Page.Add(model);
            if (repository.entry != Page[0])
            {
                repository.entry = Page[0];
            }

            repository.Save();
        }

        private bool CanLoad(object parameter)
        {
            if(Page.Count == 0)
            {
                return false;
            }

            return true;
        }

        public void LoadPage(object parameter)
        {
            Model model = repository.Load();

            this.model = model;

            RaisePropertyChanged("timesClicked");
            RaisePropertyChanged("firstNumber");
            RaisePropertyChanged("secondNumber");
            RaisePropertyChanged("resultString");
            RaisePropertyChanged("headsTailsString");
            RaisePropertyChanged("nameString");
        }


        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class ViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public delegate void ICommandOnExecute(object parameter);

        public delegate bool ICommandOnCanExecute(object parameter);

        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        public ViewModelCommand(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
