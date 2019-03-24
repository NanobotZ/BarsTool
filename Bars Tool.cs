using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarsTool {
	public partial class BarsViewerForm : Form {
		public static TextBox consoleBox;
		public BOM barsBom;
		public string windowTitle;

		public static readonly string AMTA_HEADER = "AMTA";
		public static readonly string BARS_HEADER = "BARS";
		public static readonly string DATA_HEADER = "DATA";
		public static readonly string MARK_HEADER = "MARK";
		public static readonly string EXT_HEADER = "EXT_";
		public static readonly string STRG_HEADER = "STRG";
		public static readonly string[] FWAV_HEADERS = { "FWAV", "FSTP" };
		public static readonly string[] FWAV_EXT = { ".bfwav", ".bfstp" };
		public static readonly string INFO_HEADER = "INFO";

		public string filePathFull;
		public string filePath;
		public string fileName;
		public long offsetsPos;

		List<TrackData> trackList;

		public BarsViewerForm(string path = null) {
			InitializeComponent();
			windowTitle = this.Text;
			filePathFull = path;
#if DEBUG
			previewB.Visible = true;
#endif
		}

		private void Form1_Load(object sender, EventArgs e) {
			consoleBox = consoleTB;
			trackList = new List<TrackData>();

			if (filePathFull != "") {
				filePathTB.Text = filePathFull;
				FileOpenB_Click(null, null); // smelly workaround
			}
		}

		private void FileChooseB_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "BARS Files|*.bars";
			openFileDialog.Title = "Selact a BARS File";
			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				if (openFileDialog.FileName.EndsWith(".bars")) {
					string fn = openFileDialog.FileName;
					filePathFull = fn;
					filePathTB.Text = fn;
				}
			}
		}

		private void FileOpenB_Click(object sender, EventArgs e) {
			// string filePathFull = filePathTB.Text;
			if (filePathFull == "" || !filePathFull.EndsWith(".bars")) {
				ConsoleWriteLine("No file choosen or incorrect file.");
				return;
			}

			int charIndex = filePathFull.LastIndexOf(Path.DirectorySeparatorChar);
			filePath = filePathFull.Substring(0, charIndex);
			fileName = filePathFull.Substring(charIndex + 1, filePathFull.Length - charIndex - 6); // -6 beacuse: -1 cause of i + 1 and -5 cause of '.bars' extension

			trackList.Clear();
			fileLB.Items.Clear();
			selectAllB.Enabled = true;
			this.Text = $"{windowTitle} - {filePathFull}";

			ConsoleWriteLine($"Opening {fileName}.bars");
			using (SBinaryReader br = new SBinaryReader(File.OpenRead(filePathFull))) {
				Stream bs = br.BaseStream;

				string bars_header = br.ReadStringBytes(BARS_HEADER.Length);
				if (bars_header != BARS_HEADER)
					ConsoleWriteLine("Not a valid BARS file.");

				bs.Seek(4, SeekOrigin.Current); // skip bars_file_length uint
				barsBom = br.ReadBOM();
				bs.Seek(2, SeekOrigin.Current); // skip unknown 2
				uint bars_count = br.ReadUInt32(barsBom);

				bs.Seek(bars_count * 4, SeekOrigin.Current); // seek for the offsets
				offsetsPos = bs.Position;

				uint[] amta_offsets = new uint[bars_count];
				uint[] track_offsets = new uint[bars_count];
				string[] track_names = new string[bars_count];

				for (int i = 0; i < bars_count; ++i) { // read the offsets
					amta_offsets[i] = br.ReadUInt32(barsBom);
					track_offsets[i] = br.ReadUInt32(barsBom);
				}

				for (uint i = 0; i < bars_count; ++i) {
					bs.Seek(amta_offsets[i], SeekOrigin.Begin); // seek to the amta offset

					string amta_header = br.ReadStringBytes(AMTA_HEADER.Length);
					if (amta_header != AMTA_HEADER)
						ConsoleWriteLine($"Track {i} has an invalid AMTA header");

					BOM amta_bom = br.ReadBOM();
					// skipping data (name, bytes, [type]): 
					// unknown 2; amta_length 4 uint; data_offset 4 uint;  mark_offset 4 uint; ext_offset 4 uint; strg_offset 4 uint
					bs.Seek(22, SeekOrigin.Current);

					string data_header = br.ReadStringBytes(DATA_HEADER.Length);
					if (data_header != DATA_HEADER)
						ConsoleWriteLine($"Track {i} has an invalid DATA header");
					uint data_length = br.ReadUInt32(amta_bom); // reading the length of the data
					int data_writing_pos = (int)bs.Position;
					bs.Seek(data_length, SeekOrigin.Current); // skipping it - no use? (at least it wasn't used in the python script)

					string mark_header = br.ReadStringBytes(MARK_HEADER.Length);
					if (mark_header != MARK_HEADER)
						ConsoleWriteLine($"Track {i} has an invalid MARK header");
					uint mark_length = br.ReadUInt32(amta_bom); // reading the length of the mark
					bs.Seek(mark_length, SeekOrigin.Current); // skipping it - no use?

					string ext_header = br.ReadStringBytes(EXT_HEADER.Length);
					if (ext_header != EXT_HEADER)
						ConsoleWriteLine($"Track {i} has an invalid EXT_ header");
					uint ext_length = br.ReadUInt32(amta_bom); // reading the length of the ext_
					bs.Seek(ext_length, SeekOrigin.Current); // skipping it - no use?

					string strg_header = br.ReadStringBytes(STRG_HEADER.Length);
					if (strg_header != STRG_HEADER)
						ConsoleWriteLine($"Track {i} has an invalid STRG header");
					uint strg_length = br.ReadUInt32(amta_bom); // reading the length of the name
					string track_name = br.ReadStringBytes((int)strg_length - 1).Replace("\0", ""); // read the name (-1 beacuse it always ended with \0 at the end - null byte) and remove null bytes
					
					track_names[i] = track_name;
					trackList.Add(new TrackData(amta_offsets[i], amta_bom, track_offsets[i], track_name, data_writing_pos));
				}

				if (trackList.Count > 0) {
					//Console.WriteLine($"Found tracks: {tracks.Count}");
					ConsoleWriteLine($"Found tracks: {trackList.Count}");
				}

				foreach (var track in trackList) {
					track.ReadData(br);
				}
			}

			SearchTB_TextChanged(null, null); // smelly workaround cause lazy
		}

		private void FileLB_SelectedIndexChanged(object sender, EventArgs e) {
			var curItems = fileLB.SelectedItems;
			if (curItems.Count > 0) {
				extractB.Enabled = true;
				if (curItems.Count == 1) {
					replaceB.Enabled = true;
					previewB.Enabled = true;
					fileInfoTB.Text = GetSelectedTracks()[0].ToString();
				}
				else {
					replaceB.Enabled = false;
					//previewB.Enabled = false;
					fileInfoTB.Clear();
				}
			}
			else {
				extractB.Enabled = false;
				replaceB.Enabled = false;
				previewB.Enabled = false;
			}
		}

		private void PreviewB_Click(object sender, EventArgs e) {
			TrackData selTrack = GetSelectedTracks()[0];
			string tempPath = Path.GetTempPath();
			if(selTrack.Extract(tempPath)) {
				Process.Start(tempPath + Path.DirectorySeparatorChar + selTrack.GetFullName);
			}
		}

		private void ExtractB_Click(object sender, EventArgs e) {
			var tracksToExtract = GetSelectedTracks();
			string folderPath = filePath + Path.DirectorySeparatorChar + fileName;
			Directory.CreateDirectory(folderPath);

			foreach (var track in tracksToExtract) {
				if (track.Extract(folderPath)) {
					ConsoleWriteLine($"Track {track.trackName} exported successfully");
				}
			}
		}

		private void ReplaceB_Click(object sender, EventArgs e) {
			var selTracks = GetSelectedTracks();
			if (selTracks.Length != 1) {
				ConsoleWriteLine("Only one file can be selected for replacement");
				return;
			}

			string trackName = selTracks[0].trackName;

			int index = 0;
			for (; index < trackList.Count; ++index) {
				if (trackList[index].trackName == trackName) {
					break;
				}
			}

			TrackData trackToReplace = trackList[index];
			string ext = trackToReplace.trackExtension;
			string format = trackToReplace.trackFormat;

			OpenFileDialog openFileDialog = new OpenFileDialog {
				Filter = $"Audio File|*.bfwav;*.bfstp",
				Title = $"Selact an Audio File to replace {trackToReplace.trackName}"
			};

			string fileName = "";
			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				fileName = openFileDialog.FileName;
				if(!fileName.EndsWith(FWAV_EXT[0]) && !fileName.EndsWith(FWAV_EXT[1])) {
					ConsoleWriteLine($"Wrong file extension, should be {FWAV_EXT[0]} or {FWAV_EXT[1]}");
					return;
				}
			}
			else {
				ConsoleWriteLine($"No replacement audio file choosen");
				return;
			}

			byte[] newBytes = File.ReadAllBytes(fileName); // read the new file
			byte[] newPaddedBytes = new byte[CalcPaddedLength(newBytes.Length)]; // create a byte array with a correct length (probably MUST be padded to a %64 == 0)
			newBytes.CopyTo(newPaddedBytes, 0); // and copy the new file to that array

			TrackData newTrack = new TrackData(trackToReplace);
			newTrack.trackOffset = 0;
			using (SBinaryReader br = new SBinaryReader(new MemoryStream(newBytes))) {
				newTrack.ReadData(br);
			}

			if (!newTrack.validTrack) {
				ConsoleWriteLine("Replacing interrupted, new track is not valid");
			}

			newTrack.trackOffset = trackToReplace.trackOffset;
			
			// this stuff is messy, sorry
			string backupPath = $"{filePath}{Path.DirectorySeparatorChar}{this.fileName}.bars.backup";
			if (!File.Exists(backupPath))
				File.Copy(filePathFull, backupPath); // make a backup if there's none
			string tempFilePath = Path.GetTempFileName();
			File.Delete(tempFilePath);
			File.Copy(filePathFull, tempFilePath); // make a copy (going to be copying data from it)
			File.Delete(filePathFull);

			int ttrLen = trackToReplace.trackData.Length;
			int ttrPadLen = CalcPaddedLength(ttrLen);
			int lengthDif = newPaddedBytes.Length - ttrPadLen;

			for (int i = index + 1; i < trackList.Count; ++i) { // update offsets
				trackList[i].trackOffset += (uint)lengthDif;
			}

			using (SBinaryReader oldBr = new SBinaryReader(File.OpenRead(tempFilePath)))
			using (SBinaryWriter newBw = new SBinaryWriter(File.OpenWrite(filePathFull))) {
				Stream oldBs = oldBr.BaseStream;
				Stream newBs = newBw.BaseStream;

				newBw.Write(oldBr.ReadBytes((int)offsetsPos)); // copy since beggining -> offsets pos
				for (int i = 0; i < trackList.Count; ++i) { // write all amta/track offsets
					trackList[i].WriteOffsets(newBw, barsBom);
				}
				oldBs.Position = newBs.Position; // meet up the streams
				int bytesToCopy = (int)(newTrack.dataWritingPos - oldBs.Position);
				newBw.Write(oldBr.ReadBytes(bytesToCopy)); // copy since end of offsets section -> track's data writing pos
				newTrack.WriteData(oldBr, newBw); 
				
				newBw.Write(oldBr.ReadBytes((int)(trackToReplace.trackOffset - (uint)oldBs.Position))); // clone since the end of the replaced track's data section to the replaced track pos
				newBw.Write(newPaddedBytes); // write the new track
				oldBs.Seek(trackToReplace.trackOffset + ttrPadLen, SeekOrigin.Begin); // in the temp file - seek to the next track
				bytesToCopy = (int)(oldBs.Length - oldBs.Position); //new FileInfo(tempFilePath).Length - oldBs.Position
				if(bytesToCopy > 0)
					newBw.Write(oldBr.ReadBytes(bytesToCopy)); // copy the rest of the file
				else {
					if (bytesToCopy < -63) // because of padding
						ConsoleWriteLine("Some data copying went wrong (shouldn't happen), please report");
				}
			}

			trackList[index] = newTrack;
			fileInfoTB.Text = newTrack.ToString();
			ConsoleWriteLine($"Correctly replaced {newTrack.trackName}");
			File.Delete(tempFilePath);
		}

		private void SearchTB_TextChanged(object sender, EventArgs e) {
			string curSearch = searchTB.Text;
			fileLB.Items.Clear();
			var tracks = new List<TrackData>();
			foreach (var track in trackList) {
				string trackName = track.trackName;
				if (curSearch == "" || trackName.ToLower().Contains(curSearch)) {
					tracks.Add(track);
				}
			}
			FillFilesLB(tracks);
		}

		private void SortCB_CheckedChanged(object sender, EventArgs e) {
			SearchTB_TextChanged(null, null);
			fileInfoTB.Clear();
		}

		private void SelectAllB_Click(object sender, EventArgs e) {
			for(int i = 0; i < fileLB.Items.Count; ++i) {
				fileLB.SetSelected(i, true);
			}
		}

		private void FillFilesLB(List<TrackData> tracks) {
			fileLB.Items.Clear();

			IEnumerable<TrackData> fillTracks = tracks;
			if (sortCB.Checked)
				fillTracks = fillTracks.OrderBy(x => x.trackName);
			foreach (var track in fillTracks) {
				fileLB.Items.Add(track.trackName);
			}
		}

		private TrackData[] GetSelectedTracks() {
			var selItems = fileLB.SelectedItems;
			TrackData[] retTracks = new TrackData[selItems.Count];

			for (int i = 0; i < selItems.Count; ++i) {
				retTracks[i] = trackList.First(x => x.trackName == selItems[i].ToString());
			}

			return retTracks;
		}

		private int CalcPaddedLength(int length) {
			int off = length % 64;
			if (off == 0)
				return length;
			return length + 64 - off;
		}

		public static void ConsoleWriteLine(string text) {
			consoleBox.AppendText(text + Environment.NewLine);
		}
	}

	public enum BOM {
		LittleEndian = 127,
		BigEndian = 255
	}
}
