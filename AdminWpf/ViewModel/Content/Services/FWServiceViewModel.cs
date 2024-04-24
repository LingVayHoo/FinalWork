using AdminWpf.Models.Content;
using AdminWpf.Models.Content.Services;
using AuthServer.Interfaces;
using AuthServer.Models.Content.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.ViewModel.Content.Services
{
    public class FWServiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IContentModel<FWService> _contentModel;
        private FWService _service;
        private bool isNew;

        public FWServiceViewModel(FWService Service)
        {
            _service = Service;
            _contentModel = new ContentApiModel<FWService>();
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public FWService Service
        {
            get { return _service; }
        }

        public Guid Id
        {
            get { return _service.Id; }
            set
            {
                _service.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Title
        {
            get { return _service.Title; }
            set
            {
                _service.Title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get { return _service.Description; }
            set
            {
                _service.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            EditProj();
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private async void EditProj()
        {

            if (Service != null && !IsNew)
            {
                await _contentModel.EditContent(Service);
            }
        }
    }
}
