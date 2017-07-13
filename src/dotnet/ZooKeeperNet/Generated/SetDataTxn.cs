using Org.Apache.Jute;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Txn
{
    public class SetDataTxn : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(SetDataTxn));

		public string Path
		{
			get;
			set;
		}

		public byte[] Data
		{
			get;
			set;
		}

		public int Version
		{
			get;
			set;
		}

		public SetDataTxn()
		{
		}

		public SetDataTxn(string path, byte[] data, int version)
		{
			this.Path = path;
			this.Data = data;
			this.Version = version;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.WriteBuffer(this.Data, "data");
			a_.WriteInt(this.Version, "version");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
			this.Data = a_.ReadBuffer("data");
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
					binaryOutputArchive.WriteBuffer(this.Data, "data");
					binaryOutputArchive.WriteInt(this.Version, "version");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //SetDataTxn.log.Error(ex);
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
			SetDataTxn setDataTxn = (SetDataTxn)obj;
			if (setDataTxn == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Path.CompareTo(setDataTxn.Path);
			if (num != 0)
			{
				return num;
			}
			num = this.Data.CompareTo(setDataTxn.Data);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Version == setDataTxn.Version) ? 0 : ((this.Version < setDataTxn.Version) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetDataTxn setDataTxn = (SetDataTxn)obj;
			if (setDataTxn == null)
			{
				return false;
			}
			if (object.ReferenceEquals(setDataTxn, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(setDataTxn.Path);
			if (!flag)
			{
				return flag;
			}
			flag = this.Data.Equals(setDataTxn.Data);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Version == setDataTxn.Version);
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
			num2 = this.Data.GetHashCode();
			num = 37 * num + num2;
			num2 = this.Version;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LSetDataTxn(sBi)";
		}
	}
}
