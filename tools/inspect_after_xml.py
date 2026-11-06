with open("Worlds/props/urban/street_lights/traffic_light_single.gob", "rb") as f:
    data = f.read()

xml_end = data.find(b"</ShaderVariables>")
pos = xml_end + len("</ShaderVariables>")
print("Offset after </ShaderVariables>:", hex(pos))
print(data[pos:pos+150])
