using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace ZooKeeperNet.Log
{
    public class Logger : IDisposable
    {
        //日志对象的缓存队列
        private static Queue<Msg> _msgs;
        //日志文件保存的路径
        private static string _path;
        //日志写入线程的控制标记
        private static bool _state;
        //日志记录的类型
        private static LogType _type;
        //日志文件生命周期的时间标记
        private static DateTime _timeSign;
        //日志文件写入流对象
        private static StreamWriter _writer;

        static Logger()
        {
            if (_msgs == null)
            {
                _state = true;
                _path = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
                if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
                _type = LogType.Daily;
                _msgs = new Queue<Msg>();
                Thread thread = new Thread(Work);
                thread.Start();
            }
        }

        //日志文件写入线程执行的方法
        private static void Work()
        {
            while (true)
            {
                //判断队列中是否存在待写入的日志
                if (_msgs.Count > 0)
                {
                    Msg msg = null;
                    lock (_msgs)
                    {
                        msg = _msgs.Dequeue();
                    }
                    if (msg != null)
                    {
                        FileWrite(msg);
                    }
                }
                else
                {
                    //判断是否已经发出终止日志并关闭的消息
                    if (_state)
                    {
                        Thread.Sleep(1);
                    }
                    else
                    {
                        FileClose();
                    }
                }
            }
        }

        //根据日志类型获取日志文件名，并同时创建文件到期的时间标记
        //通过判断文件的到期时间标记将决定是否创建新文件。
        private static string GetFilename()
        {
            var now = DateTime.Now;
            var format = "";
            switch (_type)
            {
                case LogType.Daily:
                    _timeSign = new DateTime(now.Year, now.Month, now.Day);
                    _timeSign = _timeSign.AddDays(1);
                    format = "yyyyMMdd'.log'";
                    break;
                case LogType.Weekly:
                    _timeSign = new DateTime(now.Year, now.Month, now.Day);
                    _timeSign = _timeSign.AddDays(7);
                    format = "yyyyMMdd'.log'";
                    break;
                case LogType.Monthly:
                    _timeSign = new DateTime(now.Year, now.Month, 1);
                    _timeSign = _timeSign.AddMonths(1);
                    format = "yyyyMM'.log'";
                    break;
                case LogType.Annually:
                    _timeSign = new DateTime(now.Year, 1, 1);
                    _timeSign = _timeSign.AddYears(1);
                    format = "yyyy'.log'";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return now.ToString(format);
        }

        //写入日志文本到文件的方法
        private static void FileWrite(Msg msg)
        {
            try
            {
                if (_writer == null)
                {
                    FileOpen();
                }
                //判断文件到期标志，如果当前文件到期则关闭当前文件创建新的日志文件
                if (DateTime.Now >= _timeSign)
                {
                    FileClose();
                    FileOpen();
                }
                if (_writer != null)
                {
                    _writer.Write(msg.Datetime);
                    _writer.Write('\t');
                    _writer.Write(msg.Type);
                    _writer.Write('\t');
                    _writer.WriteLine(msg.Text);
                    _writer.Flush();
                }
            }
            catch (Exception e)
            {
                Console.Out.Write(e);
            }
        }

        //打开文件准备写入
        private static void FileOpen()
        {
            _writer = new StreamWriter(_path + GetFilename(), true, Encoding.UTF8);
        }

        //关闭打开的日志文件
        private static void FileClose()
        {
            if (_writer != null)
            {
                _writer.Flush();
                _writer.Close();
                _writer.Dispose();
                _writer = null;
            }
        }

        /// <summary>
        /// 写入新日志，根据指定的日志对象Msg
        /// </summary>
        /// <param name="msg">日志内容对象</param>
        public static void Write(Msg msg)
        {
            if (msg != null)
            {
                lock (_msgs)
                {
                    _msgs.Enqueue(msg);
                }
            }
        }

        /// <summary>
        /// 写入新日志，根据指定的日志内容和信息类型，采用当前时间为日志时间写入新日志
        /// </summary>
        /// <param name="text">日志内容</param>
        /// <param name="type">信息类型</param>
        public static void Write(string text, MsgType type)
        {
            Write(new Msg(text, type));
        }

        public static void Write(string text)
        {
            Write(new Msg(text, MsgType.Information));
        }

        /// <summary>
        /// 写入新日志，根据指定的日志时间、日志内容和信息类型写入新日志
        /// </summary>
        /// <param name="dt">日志时间</param>
        /// <param name="text">日志内容</param>
        /// <param name="type">信息类型</param>
        public void Write(DateTime dt, string text, MsgType type)
        {
            Write(new Msg(dt, text, type));
        }

        /// <summary>
        /// 写入新日志，根据指定的异常类和信息类型写入新日志
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="type">信息类型</param>
        public void Write(Exception e, MsgType type)
        {
            Write(new Msg(e.Message, type));
        }

        /// <summary>
        /// 销毁日志对象
        /// </summary>
        public void Dispose()
        {
            _state = false;
        }
    }

    /// <summary>
    /// 表示一个日志记录的对象
    /// </summary>
    public class Msg
    {
        //日志记录的时间
        //日志记录的内容
        //日志记录的类型

        /// <summary>
        /// 创建新的日志记录实例;日志记录的内容为空,消息类型为MsgType.Unknown,日志时间为当前时间
        /// </summary>
        public Msg() : this("", MsgType.Unknown){ }

        /// <summary>
        /// 创建新的日志记录实例;日志事件为当前时间
        /// </summary>
        /// <param name="t">日志记录的文本内容</param>
        /// <param name="p">日志记录的消息类型</param>
        public Msg(string t, MsgType p) : this(DateTime.Now, t, p){ }

        /// <summary>
        /// 创建新的日志记录实例;
        /// </summary>
        /// <param name="dt">日志记录的时间</param>
        /// <param name="t">日志记录的文本内容</param>
        /// <param name="p">日志记录的消息类型</param>
        public Msg(DateTime dt, string t, MsgType p)
        {
            Datetime = dt;
            Type = p;
            Text = t;
        }

        /// <summary>
        /// 获取或设置日志记录的时间
        /// </summary>
        public DateTime Datetime { get; set; }

        /// <summary>
        /// 获取或设置日志记录的文本内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 获取或设置日志记录的消息类型
        /// </summary>
        public MsgType Type { get; set; }

        public new string ToString()
        {
            return Datetime.ToString(CultureInfo.CurrentCulture) + "\t" + Text + "\n";
        }
    }

    /// <summary>
    /// 日志类型的枚举
    /// </summary>
    /// <remarks>日志类型枚举指示日志文件创建的方式，如果日志比较多可考虑每天创建一个日志文件
    /// 如果日志量比较小可考虑每周、每月或每年创建一个日志文件</remarks>
    public enum LogType
    {
        /// <summary>
        /// 此枚举指示每天创建一个新的日志文件
        /// </summary>
        Daily,

        /// <summary>
        /// 此枚举指示每周创建一个新的日志文件
        /// </summary>
        Weekly,

        /// <summary>
        /// 此枚举指示每月创建一个新的日志文件
        /// </summary>
        Monthly,

        /// <summary>
        /// 此枚举指示每年创建一个新的日志文件
        /// </summary>
        Annually
    }

    /// <summary>
    /// 日志消息类型的枚举
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 指示未知信息类型的日志记录
        /// </summary>
        Unknown,

        /// <summary>
        /// 指示普通信息类型的日志记录
        /// </summary>
        Information,

        /// <summary>
        /// 指示警告信息类型的日志记录
        /// </summary>
        Warning,

        /// <summary>
        /// 指示错误信息类型的日志记录
        /// </summary>
        Error,

        /// <summary>
        /// 指示成功信息类型的日志记录
        /// </summary>
        Success
    }
}