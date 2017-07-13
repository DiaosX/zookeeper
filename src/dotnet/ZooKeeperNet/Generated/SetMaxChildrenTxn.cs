using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Txn
{
	public class SetMaxChildrenTxn : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(SetMaxChildrenTxn));

		public string Path
		{
			get;
			set;
		}

		public int Max
		{
			get;
			set;
		}

		public SetMaxChildrenTxn()
		{
		}

		public SetMaxChildrenTxn(string path, int max)
		{
			this.Path = path;
			this.Max = max;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.WriteInt(this.Max, "max");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
			this.Max = a_.ReadInt("max");
			a_.EndRecord(tag);
		}

		public override string ToString()
		{
			try
			{
				System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
				using (EndianBinaryWriter endianBinaryWriter = new EndianBinaryWriter(EndianBitConverter.Big, memoryStream, System.Text.Encoding.UTF8))
				{
					BinaryOutputArchive binaryOutputArchive = new BinaryOutputArchive(endianBinaryWriter);
					binaryOutputArchive.StartRecord(this, string.Empty);
					binaryOutputArchive.WriteString(this.Path, "path");
					binaryOutputArchive.WriteInt(this.Max, "max");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //SetMaxChildrenTxn.log.Error(ex);
                Logger.Write(ex.Message, MsgType.Error);

            }
            return "ERROR";
		}

		public void Write(EndianBinaryWriter writer)
		{
			BinaryOutputArchive a_ = new BinaryOutputArchive(writer);
			this.Serialize(a_, string.Empty);
		}

		public void ReadFields(EndianBinaryReader reader)
		{
			BinaryInputArchive a_ = new BinaryInputArchive(reader);
			this.Deserialize(a_, string.Empty);
		}

		public int CompareTo(object obj)
		{
			SetMaxChildrenTxn setMaxChildrenTxn = (SetMaxChildrenTxn)obj;
			if (setMaxChildrenTxn == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Path.CompareTo(setMaxChildrenTxn.Path);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Max == setMaxChildrenTxn.Max) ? 0 : ((this.Max < setMaxChildrenTxn.Max) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetMaxChildrenTxn setMaxChildrenTxn = (SetMaxChildrenTxn)obj;
			if (setMaxChildrenTxn == null)
			{
				return false;
			}
			if (object.ReferenceEquals(setMaxChildrenTxn, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(setMaxChildrenTxn.Path);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Max == setMaxChildrenTxn.Max);
			if (!flag)
			{
				return flag;
			}
			return flag;
		}

		public override int GetHashCode()
		{
			int num = 17;
			int num2 = base.GetType().GetHashCode();
			num = 37 * num + num2;
			num2 = this.Path.GetHashCode();
			num = 37 * num + num2;
			num2 = this.Max;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LSetMaxChildrenTxn(si)";
		}
	}
}
