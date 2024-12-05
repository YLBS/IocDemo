using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tool
{
    public static class LogConfig
    {
        /// <summary>
        /// 添加异常日志
        /// </summary>
        /// <param name="context">内容</param>
        /// <param name="configName">文件名</param>
        /// <returns></returns>
        public static string TestSetConfig(string context, string configName = "异常日志")
        {
            string logPath = string.Empty;
            try
            {
                string time2 = DateTime.Now.ToString("yyyy-MM-dd");
                logPath = $"log/{DateTime.Now.Year}年/{DateTime.Now.Month}月/{time2}{configName}.txt";
                Directory.CreateDirectory(Path.GetDirectoryName(logPath)); // 确保目录存在
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("\n   " + DateTime.Now + "\n msg:" + context + "\n");
                    sw.Flush();
                    sw.Close();
                }
                return "写入成功：";
            }
            catch (Exception e)
            {
                FailedLog(logPath, e.Message);
                return "写入失败：" + e.Message;
            }
        }
        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="pathName">路径</param>
        /// <param name="fileText">文本内容</param>
        /// <returns>写入是否成功</returns>
        public static bool WriteFile(string pathName, string fileText)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(pathName)); // 确保目录存在
                using (StreamWriter sw = File.AppendText(pathName))
                {
                    sw.WriteLine(fileText);
                    sw.Flush();
                    sw.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                FailedLog(pathName, e.Message);
                return false;
            }
        }
        private static void FailedLog(string pathName, string msg)
        {
            string time2 = DateTime.Now.ToString("yyyy-MM-dd");
            string logPath = $"log/{DateTime.Now.Year}年/{time2}写入失败.txt";
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logPath)); // 确保目录存在
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("\n   " + DateTime.Now + "\n 写入路径:" + pathName + "\n msg:" + msg + "\n");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
