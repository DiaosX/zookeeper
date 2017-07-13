using log4net;
using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Data
{
	public class ZKId : IRecord, System.IComparable
	{
		private static ILog log = LogManager.GetLogger(typeof(ZKId));

		public string Scheme
		{
			get;
			set;
		}

		public string Id
		{
			get;
			set;
		}

		public ZKId()
		{
		}

		public ZKId(string scheme, string id)
		{
			this.Scheme = scheme;
			this.Id = id;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Scheme, "scheme");
			a_.WriteString(this.Id, "id");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Scheme = a_.ReadString("scheme");
			this.Id = a_.ReadString("id");
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
					binaryOutputArchive.WriteString(this.Scheme, "scheme");
					binaryOutputArchive.WriteString(this.Id, "id");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
				ZKId.log.Error(ex);
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
			ZKId zKId = (ZKId)obj;
			if (zKId == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Scheme.CompareTo(zKId.Scheme);
			if (num != 0)
			{
				return num;
			}
			num = this.Id.CompareTo(zKId.Id);
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ZKId zKId = (ZKId)obj;
			if (zKId == null)
			{
				return false;
			}
			if (object.ReferenceEquals(zKId, this))
			{
				return true;
			}
			bool flag = this.Scheme.Equals(zKId.Scheme);
			if (!flag)
			{
				return flag;
			}
			flag = this.Id.Equals(zKId.Id);
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
			hashCode = this.Scheme.GetHashCode();
			num = 37 * num + hashCode;
			hashCode = this.Id.GetHashCode();
			return 37 * num + hashCode;
		}

		public static string Signature()
		{
			return "LId(ss)";
		}
	}
}
