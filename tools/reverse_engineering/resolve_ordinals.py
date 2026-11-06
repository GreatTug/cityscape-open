import pefile

dll_path = r"C:\CityScape 1.8 Autodesk Promo\libeay32.dll"
pe = pefile.PE(dll_path)

ord_to_name = {}
for exp in pe.DIRECTORY_ENTRY_EXPORT.symbols:
    ord_to_name[exp.ordinal] = exp.name.decode('ascii') if exp.name else f"ord_{exp.ordinal}"

ords = [490, 492, 2996, 420, 400, 944, 943, 415, 395, 202, 3713, 200, 204, 2630, 2821, 2936, 333, 1252, 3109, 2589, 469, 464, 484, 485, 497, 52, 2927, 3879, 3019, 276, 266, 269, 78, 2894, 961, 255, 3067, 256, 2656, 2660, 223, 227, 493, 491, 66]

for o in ords:
    print(f"ord({o}): {ord_to_name.get(o, 'UNKNOWN')}")
