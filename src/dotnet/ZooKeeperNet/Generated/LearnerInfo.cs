using Org.Apache.Jute;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Server.Quorum
{
    public class LearnerInfo : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(LearnerInfo));

		public long Serverid
		{
			get;
			set;
		}

		public int ProtocolVersion
		{
			get;
			set;
		}

		public LearnerInfo()
		{
		}

		public LearnerInfo(long serverid, int protocolVersion)
		{
			this.Serverid = serverid;
			this.ProtocolVersion = protocolVersion;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteLong(this.Serverid, "serverid");
			a_.WriteInt(this.ProtocolVersion, "protocolVersion");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Serverid = a_.ReadLong("serverid");
			this.ProtocolVersion = a_.ReadInt("protocolVersion");
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
					binaryOutputArchive.WriteLong(this.Serverid, "serverid");
					binaryOutputArchive.WriteInt(this.ProtocolVersion, "protocolVersion");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //LearnerInfo.log.Error(ex);
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
			LearnerInfo learnerInfo = (LearnerInfo)obj;
			if (learnerInfo == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = (this.Serverid == learnerInfo.Serverid) ? 0 : ((this.Serverid < learnerInfo.Serverid) ? -1 : 1);
			if (num != 0)
			{
				return num;
			}
			num = ((this.ProtocolVersion == learnerInfo.ProtocolVersion) ? 0 : ((this.ProtocolVersion < learnerInfo.ProtocolVersion) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LearnerInfo learnerInfo = (LearnerInfo)obj;
			if (learnerInfo == null)
			{
				return false;
			}
			if (object.ReferenceEquals(learnerInfo, this))
			{
				return true;
			}
			bool flag = this.Serverid == learnerInfo.Serverid;
			if (!flag)
			{
				return flag;
			}
			flag = (this.ProtocolVersion == learnerInfo.ProtocolVersion);
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
			num2 = (int)this.Serverid;
			num = 37 * num + num2;
			num2 = this.ProtocolVersion;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LLearnerInfo(li)";
		}
	}
}
