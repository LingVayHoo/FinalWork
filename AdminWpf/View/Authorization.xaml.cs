using AdminWpf.Models;
using AuthServer.Interfaces;
using AuthServer.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private IAuth _auth;
        public Authorization()
        {
            InitializeComponent();
            _auth = new AuthApiModel();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            FWLoginRequest fWLoginRequest = new()
            {
                UserName = UsernameFileld.Text,
                Password = PasswordFileld.Text
            };

            var r = await _auth.PostLogin(fWLoginRequest);
            if (r.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                SaveToken(r);
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }

        private void SaveToken(HttpResponseMessage message)
        {
            TokenManager.WriteDataToJson(message.Content.ReadAsStringAsync().Result);
        }
    }
}
