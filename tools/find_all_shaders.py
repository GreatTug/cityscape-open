with open("Worlds/props/urban/street_lights/traffic_light_single.gob", "rb") as f:
    data = f.read()

pos = 0
while True:
    pos = data.find(b"<ShaderVariables>", pos)
    if pos == -1:
        break
    end_xml = data.find(b"</ShaderVariables>", pos)
    print(f"ShaderVariables at 0x{pos:X} .. 0x{end_xml:X}")
    print(data[pos:end_xml+18].decode('ascii', errors='ignore')[:150] + "...")
    pos = end_xml + 18
print("After last ShaderVariables, pos is:", hex(pos))
print(data[pos:pos+200])
