with open(r"src\palDotNet\palDotNet\palVisual\ControlRender.cs", "rb") as f:
    data = f.read()

idx = data.find(b"ORTHOGRAPHIC")
print("Found ORTHOGRAPHIC at byte:", idx)
chunk = data[idx - 100 : idx + 200]
print("Repr of chunk:")
print(repr(chunk))
