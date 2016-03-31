﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using MediaPoint.Subtitles.Forms;

// The PAC format was developed by Screen Electronics
// The PAC format save the contents, time code, position, justification, and italicization of each subtitle. The choice of font is not saved.

namespace MediaPoint.Subtitles.Logic.SubtitleFormats
{

    public class Pac : SubtitleFormat
    {
        /// <summary>
        /// Contains Swedish, Danish, German, Spanish, and French letters
        /// </summary>
        static List<int> _latinCodes = new List<int> {
            0xe041, // Ã
            0xe04e, // Ñ
            0xe04f, // Õ
            0xe061, // ã
            0xe06e, // ñ
            0xe06f, // õ
            0xe161, // å
            0xe141, // Å
            0x23, // £
            0x7c, // æ
            0x7d, // ø
            0x7e, // §
            0x5c, // Æ
            0x5d, // Ø
            0x5e, // ÷
            0x5f, // -
            0xE54f, // Ö
            0xE56f, // ö
            0xe541, // Ä
            0xe561, // ä
            0xe555, // Ü
            0xe575, // ü
            0x81,   // ß
            0xe241, // Á
            0xe249, // Í
            0xe255, // Ú
            0xe259, // Ý
            0xe261, // á
            0xe265, // é
            0xe269, // í
            0xe245, // É
            0xe275, // ú
            0xe279, // ý
            0xe361, // à
            0xe365, // è
            0xe36f, // ò
            0xe345, // È
            0xe349, // Ì
            0xe34f, // Ò
            0xe369, // ì
            0xe443, // Ĉ
            0xe447, // Ĝ
            0xe448, // Ĥ
            0xe44A, // Ĵ
            0xe453, // Ŝ
            0xeA55, // Ŭ
            0xe463, // ĉ
            0xe467, // ĝ
            0xe468, // ĥ
            0xe46A, // ĵ
            0xe473, // ŝ
            0xeA75, // ŭ
            0xe341, // À
            0xe361, // à
            0xe441, // Â
            0xe461, // â
            0xe643, // Ç
            0xe663, // ç
            0xe445, // Ê
            0xe465, // ê
            0xe545, // Ë
            0xe565, // ë
            0xe56f, // ö
            0xe449, // Î
            0xe469, // î
            0xe549, // Ï
            0xe569, // ï
            0xe44F, // Ô
            0xe46F, // ô
            0xe355, // Ù
            0xe375, // ù
            0xe455, // Û
            0xe475, // û
            0xe559, // Ÿ
            0xe579, // ÿ
            0xeb41, // Ą
            0xeb61, // ą
            0xe243, // Ć
            0xe263, // ć
            0xeB45, // Ę
            0xeB65, // ę
            0x9c,   // Ł
            0xbc,   // ł
            0xe24e, // Ń
            0xe26e, // ń
            0xe24f, // Ó
            0xe26f, // ó
            0xe253, // Ś
            0xe273, // ś
            0xe25a, // Ź
            0xe27a, // ź
            0xe85a, // Ż
            0xe87a, // ż
        };

        static List<string> _latinLetters = new List<string> {
            "Ã",
            "Ñ",
            "Õ",
            "ã",
            "ñ",
            "õ",
            "å",
            "Å",
            "£",
            "æ",
            "ø",
            "§",
            "Æ",
            "Ø",
            "÷",
            "-",
            "Ö",
            "ö",
            "Ä",
            "ä",
            "Ü",
            "ü",
            "ß",
            "Á",
            "Í",
            "Ú",
            "Ý",
            "á",
            "é",
            "í",
            "É",
            "ú",
            "ý",
            "à",
            "è",
            "ò",
            "È",
            "Ì",
            "Ò",
            "ì",
            "Ĉ",
            "Ĝ",
            "Ĥ",
            "Ĵ",
            "Ŝ",
            "Ŭ",
            "ĉ",
            "ĝ",
            "ĥ",
            "ĵ",
            "ŝ",
            "ŭ",
            "À",
            "à",
            "Â",
            "â",
            "Ç",
            "ç",
            "Ê",
            "ê",
            "Ë",
            "ë",
            "ö",
            "Î",
            "î",
            "Ï",
            "ï",
            "Ô",
            "ô",
            "Ù",
            "ù",
            "Û",
            "û",
            "Ÿ",
            "ÿ",
            "Ą",
            "ą",
            "Ć",
            "ć",
            "Ę",
            "ę",
            "Ł",
            "ł",
            "Ń",
            "ń",
            "Ó",
            "ó",
            "Ś",
            "ś",
            "Ź",
            "ź",
            "Ż",
            "ż",
        };

