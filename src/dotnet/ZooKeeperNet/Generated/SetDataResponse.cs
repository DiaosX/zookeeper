using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class SetDataResponse : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(SetDataResponse));

		public Stat Stat
		{
			get;
			set;
		}

		public SetDataResponse()
		{
		}

		public SetDataResponse(Stat stat)
		{
			this.Stat = stat;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteRecord(this.Stat, "stat");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
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
					binaryOutputArchive.WriteRecord(this.Stat, "stat");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //SetDataResponse.log.Error(ex);
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
			SetDataResponse setDataResponse = (SetDataResponse)obj;
			if (setDataResponse == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Stat.CompareTo(setDataResponse.Stat);
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetDataResponse setDataResponse = (SetDataResponse)obj;
			if (setDataResponse == null)
			{
				return false;
			}
			if (object.ReferenceEquals(setDataResponse, this))
			{
				return true;
			}
			bool flag = this.Stat.Equals(setDataResponse.Stat);
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
			hashCode = this.Stat.GetHashCode();
			return 37 * num + hashCode;
		}

		public static string Signature()
		{
			return "LSetDataResponse(LStat(lllliiiliil))";
		}
	}
}
