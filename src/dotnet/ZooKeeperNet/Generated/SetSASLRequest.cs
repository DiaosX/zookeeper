using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
	public class SetSASLRequest : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(SetSASLRequest));

		public byte[] Token
		{
			get;
			set;
		}

		public SetSASLRequest()
		{
		}

		public SetSASLRequest(byte[] token)
		{
			this.Token = token;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteBuffer(this.Token, "token");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Token = a_.ReadBuffer("token");
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
					binaryOutputArchive.WriteBuffer(this.Token, "token");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //SetSASLRequest.log.Error(ex);
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
			SetSASLRequest setSASLRequest = (SetSASLRequest)obj;
			if (setSASLRequest == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Token.CompareTo(setSASLRequest.Token);
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetSASLRequest setSASLRequest = (SetSASLRequest)obj;
			if (setSASLRequest == null)
			{
				return false;
			}
			if (object.ReferenceEquals(setSASLRequest, this))
			{
				return true;
			}
			bool flag = this.Token.Equals(setSASLRequest.Token);
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
			hashCode = this.Token.GetHashCode();
			return 37 * num + hashCode;
		}

		public static string Signature()
		{
			return "LSetSASLRequest(B)";
		}
	}
}
