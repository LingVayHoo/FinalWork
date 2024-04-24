using AdminWpf.Models.Content;
using AuthServer.Interfaces;
using AuthServer.Models.Contacts;
using AuthServer.Models.Content.Projects;
using AuthServer.Models.Title;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.ViewModel.Content.Contacts
{
    public class FWContactViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private IContentModel<FWContact> _contentModel;

        private FWContact _contact;

        public FWContactViewModel()
        {
            _contentModel = new ContentApiModel<FWContact>();
            _contact = _contentModel.GetAllContent().ElementAt(0);
        }

        public FWContact Contact
        { 
            get { return _contact; } 
        }

        public Guid Id
        {
            get { return _contact.Id; }
            set
            {
                _contact.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Address
        {
            get { return _contact.Address; }
            set
            {
                _contact.Address = value;
                OnPropertyChanged("Address");
            }
        }

        public string Email
        {
            get { return _contact.Email; }
            set
            {
                _contact.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Phone
        {
            get { return _contact.Phone; }
            set
            {
                _contact.Phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (Contact != null) _contentModel.EditContent(Contact);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
