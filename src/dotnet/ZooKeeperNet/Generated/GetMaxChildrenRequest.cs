using Org.Apache.Jute;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetMaxChildrenRequest : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(GetMaxChildrenRequest));

		public string Path
		{
			get;
			set;
		}

		public GetMaxChildrenRequest()
		{
		}

		public GetMaxChildrenRequest(string path)
		{
			this.Path = path;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
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
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //GetMaxChildrenRequest.log.Error(ex);
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
			GetMaxChildrenRequest getMaxChildrenRequest = (GetMaxChildrenRequest)obj;
			if (getMaxChildrenRequest == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Path.CompareTo(getMaxChildrenRequest.Path);
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetMaxChildrenRequest getMaxChildrenRequest = (GetMaxChildrenRequest)obj;
			if (getMaxChildrenRequest == null)
			{
				return false;
			}
			if (object.ReferenceEquals(getMaxChildrenRequest, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(getMaxChildrenRequest.Path);
			if (!flag)
			{
				return flag;
			}
			return flag;
		}

		public override int GetHashCode()
		{
			int num = 17;
			int hashCode = base.GetType().GetHashCode();
			num = 37 * num + hashCode;
			hashCode = this.Path.GetHashCode();
			return 37 * num + hashCode;
		}

		public static string Signature()
		{
			return "LGetMaxChildrenRequest(s)";
		}
	}
}
