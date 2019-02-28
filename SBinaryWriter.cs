using System;
using System.IO;
using System.Linq;
using System.Text;

namespace BarsTool {
	class SBinaryWriter : BinaryWriter {
		private BOM bom = BitConverter.IsLittleEndian ? BOM.LittleEndian : BOM.BigEndian;

		public SBinaryWriter(Stream output) : base(output) {
		}

		public SBinaryWriter(Stream output, Encoding encoding) : base(output, encoding) {
		}

		public SBinaryWriter(Stream output, Encoding encoding, bool leaveOpen) : base(output, encoding, leaveOpen) {
		}

		public void Write(ushort value, BOM bom) {
			if(!ReverseRequired(bom)) {
				base.Write(value);
				return;
			}
				
			byte[] bytes = ReverseBytes(BitConverter.GetBytes(value));
			Write(bytes);
		}

		public void Write(uint value, BOM bom) {
			if (!ReverseRequired(bom)) {
				base.Write(value);
				return;
			}

			byte[] bytes = ReverseBytes(BitConverter.GetBytes(value));
			Write(bytes);
		}

		public void Write(BOM bom) {
			if(bom == BOM.BigEndian) {
				Write((byte)0xFE);
				Write((byte)0xFF);
			}
			if (bom == BOM.LittleEndian) {
				Write((byte)0xFF);
				Write((byte)0xFE);
			}
		}

		private bool ReverseRequired(BOM bom) {
			return this.bom != bom;
		}

		private byte[] ReverseBytes(byte[] bytes) {
			return bytes.Reverse().ToArray();
		}
	}
}
