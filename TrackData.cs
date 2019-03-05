using System;
using System.IO;

namespace BarsTool {
	class TrackData {
		public BOM infoBom;
		public BOM amtaBom;

		public uint amtaOffset;
		public uint trackOffset;

		public bool validTrack;

		public string trackName;
		public string trackExtension;
		public string trackFormat;

		public byte[] trackData;

		//INFO
		public int dataWritingPos;
		public byte soundEncoding;
		public uint sampleRate;
		public byte channelAmount;
		public byte trackLooping;
		public uint loopStart;
		public uint loopEnd;
		public uint origLoopStart;
		public uint origLoopEnd;

		public float length;

		public TrackData(uint amtaOffset, BOM amtaBom, uint trackOffset, string trackName, int dataWritingPos) {
			this.amtaOffset = amtaOffset;
			this.amtaBom = amtaBom;
			this.trackOffset = trackOffset;
			this.trackName = trackName;
			this.dataWritingPos = dataWritingPos;
			validTrack = false;
		}

		public TrackData(TrackData tbc) { // to be cloned
			amtaOffset = tbc.amtaOffset;
			amtaBom = tbc.amtaBom;
			trackOffset = tbc.trackOffset;

			validTrack = tbc.validTrack;

			trackName = tbc.trackName;

			dataWritingPos = tbc.dataWritingPos;
		}

		public void ReadData(SBinaryReader br) {
			// Useful FWAV and FSTP file format info:
			// http://mk8.tockdom.com/wiki/BFWAV_(File_Format)
			// http://mk8.tockdom.com/wiki/BFSTM_(File_Format)
			Stream bs = br.BaseStream;

			bs.Seek(trackOffset, SeekOrigin.Begin);
			string fwav_header = br.ReadStringBytes(4); // read header

			for (int i = 0; i < BarsViewerForm.FWAV_HEADERS.Length; ++i) {
				if (fwav_header == BarsViewerForm.FWAV_HEADERS[i]) {
					trackFormat = BarsViewerForm.FWAV_HEADERS[i];
					trackExtension = BarsViewerForm.FWAV_EXT[i];
					validTrack = true;
					break;
				}
			}
			if (!validTrack) {
				ConsoleWriteLine($"Track {trackName} has an invalid FWAV/FSTP header (found: {fwav_header}) or is invalid");
				return;
			}
			validTrack = true;

			infoBom = br.ReadBOM();
			bs.Seek(6, SeekOrigin.Current); // skip 6 bytes (header size 2, version number 4)
			int trackLength = (int)br.ReadUInt32(infoBom); // read track length
			bs.Seek(48, SeekOrigin.Current); // skip to INFO section
			string infoMagic = br.ReadStringBytes(4);
			if (infoMagic != BarsViewerForm.INFO_HEADER) {
				ConsoleWriteLine($"Track {trackName} has an invalid INFO header");
				validTrack = false;
				return;
			}

			if (trackFormat == "FWAV") { // http://mk8.tockdom.com/wiki/BFWAV_(File_Format)
				bs.Seek(4, SeekOrigin.Current);
				soundEncoding = br.ReadByte();
				trackLooping = br.ReadByte();
				bs.Seek(2, SeekOrigin.Current); // skip padding
				sampleRate = br.ReadUInt32(infoBom);
				loopStart = br.ReadUInt32(infoBom);
				loopEnd = br.ReadUInt32(infoBom);
				origLoopStart = br.ReadUInt32(infoBom);
				origLoopEnd = loopEnd;
				channelAmount = (byte)br.ReadUInt32(infoBom);
			}
			else if (trackFormat == "FSTP") { // http://mk8.tockdom.com/wiki/BFSTM_(File_Format) BFSTM reference link, but INFO structured like in BFSTP
				bs.Seek(28, SeekOrigin.Current);
				soundEncoding = br.ReadByte();
				trackLooping = br.ReadByte();
				channelAmount = br.ReadByte();
				bs.Seek(1, SeekOrigin.Current); // skip Number of regions
				sampleRate = br.ReadUInt32(infoBom);
				loopStart = br.ReadUInt32(infoBom);
				loopEnd = br.ReadUInt32(infoBom);
				bs.Seek(52, SeekOrigin.Current);
				origLoopStart = br.ReadUInt32(infoBom);
				origLoopEnd = br.ReadUInt32(infoBom);
			}
			else {
				ConsoleWriteLine("Shouldn't happen, but file format not supported");
				validTrack = false;
				return;
			}
			length = loopEnd / (float)sampleRate;

			// DEBUG - both of those could happen at once O.o
			//if (loopEnd < origLoopEnd) {
			//	ConsoleWriteLine($"loopEnd < origLoopEnd in {trackName}");
			//}
			//if (loopEnd > origLoopEnd) {
			//	ConsoleWriteLine($"loopEnd > origLoopEnd in {trackName}");
			//}

			bs.Seek(trackOffset, SeekOrigin.Begin); // seek back to the beggining of the track data
			trackData = br.ReadBytes(trackLength);

			//return validTrack;
		}

