using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoboG
{
    public static class Translations
    {
        private static Dictionary<String, String> _Languages = new Dictionary<String, String>();
        public static Dictionary<String, String> Languages { get { return _Languages; } }

        private static Dictionary<String, Dictionary<String, String>> _Translations = new Dictionary<string, Dictionary<String, String>>();

        public static String Get(String Name)
        {
            try
            {
                return Get(Settings.Language, Name);
            }
            catch (Exception ex)
            {
                HelperFunctions.ShowError(ex);
            }
            return "";
        }

        /// <summary>A translated text with values filled in: Format("JobsRunning", 3) with "{0} running" gives "3 running".</summary>
        public static String Format(String Name, params object[] Args)
        {
            String Text = Get(Name);
            try
            {
                return String.Format(Text, Args);
            }
            catch (FormatException)
            {
                return Text;
            }
        }

        /// <summary>Sets a control's text from the translations; leaves it as designed if the key is missing everywhere.</summary>
        public static void Set(System.Windows.Forms.Control Control, String Name)
        {
            String Text = Get(Name);
            if (Text != "") Control.Text = Text;
        }

        public static void Set(System.Windows.Forms.ToolStripItem Item, String Name)
        {
            String Text = Get(Name);
            if (Text != "") Item.Text = Text;
        }

        public static String Get(String Language, String Name)
        {
            try
            {
                if (!_Translations.ContainsKey(Language)) return "";
                if (_Translations[Language].ContainsKey(Name)) return _Translations[Language][Name];
                else if (Language != "en-US" && _Translations["en-US"].ContainsKey(Name))
                {
                    return _Translations["en-US"][Name];
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.ShowError(ex);
            }
            return "";
        }

        public static bool LoadTranslations()
        {
            try
            {
                _Translations.Clear();
                String Path = System.Windows.Forms.Application.StartupPath + @"\Translations.xml";
                if (!System.IO.File.Exists(Path))
                {
                    HelperFunctions.ShowError("Could not find the file Translations.xml.");
                    return false;
                }
                using (XmlTextReader Rd = HelperFunctions.LoadXMLFile(Path))
                {
                    Rd.WhitespaceHandling = WhitespaceHandling.Significant;
                    String Language = null;
                    int Level = 0;
                    while (Rd.Read())
                    {
                        if (Rd.NodeType == XmlNodeType.Element)
                        {
                            Level++;
                            if (Level == 2)
                            {
                                Language = Rd.Name;
                                String Name = Rd.GetAttribute("name");
                                if (Name == null) Name = "";
                                _Languages.Add(Name, Language);
                                _Translations.Add(Rd.Name, new Dictionary<String, String>());
                                _Translations[Language].Add("Name", Name);
                            }
                            else if (Level == 3 && Language != null)
                            {
                                if (!_Translations[Language].ContainsKey(Rd.Name)) _Translations[Language].Add(Rd.Name, Rd.ReadString());
                                else HelperFunctions.ShowError("Translation name is used more than once: " + Rd.Name);
                            }
                        }
                        if (Rd.NodeType == XmlNodeType.EndElement)
                        {
                            Level--;
                        }
                    }
                    return true;
                }
            }
            catch (XmlException ex)
            {
                HelperFunctions.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                HelperFunctions.ShowError(ex.Message);
            }
            return false;
        }
    }

    public class Translation
    {
        private String _Language = "";
        public String Language { get { return _Language; } set { _Language = value; } }

        private Dictionary<String, String> _Translations = new Dictionary<string,string>();
        public Dictionary<String, String> Translations { get { return _Translations; } }

        public Translation(String Language)
        {
            _Language = Language;
        }
    }
}
