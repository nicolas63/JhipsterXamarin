using System;
using System.Collections.Generic;
using System.Text;

namespace JhipsterXamarin.Services
{
    public interface IListService
    {
        List<string> GenerateList(int nb);
    }
}
