import mmap

# 0 Piano
# 13 Marimba
# 27 Electric Guitar (jazz)
instrument = 27
instrument_name = "ElectricGuitarJazz"
midi_notes = list(range(72,109))
midi_names = ["C5", "CSharp5", "D5", "DSharp5", "E5", "F5", "FSharp5", "G5", "GSharp5", "A5", "ASharp5", "B5", "C6", "CSharp6", "D6", "DSharp6", "E6", "F6", "FSharp6", "G6", "GSharp6", "A6", "ASharp6", "B6", "C7", "CSharp7", "D7", "DSharp7", "E7", "F7", "FSharp7", "G7", "GSharp7", "A7", "ASharp7", "B7", "C8"]

notes_names = list(zip(midi_notes, midi_names))
#print(notes_names[0][0])

# load it
with open("PianoC5Template.mid", 'r+b') as f:
	# memory-map the file, size 0 means whole file
	mm = mmap.mmap(f.fileno(), 0)
	

for note_name in notes_names:
	# be sure you created the instrument_name + Midi Directory
	with open(instrument_name + "Midi/" + instrument_name + note_name[1] +".mid", 'wb') as f:
		mm.seek(0)
		mm[69] = instrument
		# change note
		mm[72] = note_name[0]
		mm[77] = note_name[0]

		print(mm.readline())
		mm.seek(0)
		#f.write_byte(mm.readline())
		f.write(mm.readline())
mm.close()