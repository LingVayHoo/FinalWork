using AdminWpf.Models.Content.Services;
using AdminWpf.Models.Content.WorkTable.MVVM;
using AuthServer.Interfaces;
using AuthServer.Models.Content.Services;
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

namespace AdminWpf.ViewModel.Content.Services
{
    public class FWServicesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //private IServices _services;
        private IContentModel<FWService> _contentModel;
        private FWServiceViewModel? _selectedService;

        public ObservableCollection<FWServiceViewModel> FWServices
        {
            get
            {
                //return Convert<FWServiceViewModel>(_services.GetServices());
                return Convert<FWServiceViewModel>(_contentModel.GetAllContent());
            }
            set { }
        }

        public FWServiceViewModel? Selected
        {
            get
            {
                return _selectedService;
            }
            set
            {
                _selectedService = value;

            }
        }

        public FWServicesViewModel(IContentModel<FWService> contentModel)  //IServices Services)
        {
            //_services = Services;
            _contentModel = contentModel;
        }

        public async void DeleteService(string id)
        {
            //await _services.DeleteService(id);
            await _contentModel.DeleteContent(id);
            OnPropertyChanged("FWServices");
        }

        private ObservableCollection<T> Convert<T>(IEnumerable original)
        {
            List<FWServiceViewModel> converted = new List<FWServiceViewModel>();
            foreach (var e in original)
            {
                converted.Add(new FWServiceViewModel(e as FWService));
            }
            return new ObservableCollection<T>(converted.Cast<T>());
        }

        public ICommand OkButton
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    if (Selected != null)
                    {
                        Selected.IsNew = false;
                        //await _services.CreateService(Selected.Service);
                        await _contentModel.CreateContent(Selected.Service);
                        OnPropertyChanged("FWServices");
                    }
                });
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
