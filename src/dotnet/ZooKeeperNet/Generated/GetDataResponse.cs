using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetDataResponse : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(GetDataResponse));

		public byte[] Data
		{
			get;
			set;
		}

		public Stat Stat
		{
			get;
			set;
		}

		public GetDataResponse()
		{
		}

		public GetDataResponse(byte[] data, Stat stat)
		{
			this.Data = data;
			this.Stat = stat;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteBuffer(this.Data, "data");
			a_.WriteRecord(this.Stat, "stat");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Data = a_.ReadBuffer("data");
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
					binaryOutputArchive.WriteBuffer(this.Data, "data");
					binaryOutputArchive.WriteRecord(this.Stat, "stat");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //GetDataResponse.log.Error(ex);
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
			GetDataResponse getDataResponse = (GetDataResponse)obj;
			if (getDataResponse == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Data.CompareTo(getDataResponse.Data);
			if (num != 0)
			{
				return num;
			}
			num = this.Stat.CompareTo(getDataResponse.Stat);
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetDataResponse getDataResponse = (GetDataResponse)obj;
			if (getDataResponse == null)
			{
				return false;
			}
			if (object.ReferenceEquals(getDataResponse, this))
			{
				return true;
			}
			bool flag = this.Data.Equals(getDataResponse.Data);
			if (!flag)
			{
				return flag;
			}
			flag = this.Stat.Equals(getDataResponse.Stat);
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
			hashCode = this.Data.GetHashCode();
			num = 37 * num + hashCode;
			hashCode = this.Stat.GetHashCode();
			return 37 * num + hashCode;
		}

		public static string Signature()
		{
			return "LGetDataResponse(BLStat(lllliiiliil))";
		}
	}
}
