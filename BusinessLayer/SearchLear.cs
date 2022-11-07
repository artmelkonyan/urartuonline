using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class SearchLear
    {
        public List<string> GetArm(string word)
        {
            if (word == null)
                return new List<string>();
            Dictionary<string, string> wordsEn = new Dictionary<string, string>();
            Dictionary<string, string> wordsEn1 = new Dictionary<string, string>();
            Dictionary<string, string> wordsEn2 = new Dictionary<string, string>();

            #region wordEn
            wordsEn2.Add("ch", "ճ");
            wordsEn2.Add("gh", "ղ");
            wordsEn2.Add("ev", "և");

            #endregion
            #region wordEn Simpl
            wordsEn.Add("a", "ա");
            wordsEn.Add("b", "բ");
            wordsEn.Add("e", "ե");
            wordsEn.Add("y", "ը");
            wordsEn.Add("i", "ի");
            wordsEn.Add("l", "լ");
            wordsEn.Add("g", "գ");
            wordsEn.Add("x", "խ");
            wordsEn.Add("c", "ծ");
            wordsEn.Add("k", "կ");
            wordsEn.Add("q", "ք");
            wordsEn.Add("h", "հ");
            wordsEn.Add("d", "դ");
            wordsEn.Add("m", "մ");
            wordsEn1.Add("y", "յ");
            wordsEn.Add("n", "ն");
            wordsEn.Add("p", "պ");
            wordsEn.Add("j", "ջ");
            wordsEn.Add("s", "ս");
            wordsEn.Add("w", "վ");
            wordsEn.Add("v", "վ");
            wordsEn.Add("t", "տ");
            wordsEn.Add("r", "ր");
            wordsEn.Add("o", "օ");
            wordsEn.Add("u", "ու");
            wordsEn.Add("z", "զ");
            #endregion
            #region wordEn1 Word 2 and doubl
            wordsEn1.Add("jh", "ժ");
            wordsEn1.Add("j", "ժ");
            wordsEn1.Add("gh", "խ");
            wordsEn1.Add("tc", "ծ");
            wordsEn1.Add("dz", "ձ");
            wordsEn1.Add("ch", "չ");
            wordsEn1.Add("sh", "շ");
            wordsEn1.Add("vo", "ո");
            wordsEn1.Add("g", "ջ");
            wordsEn1.Add("t", "թ");
            wordsEn1.Add("ph", "փ");
            #endregion
            var w1 = Translate(word, wordsEn);
            w1 = Translate(w1, wordsEn1);
            w1 = Translate(w1, wordsEn2);
            var w2 = Translate(word, wordsEn1);
            w2 = Translate(w2, wordsEn2);
            w2 = Translate(w2, wordsEn);
            var w3 = Translate(word, wordsEn2);
            w3 = Translate(w3, wordsEn1);
            w3 = Translate(w3, wordsEn);
            var w4 = Translate(word, wordsEn1);
            w4 = Translate(w4, wordsEn);
            var w5 = Translate(word, wordsEn2);
            w5 = Translate(w5, wordsEn);
            var w6 = Translate(word, wordsEn2);
            w6 = Translate(w6, wordsEn);

            var wC1 = word.Replace("c", "ց");
            wC1 = Translate(wC1, wordsEn);
            var wC2 = word.Replace("c", "ց");
            wC2 = Translate(wC2, wordsEn1);
            wC2 = Translate(wC2, wordsEn2);
            wC2 = Translate(wC2, wordsEn);
            var wC3 = word.Replace("c", "ց");
            wC3 = Translate(wC3, wordsEn2);
            wC3 = Translate(wC3, wordsEn1);
            wC3 = Translate(wC3, wordsEn);
            var wC4 = word.Replace("c", "ց");
            wC4 = Translate(wC4, wordsEn1);
            wC4 = Translate(wC4, wordsEn);
            var wC5 = word.Replace("c", "ց");
            wC5 = Translate(wC5, wordsEn1);
            wC5 = Translate(wC5, wordsEn);
            //"r", "ռ"
            var wR1 = word.Replace("r", "ռ");
            wR1 = Translate(wR1, wordsEn);
            var wR2 = word.Replace("r", "ռ");
            wR2 = Translate(wR2, wordsEn1);
            wR2 = Translate(wR2, wordsEn2);
            wR2 = Translate(wR2, wordsEn);
            var wR3 = word.Replace("r", "ռ");
            wR3 = Translate(wR3, wordsEn2);
            wR3 = Translate(wR3, wordsEn1);
            wR3 = Translate(wR3, wordsEn);
            var wR4 = word.Replace("r", "ռ");
            wR4 = Translate(wR4, wordsEn1);
            wR4 = Translate(wR4, wordsEn);
            var wR5 = word.Replace("r", "ռ");
            wR5 = Translate(wR5, wordsEn1);
            wR5 = Translate(wR5, wordsEn);
            ///"e", "է"
            var wE1 = word.Replace("e", "է");
            wE1 = Translate(wE1, wordsEn);
            var wE2 = word.Replace("e", "է");
            wE2 = Translate(wE2, wordsEn1);
            wE2 = Translate(wE2, wordsEn2);
            wE2 = Translate(wE2, wordsEn);
            var wE3 = word.Replace("e", "է");
            wE3 = Translate(wE3, wordsEn2);
            wE3 = Translate(wE3, wordsEn1);
            wE3 = Translate(wE3, wordsEn);
            var wE4 = word.Replace("e", "է");
            wE4 = Translate(wE4, wordsEn1);
            wE4 = Translate(wE4, wordsEn);
            var wE5 = word.Replace("e", "է");
            wE5 = Translate(wE5, wordsEn1);
            wE5 = Translate(wE5, wordsEn);
            var res = new List<string>() { w1, w2, w3, w4, w5, w6, wC1, wC2, wC3, wC4, wC5, wR1, wR2, wR3, wR4, wR5, wE1, wE2, wE3, wE4, wE5 };
            res = res.Distinct().ToList();
            return res;
        }
        public List<string> HardTranslate(string word)
        {
            var check = GetArm(word);
            if (check.Count < 2)
                return check;
            var words = new List<Tuple<string, List<string>>>();
            for (int i = 0; i < word.Length; i++)
            {
                var select = word[i].ToString();
                var t = GetArm(select);
                words.Add(new Tuple<string, List<string>>(select, t));
            }
            int variationCount = 1;
            var counts = words.Where(x => x.Item2.Count > 1).Select(x =>
             {
                 return x.Item2.Count;
             });
            foreach (var item in counts)
            {
                variationCount *= item;
            }
            var res = new List<string>();
            for (int i = 0; i < variationCount; i++)
            {
                res.Add("");
            }
            var index = 1;
            foreach (var item in words)
            {
                foreach (var tar in item.Item2)
                {
                    if (item.Item2.Count == 1)
                    {
                        for (int i = 0; i < res.Count; i++)
                        {
                            res[i] += tar;
                        }
                        continue;
                    }
                    for (int i = 0; i < res.Count; i++)
                    {
                        if (i == 0 && (index) % 2 == 0)
                        {
                            res[i] += tar;
                        }
                    }
                    index++;
                }
            }
            return res;
        }
        public string Translate(string source, Dictionary<string, string> words)
        {
            if (source==null|| source.Length==0)
                return "";
            foreach (KeyValuePair<string, string> pair in words)
            {
                source = source.Replace(pair.Key, pair.Value);
            }
            return source;
        }
        public string TranslatorEngHy(string InpStr)
        {
            if (InpStr == null)
            {
                return "";
            }
            string[] HyArrSource = new string[] { "Ա", "ա", "Բ", "բ", "Գ", "գ", "Դ", "դ", "Ե", "ե", "Զ", "զ", "Է", "է", "Ը", "ը", "Թ", "թ", "Ժ", "ժ", "Ի", "ի",
            "Լ", "լ", "Խ", "խ", "Ծ", "ծ", "Կ", "կ", "Հ", "հ", "Ձ", "ձ", "Ղ", "ղ", "Ճ", "ճ", "Մ", "մ", "Յ", "յ", "Ն", "ն", "Շ", "շ", "Ո", "ո",
            "Չ", "չ", "Պ", "պ", "Ջ", "ջ", "Ռ", "ռ", "Ս", "ս", "Վ", "վ", "Տ", "տ", "Ր", "ր", "Ց", "ց", "ւ", "Փ", "փ", "Ք", "ք", "և", "Օ",
            "օ", "Ֆ", "ֆ", "", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-", " ",".",",","-","/"};

            string[] EngArrSource = new string[]{ "A", "a", "B", "b", "G", "g", "D", "d", "Ye", "e", "Z", "z", "E", "e", "Y", "y", "T",
            "t", "Zh", "zh", "I", "i", "L", "l", "Kh", "kh", "Ts", "ts", "K", "k", "H", "h", "Dz", "dz", "Gh", "gh", "Ch", "ch", "M", "m",
            "Y", "y", "N", "n", "Sh", "sh", "Vo", "o", "Ch", "ch", "P", "p", "J", "j", "R", "r", "S", "s", "V", "v", "T", "t", "R", "r", "C",
            "c", "u", "Ph", "ph", "Q", "q", "ev", "O", "o", "F", "f", "", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-", " ",".",",","-","/"};

            string EngString = "";

            for (int i = 0; i < InpStr.Length; i++)
            {
                int hyindex = 0;
                string tempStr = "" + InpStr[i];
                for (int j = 0; j < EngArrSource.Length; j++)
                {
                    if (tempStr == EngArrSource[j])
                    {
                        hyindex = j;
                    }
                }
                EngString += HyArrSource[hyindex];
            }

            return EngString;
        }
    }
}
