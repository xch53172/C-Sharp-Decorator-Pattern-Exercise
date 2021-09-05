using System;
using System.IO;
//沒有使用Decorator Pattern撰寫
namespace Non_Design_Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            TxtFile txtFile = new TxtFile("txtTest");  //建立一個txt檔
            txtFile.WriteString("跳蚤是大帥哥");  //寫入"跳蚤是大帥哥"
            txtFile.WriteDate();    //寫入今天日期
            txtFile.WriteHelloWorld();  //寫入固定字串"Hello, World!"
            txtFile.CloseFile();    //關閉txt檔案
        }
    }
    public class TxtFile
    {
        private StreamWriter sw;
        public TxtFile(string name)
        {
            sw = new StreamWriter(name + ".txt", true); //建立txt檔
        }
        public void WriteString(string s)   //將設定的字串寫入檔案
        {
            sw.WriteLine(s);
        }
        public void WriteDate() //將今天的日期寫入檔案
        {
            DateTime now = DateTime.Today;
            sw.WriteLine(now.ToString("yyyy/MM/dd"));
        }
        public void WriteHelloWorld()   //寫入固定字串"Hello, World!"
        {
            sw.WriteLine("Hello, World!");
        }
        public void CloseFile() //關閉檔案
        {
            sw.Close();
        }

    }
}
