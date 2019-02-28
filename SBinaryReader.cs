using System;
using System.IO;
using System.Linq;
using System.Text;

namespace BarsTool {
	class SBinaryReader : BinaryReader {
		private BOM bom = BitConverter.IsLittleEndian ? BOM.LittleEndian : BOM.BigEndian;

		public SBinaryReader(Stream input) : base(input) {
			
		}

		public SBinaryReader(Stream input, Encoding encoding) : base(input, encoding) {
			
		}

		public SBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) {
			
		}

		public ushort ReadUInt16(BOM bom) {
			if (!ReverseRequired(bom))
				return base.ReadUInt16();

			byte[] buf = ReadAndReverse(2);
			return (ushort)(buf[0] | buf[1] << 8);
		}

		public uint ReadUInt32(BOM bom) {
			if (!ReverseRequired(bom))
				return base.ReadUInt32();

			byte[] buf = ReadAndReverse(4);
			return (uint)(buf[0] | buf[1] << 8 | buf[2] << 16 | buf[3] << 24);
		}

		public BOM ReadBOM() {
			byte[] bytes = ReadBytes(2);
			return bytes[0] == 0xFE ? BOM.BigEndian : BOM.LittleEndian;
		}

		private bool ReverseRequired(BOM bom) {
			return this.bom != bom;
		}

		public string ReadStringBytes(int numBytes) {
			return new string(ReadChars(numBytes));
		}

		private byte[] ReadAndReverse(int numBytes) {
			byte[] bytes = ReadBytes(numBytes);
			return bytes.Reverse().ToArray();
		}
	}
}
