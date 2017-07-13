using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Txn
{
    public class CreateTxn : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(CreateTxn));

		public string Path
		{
			get;
			set;
		}

		public byte[] Data
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<ACL> Acl
		{
			get;
			set;
		}

		public bool Ephemeral
		{
			get;
			set;
		}

		public int ParentCVersion
		{
			get;
			set;
		}

		public CreateTxn()
		{
		}

		public CreateTxn(string path, byte[] data, System.Collections.Generic.IEnumerable<ACL> acl, bool ephemeral, int parentCVersion)
		{
			this.Path = path;
			this.Data = data;
			this.Acl = acl;
			this.Ephemeral = ephemeral;
			this.ParentCVersion = parentCVersion;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.WriteBuffer(this.Data, "data");
			a_.StartVector<ACL>(this.Acl, "acl");
			if (this.Acl != null)
			{
				foreach (ACL current in this.Acl)
				{
					a_.WriteRecord(current, "e1");
				}
			}
			a_.EndVector<ACL>(this.Acl, "acl");
			a_.WriteBool(this.Ephemeral, "ephemeral");
			a_.WriteInt(this.ParentCVersion, "parentCVersion");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
			this.Data = a_.ReadBuffer("data");
			IIndex index = a_.StartVector("acl");
			if (index != null)
			{
				System.Collections.Generic.List<ACL> list = new System.Collections.Generic.List<ACL>();
				while (!index.Done())
				{
					ACL aCL = new ACL();
					a_.ReadRecord(aCL, "e1");
					list.Add(aCL);
					index.Incr();
				}
				this.Acl = list;
			}
			a_.EndVector("acl");
			this.Ephemeral = a_.ReadBool("ephemeral");
			this.ParentCVersion = a_.ReadInt("parentCVersion");
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
					binaryOutputArchive.WriteString(this.Path, "path");
					binaryOutputArchive.WriteBuffer(this.Data, "data");
					binaryOutputArchive.StartVector<ACL>(this.Acl, "acl");
					if (this.Acl != null)
					{
						foreach (ACL current in this.Acl)
						{
							binaryOutputArchive.WriteRecord(current, "e1");
						}
					}
					binaryOutputArchive.EndVector<ACL>(this.Acl, "acl");
					binaryOutputArchive.WriteBool(this.Ephemeral, "ephemeral");
					binaryOutputArchive.WriteInt(this.ParentCVersion, "parentCVersion");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                Logger.Write(ex.Message, MsgType.Error);
                //CreateTxn.log.Error(ex);
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
			throw new System.InvalidOperationException("comparing CreateTxn is unimplemented");
		}

		public override bool Equals(object obj)
		{
			CreateTxn createTxn = (CreateTxn)obj;
			if (createTxn == null)
			{
				return false;
			}
			if (object.ReferenceEquals(createTxn, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(createTxn.Path);
			if (!flag)
			{
				return flag;
			}
			flag = this.Data.Equals(createTxn.Data);
			if (!flag)
			{
				return flag;
			}
			flag = this.Acl.Equals(createTxn.Acl);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Ephemeral == createTxn.Ephemeral);
			if (!flag)
			{
				return flag;
			}
			flag = (this.ParentCVersion == createTxn.ParentCVersion);
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
			num2 = this.Path.GetHashCode();
			num = 37 * num + num2;
			num2 = this.Data.GetHashCode();
			num = 37 * num + num2;
			num2 = this.Acl.GetHashCode();
			num = 37 * num + num2;
			num2 = (this.Ephemeral ? 0 : 1);
			num = 37 * num + num2;
			num2 = this.ParentCVersion;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LCreateTxn(sB[LACL(iLId(ss))]zi)";
		}
	}
}
