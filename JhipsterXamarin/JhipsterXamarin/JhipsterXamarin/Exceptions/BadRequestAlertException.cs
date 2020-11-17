using JhipsterXamarin.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JhipsterXamarin.Exceptions
{
    public class BadRequestAlertException : BaseException
    {
        public BadRequestAlertException(string detail, string entityName, string errorKey) : this(
            ErrorConstants.DefaultType, detail, entityName, errorKey)
        {
        }

        public BadRequestAlertException(string type, string detail, string entityName, string errorKey) : base(type, detail, entityName, errorKey)
        {
        }
    }
}
