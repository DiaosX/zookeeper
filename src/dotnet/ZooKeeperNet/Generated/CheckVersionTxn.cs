using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Txn
{
	public class CheckVersionTxn : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(CheckVersionTxn));

		public string Path
		{
			get;
			set;
		}

		public int Version
		{
			get;
			set;
		}

		public CheckVersionTxn()
		{
		}

		public CheckVersionTxn(string path, int version)
		{
			this.Path = path;
			this.Version = version;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.WriteInt(this.Version, "version");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
			this.Version = a_.ReadInt("version");
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
					binaryOutputArchive.WriteInt(this.Version, "version");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //CheckVersionTxn.log.Error(ex);
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
			CheckVersionTxn checkVersionTxn = (CheckVersionTxn)obj;
			if (checkVersionTxn == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Path.CompareTo(checkVersionTxn.Path);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Version == checkVersionTxn.Version) ? 0 : ((this.Version < checkVersionTxn.Version) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CheckVersionTxn checkVersionTxn = (CheckVersionTxn)obj;
			if (checkVersionTxn == null)
			{
				return false;
			}
			if (object.ReferenceEquals(checkVersionTxn, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(checkVersionTxn.Path);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Version == checkVersionTxn.Version);
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
			num2 = this.Version;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LCheckVersionTxn(si)";
		}
	}
}