        static List<int> _hebrewCodes = new List<int>
        {
            0xa0, // א
            0xa1, // ב
            0xa2, // ג
            0xa3, // ד
            0xa4, // ה
            0xa5, // ו
            0xa6, // ז
            0xa7, // ח
            0xa8, // ץ
            0xa9, // י
            0xaa, // ף
            0xab, // מ
            0xac, // ל
            0xad, // ם
            0xae, // מ
            0xaf, // ן
            0xb0, // נ
            0xb1, // ס
            0xb3, // ך
            0xb2, // ע
            0xb4, // פ
            0xb5, // ט
            0xb6, // צ
            0xb7, // ק
            0xb8, // ר
            0xb9, // ש
            0xba, // י

        };

        static List<string> _hebrewLetters = new List<string>
        {
            "א", // 0xa0
            "ב", // 0xa1
            "ג", // 0xa2
            "ד", // 0xa3
            "ה", // 0xa4
            "ו", // 0xa5
            "ז", // 0xa6
            "ח", // 0xa7
            "ץ", // 0xa8
            "י", // 0xa9
            "ף", // 0xaa
            "מ", // 0xab
            "ל", // 0xac
            "ם", // 0xad
            "מ", // 0xae
            "ן", // 0xaf
            "נ", // 0xb0
            "ס", // 0xb1
            "ע", // 0xb2
            "ך", // 0xb3
            "פ", // 0xb4
            "ט", // 0xb5
            "צ", // 0xb6
            "ק", // 0xb7
            "ר", // 0xb8
            "ש", // 0xb9
            "י", // 0xba
        };



        static List<int> _arabicCodes = new List<int> {
            0xe081, //=أ
            0xe09b, //=ؤ
            0xe09c, //=ئ
            0xe181, //=إ
            0xe281, //=آ
            0xe781, //=اً
            0x80,
            0x81, //=ا
            0x82, //=ب
            0x83, //=ت
            0x84, //=ث
            0x85, //=ج
            0x86, //=ح
            0x87, //=خ
            0x88, //=د
            0x89, //=ذ
            0x8A, //=ر
            0x8b, //=ز
            0x8c, //=س
            0x8d, //=ش
            0x8e, //=ص
            0x8f, //=ض
            0x90, //=ظ
            0x91, //=ط
            0x92, //=ع
            0x93, //=غ
            0x94, //=ف
            0x95, //=ق
            0x96, //=ك
            0x97, //=ل
            0x98, //=م
            0x99, //=ن
            0x9A, //=ه
            0x9b, //=و
            0x9c, //=ى
            0x9d, //=ة
            0x9f, //=ي
            0xa0, //=ء
        };

        static List<string> _arabicLetters = new List<string> {
            "أ",
            "ؤ",
            "ئ",
            "إ",
            "آ",
            "اً",
            "ـ",
            "ا",
            "ب",
            "ت",
            "ث",
            "ج",
            "ح",
            "خ",
            "د",
            "ذ",
            "ر",
            "ز",
            "س",
            "ش",
            "ص",
            "ض",
            "ظ",
            "ط",
            "ع",
            "غ",
            "ف",
            "ق",
            "ك",
            "ل",
            "م",
            "ن",
            "ه",
            "و",
            "ى",
            "ة",
            "ي",
            "ء",
        };


