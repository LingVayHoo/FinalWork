using AdminWpf.Models.Content;
using AuthServer.Interfaces;
using AuthServer.Models.Content.Blog;
using AuthServer.Models.Content.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.ViewModel.Content.Blog
{
    public class FWBlogItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IContentModel<FWBlogItem> _contentModel;
        private FWBlogItem _blogItem;
        private bool _isNew;

        public FWBlogItemViewModel(FWBlogItem blogItem)
        {
            _blogItem = blogItem;
            _contentModel = new ContentApiModel<FWBlogItem>();
        }

        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        public FWBlogItem BlogItem
        {
            get { return _blogItem; }
        }

        public Guid Id
        {
            get { return _blogItem.Id; }
            set
            {
                _blogItem.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Title
        {
            get { return _blogItem.Title; }
            set
            {
                _blogItem.Title = value;
                OnPropertyChanged("Title");
            }
        }

        public string ShortDescription
        {
            get { return _blogItem.ShortDescription; }
            set
            {
                _blogItem.ShortDescription = value;
                OnPropertyChanged("ShortDescription");
            }
        }

        public string Description
        {
            get { return _blogItem.Description; }
            set
            {
                _blogItem.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public DateTime PublicationDate
        {
            get { return _blogItem.PublicationDate; }
            set 
            { 
                _blogItem.PublicationDate = value;
                OnPropertyChanged("PublicationDate");
            }
            
        }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            EditProj(prop);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private async void EditProj(string prop)
        {
            if (BlogItem != null && !IsNew)
            {
                if (prop != "PublicationDate")
                {
                    PublicationDate = DateTime.Now;
                    await _contentModel.EditContent(BlogItem);
                }
            }
        }
    }
}
