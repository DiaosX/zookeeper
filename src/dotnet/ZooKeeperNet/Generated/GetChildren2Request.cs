using Org.Apache.Jute;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetChildren2Request : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(GetChildren2Request));

		public string Path
		{
			get;
			set;
		}

		public bool Watch
		{
			get;
			set;
		}

		public GetChildren2Request()
		{
		}

		public GetChildren2Request(string path, bool watch)
		{
			this.Path = path;
			this.Watch = watch;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.WriteBool(this.Watch, "watch");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
			this.Watch = a_.ReadBool("watch");
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
					binaryOutputArchive.WriteBool(this.Watch, "watch");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //GetChildren2Request.log.Error(ex);
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
			GetChildren2Request getChildren2Request = (GetChildren2Request)obj;
			if (getChildren2Request == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Path.CompareTo(getChildren2Request.Path);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Watch == getChildren2Request.Watch) ? 0 : (this.Watch ? 1 : -1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetChildren2Request getChildren2Request = (GetChildren2Request)obj;
			if (getChildren2Request == null)
			{
				return false;
			}
			if (object.ReferenceEquals(getChildren2Request, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(getChildren2Request.Path);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Watch == getChildren2Request.Watch);
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
			num2 = (this.Watch ? 0 : 1);
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LGetChildren2Request(sz)";
		}
	}
}
