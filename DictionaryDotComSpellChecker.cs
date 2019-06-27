using System;
using System.Net.Http;
using System.Net;
using System.Text;  // for class Encoding
using System.IO;    // for StreamReader

using SpellChecker.Contracts;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SpellChecker.Core
{

    /// <summary>
    /// This is a dictionary based spell checker that uses dictionary.com to determine if
    /// a word is spelled correctly.
    /// 
    /// The URL to do this looks like this: http://dictionary.reference.com/browse/<word>
    /// where <word> is the word to be checked.
    /// 
    /// We look for something in the response that gives us a clear indication whether the
    /// word is spelled correctly or not.
    /// </summary>
    public class DictionaryDotComSpellChecker :
        ISpellChecker
    {
        string url = "http://dictionary.reference.com/browse/";

        public bool Check(string word)
        {
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["word"] = word;

                url = url + word;
                try
                {
                    var response = wb.UploadValues(url, "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);

                    if (responseInString.Contains("description"))
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }


            }

            
        }

    }



}
