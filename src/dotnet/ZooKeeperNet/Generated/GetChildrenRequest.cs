using Org.Apache.Jute;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetChildrenRequest : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(GetChildrenRequest));

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

		public GetChildrenRequest()
		{
		}

		public GetChildrenRequest(string path, bool watch)
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
                //GetChildrenRequest.log.Error(ex);
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
			GetChildrenRequest getChildrenRequest = (GetChildrenRequest)obj;
			if (getChildrenRequest == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Path.CompareTo(getChildrenRequest.Path);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Watch == getChildrenRequest.Watch) ? 0 : (this.Watch ? 1 : -1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetChildrenRequest getChildrenRequest = (GetChildrenRequest)obj;
			if (getChildrenRequest == null)
			{
				return false;
			}
			if (object.ReferenceEquals(getChildrenRequest, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(getChildrenRequest.Path);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Watch == getChildrenRequest.Watch);
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
			return "LGetChildrenRequest(sz)";
		}
	}
}
