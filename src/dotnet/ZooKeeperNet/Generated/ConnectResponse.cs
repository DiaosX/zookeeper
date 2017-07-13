using Org.Apache.Jute;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class ConnectResponse : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(ConnectResponse));

		public int ProtocolVersion
		{
			get;
			set;
		}

		public int TimeOut
		{
			get;
			set;
		}

		public long SessionId
		{
			get;
			set;
		}

		public byte[] Passwd
		{
			get;
			set;
		}

		public ConnectResponse()
		{
		}

		public ConnectResponse(int protocolVersion, int timeOut, long sessionId, byte[] passwd)
		{
			this.ProtocolVersion = protocolVersion;
			this.TimeOut = timeOut;
			this.SessionId = sessionId;
			this.Passwd = passwd;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteInt(this.ProtocolVersion, "protocolVersion");
			a_.WriteInt(this.TimeOut, "timeOut");
			a_.WriteLong(this.SessionId, "sessionId");
			a_.WriteBuffer(this.Passwd, "passwd");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.ProtocolVersion = a_.ReadInt("protocolVersion");
			this.TimeOut = a_.ReadInt("timeOut");
			this.SessionId = a_.ReadLong("sessionId");
			this.Passwd = a_.ReadBuffer("passwd");
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
					binaryOutputArchive.WriteInt(this.ProtocolVersion, "protocolVersion");
					binaryOutputArchive.WriteInt(this.TimeOut, "timeOut");
					binaryOutputArchive.WriteLong(this.SessionId, "sessionId");
					binaryOutputArchive.WriteBuffer(this.Passwd, "passwd");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //ConnectResponse.log.Error(ex);
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
			ConnectResponse connectResponse = (ConnectResponse)obj;
			if (connectResponse == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = (this.ProtocolVersion == connectResponse.ProtocolVersion) ? 0 : ((this.ProtocolVersion < connectResponse.ProtocolVersion) ? -1 : 1);
			if (num != 0)
			{
				return num;
			}
			num = ((this.TimeOut == connectResponse.TimeOut) ? 0 : ((this.TimeOut < connectResponse.TimeOut) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.SessionId == connectResponse.SessionId) ? 0 : ((this.SessionId < connectResponse.SessionId) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = this.Passwd.CompareTo(connectResponse.Passwd);
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ConnectResponse connectResponse = (ConnectResponse)obj;
			if (connectResponse == null)
			{
				return false;
			}
			if (object.ReferenceEquals(connectResponse, this))
			{
				return true;
			}
			bool flag = this.ProtocolVersion == connectResponse.ProtocolVersion;
			if (!flag)
			{
				return flag;
			}
			flag = (this.TimeOut == connectResponse.TimeOut);
			if (!flag)
			{
				return flag;
			}
			flag = (this.SessionId == connectResponse.SessionId);
			if (!flag)
			{
				return flag;
			}
			flag = this.Passwd.Equals(connectResponse.Passwd);
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
			num2 = this.ProtocolVersion;
			num = 37 * num + num2;
			num2 = this.TimeOut;
			num = 37 * num + num2;
			num2 = (int)this.SessionId;
			num = 37 * num + num2;
			num2 = this.Passwd.GetHashCode();
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LConnectResponse(iilB)";
		}
	}
}
