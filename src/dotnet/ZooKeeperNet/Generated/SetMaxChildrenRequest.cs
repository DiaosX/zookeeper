using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;
using ZooKeeperNet.Log;

namespace Org.Apache.Zookeeper.Proto
{
	public class SetMaxChildrenRequest : IRecord, System.IComparable
	{
		//private static ILog log = LogManager.GetLogger(typeof(SetMaxChildrenRequest));

		public string Path
		{
			get;
			set;
		}

		public int Max
		{
			get;
			set;
		}

		public SetMaxChildrenRequest()
		{
		}

		public SetMaxChildrenRequest(string path, int max)
		{
			this.Path = path;
			this.Max = max;
		}

		public void Serialize(IOutputArchive a_, string tag)
		{
			a_.StartRecord(this, tag);
			a_.WriteString(this.Path, "path");
			a_.WriteInt(this.Max, "max");
			a_.EndRecord(this, tag);
		}

		public void Deserialize(IInputArchive a_, string tag)
		{
			a_.StartRecord(tag);
			this.Path = a_.ReadString("path");
			this.Max = a_.ReadInt("max");
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
					binaryOutputArchive.WriteInt(this.Max, "max");
					binaryOutputArchive.EndRecord(this, string.Empty);
					memoryStream.Position = 0L;
					return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (System.Exception ex)
			{
                //SetMaxChildrenRequest.log.Error(ex);
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
			SetMaxChildrenRequest setMaxChildrenRequest = (SetMaxChildrenRequest)obj;
			if (setMaxChildrenRequest == null)
			{
				throw new System.InvalidOperationException("Comparing different types of records.");
			}
			int num = this.Path.CompareTo(setMaxChildrenRequest.Path);
			if (num != 0)
			{
				return num;
			}
			num = ((this.Max == setMaxChildrenRequest.Max) ? 0 : ((this.Max < setMaxChildrenRequest.Max) ? -1 : 1));
			if (num != 0)
			{
				return num;
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetMaxChildrenRequest setMaxChildrenRequest = (SetMaxChildrenRequest)obj;
			if (setMaxChildrenRequest == null)
			{
				return false;
			}
			if (object.ReferenceEquals(setMaxChildrenRequest, this))
			{
				return true;
			}
			bool flag = this.Path.Equals(setMaxChildrenRequest.Path);
			if (!flag)
			{
				return flag;
			}
			flag = (this.Max == setMaxChildrenRequest.Max);
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
			num2 = this.Max;
			return 37 * num + num2;
		}

		public static string Signature()
		{
			return "LSetMaxChildrenRequest(si)";
		}
	}
}
