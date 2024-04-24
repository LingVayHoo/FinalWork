using AdminWpf.ViewModel.Content.Blog;
using AuthServer.Models.Content.Blog;
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
    /// Логика взаимодействия для AddBlogItemWindow.xaml
    /// </summary>
    public partial class AddBlogItemWindow : Window
    {
        public AddBlogItemWindow()
        {
            InitializeComponent();
        }

        public AddBlogItemWindow(FWBlogItemsViewModel fWBlogItemsViewModel)
        {
            InitializeComponent();
            //_services = service;
            //_contentModel = contentModel;
            fWBlogItemsViewModel.Selected = new FWBlogItemViewModel(new FWBlogItem())
            {
                IsNew = true
            };
            DataContext = fWBlogItemsViewModel;
            OkButton.Click += delegate
            {
                this.DialogResult = true;
            };
        }
    }
}
