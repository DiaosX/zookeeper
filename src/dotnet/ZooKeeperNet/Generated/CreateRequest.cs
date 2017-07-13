using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class CreateRequest : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(CreateRequest));

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

		public System.Collections.Generic.IEnumerable<ACL> Acl
		{
			get;
			set;
		}

		public int Flags
		{
			get;
			set;
		}

		public CreateRequest()
		{
		}

		public CreateRequest(string path, byte[] data, System.Collections.Generic.IEnumerable<ACL> acl, int flags)
		{
			this.Path = path;
			this.Data = data;
			this.Acl = acl;
			this.Flags = flags;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.WriteBuffer(this.Data, "data");
			a_.StartVector<ACL>(this.Acl, "acl");
			if (this.Acl != null)
			{
				foreach (ACL current in this.Acl)
				{
					a_.WriteRecord(current, "e1");
				}
			}
			a_.EndVector<ACL>(this.Acl, "acl");
			a_.WriteInt(this.Flags, "flags");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
			this.Data = a_.ReadBuffer("data");
			IIndex index = a_.StartVector("acl");
			if (index != null)
			{
				System.Collections.Generic.List<ACL> list = new System.Collections.Generic.List<ACL>();
				while (!index.Done())
				{
					ACL aCL = new ACL();
					a_.ReadRecord(aCL, "e1");
					list.Add(aCL);
					index.Incr();
				}
				this.Acl = list;
			}
			a_.EndVector("acl");
			this.Flags = a_.ReadInt("flags");
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
					binaryOutputArchive.StartVector<ACL>(this.Acl, "acl");
					if (this.Acl != null)
					{
						foreach (ACL current in this.Acl)
						{
							binaryOutputArchive.WriteRecord(current, "e1");
						}
					}
					binaryOutputArchive.EndVector<ACL>(this.Acl, "acl");
					binaryOutputArchive.WriteInt(this.Flags, "flags");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //CreateRequest.log.Error(ex);
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
			throw new System.InvalidOperationException("comparing CreateRequest is unimplemented");
		}

		public override bool Equals(object obj)
		{
			CreateRequest createRequest = (CreateRequest)obj;
			if (createRequest == null)
			{
				return false;
			}
			if (object.ReferenceEquals(createRequest, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(createRequest.Path);
			if (!flag)
			{
				return flag;
			}
			flag = this.Data.Equals(createRequest.Data);
			if (!flag)
			{
				return flag;
			}
			flag = this.Acl.Equals(createRequest.Acl);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Flags == createRequest.Flags);
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
			num2 = this.Acl.GetHashCode();
			num = 37 * num + num2;
			num2 = this.Flags;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LCreateRequest(sB[LACL(iLId(ss))]i)";
		}
	}
}
