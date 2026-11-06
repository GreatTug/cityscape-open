with open("CityScape.exe", "rb") as f:
    data = f.read()

import re
for m in re.finditer(rb'<PixelActive %s>', data):
    start = max(0, m.start() - 20)
    end = min(len(data), m.end() + 60)
    print("Found:", data[start:end])

for m in re.finditer(rb'%s bin ver', data):
    start = max(0, m.start() - 20)
    end = min(len(data), m.end() + 60)
    print("Found bin ver:", data[start:end])

for m in re.finditer(rb'bin ver', data):
    print("Found bin ver at", hex(m.start()))
