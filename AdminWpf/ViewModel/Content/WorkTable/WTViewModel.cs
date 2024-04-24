using AuthServer.Interfaces;
using AuthServer.Models.Content.Requests;
using Azure.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminWpf.Models.Content.WorkTable.MVVM
{
    public class WTViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private FWRequestViewModel? selectedRequest;
        private FilteredRequests filteredRequests;
        private IRequest _request;
        private DateTime _start;
        private DateTime _end;
        private IList<string> statuses = new List<string>
        {
            new string("Получена"),
            new string("В работе"),
            new string("Выполнена"),
            new string("Отклонена"),
            new string("Отменена")
        };

        public ObservableCollection<FWRequestViewModel> FWRequests 
        { 
            get
            {
                return Convert<FWRequestViewModel>(GetFilteredRequests());
            }
        }

        public ObservableCollection<string> RequestStatuses = new ObservableCollection<string>
        {
            new string("Получена"),
            new string("В работе"),
            new string("Выполнена"),
            new string("Отклонена"),
            new string("Отменена")
        };

        public IList<string> Statuses
        {
            get { return statuses; }
            set { statuses = value; }
        }

        public FWRequestViewModel? SelectedRequest
        {
            get 
            { 
                return selectedRequest; 
            }
            set
            {
                selectedRequest = value;
            }
        }

        public DateTime Start
        {
            get { return _start; }
            set
            {
                _start = GetTime(value, true);
            }
        }

        private DateTime StartFinal { get; set; }

        public DateTime End
        {
            get { return _end; }
            set
            {
                _end = GetTime(value, false);
            }
        }

        private DateTime EndFinal { get; set; }

        public string TotalRequestsDisplayed
        {
            get
            {
                return $"Всего {FWRequests.Count} заявок";                
            }
            set { }
        }

        #region FilterButtons
        public ICommand ClickToday
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    StartFinal = DateTime.Today;
                    EndFinal = DateTime.Today.AddHours(24);
                    OnPropertyChanged("FWRequests");
                    OnPropertyChanged("TotalRequestsDisplayed");
                });
            }
        }

        public ICommand ClickYesterday
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    StartFinal = DateTime.Today.AddHours(-24);
                    EndFinal = DateTime.Today;
                    OnPropertyChanged("FWRequests");
                    OnPropertyChanged("TotalRequestsDisplayed");
                });
            }
        }

        public ICommand ClickWeek
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    StartFinal = DateTime.Today.AddDays(-6);
                    EndFinal = DateTime.Today.AddHours(24);
                    OnPropertyChanged("FWRequests");
                    OnPropertyChanged("TotalRequestsDisplayed");
                });
            }
        }

        public ICommand ClickMonth
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    StartFinal = DateTime.Today.AddDays(-27);
                    EndFinal = DateTime.Today.AddHours(24);
                    OnPropertyChanged("FWRequests");
                    OnPropertyChanged("TotalRequestsDisplayed");
                });
            }
        }

        public ICommand ClickCustom
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    StartFinal = Start;
                    EndFinal = End;
                    OnPropertyChanged("FWRequests");
                    OnPropertyChanged("TotalRequestsDisplayed");
                });
            }
        }
        #endregion
        public WTViewModel(IRequest request)
        {
            _request = request;
            filteredRequests = new();
            Start = new DateTime(2024, 01, 01, 0, 0, 0);
            StartFinal = Start;
            End = DateTime.Today.AddHours(24);
            EndFinal = End;
        }



        private ObservableCollection<T> Convert<T>(IEnumerable original)
        {
            List<FWRequestViewModel> converted = new List<FWRequestViewModel>();
            foreach (var e in original)
            {
                converted.Add(new FWRequestViewModel(e as FWRequest));
            }
            return new ObservableCollection<T>(converted.Cast<T>());
        }

        private DateTime GetTime(DateTime? value, bool isStart)
        {
            if (value == null)
            {
                if (isStart) return DateTime.MinValue;
                else return DateTime.Today.AddHours(24);
            }
            else if (value > DateTime.Today)
            {
                return DateTime.Today.AddHours(24);
            }
            else
            {
                return value.Value;
            }
        }

        private IEnumerable<FWRequest> GetFilteredRequests()
        {
            return filteredRequests.GetFilteredData(
                _request.GetRequests(),
                StartFinal,
                EndFinal);
        }



        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
