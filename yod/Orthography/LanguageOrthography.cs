using System;
using System.Collections.Generic;
using yod.Phonology;

namespace yod.Orthography
{
    public class LanguageOrthography
    {
        public Dictionary<string, string> Rules;
        LanguagePhonology language;

        public Dictionary<string, List<string>> Orthographies = new Dictionary<string, List<string>>
        {
            {"m̥", new List<string> {"m", "ṃ", "ḿ", "ṁ"}},
            {"m", new List<string> {"m", "ṁ", "ṃ"}},
            {"ɱ", new List<string> {"m", "ṃ", "ɱ", "ḿ", "ṁ"}},
            {"n̼", new List<string> {"n", "ń", "ñ", "ň", "ǹ", "ṅ", "ṇ", "н"}},
            {"n̥", new List<string> {"n", "ń", "ñ", "ň", "ǹ", "ṅ", "ṇ", "н"}},
            {"n", new List<string> {"n", "ń", "ñ", "ň", "ǹ", "ṅ", "ṇ", "н"}},
            {"ɲ̊", new List<string> {"nj","n", "ń", "ñ", "ň", "ǹ", "ṅ", "ṇ", "ɲ", "ɲ̊", "н", "ң", "ny", "gn", "nh"}},
            {"ɲ", new List<string> {"nj", "n", "ń", "ñ", "ň", "ǹ", "ṅ", "ṇ", "ɲ", "ɲ̊", "н", "ң", "ny", "gn", "nh"}},
            {"ŋ̊", new List<string> {"ng", "n", "ń", "ñ", "ň", "ǹ", "ṅ", "ṇ", "н", "nk", "ŋ", "ң", "ӈ", "ҥ"}},
            {"ŋ", new List<string> {"ng", "n", "ń", "ñ", "ň", "ǹ", "ṅ", "ṇ", "н", "nk", "ŋ", "ң", "ӈ", "ҥ"}},
            {"ɴ", new List<string> {"nġ", "n", "ń", "ñ", "ň", "ǹ", "ṅ", "ṇ", "н", "ng", "nk", "ŋ", "ң", "ӈ", "ҥ"}},
            {"p", new List<string> {"p", "п", "ṕ", "ṗ"}},
            {"b", new List<string> {"b", "б", "ḅ", "ɓ", "ḃ", "ḇ"}},
            {"p̪", new List<string> {"ṗ", "ᵱ", "ƥ", "p", "п", "ṕ"}},
            {"b̪", new List<string> {"ḃ", "b", "б", "ḅ", "ɓ", "ḇ"}},
            {"t̼", new List<string> {"ṭ", "ť", "ʈ", "ț", "t", "th", "ṯ"}},
            {"d̼", new List<string> {"ḍ", "ḑ", "ḓ", "ď", "ḏ", "ḋ", "d", "dh"}},
            {"t", new List<string> {"t", "ṭ", "ť", "ʈ", "ț","ṯ"}},
            {"d", new List<string> {"d", "ḍ", "ḑ", "ḓ", "ď", "ḏ", "ḋ"}},
            {"ʈ", new List<string> {"ṯ", "ṭ", "ť", "ʈ", "ț", "t", "th"}},
            {"ɖ", new List<string> {"ḏ", "ḍ", "ḑ", "ḓ", "ď", "ḋ", "d", "dh"}},
            {"c", new List<string> {"ç", "cj", "qu", "kj", "ċ", "ĉ", "ƈ", "qj", "kh", "q", "ƙ", "ꝁ", "ḱ", "ǩ", "ḳ", "ḵ"}},
            {"ɟ", new List<string> {"ġ", "ģ", "ɠ", "ĝ", "ǥ", "ğ", "gj", "gh"}},
            {"k", new List<string> {"k", "ç", "c", "к", "q", "ċ", "ĉ", "ƈ", "kh", "ƙ", "ꝁ", "ḱ", "ǩ", "ḳ", "ḵ"}},
            {"g", new List<string> {"g", "ġ", "ģ", "ɠ", "ĝ", "ǥ", "ğ", "gh", "г"}},
            {"q", new List<string> {"q", "ꝗ", "ʠ", "ɋ", "ꝙ", "q̃"}},
            {"ɢ", new List<string> {"ǥ", "ġ", "ģ", "ɠ", "ĝ", "ğ", "gj", "gh"}},
            {"ʡ", new List<string> {"q̃", "q", "ꝗ", "ʠ", "ɋ", "ꝙ", "ʡ"}},
            {"ʔ", new List<string> {"-", ".", "'", "·", "ʔ"}},
            {"t͡s", new List<string> {"ts","c","ç","ц","tz"}},
            {"d͡z", new List<string> {"dz","z","ẓ","dẓ"}},
            {"t͡ʃ", new List<string> {"ć", "ch", "tsh", "tš", "tʃ"}},
            {"d͡ʒ", new List<string> {"đ", "dzh", "dž", "dj", "ḋ"}},
            {"ʈ͡ʂ", new List<string> {"č", "ch", "tsh", "tš", "tʃ"}},
            {"ɖ͡ʐ", new List<string> {"đ", "dzh", "dž", "dj", "ḋ"}},
            {"t͡ɕ", new List<string> {"č", "ch", "tsh", "tš", "tʃ"}},
            {"d͡ʑ", new List<string> {"đ", "dzh", "dž", "dj", "ḋ"}},
            {"p͡ɸ", new List<string> {"ṕ", "pf", "p", "ph"}},
            {"b͡β", new List<string> {"ḅ", "bh", "bv", "b"}},
            {"p̪͡f", new List<string> {"pf", "ṗ", "ṕ"}},
            {"b̪͡v", new List<string> {"bv", "v", "ṿ"}},
            {"t͡θ", new List<string> {"th", "dth", "tθ", "tẑ"}},
            {"d͡ð", new List<string> {"dth", "dð", "dẑ"}},
            {"t͡θ̠", new List<string>  {"th","tθ", "tẑ" }},
            {"d͡ð̠", new List<string> {"dth", "dð", "dẑ"}},
            {"t̠͡ɹ̠̊˔", new List<string> {"tr", "tɹ", "ʈr", "ṯr"}},
            {"d̠͡ɹ̠˔", new List<string> {"dr", "dɹ", "ḏr", "ḏɹ"}},
            {"c͡ç", new List<string> {"kj", "ty", "ch", "ƈ", "ċ"}},
            {"ɟ͡ʝ", new List<string> {"gj","dy", "dj"}},
            {"k͡x", new List<string> {"kh", "k", "ḱ", "ḵ", "ḱh", "ḵh", "ǩ", "ǩh", "ḳ", "ḳh"}},
            {"ɡ͡ɣ", new List<string> {"gh", "ǥ", "ǥh", "ģ", "ģh", "ġ", "ġh"}},
            {"q͡χ", new List<string> {"ǩh", "kh", "k", "ḱ", "ḵ", "ḱh", "ḵh", "ǩ", "ḳ", "ḳh"}},
            {"ʔ͡h", new List<string> {"'h", "ḥ", "ḣ", "ḩ"}},
            {"s", new List<string> {"s", "ś", "ṡ", "ṣ", "ꞩ", "ŝ", "š", "ş", "ș", "s̈", "ſ", "ç", "с"}},
            {"z", new List<string> {"z", "з", "ź", "ẑ", "ž", "ż", "ẓ", "ẕ", "ƶ", "ʐ", "ɀ"}},
            {"ʃ", new List<string> {"š", "sh", "ŝ", "ʃ", "ꞩ", "s̈", "ș", "ш", "sj"}},
            {"ʒ", new List<string> {"ž", "zh", "ж", "j", "zj", "ƶ", "ɀ"}},
            {"ʂ", new List<string> {"š", "ʂ", "sh", "ŝ", "ʃ", "ꞩ", "s̈", "ș", "ш", "sj"}},
            {"ʐ", new List<string> {"ž", "ʐ", "zh", "ж", "j", "zj", "ƶ"}},
            {"ɕ", new List<string> {"š", "ɕ", "ʂ", "sh", "ŝ", "ʃ", "ꞩ", "s̈", "ș", "ш", "sj"}},
            {"ʑ", new List<string> {"ž", "ʑ", "ʐ", "zh", "ж", "j", "zj", "ƶ"}},
            {"ɸ", new List<string> {"fh", "f", "ƒ", "ḟ", "ɸ", "ƒh", "ḟh", "ɸh"}},
            {"β", new List<string> {"bh", "vh", "ḃ", "ḅ", "ḇ", "ḃh", "ḅh", "ḇh", "β", "ṿ", "ꝟ", "ṿh", "ꝟh", "ⱴ", "ⱴh"}},
            {"f", new List<string> {"f", "ƒ", "ḟ"}},
            {"v", new List<string> {"v", "ⱴ", "ṿ", "ⱱ", "ѵ"}},
            {"θ̼", new List<string> {"th", "θ", "θ'"}},
            {"ð̼", new List<string> {"ð'", "ð", "th'"}},
            {"θ", new List<string> {"th", "θ", "ṯ", "ʈ", "ť"}},
            {"ð", new List<string> {"ð", "th", "ʈh", "ṯh", "ť"}},
            {"θ̱", new List<string> {"th", "ʈh", "θ", "ṯ", "ʈ", "ť"}},
            {"ð̠", new List<string> {"ð", "ðh", "th", "ʈh", "ṯh", "ť"}},
            {"ɹ̠̊˔", new List<string> {"ɍ", "r", "ř", "ŗ", "ṙ", "ȑ", "ȓ", "ṛ", "ɽ", "ɹ̠"}},
            {"ɹ̠˔", new List<string> {"ɍ", "ŗ", "r", "ř", "ṙ", "ȑ", "ȓ", "ṛ", "ɽ", "ɹ̠"}},
            {"ç", new List<string> {"ḧ", "ḩ", "ⱨ", "ḧj", "ḩj", "ⱨj", "ç", "hj"}},
            {"ʝ", new List<string> {"ȷ", "j", "ɉ", "ĵ"}},
            {"x", new List<string> {"xh", "ḵh", "x", "xj", "ẋ"}},
            {"ɣ", new List<string> {"ğ", "gh", "ğh", "ɣ"}},
            {"χ", new List<string> {"ẍ", "ẋ", "xh", "ẋh", "ẍh", "ch"}},
            {"ʁ", new List<string> {"ɽ", "rr", "ɍ", "ʁ", "r'"}},
            {"ħ", new List<string> {"h", "ḥ", "ɦ", "ḩ", "ⱨ", "ḫ", "ḣ"}},
            {"ʕ", new List<string> {"r", "ɍ", "ř", "ŗ", "ṙ", "ȑ", "ȓ", "ṛ", "ɽ", "ɹ̠"}},
            {"h", new List<string> {"h", "ḥ", "ɦ", "ḩ", "ⱨ", "ḫ", "ḣ"}},
            {"ɦ", new List<string> {"h", "ḥ", "ɦ", "ḩ", "ⱨ", "ḫ", "ḣ", "ħ"}},
            {"ʋ̥", new List<string> {"v", "w", "f", "ʋ", "ṿ", "ṽ", "ⱴ"}},
            {"ʋ", new List<string> {"v", "w", "f", "ʋ", "ṿ", "ṽ", "ⱴ"}},
            {"ɹ̥", new List<string> {"r", "ɍ", "ř", "ŗ", "ṙ", "ȑ", "ȓ", "ṛ", "ɽ", "ɹ̠"}},
            {"ɹ", new List<string> {"r", "ɍ", "ř", "ŗ", "ṙ", "ȑ", "ȓ", "ṛ", "ɽ", "ɹ̠"}},
            {"ɻ̊", new List<string> {"r", "ɍ", "ř", "ŗ", "ṙ", "ȑ", "ȓ", "ṛ", "ɽ", "ɹ̠"}},
            {"ɻ", new List<string> {"r", "ɍ", "ř", "ŗ", "ṙ", "ȑ", "ȓ", "ṛ", "ɽ", "ɹ̠"}},
            {"j̊", new List<string> {"y", "j", "я", "ỵ", "ÿ", "ỹ", "ẏ", "ɉ", "ĵ", "ȷ", "ʝ"}},
            {"j", new List<string> {"y", "j", "я", "ỵ", "ÿ", "ỹ", "ẏ", "ɉ", "ĵ", "ȷ", "ʝ"}},
            {"ɰ̊", new List<string> {"ĝ", "g", "ğ", "ǥ", "ɠ", "ġ"}},
            {"ɰ", new List<string> {"ĝ", "g", "ğ", "ǥ", "ɠ", "ġ"}},
            {"ʍ", new List<string> {"hw", "w", "ŵ", "wh", "hŵ", "ẅ", "hẅ", "ẃ"}},
            {"w", new List<string> {"w", "ẃ", "ẁ", "ŵ", "ẅ", "ẇ", "ẉ"}},
            {"ⱱ", new List<string> {"ⱱ", "vw", "ꝟ", "ṽ", "ⱴ"}},
            {"ɾ̥", new List<string> {"r", "ŗ", "ɍ", "ŕ", "ṙ", "ṛ"}},
            {"ɾ", new List<string> {"r", "ŗ", "ɍ", "ŕ", "ṙ", "ṛ"}},
            {"ɽ̊", new List<string> {"r", "ŗ", "ɍ", "ŕ", "ṙ", "ṛ"}},
            {"ɽ", new List<string> {"r", "ŗ", "ɍ", "ŕ", "ṙ", "ṛ"}},
            {"B", new List<string> {"br", "ƀ", "ḇ", "ƀr", "ḇr"}},
            {"ʙ̪", new List<string> {"br", "ƀ", "ḇ", "ƀr", "ḇr"}},
            {"r̥", new List<string> {"r", "ŗ", "ɍ", "ŕ", "ṙ", "ṛ"}},
            {"r", new List<string> {"r", "ŗ", "ɍ", "ŕ", "ṙ", "ṛ"}},
            {"ʀ̥", new List<string> {"r", "ŗ", "ɍ", "ŕ", "ṙ", "ṛ"}},
            {"R", new List<string> {"ŗ", "r", "ɍ", "ŕ", "ṙ", "ṛ"}},
            {"ʜ", new List<string> {"ḩ", "h", "x", "ẋ", "ḩx", "hx", "ḩẋ", "hẋ",}},
            {"ʢ", new List<string> {"w", "ẃ", "ẁ", "ŵ", "ẅ", "ẇ", "ẉ"}},
            {"t͡ɬ", new List<string> {"tl", "tɬ"}},
            {"d͡ɮ", new List<string> {"dɮ", "λ", "dl"}},
            {"c͡ʎ̥˔", new List<string> {"tl", "kl", "cl"}},
            {"k͡ʟ̝̊", new List<string> {"kl", "лl", "kʎ"}},
            {"ɡ͡ʟ̝", new List<string> {"gl", "gʎ"}},
            {"ɬ", new List<string> {"ł", "ɬ", "hl", "hł", "hɬ", "sɬ", "sł"}},
            {"ɮ", new List<string> {"ɬ", "ł", "hl", "hł", "hɬ", "sɬ", "sł"}},
            {"ʎ̥˔", new List<string> {"sɬ","ł", "ɬ", "hl", "hł", "hɬ", "sł"}},
            {"ʎ̝", new List<string> {"sɬ", "ł", "ɬ", "hl", "hł", "hɬ", "sł"}},
            {"ʟ̝̊", new List<string> {"xł", "ł", "ɬ", "hl", "hł", "hɬ", "xɬ"}},
            {"ʟ̝", new List<string> {"gł", "ł", "ɬ", "hl", "hł", "hɬ", "gɬ"}},
            {"l̥", new List<string> {"l", "ľ", "ļ", "ŀ", "ḷ", "ɭ"}},
            {"l", new List<string> {"l", "ľ", "ļ", "ŀ", "ḷ", "ɭ"}},
            {"ɭ̊", new List<string> {"l", "ľ", "ļ", "ŀ", "ḷ", "ɭ"}},
            {"ɭ", new List<string> {"l", "ľ", "ļ", "ŀ", "ḷ", "ɭ"}},
            {"ʎ̥", new List<string> {"ly", "ʎ", "ľy", "ļy", "ŀy", "ḷy", "ɭy"}},
            {"ʎ", new List<string> {"ly", "ʎ", "ľy", "ļy", "ŀy", "ḷy", "ɭy"}},
            {"ʟ̥", new List<string> {"ɭ", "l", "ľ", "ļ", "ŀ", "ḷ"}},
            {"ʟ", new List<string> {"ɭ", "l", "ľ", "ļ", "ŀ", "ḷ"}},
            {"ʟ̠", new List<string> {"ɭ", "l", "ľ", "ļ", "ŀ", "ḷ"}},
            {"ɺ", new List<string> {"l", "ľ", "ļ", "ŀ", "ḷ", "ɭ"}},
            {"ɭ̆", new List<string> {"l", "ľ", "ļ", "ŀ", "ḷ", "ɭ"}},
            {"ʎ̮", new List<string> {"ly", "ʎ", "ľy", "ļy", "ŀy", "ḷy", "ɭy"}},
            {"ʟ̆", new List<string> {"ɭ", "l", "ľ", "ļ", "ŀ", "ḷ"}},

            {"i", new List<string> {"i", "ị", "ĭ", "î", "ǐ", "ǐ", "ɨ", "ï", "ḯ", "í", "ì", "ȉ", "į", "ī", "ᶖ", "ỉ", "ȋ", "ĩ", "ḭ", "ı" }},
            {"y", new List<string> {"u", "y", "ŭ", "ʉ", "ụ", "ü", "ú", "ù", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ" }},
            {"ɨ", new List<string> {"ʉ", "u", "ŭ", "ụ", "ü", "ú", "ù", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ" }},
            {"ʉ", new List<string> {"u", "oo'", "ŭ", "ʉ", "ụ", "ü", "ú", "ù", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ" }},
            {"ɯ", new List<string> {"ų", "ʉ", "u", "ŭ", "ụ", "ü", "ú", "ù", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ȕ" }},
            {"u", new List<string> {"u", "oo'", "ų", "ʉ", "u", "ŭ", "ụ", "ü", "ú", "ù", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ȕ" }},
            {"ɪ", new List<string> {"i", "ị", "ĭ", "î", "ǐ", "ǐ", "ɨ", "ï", "ḯ", "í", "ì", "ȉ", "į", "ī", "ᶖ", "ỉ", "ȋ", "ĩ", "ḭ", "ı" }},
            {"ʏ", new List<string> {"ú", "u", "ŭ", "ʉ", "ụ", "ü", "ù", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ" }},
            {"ɪ̈", new List<string> {"ù", "ú", "u", "ŭ", "ʉ", "ụ", "ü", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ" }},
            {"ʊ̈", new List<string> {"ù", "ú", "u", "ŭ", "ʉ", "ụ", "ü", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ" }},
            {"ʊ", new List<string> {"u", "ù", "y", "ú", "ŭ", "ʉ", "ụ", "ü", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ" }},
            {"e", new List<string> {"e", "eh", "ĕ", "ḝ", "ȇ", "ê", "ẻ", "ḙ", "ě", "ɇ", "ė", "ẹ", "ë", "è", "ȅ", "é", "ē", "ẽ", "ḛ", "ę", "ᶒ", "ⱸ"}},
            {"ø", new List<string> {"ö", "ø", "ō", "ǫ", "õ", "eu'", "ŏ"}},
            {"ɘ", new List<string> {"œ","ë", "ø", "ō", "ǫ", "õ", "eu'", "ŏ"}},
            {"ɵ", new List<string> {"ö", "ɵ", "ø", "ō", "ǫ", "õ", "ŏ"}},
            {"ɤ", new List<string> {"ɤ", "ë", "ĕ", "ḝ", "ȇ", "ê", "ẻ", "ḙ", "ě", "ɇ"}},
            {"o", new List<string> {"o", "ó", "õ", "ō", "å", "ọ", "ȯ", "ò"}},
            {"ə", new List<string> {"ə", "e", "a", "ĕ", "ḝ", "ȇ", "ê", "ẻ", "ḙ", "ě", "ɇ", "ė", "ẹ", "ë", "è", "ȅ", "é", "ē", "ẽ", "ḛ", "ę", "ᶒ", "ⱸ"}},
            {"ɛ", new List<string> {"ė", "e", "ĕ", "ḝ", "ȇ", "ê", "ẻ", "ḙ", "ě", "ɇ", "ė", "ẹ", "ë", "è", "ȅ", "é", "ē", "ẽ", "ḛ", "ę", "ᶒ", "ⱸ"}},
            {"œ", new List<string> {"œ", "õ", "ö", "ĕ", "ū", "ě"}},
            {"ɜ", new List<string> {"ɜ", "œ", "õ", "ö", "ĕ", "ū"}},},
            {"ɞ", new List<string> {"u", "ŭ", "ʉ", "ụ", "ü", "ù", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ", "ú",}},
            {"ʌ", new List<string> {"u", "ŭ", "ʉ", "ụ", "ü", "ù", "û", "ǔ", "ű", "ŭ", "ư", "ủ", "ū", "ũ", "ų", "ȕ", "ú",}},
            {"ɔ", new List<string> {"o", "å", "ö", "ɵ", "ø", "ō", "ǫ", "õ", "ŏ"}},
            {"æ", new List<string> {"a", "ḁ", "ẚ", "ă", "ȃ", "â", "ⱥ", "ǎ", "ȧ", "ạ", "ä", "à", "ȁ", "á", "ā", "ã", "ą", "ᶏ"}},
            {"ɐ", new List<string> {"ā", "a", "ḁ", "ẚ", "ă", "ȃ", "â", "ⱥ", "ǎ", "ȧ", "ạ", "ä", "à", "ȁ", "á", "ã", "ą", "ᶏ"}},
            {"a", new List<string> {"ȃ", "ā", "a", "ḁ", "ẚ", "ă", "â", "ⱥ", "ǎ", "ȧ", "ạ", "ä", "à", "ȁ", "á", "ã", "ą", "ᶏ"}},
            {"ɶ", new List<string> {"œ", "õ", "ö", "ĕ", "ū", "ě"}}
            {"ä", new List<string> {"ȃ", "ā", "a", "ḁ", "ẚ", "ă", "â", "ⱥ", "ǎ", "ȧ", "ạ", "ä", "à", "ȁ", "á", "ã", "ą", "ᶏ"}},
            {"ɑ", new List<string> {"ȃ", "ā", "a", "ḁ", "ẚ", "ă", "â", "ⱥ", "ǎ", "ȧ", "ạ", "ä", "à", "ȁ", "á", "ã", "ą", "ᶏ"}},
            {"ɒ", new List<string> {"ō", "o", "ó", "õ", "å", "ọ", "ȯ", "ò"}},
        };

        public static readonly Dictionary<string, string> DefaultOrthography = new Dictionary<string, string>
        {
            #region rules
            {"m", "m"},
            {"n", "n"},
            {"ɲ", "ny"},
            {"ŋ", "ng"},
            {"p", "p"},
            {"b", "b"},
            {"t", "t"},
            {"d", "d"},
            {"k", "k"},
            {"g", "g"},
            {"ʔ", "-"},
            {"t͡s", "ts"},
            {"d͡z", "dz"},
            {"t͡ʃ", "ch"},
            {"d͡ʒ", "dzh"},
            {"ʈ͡ʂ", "tsh"},
            {"ɖ͡ʐ", "dzh"},
            {"t͡ɕ", "tsh"},
            {"d͡ʑ", "dzh"},
            {"t͡θ", "th"},
            {"d͡ð", "th"},
            {"s", "s"},
            {"z", "z"},
            {"ʃ", "sh"},
            {"ʒ", "zh"},
            {"ʂ", "sh"},
            {"ʐ", "zh"},
            {"ɕ", "sh"},
            {"ʑ", "zh"},
            {"f", "f"},
            {"v", "v"},
            {"θ", "th"},
            {"ð", "th"},
            {"x", "h"},
            {"h", "h"},
            {"ʋ", "v"},
            {"ɹ", "r"},
            {"j", "y"},
            {"ʍ", "wh"},
            {"w", "w"},
            {"B", "br"},
            {"r", "r"},
            {"R", "rr"},
            {"l", "l"},
            {"ʎ", "ly"},
            {"a", "a"},
            {"e", "e"},
            {"i", "i"},
            {"o", "o"},
            {"u", "u"},
            {"ə", "e"},

            #endregion
        };

        public LanguageOrthography(LanguagePhonology lang) : this(DefaultOrthography, lang)
        {
        }

        // todo: load orthography from json file

        public LanguageOrthography(Dictionary<string, string> rules, LanguagePhonology lang)
        {
            Rules = rules;
            language = lang;

            foreach (var c in language.Phonemes.Consonants)
            {
                if (!rules.ContainsKey(c.Symbol)) throw new Exception("Orthography does not contain a grapheme for the (consonant) phoneme [" + c.Symbol + "]");
            }
            foreach (var v in language.Phonemes.Vowels)
            {
                if (!rules.ContainsKey(v.Symbol)) throw new Exception("Orthography does not contain a grapheme for the (vowel) phoneme [" + v.Symbol + "]");
            }
        }

        public string Orthographize(Phoneme phoneme)
        {
            return Rules[phoneme.Symbol];
        }

        public string Orthographize(Syllable syllable)
        {
            var s = "";
            foreach (var phoneme in syllable.Phonemes)
            {
                s += Orthographize(phoneme);
            }
            return s;
        }

        public string Orthographize(Word word)
        {
            var s = "";
            foreach (var syllable in word.Syllables)
            {
                s += Orthographize(syllable);
            }
            return s;
        }
    }
}