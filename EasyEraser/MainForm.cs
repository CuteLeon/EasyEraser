using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyEraser
{
    public partial class MainForm : Form
    {
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        /// <summary>
        /// 擦除任务完成?
        /// </summary>
        bool EraseTaskFinished = false;
        /// <summary>
        /// 托盘消息通知对象
        /// </summary>
        NotifyIcon UnityNotifyIcon = new NotifyIcon();
        /// <summary>
        /// 擦除目标路径
        /// </summary>
        string TargetDirectory = string.Empty;
        /// <summary>
        /// 擦除任务Worker
        /// </summary>
        BackgroundWorker EraseWorker = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();

            if (Environment.GetCommandLineArgs().Length < 2) ExitMe();
            TargetDirectory = Environment.GetCommandLineArgs()[1];
            if (string.IsNullOrEmpty(TargetDirectory)) ExitMe();
            if (!Directory.Exists(TargetDirectory)) ExitMe();

            Ini();
        }

        private void Ini()
        {
            UnityNotifyIcon.Text = "正在擦除目录和文件...";
            UnityNotifyIcon.Icon = this.Icon;
            UnityNotifyIcon.Visible = true;
            UnityNotifyIcon.MouseClick += delegate { this.Visible = !this.Visible; };
            TitleLabel.MouseDown += delegate {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            };
            MinButton.MouseEnter += delegate { MinButton.Image = UnityResource.Min_1; };
            MinButton.MouseLeave += delegate { MinButton.Image = UnityResource.Min_0; };
            MinButton.MouseDown += delegate { MinButton.Image = UnityResource.Min_2; };
            MinButton.MouseUp += delegate { MinButton.Image = UnityResource.Min_1; };
            MinButton.Click += delegate { HideMe(); };
            this.FormClosing += delegate (object s, FormClosingEventArgs e)
            {
                if (EraseTaskFinished)
                {
                    return;
                }
                else
                {
                    e.Cancel = true;
                    HideMe();
                }
            };
            this.VisibleChanged += delegate { ProgressTimer.Enabled = this.Visible; };

            EraseWorker.WorkerSupportsCancellation = true;
            EraseWorker.DoWork += new DoWorkEventHandler((s, e) =>
            {
                EraseDirectory(TargetDirectory);
            });
            EraseWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((s, e) =>
            {
                Application.DoEvents();
                IconLabel.Image = UnityResource.Finished;
                MessageLabel.Text = "痕迹擦除完成！即将退出擦除工具...";
                Application.DoEvents();
                UnityNotifyIcon.ShowBalloonTip(3000, "痕迹擦除完成！", "目录和文件擦除完成！\n即将退出擦除工具...", ToolTipIcon.Info);
                Thread.Sleep(3000);
                ExitMe();
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width - 3, Screen.PrimaryScreen.Bounds.Height - this.Height - 300);
            this.TopMost = true;

            ProgressTimer.Start();
        }

        private void HideMe()
        {
            this.Hide();
            UnityNotifyIcon.ShowBalloonTip(3000, "正在擦除目录和文件...", "擦除程序已经被最小化，\n将在后台继续擦除目录和文件...", ToolTipIcon.Info);
        }
        private void ExitMe()
        {
            UnityNotifyIcon.Visible = false;
            ProgressTimer.Stop();
            EraseTaskFinished = true;
            Environment.Exit(1);
        }

        int ProgressImageIndex = 0;
        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            ProgressImageIndex = (ProgressImageIndex + 1) % 12;
            IconLabel.Image?.Dispose();
            IconLabel.Image = UnityResource.ResourceManager.GetObject($"ProgressBar_{ProgressImageIndex}") as Bitmap;
        }


        /// <summary>
        /// 擦除目录
        /// </summary>
        /// <param name="TargetDirectory">目标目录</param>
        private void EraseDirectory(string TargetDirectory)
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => { MessageLabel.Text = $"擦除目录：{TargetDirectory}"; }));
            else
                MessageLabel.Text = $"擦除目录：{TargetDirectory}";

            Debug.Print("——————————————————————————————\n>>> 擦除目录：{0}", TargetDirectory);

            //对文件并行计算
            Parallel.ForEach<string>(Directory.GetFiles(TargetDirectory), new Action<string>(
                (ChildFilePath) =>
                {
                    EraseFile(ChildFilePath);
                    RenameFileAndDelete(ChildFilePath);
                }
            ));

            //对目录串行计算
            foreach (string ChildDirectory in Directory.GetDirectories(TargetDirectory))
                EraseDirectory(ChildDirectory);

            Debug.Print("删除目录：{0}", TargetDirectory);
            try
            {
                Directory.Delete(TargetDirectory);
            }
            catch (Exception ex)
            {
                Debug.Print("删除目录遇到异常：{0}", ex.Message);
            }
        }

        /// <summary>
        /// 擦除文件
        /// </summary>
        /// <param name="TargetFile">目标文件</param>
        private void EraseFile(string TargetFile)
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => { MessageLabel.Text = $"擦除文件：{TargetFile}"; }));
            else
                MessageLabel.Text = $"擦除文件：{TargetFile}";
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

        private void MainForm_Shown(object sender, EventArgs e)
        {
            EraseWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 混淆文件名并删除
        /// </summary>
        /// <param name="FilePath"></param>
        private void RenameFileAndDelete(string FilePath)
        {
            try
            {
                string NewFilePath = Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileName(Path.GetRandomFileName());
                File.Move(FilePath, NewFilePath);
                File.Delete(NewFilePath);
            }
            catch (Exception ex)
            {
                Debug.Print("混淆文件名并删除时遇到异常：{0}", ex.Message);
            }
        }

    }
}
