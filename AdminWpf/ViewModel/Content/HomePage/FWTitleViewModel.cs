using AuthServer.Interfaces;
using AuthServer.Models.Content.Requests;
using AuthServer.Models.Title;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models.Content.HomePage
{
    internal class FWTitleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ITitle _titleModel;

        private FWTitle _title;

        public FWTitleViewModel()
        {
            _titleModel = new TitleApiModel();
            _title = _titleModel.GetTitles().ElementAt(0);
        }

        public FWTitle Title
        {
            get { return _title; }
        }

        public Guid Id
        {
            get { return _title.Id; }
            set
            {
                _title.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string MainTitle
        {
            get { return _title.MainTitle; }
            set
            {
                _title.MainTitle = value;
                OnPropertyChanged("MainTitle");
            }
        }

        public string QuoteTitle
        {
            get { return _title.QuoteTitle; }
            set
            {
                _title.QuoteTitle = value;
                OnPropertyChanged("QuoteTitle");
            }
        }

        public string ButtonTitle
        {
            get { return _title.ButtonTitle; }
            set
            {
                _title.ButtonTitle = value;
                OnPropertyChanged("ButtonTitle");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (Title != null) _titleModel.EditTitle(Title);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
