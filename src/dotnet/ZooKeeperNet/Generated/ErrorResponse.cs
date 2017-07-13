using Org.Apache.Jute;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class ErrorResponse : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(ErrorResponse));

		public int Err
		{
			get;
			set;
		}

		public ErrorResponse()
		{
		}

		public ErrorResponse(int err)
		{
			this.Err = err;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteInt(this.Err, "err");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Err = a_.ReadInt("err");
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
					binaryOutputArchive.WriteInt(this.Err, "err");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //ErrorResponse.log.Error(ex);
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
			ErrorResponse errorResponse = (ErrorResponse)obj;
			if (errorResponse == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = (this.Err == errorResponse.Err) ? 0 : ((this.Err < errorResponse.Err) ? -1 : 1);
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ErrorResponse errorResponse = (ErrorResponse)obj;
			if (errorResponse == null)
			{
				return false;
			}
			if (object.ReferenceEquals(errorResponse, this))
			{
				return true;
			}
			bool flag = this.Err == errorResponse.Err;
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
			num2 = this.Err;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LErrorResponse(i)";
		}
	}
}
