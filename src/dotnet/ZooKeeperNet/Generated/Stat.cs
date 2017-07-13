using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Data
{
	public class Stat : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(Stat));

		public long Czxid
		{
			get;
			set;
		}

		public long Mzxid
		{
			get;
			set;
		}

		public long Ctime
		{
			get;
			set;
		}

		public long Mtime
		{
			get;
			set;
		}

		public int Version
		{
			get;
			set;
		}

		public int Cversion
		{
			get;
			set;
		}

		public int Aversion
		{
			get;
			set;
		}

		public long EphemeralOwner
		{
			get;
			set;
		}

		public int DataLength
		{
			get;
			set;
		}

		public int NumChildren
		{
			get;
			set;
		}

		public long Pzxid
		{
			get;
			set;
		}

		public Stat()
		{
		}

		public Stat(long czxid, long mzxid, long ctime, long mtime, int version, int cversion, int aversion, long ephemeralOwner, int dataLength, int numChildren, long pzxid)
		{
			this.Czxid = czxid;
			this.Mzxid = mzxid;
			this.Ctime = ctime;
			this.Mtime = mtime;
			this.Version = version;
			this.Cversion = cversion;
			this.Aversion = aversion;
			this.EphemeralOwner = ephemeralOwner;
			this.DataLength = dataLength;
			this.NumChildren = numChildren;
			this.Pzxid = pzxid;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteLong(this.Czxid, "czxid");
			a_.WriteLong(this.Mzxid, "mzxid");
			a_.WriteLong(this.Ctime, "ctime");
			a_.WriteLong(this.Mtime, "mtime");
			a_.WriteInt(this.Version, "version");
			a_.WriteInt(this.Cversion, "cversion");
			a_.WriteInt(this.Aversion, "aversion");
			a_.WriteLong(this.EphemeralOwner, "ephemeralOwner");
			a_.WriteInt(this.DataLength, "dataLength");
			a_.WriteInt(this.NumChildren, "numChildren");
			a_.WriteLong(this.Pzxid, "pzxid");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Czxid = a_.ReadLong("czxid");
			this.Mzxid = a_.ReadLong("mzxid");
			this.Ctime = a_.ReadLong("ctime");
			this.Mtime = a_.ReadLong("mtime");
			this.Version = a_.ReadInt("version");
			this.Cversion = a_.ReadInt("cversion");
			this.Aversion = a_.ReadInt("aversion");
			this.EphemeralOwner = a_.ReadLong("ephemeralOwner");
			this.DataLength = a_.ReadInt("dataLength");
			this.NumChildren = a_.ReadInt("numChildren");
			this.Pzxid = a_.ReadLong("pzxid");
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
					binaryOutputArchive.WriteLong(this.Czxid, "czxid");
					binaryOutputArchive.WriteLong(this.Mzxid, "mzxid");
					binaryOutputArchive.WriteLong(this.Ctime, "ctime");
					binaryOutputArchive.WriteLong(this.Mtime, "mtime");
					binaryOutputArchive.WriteInt(this.Version, "version");
					binaryOutputArchive.WriteInt(this.Cversion, "cversion");
					binaryOutputArchive.WriteInt(this.Aversion, "aversion");
					binaryOutputArchive.WriteLong(this.EphemeralOwner, "ephemeralOwner");
					binaryOutputArchive.WriteInt(this.DataLength, "dataLength");
					binaryOutputArchive.WriteInt(this.NumChildren, "numChildren");
					binaryOutputArchive.WriteLong(this.Pzxid, "pzxid");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //Stat.log.Error(ex);
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
			Stat stat = (Stat)obj;
			if (stat == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = (this.Czxid == stat.Czxid) ? 0 : ((this.Czxid < stat.Czxid) ? -1 : 1);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Mzxid == stat.Mzxid) ? 0 : ((this.Mzxid < stat.Mzxid) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Ctime == stat.Ctime) ? 0 : ((this.Ctime < stat.Ctime) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Mtime == stat.Mtime) ? 0 : ((this.Mtime < stat.Mtime) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Version == stat.Version) ? 0 : ((this.Version < stat.Version) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Cversion == stat.Cversion) ? 0 : ((this.Cversion < stat.Cversion) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Aversion == stat.Aversion) ? 0 : ((this.Aversion < stat.Aversion) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.EphemeralOwner == stat.EphemeralOwner) ? 0 : ((this.EphemeralOwner < stat.EphemeralOwner) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.DataLength == stat.DataLength) ? 0 : ((this.DataLength < stat.DataLength) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.NumChildren == stat.NumChildren) ? 0 : ((this.NumChildren < stat.NumChildren) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Pzxid == stat.Pzxid) ? 0 : ((this.Pzxid < stat.Pzxid) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Stat stat = (Stat)obj;
			if (stat == null)
			{
				return false;
			}
			if (object.ReferenceEquals(stat, this))
			{
				return true;
			}
			bool flag = this.Czxid == stat.Czxid;
			if (!flag)
			{
				return flag;
			}
			flag = (this.Mzxid == stat.Mzxid);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Ctime == stat.Ctime);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Mtime == stat.Mtime);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Version == stat.Version);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Cversion == stat.Cversion);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Aversion == stat.Aversion);
			if (!flag)
			{
				return flag;
			}
			flag = (this.EphemeralOwner == stat.EphemeralOwner);
			if (!flag)
			{
				return flag;
			}
			flag = (this.DataLength == stat.DataLength);
			if (!flag)
			{
				return flag;
			}
			flag = (this.NumChildren == stat.NumChildren);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Pzxid == stat.Pzxid);
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
			num2 = (int)this.Czxid;
			num = 37 * num + num2;
			num2 = (int)this.Mzxid;
			num = 37 * num + num2;
			num2 = (int)this.Ctime;
			num = 37 * num + num2;
			num2 = (int)this.Mtime;
			num = 37 * num + num2;
			num2 = this.Version;
			num = 37 * num + num2;
			num2 = this.Cversion;
			num = 37 * num + num2;
			num2 = this.Aversion;
			num = 37 * num + num2;
			num2 = (int)this.EphemeralOwner;
			num = 37 * num + num2;
			num2 = this.DataLength;
			num = 37 * num + num2;
			num2 = this.NumChildren;
			num = 37 * num + num2;
			num2 = (int)this.Pzxid;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LStat(lllliiiliil)";
		}
	}
}
