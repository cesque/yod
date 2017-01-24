using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public class Word
    {
        public Phonology.Word Phonemes;
        public string EnglishLemma;
        public List<string> Tags;
        public List<InputWord> SubWords;

        public Word(Phonology.Word phonemes, string english, List<string> tags)
        {
            Phonemes = phonemes;
            EnglishLemma = english;
            Tags = tags;
        }

        public List<string> GetSmallCapsTags()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var small = "ᴀʙᴄᴅᴇꜰɢʜɪᴊᴋʟᴍɴᴏᴘ ʀꜱᴛᴜᴠᴡ ʏᴢ";
            var dict = new Dictionary<char, char>();
            for(var i = 0; i<chars.Length; i++)
            {
                dict.Add(chars[i], small[i]);
            }

            var list = new List<string>();

            foreach(var tag in Tags)
            {
                var t = "";
                foreach(char c in tag)
                {
                    t += dict[c];
                }
                list.Add(t);
            }

            return list;
        }
    }
}
