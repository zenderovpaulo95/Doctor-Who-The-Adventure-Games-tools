﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DoctorWhoToolsWorking
{
    class Methods
    {
        public static bool IsNumber(string str)
        {
            try
            {
                int num = Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public class ht //Данные о нескольких текстурах
        {

            public byte[] tex_data; //Данные текстур, которые прописаны в координатах
            public byte[] x_start; //Какие-то нулевые значения
            public byte[] y_start; //Начало блока с шрифтами (Обычно это используется в мультишрифтах, которые нарисованы на 1 текстуре)
            public byte[] x_end; //Непонятные данные. Что они означают, так и не понял.
            public byte[] y_end; //Конец блока с шрифтами
            public byte[] tex_num; //Номер текстуры

            public ht() { }
            public ht(byte[] _tex_data, byte[] _x_start, byte[] _y_start,
                byte[] _x_end, byte[] _y_end, byte[] _tex_num)
            {
                this.tex_data = _tex_data;
                this.x_start = _x_start;
                this.y_start = _y_start;
                this.x_end = _x_end;
                this.y_end = _y_end;
                this.tex_num = _tex_num;
            }
        }



        public static string GetFileNameOnly(string name, string del)
        {
            return name.Replace(del, string.Empty);
        }

         public static byte[] ReadFull(Stream stream)
        {
            byte[] buffer = new byte[3207];

            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

       /* public static string ConvertToAnotherLanguage(string str, int ASCII_N)
        {
            try
            {
                Encoding UTF8_text = Encoding.UTF8;
                byte[] temp_string = new byte[str.Length];
                temp_string = UTF8_text.GetBytes(str);
                temp_string = Encoding.Convert(UTF8_text, ASCIIEncoding.GetEncoding(1252), temp_string);
                return ASCIIEncoding.GetEncoding(ASCII_N).GetString(temp_string);
            }
            catch
            {
                return "Error";
            }
        }*/

       /* public static string ConvertToLatin(string str, int ASCII_N)
        {
            try
            {
                Encoding UTF8_text = Encoding.UTF8;
                byte[] temp_string = new byte[str.Length];
                string temp_str;
                temp_string = ASCIIEncoding.GetEncoding(ASCII_N).GetBytes(str);
                temp_str = ASCIIEncoding.GetEncoding(1252).GetString(temp_string);
                temp_string = Encoding.Convert(ASCIIEncoding.GetEncoding(1252), UTF8_text, temp_string);
                return UTF8_text.GetString(temp_string);
            }
            catch
            {
                return "Error";
            }
        }*/

        public static string ConvertHexToString(byte[] array, int poz, int str_length) //Функция для преобразования байтов в символы
        {
            byte[] bTemp = new byte[str_length];
            Array.Copy(array, poz, bTemp, 0, bTemp.Length);
            return UnicodeEncoding.UTF8.GetString(bTemp);
        }

        public static int FindStartOfStringSomething(byte[] array, int offset, string string_something) //Для поиска нужных параметров в файле
        {
            int poz = offset;
            while (ConvertHexToString(array, poz, string_something.Length) != string_something)
            {
                poz++;
                if (ConvertHexToString(array, poz, string_something.Length) == string_something)
                {
                    return poz;
                }
                if ((poz + string_something.Length + 1) > array.Length)
                {
                    poz = -1;
                    break;
                }
            }
            return poz;
        }
        public static bool CheckHeader(byte[] array, int offset, string header)
        {
            if (FindStartOfStringSomething(array, offset, header) == -1)
            {
                return false;
            }
        else return true;
        }

        /*public class FontCoordinates
        {
            public 
        }*/
    }
}