using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Data
{
	public class StatPersisted : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(StatPersisted));

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

		public long Pzxid
		{
			get;
			set;
		}

		public StatPersisted()
		{
		}

		public StatPersisted(long czxid, long mzxid, long ctime, long mtime, int version, int cversion, int aversion, long ephemeralOwner, long pzxid)
		{
			this.Czxid = czxid;
			this.Mzxid = mzxid;
			this.Ctime = ctime;
			this.Mtime = mtime;
			this.Version = version;
			this.Cversion = cversion;
			this.Aversion = aversion;
			this.EphemeralOwner = ephemeralOwner;
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
					binaryOutputArchive.WriteLong(this.Pzxid, "pzxid");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //StatPersisted.log.Error(ex);
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
			StatPersisted statPersisted = (StatPersisted)obj;
			if (statPersisted == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = (this.Czxid == statPersisted.Czxid) ? 0 : ((this.Czxid < statPersisted.Czxid) ? -1 : 1);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Mzxid == statPersisted.Mzxid) ? 0 : ((this.Mzxid < statPersisted.Mzxid) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Ctime == statPersisted.Ctime) ? 0 : ((this.Ctime < statPersisted.Ctime) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Mtime == statPersisted.Mtime) ? 0 : ((this.Mtime < statPersisted.Mtime) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Version == statPersisted.Version) ? 0 : ((this.Version < statPersisted.Version) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Cversion == statPersisted.Cversion) ? 0 : ((this.Cversion < statPersisted.Cversion) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Aversion == statPersisted.Aversion) ? 0 : ((this.Aversion < statPersisted.Aversion) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.EphemeralOwner == statPersisted.EphemeralOwner) ? 0 : ((this.EphemeralOwner < statPersisted.EphemeralOwner) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Pzxid == statPersisted.Pzxid) ? 0 : ((this.Pzxid < statPersisted.Pzxid) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			StatPersisted statPersisted = (StatPersisted)obj;
			if (statPersisted == null)
			{
				return false;
			}
			if (object.ReferenceEquals(statPersisted, this))
			{
				return true;
			}
			bool flag = this.Czxid == statPersisted.Czxid;
			if (!flag)
			{
				return flag;
			}
			flag = (this.Mzxid == statPersisted.Mzxid);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Ctime == statPersisted.Ctime);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Mtime == statPersisted.Mtime);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Version == statPersisted.Version);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Cversion == statPersisted.Cversion);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Aversion == statPersisted.Aversion);
			if (!flag)
			{
				return flag;
			}
			flag = (this.EphemeralOwner == statPersisted.EphemeralOwner);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Pzxid == statPersisted.Pzxid);
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
			num2 = (int)this.Pzxid;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LStatPersisted(lllliiill)";
		}
	}
}