		public void WriteOffsets(SBinaryWriter bw, BOM bom) {
			bw.Write(amtaOffset, bom);
			bw.Write(trackOffset, bom);
		}

		public void WriteData(SBinaryReader br, SBinaryWriter bw) {
			Stream bs = br.BaseStream;
			
			bw.Write(br.ReadBytes(4)); //copy padding (why the hell not)
			
			bw.Write(loopEnd, amtaBom);


			byte trackHeader = 0;
			if (trackExtension == "FWAV")
				trackHeader = 0;
			if (trackExtension == "FSTP")
				trackHeader = 1;
			bw.Write(trackHeader);

			bw.Write(channelAmount);

			bw.Write(trackHeader);

			byte formatLoop = 0;
			if (trackExtension == "FWAV")
				formatLoop = 2;
			else if (trackExtension == "FSTP")
				formatLoop = 3;
			if (Convert.ToBoolean(trackLooping))
				formatLoop += 4;
			bw.Write(formatLoop);

			
			bs.Seek(8, SeekOrigin.Current); // seek to catch up with prev. written data
			bw.Write(br.ReadBytes(4)); // copy unknown

			bw.Write(sampleRate, amtaBom);

			bw.Write(origLoopStart, amtaBom);

			bw.Write(origLoopEnd, amtaBom);

			bs.Seek(12, SeekOrigin.Current); // seek to catch up with prev. written data
			bw.Write(br.ReadBytes(4)); // copy unknown

			uint formatMark = 0;
			if (trackExtension == "FWAV")
				formatMark = 0;
			else if (trackExtension == "FSTP")
				formatMark = 2;
			bw.Write(formatMark);

			bs.Seek(4, SeekOrigin.Current); // seek to catch up with prev. written data
			bw.Write(br.ReadBytes(64)); // copy the rest (unknown)
		}

		public bool Extract(string path) {
			using (var fs = new FileStream(path + Path.DirectorySeparatorChar + trackName + trackExtension, FileMode.OpenOrCreate)) {
				fs.SetLength(0);
				fs.Write(trackData, 0, trackData.Length);
			}
			return true;
		}

		public override string ToString() {
			string info = "";
			string nl = Environment.NewLine;
			string encoding;
			switch (soundEncoding) {
				case 0:		encoding = "PCM8";		break;
				case 1:		encoding = "PCM16";		break;
				case 2:		encoding = "DSP ADPCM";	break;
				case 3:		encoding = "IMA ADPCM";	break;
				default:	encoding = "unknown";	break;
			}
			bool isLooping = Convert.ToBoolean(trackLooping);
			info += $"Name: {trackName}{nl}";
			info += $"Extension: {trackExtension}{nl}";
			info += $"Encoding: {encoding}{nl}";
			info += $"Channels: {channelAmount}{nl}";
			info += $"Sample rate: {sampleRate}{nl}";
			info += $"Length: {length}s{nl}";
			info += $"Looping: {isLooping}{nl}";
			//if(isLooping) {
				info += $"loopStart: {loopStart} ({loopStart/(float)sampleRate}s){nl}";
				info += $"loopEnd: {loopEnd} ({loopEnd / (float)sampleRate}s){nl}";
				info += $"origLoopStart: {origLoopStart}  ({origLoopStart / (float)sampleRate}s){nl}";
				info += $"origLoopEnd: {origLoopEnd} ({origLoopEnd / (float)sampleRate}s){nl}";
			//}
			return info;
		}

		private void ConsoleWriteLine(string text) {
			BarsViewerForm.ConsoleWriteLine(text);
		}
	}
}
