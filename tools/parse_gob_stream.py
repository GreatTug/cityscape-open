import struct
import re

with open(r"Worlds/props/natural/trees/tree_oak03.gob", "rb") as f:
    data = f.read()

print(f"Data length: {len(data)} bytes")

# Let's inspect XML shader variables:
xml_start = data.find(b"<ShaderVariables>")
xml_end = data.find(b"</ShaderVariables>")
if xml_start != -1 and xml_end != -1:
    xml_str = data[xml_start : xml_end + len("</ShaderVariables>")].decode('utf-8', errors='ignore')
    print("ShaderVariables XML:")
    print(xml_str)
    print(f"XML ends at 0x{xml_end + len('</ShaderVariables>'):X}")

after_xml = xml_end + len("</ShaderVariables>")
print("Bytes after XML (next 64 bytes):", data[after_xml:after_xml+64])