        static List<int> _cyrillicCodes = new List<int> {
            0x20, //space
            0x21, //!
            0x22, //Э
            0x23, // /
            0x24, //?
            0x25, //:
            0x26, //.
            0x27, //э
            0x28, //(
            0x29, //)
            0x2c, //,
            0x2d, //_
            0x2e, //ю
            0x3c, //>
            0x41, //Ф
            0x42, //И
            0x43, //C
            0x44, //В
            0x45, //У
            0x46, //F
            0x48, //Р
            0x49, //Ш
            0x4a, //О
            0x4d, //Ь
            0x4e, //Т
            0x53, //Ы
            0x57, //Ц
            0x58, //Ч
            0x5a, //Я
            0x5b, //х
            0x5d, //ъ
            0x5e, //,
            0x5f, //-
            0x61, //ф
            0x62, //и
            0x63, //c
            0x64, //в
            0x65, //у
            0x66, //a
            0x67, //п
            0x68, //p
            0x69, //ш
            0x6c, //д
            0x6d, //ь
            0x6e, //т
            0x6f, //э
            0x70, //з
            0x73, //ы
            0x74, //e
            0x75, //г
            0x77, //ц
            0x78, //ч
            0x79, //н
            0x7a, //я
            0x7b, //Х
            0x7d, //Ъ
            0x81, //Ю
            0x92, //ђ
            0x94, //,
            0x95, //-
            0x96, //і
            0x98, //ј
            0x99, //љ
            0x9a, //њ
            0x9b, //ћ
            0x9d, //§
            0x9f, //џ
            0xac, //C
            0xad, //D
            0xae, //E
            0xaf, //F
            0xb0, //G
            0xb1, //H
            0xb2, //'
            0xb3, //"
            0xb4, //I
            0xb5, //J
            0xb6, //K
            0xb7, //L
            0xb8, //M
            0xb9, //N
            0xba, //P
            0xbb, //Q
            0xbc, //R
            0xbd, //S
            0xbe, //T
            0xbf, //U
            0xc0, //V
            0xc2, //W
            0xc3, //X
            0xc4, //Y
            0xc5, //Z
            0xc6, //b
            0xc7, //c
            0xc8, //d
            0xc9, //e
            0xca, //f
            0xcb, //g
            0xcc, //h
            0xcd, //i
            0xce, //j
            0xcf, //k
            0xd1, //l
            0xd2, //m
            0xd3, //n
            0xd4, //o
            0xd5, //p
            0xd6, //q
            0xd7, //r
            0xd8, //s
            0xd9, //t
            0xda, //u
            0xdb, //v
            0xdc, //w
            0xdd, //э
            0xde, //ю
            0xdf, //z
            0xe065, //ў
            0xe574, //ё
            0xe272, //ќ
            0xe275, //ѓ
            0xe596, //ї
            0x6938, //ш

        };

        static List<string> _cyrillicLetters = new List<string> {
            " ", //0x20
            "!", //0x21
            "Э", //0x22
            "/", //0x23
            "?", //0x24
            ":", //0x25
            ".", //0x26
            "э", //0x27
            "(", //0x28
            ")", //0x29
            ",", //0x2c
            "_", //0x2d
            "ю", //0x2e
            ">", //0x3c
            "Ф", //0x41
            "И", //0x42
            "C", //0x43
            "B", //0x44
            "У", //0x45
            "F", //0x46
            "Р", //0x48
            "Ш", //0x49
            "О", //0x4a
            "Ь", //0x4d
            "Т", //0x4e
            "Ы", //0x53
            "Ц", //0x57
            "Ч", //0x58
            "Я", //0x5a
            "х", //0x5b
            "ъ", //0x5d
            ",", //0x5e
            "-", //0x5f
            "ф", //0x61
            "и", //0x62
            "с", //0x63
            "в", //0x64
            "у", //0x65
            "a", //0x66
            "п", //0x67
            "p", //0x68
            "ш", //0x69
            "д", //0x6c
            "ь", //0x6d
            "т", //0x6e
            "э", //0x6f
            "з", //0x70
            "ы", //0x73
            "e", //0x74
            "г", //0x75
            "ц", //0x77
            "ч", //0x78
            "н", //0x79
            "я", //0x7a
            "Х", //0x7b
            "Ъ", //0x7d
            "Ю", //0x81
            "ђ", //0x92
            ",", //0x94
            "-", //0x95
            "і", //0x96
            "ј", //0x98
            "љ", //0x99
            "ћ", //0x9b
            "њ", //0x9a
            "§", //0x9d
            "џ", //0x9f
            "C", //0xac
            "D", //0xad
            "E", //0xae
            "F", //0xaf
            "G", //0xb0
            "H", //0xb1
            "'", //0xb2
            "\"", //0xb3
            "I", //0xb4
            "J", //0xb5
            "K", //0xb6
            "L", //0xb7
            "M", //0xb8
            "N", //0xb9
            "P", //0xba
            "Q", //0xbb
            "R", //0xbc
            "S", //0xbd
            "T", //0xbe
            "U", //0xbf
            "V", //0xc0
            "W", //0xc2
            "X", //0xc3
            "Y", //0xc4
            "Z", //0xc5
            "b", //0xc6
            "c", //0xc7
            "d", //0xc8
            "e", //0xc9
            "f", //0xca
            "g", //0xcb
            "h", //0xcc
            "i", //0xcd
            "j", //0xce
            "k", //0xcf
            "l", //0xd1
            "m", //0xd2
            "n", //0xd3
            "o", //0xd4
            "p", //0xd5
            "q", //0xd6
            "r", //0xd7
            "s", //0xd8
            "t", //0xd9
            "u", //0xda
            "v", //0xdb
            "w", //0xdc
            "э", //0xdd
            "ю", //0xde
            "z", //0xdf
            "ў", //0xe065
            "ё", //0xe574
            "ќ", //0xe272
            "ѓ", //0xe275
            "ї", //0xe596
            "ш", //0x6938
        };


