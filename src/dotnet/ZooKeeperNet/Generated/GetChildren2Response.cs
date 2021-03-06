using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetChildren2Response : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(GetChildren2Response));

		public System.Collections.Generic.IEnumerable<string> Children
		{
			get;
			set;
		}

		public Stat Stat
		{
			get;
			set;
		}

		public GetChildren2Response()
		{
		}

		public GetChildren2Response(System.Collections.Generic.IEnumerable<string> children, Stat stat)
		{
			this.Children = children;
			this.Stat = stat;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.StartVector<string>(this.Children, "children");
			if (this.Children != null)
			{
				foreach (string current in this.Children)
				{
					a_.WriteString(current, current);
				}
			}
			a_.EndVector<string>(this.Children, "children");
			a_.WriteRecord(this.Stat, "stat");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			IIndex index = a_.StartVector("children");
			if (index != null)
			{
				System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
				while (!index.Done())
				{
					string item = a_.ReadString("e1");
					list.Add(item);
					index.Incr();
				}
				this.Children = list;
			}
			a_.EndVector("children");
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
					binaryOutputArchive.StartVector<string>(this.Children, "children");
					if (this.Children != null)
					{
						foreach (string current in this.Children)
						{
							binaryOutputArchive.WriteString(current, current);
						}
					}
					binaryOutputArchive.EndVector<string>(this.Children, "children");
					binaryOutputArchive.WriteRecord(this.Stat, "stat");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //GetChildren2Response.log.Error(ex);
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
			throw new System.InvalidOperationException("comparing GetChildren2Response is unimplemented");
		}

		public override bool Equals(object obj)
		{
			GetChildren2Response getChildren2Response = (GetChildren2Response)obj;
			if (getChildren2Response == null)
			{
				return false;
			}
			if (object.ReferenceEquals(getChildren2Response, this))
			{
				return true;
			}
			bool flag = this.Children.Equals(getChildren2Response.Children);
			if (!flag)
			{
				return flag;
			}
			flag = this.Stat.Equals(getChildren2Response.Stat);
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
			hashCode = this.Children.GetHashCode();
			num = 37 * num + hashCode;
			hashCode = this.Stat.GetHashCode();
			return 37 * num + hashCode;
		}

		public static string Signature()
		{
			return "LGetChildren2Response([s]LStat(lllliiiliil))";
		}
	}
}
