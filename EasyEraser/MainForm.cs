using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyEraser
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 测试目录
        /// </summary>
        string TargetDirectory = @"D:\EraseTestDirectory";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 擦除目录
        /// </summary>
        /// <param name="TargetDirectory">目标目录</param>
        private void EraseDirectory(string TargetDirectory)
        {
            Debug.Print("——————————————————————————————\n>>> 擦除目录：{0}", TargetDirectory);

            //对文件并行计算
            Parallel.ForEach<string>(Directory.GetFiles(TargetDirectory), new Action<string>(
                (ChildFilePath) => 
                {
                    EraseFile(ChildFilePath);
                }
            ));

            //对目录串行计算
            foreach (string ChildDirectory in Directory.GetDirectories(TargetDirectory))
                EraseDirectory(ChildDirectory);
        }

        /// <summary>
        /// 擦除文件
        /// </summary>
        /// <param name="TargetFile">目标文件</param>
        private void EraseFile(string TargetFile)
        {
            Debug.Print("擦除文件：{0}", TargetFile);
            if (!File.Exists(TargetFile)) return;
            try
            {
                using (FileStream EraseStream = new FileStream(TargetFile, FileMode.Open))
                {
                    //用 00 擦除
                    EraseFileByTargetValue(EraseStream);
                    //用 FF 擦除
                    EraseFileByTargetValue(EraseStream, 0xFF);
                    //随机擦除
                    EraseFileByRandomValue(EraseStream);
                    //随机擦除
                    EraseFileByRandomValue(EraseStream);
                    //随机擦除
                    EraseFileByRandomValue(EraseStream);
                    //用 FF 擦除
                    EraseFileByTargetValue(EraseStream);

                    Debug.Print("文件长度：{0} ,\t流对象哈希值：{1}", EraseStream.Length, EraseStream.GetHashCode());
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"擦除文件 {TargetFile} 遇到异常：{ex.Message}");
            }
            Debug.Print("————————————————");
        }

        /// <summary>
        /// 使用特定值擦除文件
        /// </summary>
        /// <param name="EraseStream">擦除文件流</param>
        /// <param name="EraseValue">填充文件值</param>
        private void EraseFileByTargetValue(FileStream EraseStream, byte EraseValue = 0)
        {
            try
            {
                //检查流可用
                if (EraseStream == null) throw new Exception("空的擦除流对象...");
                if (!EraseStream.CanSeek) throw new Exception("不可定位的擦除流对象...");
                if (!EraseStream.CanWrite) throw new Exception("不可写入的擦除流对象...");
                //擦除缓冲
                byte[] EraseBuffer = new byte[512];
                //使用特定值填充缓冲区
                for (int Index = 0; Index < 512; Index++)
                    EraseBuffer[Index] = EraseValue;
                //重定位擦除文件流
                EraseStream.Seek(0, SeekOrigin.Begin);
                //填充擦除缓冲到流
                while (EraseStream.Position < EraseStream.Length)
                {
                    EraseStream.Write(EraseBuffer, 0, 512);
                }
                //应用缓冲
                EraseStream.Flush();
                Debug.Print($"使用 0x{EraseValue.ToString("X").PadRight(2)} 擦除文件 {EraseStream.Name} 完毕！");
            }
            catch (Exception ex)
            {
                Debug.Print($"使用 0x{EraseValue.ToString("X").PadRight(2)} 擦除文件 {EraseStream.Name} 遇到异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 使用随机值擦除文件
        /// </summary>
        /// <param name="EraseStream">擦除文件流</param>
        private void EraseFileByRandomValue(FileStream EraseStream)
        {
            try
            {
                //检查流可用
                if (EraseStream == null) throw new Exception("空的擦除流对象...");
                if (!EraseStream.CanSeek) throw new Exception("不可定位的擦除流对象...");
                if (!EraseStream.CanWrite) throw new Exception("不可写入的擦除流对象...");
                //擦出缓冲
                Random EraseRandom = new Random();
                byte[] EraseBuffer = new byte[512];
                //重定位擦除文件流
                EraseStream.Seek(0, SeekOrigin.Begin);
                //填充擦除缓冲到流
                while (EraseStream.Position < EraseStream.Length)
                {
                    //使用随机值填充缓冲
                    EraseRandom.NextBytes(EraseBuffer);
                    EraseStream.Write(EraseBuffer, 0, 512);
                }
                //应用缓冲
                EraseStream.Flush();
                Debug.Print($"使用随机值擦除文件 {EraseStream.Name} 完毕！");
            }
            catch (Exception ex)
            {
                Debug.Print($"使用随机值擦除文件 {EraseStream.Name} 遇到异常：{ex.Message}");
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            DateTime StartTime = DateTime.Now;
            EraseDirectory(TargetDirectory);
            DateTime FinishTime = DateTime.Now;
            MessageBox.Show($"擦除文件用时：{(FinishTime - StartTime).TotalSeconds.ToString()} s");
        }
    }
}
