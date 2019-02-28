## BarsTool

BarsTool is a GUI-style .bars file editor, which allows you to easily extract or replace sound files embedded inside.
It was made with The Legend of Zelda: Breath of the Wild in mind.

This tool assumes your new audio files are 100% fine, please make sure they work before replacing anything.

Please remember that this tool is new (read: beta) and was not tested extensively.

### Download

For download click the "X release(s)" button just below "BARS file editor for TLOZ: Breath of The Wild" text.

### Features

- Supports both WiiU and Switch (Big Endian and Little Endian byte order)
- Gives you the important information about a selected audio file.
- Supports replacing BFWAV and BFSTP files.
- Can correctly replace audio files with properties different than in the original audio (more on this below).
- Automatically makes a backup of the original .bars file next to it with a .backup extension added.

### IMPORTANT!

If the resulting .bars file is bigger in size than the original file, make sure to update the RSTB file:
https://zeldamods.org/wiki/Help:Updating_the_RSTB

This may also result with bugs when the console's memory runs out because of the bigger audio sizes.

### Perfect conditions for replacing an audio file

Those don't have to be met AT ALL, but can sometimes result in something not working (Beware section above) when different:
- the same audio format (.bfwav or .bfstp)
- the same or shorter length
- the same audio encoding
- the same amount of audio channels
- the same sample rate

This should be met:
- the same looping status (with this - correctly set loopStart frame etc.)

### Bug Reporting

While reporting a bug, please specify:
- the console you're using (WiiU / Switch)
- the name of the bars file you're trying to edit (BoTW only)
- (if possible) a sound file you want to replace the original with (BoTW only)

### Thanks

Thanks to SamusAranX's [bars_extractor.py](https://gist.github.com/SamusAranX/6eb8b6fd1777b17afc3107a979c2409a) script that helped a lot with figuring out .bars format.