        private string _fileName = string.Empty;
        private int _codePage = -1;

        public override string Extension
        {
            get { return ".pac"; }
        }

        public override string Name
        {
            get { return "PAC (Screen Electronics)"; }
        }

        public override bool HasLineNumber
        {
            get { return false; }
        }

        public override bool IsTimeBased
        {
            get { return true; }
        }

        public void Save(string fileName, Subtitle subtitle)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            _fileName = fileName;

            // header
            fs.WriteByte(1);
            for (int i=1; i<23; i++)
                fs.WriteByte(0);
            fs.WriteByte(0x60);

            // paragraphs
            int number = 0;
            foreach (Paragraph p in subtitle.Paragraphs)
            {
                WriteParagraph(fs, p, number, number +1 == subtitle.Paragraphs.Count);
                number++;
            }

            // footer
            fs.WriteByte(0xff);
            for (int i=0; i<11; i++)
                fs.WriteByte(0);
            fs.WriteByte(0x11);
            fs.WriteByte(0);
            byte[] footerBuffer = System.Text.Encoding.ASCII.GetBytes("dummy end of file");
            fs.Write(footerBuffer, 0, footerBuffer.Length);

            fs.Close();
        }

        private void WriteParagraph(FileStream fs, Paragraph p, int number, bool isLast)
        {
            WriteTimeCode(fs, p.StartTime);
            WriteTimeCode(fs, p.EndTime);

            if (_codePage == -1)
                GetCodePage(null, 0, 0);

            string text = MakePacItalicsAndRemoveOtherTags(p.Text);
            text = text.Replace(Environment.NewLine, System.Text.Encoding.Default.GetString(new byte[] { 0xfe, 0x02, 0x03 })); // fix line breaks

            Encoding encoding = GetEncoding(_codePage);
            byte[] textBuffer;
            if (_codePage == 3)
                textBuffer = GetArabicBytes(Utilities.FixEnglishTextInRightToLeftLanguage(text, "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"));
            else if (_codePage == 4)
                textBuffer = GetHebrewBytes(Utilities.FixEnglishTextInRightToLeftLanguage(text, "0123456789abcdefghijklmnopqrstuvwxyz"));
            else if (_codePage == 0)
                textBuffer = GetLatinBytes(encoding, text);
            else if (_codePage == 6)
                textBuffer = GetCyrillicBytes(text);
            else
                textBuffer = encoding.GetBytes(text);

            // write text length
            UInt16 length = (UInt16)(textBuffer.Length + 4);
            fs.Write(BitConverter.GetBytes(length), 0, 2);

            fs.WriteByte(0x0a); // sometimes 0x0b?
            fs.WriteByte(0xfe);
            fs.WriteByte(0x02); //2=centered, 1=left aligned, 0=right aligned, 09=Fount2 (large font), 
                                //55=safe area override (too long line), 0A=Fount2 + centered, 06=centered + safe area override
            fs.WriteByte(0x03);

            fs.Write(textBuffer, 0, textBuffer.Length);

            if (!isLast)
            {
                fs.WriteByte(0);
                fs.WriteByte((byte)(number + 1));
                fs.WriteByte(0);
                fs.WriteByte(0x60);
            }
        }

