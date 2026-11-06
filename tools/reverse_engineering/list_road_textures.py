import os

tex_roads = r"c:\cityscape-open\Worlds\textures\roads"
print("Road textures:")
for f in sorted(os.listdir(tex_roads)):
    print(f"  {f}")
