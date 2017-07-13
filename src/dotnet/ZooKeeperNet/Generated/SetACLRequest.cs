using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class SetACLRequest : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(SetACLRequest));

		public string Path
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<ACL> Acl
		{
			get;
			set;
		}

		public int Version
		{
			get;
			set;
		}

		public SetACLRequest()
		{
		}

		public SetACLRequest(string path, System.Collections.Generic.IEnumerable<ACL> acl, int version)
		{
			this.Path = path;
			this.Acl = acl;
			this.Version = version;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.StartVector<ACL>(this.Acl, "acl");
			if (this.Acl != null)
			{
				foreach (ACL current in this.Acl)
				{
					a_.WriteRecord(current, "e1");
				}
			}
			a_.EndVector<ACL>(this.Acl, "acl");
			a_.WriteInt(this.Version, "version");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
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
					binaryOutputArchive.StartVector<ACL>(this.Acl, "acl");
					if (this.Acl != null)
					{
						foreach (ACL current in this.Acl)
						{
							binaryOutputArchive.WriteRecord(current, "e1");
						}
					}
					binaryOutputArchive.EndVector<ACL>(this.Acl, "acl");
					binaryOutputArchive.WriteInt(this.Version, "version");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //SetACLRequest.log.Error(ex);
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
			throw new System.InvalidOperationException("comparing SetACLRequest is unimplemented");
		}

		public override bool Equals(object obj)
		{
			SetACLRequest setACLRequest = (SetACLRequest)obj;
			if (setACLRequest == null)
			{
				return false;
			}
			if (object.ReferenceEquals(setACLRequest, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(setACLRequest.Path);
			if (!flag)
			{
				return flag;
			}
			flag = this.Acl.Equals(setACLRequest.Acl);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Version == setACLRequest.Version);
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
			num2 = this.Acl.GetHashCode();
			num = 37 * num + num2;
			num2 = this.Version;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LSetACLRequest(s[LACL(iLId(ss))]i)";
		}
	}
}
