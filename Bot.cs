using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPFAkorList
{
    public class Bot
    {
        public string[] func(string sa)
        {
            WebRequest SiteyeBaglantiTalebi = HttpWebRequest.Create("http://www.akormerkezi.com/ara.asp?q=" + sa);
            WebResponse GelenCevap = SiteyeBaglantiTalebi.GetResponse();
            StreamReader CevapOku = new StreamReader(GelenCevap.GetResponseStream());
            string KaynakKodlar = CevapOku.ReadToEnd();
            int IcerikBaslangicIndex = KaynakKodlar.IndexOf("<table width=\"97%\">") + 20;
            int IcerikBitisIndex = KaynakKodlar.Substring(IcerikBaslangicIndex).IndexOf("</table>");

            string a = KaynakKodlar.Substring(IcerikBaslangicIndex, IcerikBitisIndex).ToString();

            a = a.Replace("\t", string.Empty);
            a = a.Replace("\r", string.Empty);
            a = a.Replace("\n", string.Empty);



            string title = "";

            MatchCollection match = Regex.Matches(a, "<font>Şarkıcı/Grup&nbsp;<a title=\"(.*?)\" href=\"(.*?)\">(.*?)</a></font>");




            string[] ID = new string[match.Count];
            for (int i = 0; i < match.Count; i++)
            {
                ID[i] = match[i].Groups[1].Value;
            }


            return ID;





        }
    }
}
