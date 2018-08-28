using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace readwriteini
{
    class readwriteini
    {
        public static string iniPath = 
            System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            + "\\Ranchargen.ini";
        //导入kernel32
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        /// <summary>
        /// 写ini 配置文件
        /// </summary>
        /// <param name="Section">标题</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        public static void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, iniPath);
        }

        /// <summary>
        /// 读取ini 配置文件
        /// </summary>
        /// <param name="Section">标题</param>
        /// <param name="Key">键</param>
        /// <returns>结果</returns>
        public static string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, iniPath);
            return temp.ToString();
        }
    }
}
