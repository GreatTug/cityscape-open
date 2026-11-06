import os
from PIL import Image

p = r"c:\cityscape-open\scratch\new_terrain_before_submit.bmp"
if os.path.exists(p):
    im = Image.open(p)
    print(f"Image size: {im.size}, mode: {im.mode}")
    # Save as png so we can check it if needed
    im.save(r"c:\cityscape-open\scratch\new_terrain_before_submit.png")
    print("Saved as PNG.")
else:
    print("Not found.")
