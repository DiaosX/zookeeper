using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Data
{
	public class StatPersistedV1 : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(StatPersistedV1));

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

		public StatPersistedV1()
		{
		}

		public StatPersistedV1(long czxid, long mzxid, long ctime, long mtime, int version, int cversion, int aversion, long ephemeralOwner)
		{
			this.Czxid = czxid;
			this.Mzxid = mzxid;
			this.Ctime = ctime;
			this.Mtime = mtime;
			this.Version = version;
			this.Cversion = cversion;
			this.Aversion = aversion;
			this.EphemeralOwner = ephemeralOwner;
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
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //StatPersistedV1.log.Error(ex);
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
			StatPersistedV1 statPersistedV = (StatPersistedV1)obj;
			if (statPersistedV == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = (this.Czxid == statPersistedV.Czxid) ? 0 : ((this.Czxid < statPersistedV.Czxid) ? -1 : 1);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Mzxid == statPersistedV.Mzxid) ? 0 : ((this.Mzxid < statPersistedV.Mzxid) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Ctime == statPersistedV.Ctime) ? 0 : ((this.Ctime < statPersistedV.Ctime) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Mtime == statPersistedV.Mtime) ? 0 : ((this.Mtime < statPersistedV.Mtime) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Version == statPersistedV.Version) ? 0 : ((this.Version < statPersistedV.Version) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Cversion == statPersistedV.Cversion) ? 0 : ((this.Cversion < statPersistedV.Cversion) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.Aversion == statPersistedV.Aversion) ? 0 : ((this.Aversion < statPersistedV.Aversion) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			num = ((this.EphemeralOwner == statPersistedV.EphemeralOwner) ? 0 : ((this.EphemeralOwner < statPersistedV.EphemeralOwner) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			StatPersistedV1 statPersistedV = (StatPersistedV1)obj;
			if (statPersistedV == null)
			{
				return false;
			}
			if (object.ReferenceEquals(statPersistedV, this))
			{
				return true;
			}
			bool flag = this.Czxid == statPersistedV.Czxid;
			if (!flag)
			{
				return flag;
			}
			flag = (this.Mzxid == statPersistedV.Mzxid);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Ctime == statPersistedV.Ctime);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Mtime == statPersistedV.Mtime);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Version == statPersistedV.Version);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Cversion == statPersistedV.Cversion);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Aversion == statPersistedV.Aversion);
			if (!flag)
			{
				return flag;
			}
			flag = (this.EphemeralOwner == statPersistedV.EphemeralOwner);
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
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LStatPersistedV1(lllliiil)";
		}
	}
}
