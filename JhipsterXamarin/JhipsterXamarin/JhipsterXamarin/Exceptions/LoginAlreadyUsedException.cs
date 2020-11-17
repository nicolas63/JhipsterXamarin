using JhipsterXamarin.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JhipsterXamarin.Exceptions
{
    public class LoginAlreadyUsedException : BadRequestAlertException
    {
        public LoginAlreadyUsedException() : base(ErrorConstants.LoginAlreadyUsedType, "Login name is already in use!",
            "userManagement", "userexists")
        {
        }
    }
}
