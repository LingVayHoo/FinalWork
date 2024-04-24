using AdminWpf.Models.Content.WorkTable.MVVM;
using AuthServer.Interfaces;
using AuthServer.Models.Content.Projects;
using AuthServer.Models.Content.Requests;
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

namespace AdminWpf.Models.Content.Projects
{
    public class FWProjectsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private IContentModel<FWProject> _contentModel;
        private FWProjectViewModel? _selectedProject;

        public ObservableCollection<FWProjectViewModel> FWProjects
        {
            get
            {
                return Convert<FWProjectViewModel>(_contentModel.GetAllContent());
            }
            set { }
        }

        public FWProjectViewModel? Selected
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;

            }
        }

        public FWProjectsViewModel(IContentModel<FWProject> contentModel)
        {
            _contentModel = contentModel;
        }

        public async void DeleteProject(string id)
        {
            await _contentModel.DeleteContent(id);
            OnPropertyChanged("FWProjects");
        }

        private ObservableCollection<T> Convert<T>(IEnumerable original)
        {
            List<FWProjectViewModel> converted = new List<FWProjectViewModel>();
            foreach (var e in original)
            {
                converted.Add(new FWProjectViewModel(e as FWProject));
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
                        await _contentModel.CreateContent(Selected.Project);
                        OnPropertyChanged("FWProjects");
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
