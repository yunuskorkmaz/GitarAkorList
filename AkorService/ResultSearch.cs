using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAkorList.AkorService
{
    public class ResultSearch
    {
        public ObservableCollection<SearchSanatci> Sanatcilar
        {
            get;
            set;
        }
        public ObservableCollection<SearchAkor> Akorlar
        {
            get;
            set;
        }
    }
}
