import pefile

pe = pefile.PE("CityScape.exe")

print("PE Characteristics:", hex(pe.FILE_HEADER.Characteristics))
if hasattr(pe, 'DIRECTORY_ENTRY_EXPORT'):
    print("Export table found! Number of exports:", len(pe.DIRECTORY_ENTRY_EXPORT.symbols))
    for exp in pe.DIRECTORY_ENTRY_EXPORT.symbols[:20]:
        name = exp.name.decode('ascii') if exp.name else "ordinal " + str(exp.ordinal)
        print("  ", name)
else:
    print("No export table in CityScape.exe (normal for an EXE).")

# But let's check RTTI Complete Object Locators and VTables!
print("\nSearching for RTTI vtables...")
