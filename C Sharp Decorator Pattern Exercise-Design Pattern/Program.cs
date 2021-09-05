using System;
using System.IO;
using System.Text;

namespace C_Sharp_Decorator_Pattern_Exercise_Design_Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseFile txtFile = new TxtFile("txtTest");  //建立一個txt檔
            txtFile = new WriteString(txtFile, "跳蚤是大帥哥");   //寫入"跳蚤是大帥哥"
            txtFile = new WriteDate(txtFile);   //寫入今天日期
            txtFile = new WriteHelloWorld(txtFile); //寫入固定字串"Hello, World!"
            txtFile = new CloseFile(txtFile);   //關閉txt檔案
            txtFile.Operation();    //執行裝飾者模式
            /*txtTest.txt
             *跳蚤是大帥哥
             *2021/09/05
             *Hello, World! 
             */

            BaseFile csvFile = new CsvFile("csvTest");  //建立一個csv檔
            csvFile = new WriteDate(csvFile);   //寫入今天日期
            csvFile = new WriteString(csvFile, "跳蚤真他媽的帥");  //寫入"跳蚤真他媽的帥"
            csvFile = new CloseFile(csvFile);   //關閉csv檔案
            csvFile.Operation();    //執行裝飾者模式
            /*csvTest.csv
             * 2021/9/5
             * 跳蚤真他媽的帥
             */
        }
    }
    public abstract class BaseFile  //所有的被裝飾者與裝飾者抽象類別,皆繼承於此
    {
        protected string filename;
        public abstract StreamWriter Operation();
    }
    public class TxtFile : BaseFile //建立TXT檔,被裝飾者
    {
        public TxtFile(string name)
        {
            base.filename = name + ".txt";
        }
        public override StreamWriter Operation() => new StreamWriter(this.filename, true);  //寫入2變數的建構子

    }
    public class CsvFile : BaseFile //建立CSV檔,被裝飾者
    {
        public CsvFile(string name)
        {
            base.filename = name + ".csv";
        }
        public override StreamWriter Operation() => new StreamWriter(this.filename, true, Encoding.Unicode);    //寫入3變數的建構子
    }
    public abstract class FileOperation : BaseFile    //所有的裝飾者類別皆繼承於此,宣告為抽象類別,是為不要被繼承
    {
        protected BaseFile baseFile;
        protected StreamWriter sw;
        public FileOperation(BaseFile b)    //進行裝飾
        {
            baseFile = b;
        }
        public abstract void ExtraOperation();
        public override StreamWriter Operation()
        {
            sw = baseFile.Operation();  //呼叫被裝飾者
            ExtraOperation();   //執行自己的方法
            return sw;
        }
    }
    public class WriteString : FileOperation    //將設定的字串寫入檔案
    {
        private string WriteStr;
        public WriteString(BaseFile bf, String str) : base(bf)    //加入裝飾
        {
            this.WriteStr = str;    //要寫入檔案的字串
        }
        public override void ExtraOperation()
        {
            base.sw.WriteLine(WriteStr);
        }
    }
    public class WriteDate : FileOperation  //將今天的日期寫入檔案
    {
        public WriteDate(BaseFile bf) : base(bf)  //加入裝飾
        {

        }
        public override void ExtraOperation()  
        {
            DateTime now = DateTime.Today;
            base.sw.WriteLine(now.ToString("yyyy/MM/dd"));
        }
    }
    public class WriteHelloWorld : FileOperation    //寫入固定字串"Hello, World!"
    {
        public WriteHelloWorld(BaseFile bf) : base(bf)
        {

        }
        public override void ExtraOperation()   
        {
            base.sw.WriteLine("Hello, World!");
        }
    }
    public class CloseFile : FileOperation  //關閉檔案
    {
        public CloseFile(BaseFile bf) : base(bf)
        {

        }
        public override void ExtraOperation()
        {
            base.sw.Close();
        }
    }
}
