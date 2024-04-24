using AdminWpf.Models.Content;
using AdminWpf.Models.Content.Projects;
using AdminWpf.Models.Content.Services;
using AdminWpf.ViewModel.Content.Blog;
using AdminWpf.ViewModel.Content.Services;
using AuthServer.Interfaces;
using AuthServer.Models;
using AuthServer.Models.Content.Blog;
using AuthServer.Models.Content.Projects;
using AuthServer.Models.Content.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdminWpf.View
{
    /// <summary>
    /// Логика взаимодействия для AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        //private IProjects? _projects;
        //private IServices? _services;
        private IContentModel<FWProject>? _contentModel;
        private FWProjectsViewModel? _projectsViewModel;
        public AddProject()
        {
            InitializeComponent();
            //_projects = new ProjectsApiModel();
            //_services = new ServicesApiModel();
            _contentModel = new ContentApiModel<FWProject>();
        }

        public AddProject(ContentWithImageApiModel<FWProject>? contentModel, FWProjectsViewModel fWProjectsViewModel)
        {
            InitializeComponent();
            //_projects = projects;
            _contentModel = contentModel;
            fWProjectsViewModel.Selected = new FWProjectViewModel(new FWProject())
            {
                IsNew = true
            };
            _projectsViewModel = fWProjectsViewModel;
            DataContext = _projectsViewModel;
            OkButton.IsEnabled = false;
            OkButton.Click += delegate
            {
                this.DialogResult = true;
            };
        }

        public AddProject(FWServicesViewModel fWServicesViewModel)
        {
            InitializeComponent();
            //_services = service;
            //_contentModel = contentModel;
            fWServicesViewModel.Selected = new FWServiceViewModel(new FWService())
            {
                IsNew = true
            };
            DataContext = fWServicesViewModel;
            UploadImageButton.Visibility = Visibility.Hidden;
            OkButton.Click += delegate
            {
                this.DialogResult = true;
            };
        }

        private async void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Jpg files(*.jpg)|*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                if (_contentModel != null && _projectsViewModel != null)
                {
                    var r = await (_contentModel as ContentWithImageApiModel<FWProject>).UploadFile(openFileDialog.FileName);
                    if (_projectsViewModel.Selected != null)
                    {
                        _projectsViewModel.Selected.ImgName = r.Content.ReadAsStringAsync().Result;
                        OkButton.IsEnabled = true;
                    }
                }
            }
        }
    }
}
