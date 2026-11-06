import struct

path = r"C:\cityscape-open\testsaves\save1.cst"
with open(path, "rb") as f:
    data = f.read()

print(f"=== {path} (len={len(data)}) ===")
quote2 = data.find(b'"', 1)
print(f"Header: {data[:quote2+1].decode('latin1')}")
payload = data[quote2+1:]
print(f"Payload len: {len(payload)}")

# Let's inspect the payload
print("Payload hex first 64 bytes:", payload[:64].hex())

for i in range(0, len(payload)-3, 4):
    ival = struct.unpack_from("<i", payload, i)[0]
    uval = struct.unpack_from("<I", payload, i)[0]
    fval = struct.unpack_from("<f", payload, i)[0]
    if 5.0 <= abs(fval) <= 1000000.0 or 5 <= ival <= 100000:
        print(f"  Offset 0x{i:03X} (+{i:3d}): int={ival:8d} (0x{uval:08X}) | float={fval:12.4f}")
