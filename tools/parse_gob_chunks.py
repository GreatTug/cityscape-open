import struct

def parse_chunks(fpath):
    print(f"\n====================\nParsing {fpath}")
    with open(fpath, "rb") as f:
        data = f.read()

    pos = 0
    # Header
    hdr = data[:17]
    print(f"Header: {hdr}")
    pos = 18 # past null byte

    # Read chunks
    while pos < len(data):
        # find next null-terminated string
        null_idx = data.find(b'\x00', pos)
        if null_idx == -1 or null_idx > pos + 64:
            break
        tag = data[pos:null_idx].decode('ascii', errors='ignore')
        pos = null_idx + 1
        
        # print tag and preview next 16 bytes
        next_bytes = data[pos:pos+32]
        print(f"Offset 0x{null_idx:X}: Tag '{tag}', next 32 bytes: {next_bytes.hex()}")

        if tag == "gxVertexBuffer":
            # inspect what follows gxVertexBuffer
            pass
        elif tag == "gxIndexBuffer":
            pass
        elif tag == "ShaderVariables":
            # skip to </ShaderVariables>
            end_xml = data.find(b"</ShaderVariables>", pos)
            if end_xml != -1:
                pos = end_xml + len("</ShaderVariables>")
                continue
        elif tag == "gxModel":
            pass
        
        if pos > 1000:
            print("... stopped after 1000 bytes ...")
            break

parse_chunks("Worlds/props/natural/trees/tree_oak03.gob")
parse_chunks("Worlds/props/urban/street_lights/traffic_light_single.gob")
