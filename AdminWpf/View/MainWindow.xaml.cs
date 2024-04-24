using AdminWpf.Models.Content;
using AdminWpf.Models.Content.WorkTable;
using AuthServer.Interfaces;
using AuthServer.Models.Content.Requests;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using AdminWpf.Models.Content.WorkTable.MVVM;
using AdminWpf.Models.Content.HomePage;
using AdminWpf.Models.Content.Projects;
using AuthServer.Models;
using System.IO;
using AdminWpf.ViewModel.Content.Services;
using AdminWpf.Models.Content.Services;
using AuthServer.Models.Content.Services;
using AuthServer.Models.Content.Projects;
using AuthServer.Models.Content.Blog;
using AdminWpf.ViewModel.Content.Blog;
using AuthServer.Models.Contacts;
using AdminWpf.ViewModel.Content.Contacts;

namespace AdminWpf.View
{    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IRequest _request;
        private IContentModel<FWService> _serviceModel;
        private IContentModel<FWProject> _projectModel;
        private IContentModel<FWBlogItem> _blogItemModel;
        private IContentModel<FWContact> _contactsModel;

        private WTViewModel tViewModel;
        private FWTitleViewModel fWTitleViewModel;
        private FWProjectsViewModel fWProjectsViewModel;
        private FWServicesViewModel fWServicesViewModel;
        private FWBlogItemsViewModel fWBlogItemsViewModel;
        private FWContactViewModel fWContactViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _request = new RequestsApiModel();
            _serviceModel = new ContentApiModel<FWService>();
            _projectModel = new ContentWithImageApiModel<FWProject>();
            _blogItemModel = new ContentApiModel<FWBlogItem>();
            _contactsModel = new ContentApiModel<FWContact>();

            tViewModel = new WTViewModel(_request);
            fWTitleViewModel = new FWTitleViewModel();
            fWProjectsViewModel = new FWProjectsViewModel(_projectModel);
            fWServicesViewModel = new FWServicesViewModel(_serviceModel);
            fWBlogItemsViewModel = new FWBlogItemsViewModel(_blogItemModel);
            fWContactViewModel = new FWContactViewModel();
            MainAuthorization();
            Preparing();
        }

        private void MainAuthorization()
        {
            Authorization authorization = new();
            authorization.ShowDialog();

            if (authorization.DialogResult.Value)
            {
                Preparing();
            }
            else
            {
                MainAuthorization();
            }
        }


        private void Preparing()
        {
            WT_Tab.DataContext = tViewModel;
            HomePage_Tab.DataContext = fWTitleViewModel;
            Projects_Tab.DataContext = fWProjectsViewModel;
            Services_Tab.DataContext = fWServicesViewModel;
            Blog_Tab.DataContext = fWBlogItemsViewModel;
            Contacts_Tab.DataContext = fWContactViewModel;
        }


        private void MenuItemEditClick(object sender, RoutedEventArgs e)
        {
            tViewModel.SelectedRequest = WT_DataGrid.SelectedItem as FWRequestViewModel;
            //if (s == null) return;
            EditWindow edit = new EditWindow(tViewModel);
            edit.ShowDialog();
        }

        private void ProjectEditClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Name == "Project_EditClick")
            {
                ProjectEditWindow editProject;
                fWProjectsViewModel.Selected = Project_ListView.SelectedItem as FWProjectViewModel;
                if (fWProjectsViewModel.Selected != null)
                {
                    editProject = new ProjectEditWindow(fWProjectsViewModel.Selected);
                    editProject.ShowDialog();
                }
            }
            else if (menuItem.Name == "ServiceEditClick")
            {
                ProjectEditWindow editProject;
                fWServicesViewModel.Selected = Services_ListView.SelectedItem as FWServiceViewModel;
                if (fWServicesViewModel.Selected != null)
                {
                    editProject = new ProjectEditWindow(fWServicesViewModel.Selected);
                    editProject.ShowDialog();
                }
            }
            else if (menuItem.Name == "Blog_EditClick")
            {
                ProjectEditWindow editProject;
                fWBlogItemsViewModel.Selected = Blog_ListView.SelectedItem as FWBlogItemViewModel;
                if (fWBlogItemsViewModel.Selected != null)
                {
                    editProject = new ProjectEditWindow(fWBlogItemsViewModel.Selected);
                    editProject.ShowDialog();
                }
            }
                                           
        }

        private void ProjectDeleteClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Name == "Project_DeleteClick")
            {
                var s = Project_ListView.SelectedItem as FWProjectViewModel;
                if (s != null)
                {
                    MessageBoxResult messageBoxResult =
                        MessageBox.Show(
                            "Вы уверены?",
                            "Подстверждение удаления",
                            MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                        fWProjectsViewModel.DeleteProject(s.Id.ToString());
                }
            }
            else if (menuItem.Name == "ServiceDeleteClick")
            {
                var s = Services_ListView.SelectedItem as FWServiceViewModel;
                if (s != null)
                {
                    MessageBoxResult messageBoxResult =
                        MessageBox.Show(
                            "Вы уверены?",
                            "Подстверждение удаления",
                            MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                        fWServicesViewModel.DeleteService(s.Id.ToString());
                }
            }
            else if (menuItem.Name == "Blog_DeleteClick")
            {
                var s = Blog_ListView.SelectedItem as FWBlogItemViewModel;
                if (s != null)
                {
                    MessageBoxResult messageBoxResult =
                        MessageBox.Show(
                            "Вы уверены?",
                            "Подстверждение удаления",
                            MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                        fWBlogItemsViewModel.DeleteBlogItem(s.Id.ToString());
                }
            }
        }

        private void ProjectCreateClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Name == "Project_CreateClick")
            {
                AddProject addProject = new AddProject(
                    _projectModel as ContentWithImageApiModel<FWProject>, 
                    fWProjectsViewModel);
                addProject.ShowDialog();
                if (addProject.DialogResult == true)
                {
                    fWProjectsViewModel.OnPropertyChanged("FWProjects");
                }
            }
            else if (menuItem.Name == "ServiceAddClick")
            {
                AddProject addProject = new AddProject(fWServicesViewModel);
                addProject.ShowDialog();
                if (addProject.DialogResult == true)
                {
                    fWServicesViewModel.OnPropertyChanged("FWServices");
                }
            }
            else if (menuItem.Name == "Blog_CreateClick")
            {
                AddBlogItemWindow addBlogItem = new AddBlogItemWindow(fWBlogItemsViewModel);
                addBlogItem.ShowDialog();
                if (addBlogItem.DialogResult == true)
                {
                    fWBlogItemsViewModel.OnPropertyChanged("FWAllBlogItems");
                }
            }

        }
    }


}