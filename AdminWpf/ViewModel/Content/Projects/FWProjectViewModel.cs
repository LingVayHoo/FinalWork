using AdminWpf.Models.Content.WorkTable.MVVM;
using AuthServer.Interfaces;
using AuthServer.Models.Content.Projects;
using AuthServer.Models.Content.Requests;
using AuthServer.Models.Content.Services;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace AdminWpf.Models.Content.Projects
{
    public class FWProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IContentModel<FWProject> _contentModel;
        private FWProject _project;
        private bool isNew;

        public FWProjectViewModel(FWProject project)
        {
            _project = project;
            _contentModel = new ContentApiModel<FWProject>();
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public FWProject Project
        {
            get { return _project; }
        }

        public Guid Id
        {
            get { return _project.Id; }
            set
            {
                _project.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Title
        {
            get { return _project.Title; }
            set
            {
                _project.Title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get { return _project.Description; }
            set
            {
                _project.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public string ImgName
        {
            get { return _project.ImgName; }
            set
            {
                _project.ImgName = value;
                OnPropertyChanged("ImgName");
            }
        }

        public string ImgPath
        {
            get
            {
                return $"https://localhost:7195/api/FWProjects/DownloadFile/{ImgName}";
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
            
            if (Project != null && !IsNew)
            {
                await _contentModel.EditContent(Project);
            }           
        }

        
    }
}
