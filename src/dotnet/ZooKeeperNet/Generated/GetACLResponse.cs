using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetACLResponse : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(GetACLResponse));

		public System.Collections.Generic.IEnumerable<ACL> Acl
		{
			get;
			set;
		}

		public Stat Stat
		{
			get;
			set;
		}

		public GetACLResponse()
		{
		}

		public GetACLResponse(System.Collections.Generic.IEnumerable<ACL> acl, Stat stat)
		{
			this.Acl = acl;
			this.Stat = stat;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.StartVector<ACL>(this.Acl, "acl");
			if (this.Acl != null)
			{
				foreach (ACL current in this.Acl)
				{
					a_.WriteRecord(current, "e1");
				}
			}
			a_.EndVector<ACL>(this.Acl, "acl");
			a_.WriteRecord(this.Stat, "stat");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
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
			this.Stat = new Stat();
			a_.ReadRecord(this.Stat, "stat");
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
					binaryOutputArchive.StartVector<ACL>(this.Acl, "acl");
					if (this.Acl != null)
					{
						foreach (ACL current in this.Acl)
						{
							binaryOutputArchive.WriteRecord(current, "e1");
						}
					}
					binaryOutputArchive.EndVector<ACL>(this.Acl, "acl");
					binaryOutputArchive.WriteRecord(this.Stat, "stat");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //GetACLResponse.log.Error(ex);
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
			throw new System.InvalidOperationException("comparing GetACLResponse is unimplemented");
		}

		public override bool Equals(object obj)
		{
			GetACLResponse getACLResponse = (GetACLResponse)obj;
			if (getACLResponse == null)
			{
				return false;
			}
			if (object.ReferenceEquals(getACLResponse, this))
			{
				return true;
			}
			bool flag = this.Acl.Equals(getACLResponse.Acl);
			if (!flag)
			{
				return flag;
			}
			flag = this.Stat.Equals(getACLResponse.Stat);
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
			hashCode = this.Acl.GetHashCode();
			num = 37 * num + hashCode;
			hashCode = this.Stat.GetHashCode();
			return 37 * num + hashCode;
		}

		public static string Signature()
		{
			return "LGetACLResponse([LACL(iLId(ss))]LStat(lllliiiliil))";
		}
	}
}
