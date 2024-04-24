using AdminWpf.Models.Content.WorkTable.MVVM;
using AuthServer.Interfaces;
using AuthServer.Models.Content.Blog;
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

namespace AdminWpf.ViewModel.Content.Blog
{
    public class FWBlogItemsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //private IServices _services;
        private IContentModel<FWBlogItem> _contentModel;
        private FWBlogItemViewModel? _selectedService;

        public ObservableCollection<FWBlogItemViewModel> FWAllBlogItems
        {
            get
            {
                return Convert<FWBlogItemViewModel>(_contentModel.GetAllContent());
            }
            set { }
        }

        public FWBlogItemViewModel? Selected
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

        public FWBlogItemsViewModel(IContentModel<FWBlogItem> contentModel)  //IServices Services)
        {
            //_services = Services;
            _contentModel = contentModel;
        }

        public async void DeleteBlogItem(string id)
        {
            //await _services.DeleteService(id);
            await _contentModel.DeleteContent(id);
            OnPropertyChanged("FWAllBlogItems");
        }

        private ObservableCollection<T> Convert<T>(IEnumerable original)
        {
            List<FWBlogItemViewModel> converted = new List<FWBlogItemViewModel>();
            foreach (var e in original)
            {
                converted.Add(new FWBlogItemViewModel(e as FWBlogItem));
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
                        Selected.PublicationDate = DateTime.Now;
                        Selected.IsNew = false;                        
                        //await _services.CreateService(Selected.Service);
                        await _contentModel.CreateContent(Selected.BlogItem);
                        OnPropertyChanged("FWAllBlogItems");
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
