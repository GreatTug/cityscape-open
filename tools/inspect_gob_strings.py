import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

# Let's inspect the text around 0x1F6F300 to 0x1F6F400
chunk = data[0x1F6F300:0x1F6F500]
print("Strings around 0x1F6F300:")
# split by null bytes
parts = [p.decode(errors='ignore') for p in chunk.split(b'\x00') if len(p) > 0]
for p in parts:
    print("  ", p)

# Also let's inspect around 0x1F6DAC0
chunk2 = data[0x1F6DAC0:0x1F6DC00]
print("\nStrings around 0x1F6DAC0:")
parts2 = [p.decode(errors='ignore') for p in chunk2.split(b'\x00') if len(p) > 0]
for p in parts2:
    print("  ", p)
