using AdminWpf.Models.Content;
using AdminWpf.Models.Content.WorkTable.MVVM;
using AuthServer.Models.Content.Requests;
using AuthServer.Models.Requests;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow(WTViewModel tViewModel)
        {
            InitializeComponent();
            DataContext = tViewModel;
            OkButton.Click += delegate
            {
                this.DialogResult = true;
            };
        }
    }
}
