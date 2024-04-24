using AuthServer.Interfaces;
using AuthServer.Models.Content.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models.Content.WorkTable.MVVM
{
    public class FWRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private IRequest _requestModel;

        private FWRequest _request;

        public FWRequestViewModel(FWRequest request)
        {
            _request = request;
            _requestModel = new RequestsApiModel();
        }

        public FWRequest Request 
        { 
            get { return _request; } 
        }


        public Guid Id
        {
            get { return _request.Id; }
            set
            {
                _request.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return _request.Name; }
            set
            {
                _request.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Email
        {
            get { return _request.Email; }
            set
            {
                _request.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Description
        {
            get { return _request.Description; }
            set
            {
                _request.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public DateTime CreatedTime
        {
            get { return _request.CreatedTime; }
            set
            {
                _request.CreatedTime = value;
                OnPropertyChanged("CreatedTime");
            }
        }

        public string RequestStatus
        {
            get { return _request.RequestStatus; }
            set
            {
                _request.RequestStatus = value;
                OnPropertyChanged("RequestStatus");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (Request != null) _requestModel.EditRequest(Request);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
