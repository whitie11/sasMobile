using SASMobileApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SASMobileApp1.Interfaces
{
  public interface IAppDataService
    {
      TokenResponseModel GetTokenResponseModel();
      void SaveTokenResponseModel(TokenResponseModel trm);
      
    }
}
