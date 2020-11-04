using System;
using System.Collections.Generic;
using System.Text;

namespace JhipsterXamarin.Services
{
    public class ListService : IListService
    {
        public List<string> GenerateList(int nbElement)
        {
            var list = new List<string>();
            for (int i = 0; i < nbElement; i++)
            {
                list.Add(i.ToString());
            }
            return list;
        }
    }
}
