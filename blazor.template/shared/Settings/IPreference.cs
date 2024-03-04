using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Settings
{
    public interface IPreference
    {
        public string LanguageCode { get; set; }
    }
}