        private string MakePacItalicsAndRemoveOtherTags(string text)
        {
            text = Utilities.RemoveHtmlFontTag(text).Trim();
            text = text.Replace("<u>", string.Empty);
            text = text.Replace("</u>", string.Empty);
            text = text.Replace("<I>", "<i>");
            text = text.Replace("</I>", "</i>");
            if (!text.Contains("<i>"))
                return text;
            
            if (Utilities.CountTagInText(text, "<i>") == 1 && text.StartsWith("<i>") && text.EndsWith("</i>"))
                    return "<" +  Utilities.RemoveHtmlTags(text).Replace(Environment.NewLine, Environment.NewLine + "<");

            var sb = new StringBuilder();
            string[] parts = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in parts)
            {
                string s = line.Trim();
                if (Utilities.CountTagInText(s, "<i>") == 1 && s.StartsWith("<i>") && s.EndsWith("</i>"))
                {
                    sb.AppendLine("<" + Utilities.RemoveHtmlTags(s));
                }
                else
                {
                    s = s.Replace("</i>", "___@___");
                    s = s.Replace("<i>", "<");
                    s = s.Replace("___@___", ">");
                    s = s.Replace(" <", "<");
                    s = s.Replace("> ", ">");
                    sb.AppendLine(s);
                }
            }
            return sb.ToString().Trim();
        }

        private void WriteTimeCode(FileStream fs, TimeCode timeCode)
        {
            // write four bytes time code
            string highPart = string.Format("{0:00}", timeCode.Hours) + string.Format("{0:00}", timeCode.Minutes);
            if (timeCode.Hours == 7 && timeCode.Minutes == 35)
                highPart = "065535";

            byte frames = (byte)MillisecondsToFrames(timeCode.Milliseconds);
            string lowPart = string.Format("{0:00}", timeCode.Seconds) + string.Format("{0:00}", frames);

            int high = int.Parse(highPart);
            if (high < 256)
            {
                fs.WriteByte((byte)high);
                fs.WriteByte(0);
            }
            else
            {
                fs.WriteByte((byte)(high % 256));
                fs.WriteByte((byte)(high / 256));
            }

            int low = int.Parse(lowPart);
            if (low < 256)
            {
                fs.WriteByte((byte)low);
                fs.WriteByte(0);
            }
            else
            {
                fs.WriteByte((byte)(low % 256));
                fs.WriteByte((byte)(low / 256));
            }
        }

        public override bool IsMine(List<string> lines, string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                FileInfo fi = new FileInfo(fileName);
                if (fi.Length > 100 && fi.Length < 1024000) // not too small or too big
                {
                    byte[] buffer = File.ReadAllBytes(fileName);

                    if (buffer[00] == 1 && // These bytes seems to be PAC files... TODO: Verify!
                        buffer[01] == 0 &&
                        buffer[02] == 0 &&
                        buffer[03] == 0 &&
                        buffer[04] == 0 &&
                        buffer[05] == 0 &&
                        buffer[06] == 0 &&
                        buffer[07] == 0 &&
                        buffer[08] == 0 &&
                        buffer[09] == 0 &&
                        buffer[10] == 0 &&
                        buffer[11] == 0 &&
                        buffer[12] == 0 &&
                        buffer[13] == 0 &&
                        buffer[14] == 0 &&
                        buffer[15] == 0 &&
                        buffer[16] == 0 &&
                        buffer[17] == 0 &&
                        buffer[18] == 0 &&
                        buffer[19] == 0 &&
                        buffer[20] == 0 &&
                        //buffer[21] < 10 && // start from number
                        buffer[22] == 0 &&
                        buffer[23] == 0x60 &&
                        fileName.ToLower().EndsWith(".pac"))
                        return true;
                }
            }
            return false;
        }

        public override string ToText(Subtitle subtitle, string title)
        {
            return "Not supported!";
        }

        public override void LoadSubtitle(Subtitle subtitle, List<string> lines, string fileName)
        {
            _fileName = fileName;
            subtitle.Paragraphs.Clear();
            subtitle.Header = null;
            byte[] buffer = File.ReadAllBytes(fileName);

            int index = 0;
            while (index < buffer.Length)
            {
                Paragraph p = GetPacParagraph(ref index, buffer);
                if (p != null)
                    subtitle.Paragraphs.Add(p);
            }
            subtitle.Renumber(1);
        }

        private Paragraph GetPacParagraph(ref int index, byte[] buffer)
        {
            while (index < 15)
            {
                index++;
            }
            bool con = true;
            while (con)
            {
                index++;
                if (index +20 >= buffer.Length)
                    return null;

                if (buffer[index] == 0xFE && (buffer[index - 15] == 0x60 ||buffer[index - 15] == 0x61))
                    con = false;
                if (buffer[index] == 0xFE && (buffer[index - 12] == 0x60 || buffer[index - 12] == 0x61))
                    con = false;
            }

            int FEIndex = index;
            int endDelimiter = 0x00;

            if (_codePage == -1)
                GetCodePage(buffer, index, endDelimiter);

            StringBuilder sb = new StringBuilder();
            index = FEIndex + 3;
            while (index < buffer.Length && buffer[index] != endDelimiter)
            {
                if (buffer[index] == 0xFF)
                {
                    sb.Append(" ");
                }
                else if (buffer[index] == 0xFE)
                {
                    sb.AppendLine();
                    index += 2;
                }
                else if (_codePage == 0)
                    sb.Append(GetLatinString(GetEncoding(_codePage), buffer, ref index));
                else if (_codePage == 3)
                    sb.Append(GetArabicString(buffer, ref index));
                else if (_codePage == 4)
                    sb.Append(GetHebrewString(buffer, ref index));
                else if (_codePage == 6)
                    sb.Append(GetCyrillicString(buffer, ref index));
                else
                {
                    sb.Append(GetEncoding(_codePage).GetString(buffer, index, 1));
                }

                index++;
            }
            if (index + 20 >= buffer.Length)
                return null;

            var p = new Paragraph();
            p.Text = sb.ToString();
            p.Text = FixItalics(p.Text);
            if (_codePage == 3)
                p.Text = Utilities.FixEnglishTextInRightToLeftLanguage(p.Text, "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            int timeStartIndex = FEIndex - 15;
            if (buffer[timeStartIndex] == 0x60)
            {
                p.StartTime = GetTimeCode(timeStartIndex + 1, buffer);
                p.EndTime = GetTimeCode(timeStartIndex + 5, buffer);
            }
            else if (buffer[timeStartIndex+3] == 0x60)
            {
                timeStartIndex += 3;
                p.StartTime = GetTimeCode(timeStartIndex + 1, buffer);
                p.EndTime = GetTimeCode(timeStartIndex + 5, buffer);
            }
            return p;
        }

        /// <summary>
        /// Fix italic tags, lines starting with ">" - whole line is italic, words between <> is italic
        /// </summary>
        private string FixItalics(string text)
        {
            int index = text.IndexOf("<");
            if (index < 0)
                return text;

            while (index >= 0 && index < text.Length-1)
            {
                text = text.Insert(index+1, "i>");
                int indexOfNewLine = text.IndexOf(Environment.NewLine, index);
                int indexOfEnd = text.IndexOf(">", index+3);
                if (indexOfNewLine < 0 && indexOfEnd < 0)
                {
                    index = -1;
                    text += "</i>";
                }
                else
                {
                    if (indexOfNewLine < 0 || (indexOfEnd > 0 && indexOfEnd < indexOfNewLine))
                    {
                        text = text.Insert(indexOfEnd, "</i");
                        index = text.IndexOf("<", indexOfEnd+3);
                    }
                    else
                    {
                        text = text.Insert(indexOfNewLine, "</i>");
                        index = text.IndexOf("<", indexOfNewLine + 4);
                    }
                }
            }
            text = text.Replace("<i>", " <i>");
            text = text.Replace("</i>", "</i> ");
            text = text.Replace("  ", " ");
            text = text.Replace(" " + Environment.NewLine , Environment.NewLine);
            return text.Trim();
        }

        public static Encoding GetEncoding(int codePage)
        {
            switch (codePage)
            {
                case 0: // Latin
                    return Encoding.GetEncoding("iso-8859-1");
                case 1: // Greek
                    return Encoding.GetEncoding("iso-8859-7");
                case 2: // Latin Czech
                    return Encoding.GetEncoding("iso-8859-2");
                case 3: // Arabic
                    return Encoding.GetEncoding("iso-8859-6");
                case 4: // Hebrew
                    return Encoding.GetEncoding("iso-8859-8");
                case 5: // Thai
                    return Encoding.GetEncoding("windows-874");
                case 6: // Cyrillic
                    return Encoding.GetEncoding("iso-8859-5");
                default: return Encoding.Default;
            }
        }

        private void GetCodePage( byte[] buffer, int index, int endDelimiter)
        {
            byte[] previewBuffer = null;

            if (buffer != null)
            {
                byte[] textSample = new byte[200];
                int textIndex = 0;
                while (index < buffer.Length && buffer[index] != endDelimiter)
                {
                    if (buffer[index] == 0xFF)
                    {
                        textSample[textIndex++] = 32; // space
                    }
                    else if (buffer[index] == 0xFE)
                    {
                        if (textIndex < textSample.Length - 3)
                        {
                            textSample[textIndex++] = buffer[index];
                            textSample[textIndex++] = buffer[index+1];
                            textSample[textIndex++] = buffer[index+2];
                        }
                        index += 3;
                    }
                    if (textIndex < textSample.Length - 1)
                        textSample[textIndex++] = buffer[index];
                    index++;
                }
                previewBuffer = new byte[textIndex];
                for (int i = 0; i < textIndex; i++)
                    previewBuffer[i] = textSample[i];
            }

			//var pacEncoding = new PacEncoding(previewBuffer, _fileName);
			//if (pacEncoding.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			//{
			//    _codePage = pacEncoding.CodePageIndex;
			//    Configuration.Settings.General.LastPacCodePage = _codePage;
			//}
			//else
			//{
			_codePage = -2;
			//}
        }

        private byte[] GetLatinBytes(Encoding encoding, string text)
        {
            int i = 0;
            byte[] buffer = new byte[text.Length * 2];
            int extra = 0;
            while (i < text.Length)
            {
                string letter = text.Substring(i, 1);
                int idx = _latinLetters.IndexOf(letter);
                if (idx >= 0)
                {
                    int byteValue = _latinCodes[idx];
                    if (byteValue < 256)
                    {
                        buffer[i + extra] = (byte)byteValue;
                    }
                    else
                    {
                        int high = byteValue / 256;
                        int low = byteValue % 256;

                        buffer[i + extra] = (byte)high;
                        extra++;
                        buffer[i + extra] = (byte)low;
                    }
                }
                else
                {
                    var values = encoding.GetBytes(letter);
                    for (int k = 0; k < values.Length; k++)
                    {
                        byte v = values[k];
                        if (k > 0)
                            extra++;
                        buffer[i + extra] = v;
                    }
                }
                i++;
            }

            byte[] result = new byte[i + extra];
            for (int j = 0; j < i + extra; j++)
                result[j] = buffer[j];
            return result;
        }


        private byte[] GetArabicBytes(string text)
        {
            return GetBytesViaLists(text, _arabicLetters, _arabicCodes);
        }

        private byte[] GetHebrewBytes(string text)
        {
            return GetBytesViaLists(text, _hebrewLetters, _hebrewCodes);
        }

        private byte[] GetCyrillicBytes(string text)
        {
            return GetBytesViaLists(text, _cyrillicLetters, _cyrillicCodes);
        }

        private byte[] GetBytesViaLists(string text, List<string> letters, List<int> codes)
        {
            int i = 0;
            byte[] buffer = new byte[text.Length * 2];
            int extra = 0;
            while (i < text.Length)
            {
                bool doubleCharacter = false;
                string letter = string.Empty;
                int idx = -1;
                if (i + 1 < text.Length)
                {
                    letter = text.Substring(i, 2);
                    idx = letters.IndexOf(letter);
                    if (idx >= 0)
                        doubleCharacter = true;
                }
                if (idx < 0)
                {
                    letter = text.Substring(i, 1);
                    idx = letters.IndexOf(letter);
                }
                if (idx >= 0)
                {
                    int byteValue = codes[idx];
                    if (byteValue < 256)
                    {
                        buffer[i + extra] = (byte)byteValue;
                    }
                    else
                    {
                        int high = byteValue / 256;
                        int low = byteValue % 256;
                        buffer[i + extra] = (byte)high;
                        if (doubleCharacter)
                        {
                            i++;
                            doubleCharacter = false;
                        }
                        else
                        {
                            extra++;
                        }
                        buffer[i + extra] = (byte)low;
                    }
                }
                else
                {
                    var values = Encoding.Default.GetBytes(letter);
                    for (int k = 0; k < values.Length; k++)
                    {
                        byte v = values[k];
                        if (k > 0)
                            extra++;
                        buffer[i + extra] = v;
                    }
                }
                i++;
                if (doubleCharacter)
                    i++;
            }

            byte[] result = new byte[i + extra];
            for (int j = 0; j < i + extra; j++)
                result[j] = buffer[j];
            return result;
        }

        public static string GetArabicString(byte[] buffer, ref int index)
        {
            byte b = buffer[index];
            if (b >= 0x20 && b < 0x70)
                return Encoding.ASCII.GetString(buffer, index, 1);

            int idx = _arabicCodes.IndexOf(b);
            if (idx >= 0)
                return _arabicLetters[idx];

            if (buffer.Length > index + 1)
            {
                idx = _arabicCodes.IndexOf(b * 256 + buffer[index + 1]);
                if (idx >= 0)
                {
                    index++;
                    return _arabicLetters[idx];
                }
            }

            return string.Format("({0})", b);
        }

        public static string GetHebrewString(byte[] buffer, ref int index)
        {
            byte b = buffer[index];
            if (b >= 0x20 && b < 0x70)
                return Encoding.ASCII.GetString(buffer, index, 1);

            int idx = _hebrewCodes.IndexOf(b);
            if (idx >= 0)
                return _hebrewLetters[idx];

            return string.Format("({0})", b);
        }

        public static string GetLatinString(Encoding encoding, byte[] buffer, ref int index)
        {
            byte b = buffer[index];

            int idx = _latinCodes.IndexOf(b);
            if (idx >= 0)
                return _latinLetters[idx];

            if (buffer.Length > index + 1)
            {
                idx = _latinCodes.IndexOf(b * 256 + buffer[index + 1]);
                if (idx >= 0)
                {
                    index++;
                    return _latinLetters[idx];
                }
            }

            return encoding.GetString(buffer, index, 1);
        }

        public static string GetCyrillicString(byte[] buffer, ref int index)
        {
            byte b = buffer[index];

            if (b >= 0x30 && b <= 0x39) // numbers
                return Encoding.ASCII.GetString(buffer, index, 1);

            int idx = _cyrillicCodes.IndexOf(b);
            if (idx >= 0)
                return _cyrillicLetters[idx];

            if (buffer.Length > index + 1)
            {
                idx = _cyrillicCodes.IndexOf(b * 256 + buffer[index + 1]);
                if (idx >= 0)
                {
                    index++;
                    return _cyrillicLetters[idx];
                }
            }

            return string.Format("({0})", b);
        }

        private TimeCode GetTimeCode(int timeCodeIndex, byte[] buffer)
        {
            if (timeCodeIndex > 0)
            {
                string highPart = string.Format("{0:000000}", buffer[timeCodeIndex] + buffer[timeCodeIndex + 1] * 256);
                string lowPart = string.Format("{0:000000}", buffer[timeCodeIndex+2] + buffer[timeCodeIndex + 3] * 256);

                int hours = int.Parse(highPart.Substring(0, 4));
                int minutes = int.Parse(highPart.Substring(4, 2));
                int second = int.Parse(lowPart.Substring(2, 2));
                int frames = int.Parse(lowPart.Substring(4, 2));

                int milliseconds = (int)((1000.0 / Configuration.Settings.General.CurrentFrameRate) * frames);
                if (milliseconds > 999)
                    milliseconds = 999;

                return new TimeCode(hours, minutes, second, milliseconds);
            }
            return new TimeCode(0, 0, 0, 0);
        }

    }
}