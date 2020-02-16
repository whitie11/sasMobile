using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Models;
using Xamarin.Forms;

namespace SASMobileApp1.Services
{
    public class AppDataService : IAppDataService
    {
        public TokenResponseModel GetTokenResponseModel()
        {
            TokenResponseModel tokenResponseModel = new TokenResponseModel();

            if (Application.Current.Properties.ContainsKey("AccessToken"))
            {
                tokenResponseModel.AccessToken = Application.Current.Properties["AccessToken"] as string;
            }
            else
            {
                tokenResponseModel.AccessToken = "";
            }

            if (Application.Current.Properties.ContainsKey("TokenType"))
            {
                tokenResponseModel.TokenType = Application.Current.Properties["TokenType"] as string;
            }
            else
            {
                tokenResponseModel.TokenType = "";
            }

            if (Application.Current.Properties.ContainsKey("ExpiresIn"))
            {
                tokenResponseModel.ExpiresIn = (int)Application.Current.Properties["ExpiresIn"];
            }
            else
            {
                tokenResponseModel.ExpiresIn = 0;
            }

            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                tokenResponseModel.UserName = Application.Current.Properties["UserName"] as string;
            }
            else
            {
                tokenResponseModel.UserName = "";
            }

            if (Application.Current.Properties.ContainsKey("IssuedAt"))
            {
                tokenResponseModel.IssuedAt = Application.Current.Properties["IssuedAt"] as string;
            }
            else
            {
                tokenResponseModel.IssuedAt = "";
            }

            if (Application.Current.Properties.ContainsKey("ExpiresAt"))
            {
                tokenResponseModel.ExpiresAt = Application.Current.Properties["ExpiresAt"] as string;
            }
            else
            {
                tokenResponseModel.ExpiresAt = "";
            }

            return tokenResponseModel;
        }



        public void SaveTokenResponseModel(TokenResponseModel trm)
        {
            Application.Current.Properties["AccessToken"] = trm.AccessToken;
            Application.Current.Properties["TokenType"] = trm.TokenType;
            Application.Current.Properties["ExpiresIn"] = trm.ExpiresIn;
            Application.Current.Properties["UserName"] = trm.UserName;
            Application.Current.Properties["IssuedAt"] = trm.IssuedAt;
            Application.Current.Properties["ExpiresAt"] = trm.ExpiresAt;
        }

        //public void ClearApplication()
        //{
        //    Application.Current.Properties.Clear();
        //}



        private string SelectedWard
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("SelectedWard"))
                {
                    return Application.Current.Properties["SelectedWard"] as string;
                }
                else 
                {
                    return "";
                }
            }
            set
            {
                Application.Current.Properties["SelectedWard"] = value;
            }
        }


        public string getSelectedWard()
        {
           
            return SelectedWard;
        }

        public void setSelectedWard(string ward)
        {
            SelectedWard = ward;
        }
    }
}




