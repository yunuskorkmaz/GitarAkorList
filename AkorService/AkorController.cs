using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPFAkorList.AkorService
{
    public class AkorController
    {

        public string Baglan2(string link)
        {
            WebRequest SiteyeBaglantiTalebi = HttpWebRequest.Create(link);
            WebResponse GelenCevap = SiteyeBaglantiTalebi.GetResponse();
            StreamReader CevapOku = new StreamReader(GelenCevap.GetResponseStream());
            string KaynakKodlar = CevapOku.ReadToEnd();
            int IcerikBaslangicIndex = KaynakKodlar.IndexOf("") + 1;
            int IcerikBitisIndex = KaynakKodlar.Substring(IcerikBaslangicIndex).IndexOf("</html>");
            string a = KaynakKodlar.Substring(IcerikBaslangicIndex, IcerikBitisIndex).ToString();
            return a;
        }

        public string Baglan(string link)
        {
            WebRequest SiteyeBaglantiTalebi = HttpWebRequest.Create(link);
            WebResponse GelenCevap = SiteyeBaglantiTalebi.GetResponse();
            StreamReader CevapOku = new StreamReader(GelenCevap.GetResponseStream());
            string KaynakKodlar = CevapOku.ReadToEnd();
            int IcerikBaslangicIndex = KaynakKodlar.IndexOf("") + 1;
            int IcerikBitisIndex = KaynakKodlar.Substring(IcerikBaslangicIndex).IndexOf("</html>");
            string a = KaynakKodlar.Substring(IcerikBaslangicIndex, IcerikBitisIndex).ToString();
            a = a.Replace("\t", string.Empty);
            a = a.Replace("\r", string.Empty);
            a = a.Replace("\n", string.Empty);
            return a;
        }

        public ResultSearch AramaYap(string aranacakKelime)
        {

            string kelime = aranacakKelime.Replace(" ", "+");

            WebRequest SiteyeBaglantiTalebi = HttpWebRequest.Create("http://www.akormerkezi.com/ara.asp?q=" + kelime);
            WebResponse GelenCevap = SiteyeBaglantiTalebi.GetResponse();
            StreamReader CevapOku = new StreamReader(GelenCevap.GetResponseStream());
            string KaynakKodlar = CevapOku.ReadToEnd();
            int IcerikBaslangicIndex = KaynakKodlar.IndexOf("<table width=\"97%\">") + 20;
            int IcerikBitisIndex = KaynakKodlar.Substring(IcerikBaslangicIndex).IndexOf("</table>");
            string a = KaynakKodlar.Substring(IcerikBaslangicIndex, IcerikBitisIndex).ToString();
            a = a.Replace("\t", string.Empty);
            a = a.Replace("\r", string.Empty);
            a = a.Replace("\n", string.Empty);

            ResultSearch ResultSearch = new ResultSearch();

            MatchCollection match = Regex.Matches(a, "<font>Şarkıcı/Grup&nbsp;<a title=\"(.*?)\" href=\"(.*?)\">(.*?)</a></font>");

            MatchCollection m_akor = Regex.Matches(a, "<font style=\"(.*?)\"><a title=\"(.*?)\" href=\"(.*?)\">(.*?) - (.*?)</a> (.*?)</font>");

            ObservableCollection<SearchSanatci> sanatci = new ObservableCollection<SearchSanatci>();

            for (int i = 0; i < match.Count; i++)
            {
                sanatci.Add(new SearchSanatci() { Adi = match[i].Groups[1].Value, Link = match[i].Groups[2].Value });

            }
            ResultSearch.Sanatcilar = sanatci;

            ObservableCollection<SearchAkor> akor = new ObservableCollection<SearchAkor>();

            for (int i = 0; i < m_akor.Count; i++)
            {

                if (m_akor[i].Groups[6].Value == "(AKOR)")
                {
                    akor.Add(new SearchAkor() { Link = m_akor[i].Groups[3].Value, Sanatci = m_akor[i].Groups[4].Value, SarkiAdi = m_akor[i].Groups[5].Value });
                }
            }
            ResultSearch.Akorlar = akor;
            return ResultSearch;
        }


        public ObservableCollection<SearchAkor> SanatciAkorList(string link)
        {
            ObservableCollection<SearchAkor> result = new ObservableCollection<SearchAkor>();

            string a = Baglan(link);


            Match match = Regex.Match(a, "<br><br><font>AkorMerkezi.com'da <b>(.*?)</b> içerikleri:</font><ul><li><a title=\"(.*?) Gitar Akorları ve Tabları\" href=\"(.*?)\"><font>(.*?) Gitar Akorları ve Tabları</font></a></li>");

            string sanatci = match.Groups[4].Value;
            string linka = match.Groups[3].Value;



            a = Baglan(linka);

            MatchCollection akorlis = Regex.Matches(a, "<td id=\"listesatir\"><a title=\"(.*?)\" href=\"(.*?)\"><font class=\"liste\">(.*?)</font></a></td><td id=\"listesatir\"><font class=\"liste\">(.*?)</font></td>");


            for (int i = 0; i < akorlis.Count; i++)
            {
                if (akorlis[i].Groups[4].Value == "AKOR")
                {
                    result.Add(new SearchAkor() { Sanatci = sanatci, Link = akorlis[i].Groups[2].Value, SarkiAdi = akorlis[i].Groups[3].Value });
                }

            }



            return result;
        }


        public Akor AkorDetayGetir(string link)
        {
            Akor result = new Akor();

            string kaynak = Baglan(link);


           Match m_sanatciadi = Regex.Match(kaynak, "<title>(.*?) - (.*?) - (.*?)</title>");

            Match bilgi = Regex.Match(kaynak, "<font class=\"i3\">(.*?)</font></a><font class=\"i3\"> - (.*?) - Akor</font>");

            string sarkiadi = bilgi.Groups[2].Value;


            Match match = Regex.Match(kaynak, "<div id=\"printsong_box\">(.*?)<br>" + sarkiadi + " Akor, AkorMerkezi.com'da yayınlanmıştır.");

            string re = match.Groups[1].Value;
            re = re.Replace("&nbsp;", " ");
            re = re.Replace("<br>", Environment.NewLine);
            re = re.Replace("<a href='http://akor.ws/'>i</a>", "");

            result.Sanatci = m_sanatciadi.Groups[1].Value;
            result.SarkiAdi = sarkiadi;
            result.AkorDetay = re;

            return result;

        }

    }
}
