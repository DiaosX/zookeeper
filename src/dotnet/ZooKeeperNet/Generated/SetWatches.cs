using Org.Apache.Jute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
	public class SetWatches : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(SetWatches));

		public long RelativeZxid
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<string> DataWatches
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<string> ExistWatches
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<string> ChildWatches
		{
			get;
			set;
		}

		public SetWatches()
		{
		}

		public SetWatches(long relativeZxid, System.Collections.Generic.IEnumerable<string> dataWatches, System.Collections.Generic.IEnumerable<string> existWatches, System.Collections.Generic.IEnumerable<string> childWatches)
		{
			this.RelativeZxid = relativeZxid;
			this.DataWatches = dataWatches;
			this.ExistWatches = existWatches;
			this.ChildWatches = childWatches;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteLong(this.RelativeZxid, "relativeZxid");
			a_.StartVector<string>(this.DataWatches, "dataWatches");
			if (this.DataWatches != null)
			{
				foreach (string current in this.DataWatches)
				{
					a_.WriteString(current, current);
				}
			}
			a_.EndVector<string>(this.DataWatches, "dataWatches");
			a_.StartVector<string>(this.ExistWatches, "existWatches");
			if (this.ExistWatches != null)
			{
				foreach (string current2 in this.ExistWatches)
				{
					a_.WriteString(current2, current2);
				}
			}
			a_.EndVector<string>(this.ExistWatches, "existWatches");
			a_.StartVector<string>(this.ChildWatches, "childWatches");
			if (this.ChildWatches != null)
			{
				foreach (string current3 in this.ChildWatches)
				{
					a_.WriteString(current3, current3);
				}
			}
			a_.EndVector<string>(this.ChildWatches, "childWatches");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.RelativeZxid = a_.ReadLong("relativeZxid");
			IIndex index = a_.StartVector("dataWatches");
			if (index != null)
			{
				System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
				while (!index.Done())
				{
					string item = a_.ReadString("e1");
					list.Add(item);
					index.Incr();
				}
				this.DataWatches = list;
			}
			a_.EndVector("dataWatches");
			IIndex index2 = a_.StartVector("existWatches");
			if (index2 != null)
			{
				System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
				while (!index2.Done())
				{
					string item2 = a_.ReadString("e1");
					list2.Add(item2);
					index2.Incr();
				}
				this.ExistWatches = list2;
			}
			a_.EndVector("existWatches");
			IIndex index3 = a_.StartVector("childWatches");
			if (index3 != null)
			{
				System.Collections.Generic.List<string> list3 = new System.Collections.Generic.List<string>();
				while (!index3.Done())
				{
					string item3 = a_.ReadString("e1");
					list3.Add(item3);
					index3.Incr();
				}
				this.ChildWatches = list3;
			}
			a_.EndVector("childWatches");
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
					binaryOutputArchive.WriteLong(this.RelativeZxid, "relativeZxid");
					binaryOutputArchive.StartVector<string>(this.DataWatches, "dataWatches");
					if (this.DataWatches != null)
					{
						foreach (string current in this.DataWatches)
						{
							binaryOutputArchive.WriteString(current, current);
						}
					}
					binaryOutputArchive.EndVector<string>(this.DataWatches, "dataWatches");
					binaryOutputArchive.StartVector<string>(this.ExistWatches, "existWatches");
					if (this.ExistWatches != null)
					{
						foreach (string current2 in this.ExistWatches)
						{
							binaryOutputArchive.WriteString(current2, current2);
						}
					}
					binaryOutputArchive.EndVector<string>(this.ExistWatches, "existWatches");
					binaryOutputArchive.StartVector<string>(this.ChildWatches, "childWatches");
					if (this.ChildWatches != null)
					{
						foreach (string current3 in this.ChildWatches)
						{
							binaryOutputArchive.WriteString(current3, current3);
						}
					}
					binaryOutputArchive.EndVector<string>(this.ChildWatches, "childWatches");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //SetWatches.log.Error(ex);
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
			throw new System.InvalidOperationException("comparing SetWatches is unimplemented");
		}

		public override bool Equals(object obj)
		{
			SetWatches setWatches = (SetWatches)obj;
			if (setWatches == null)
			{
				return false;
			}
			if (object.ReferenceEquals(setWatches, this))
			{
				return true;
			}
			bool flag = this.RelativeZxid == setWatches.RelativeZxid;
			if (!flag)
			{
				return flag;
			}
			flag = this.DataWatches.Equals(setWatches.DataWatches);
			if (!flag)
			{
				return flag;
			}
			flag = this.ExistWatches.Equals(setWatches.ExistWatches);
			if (!flag)
			{
				return flag;
			}
			flag = this.ChildWatches.Equals(setWatches.ChildWatches);
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
			num2 = (int)this.RelativeZxid;
			num = 37 * num + num2;
			num2 = this.DataWatches.GetHashCode();
			num = 37 * num + num2;
			num2 = this.ExistWatches.GetHashCode();
			num = 37 * num + num2;
			num2 = this.ChildWatches.GetHashCode();
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LSetWatches(l[s][s][s])";
		}
	}
}
