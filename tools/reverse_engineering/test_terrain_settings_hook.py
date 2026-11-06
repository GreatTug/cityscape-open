import subprocess
import time
import ctypes
import struct
import sys
sys.path.append('.')
from scratch.get_base import get_module_base

kernel32 = ctypes.windll.kernel32
user32 = ctypes.windll.user32

proc = subprocess.Popen(['CityScape.exe'], cwd=r'c:\cityscape-open')
time.sleep(5)

hproc = kernel32.OpenProcess(0x1F0FFF, False, proc.pid)
base = get_module_base(hproc)

def read_u32(addr):
    buf = ctypes.create_string_buffer(4)
    r = ctypes.c_size_t()
    kernel32.ReadProcessMemory(hproc, ctypes.c_void_p(addr), buf, 4, ctypes.byref(r))
    return struct.unpack('<I', buf.raw)[0]

def read_f32(addr):
    buf = ctypes.create_string_buffer(4)
    r = ctypes.c_size_t()
    kernel32.ReadProcessMemory(hproc, ctypes.c_void_p(addr), buf, 4, ctypes.byref(r))
    return struct.unpack('<f', buf.raw)[0]

# Let's find main window
hwnds = []
def enum_cb(h, _):
    pid = ctypes.c_ulong()
    user32.GetWindowThreadProcessId(h, ctypes.byref(pid))
    if pid.value == proc.pid and user32.IsWindowVisible(h):
        hwnds.append(h)
    return True

CB = ctypes.WINFUNCTYPE(ctypes.c_bool, ctypes.c_void_p, ctypes.c_void_p)(enum_cb)
user32.EnumWindows(CB, 0)
print(f'Visible windows: {[hex(h) for h in hwnds]}')

# Dismiss Welcome dialog if any
for h in hwnds:
    buf = ctypes.create_unicode_buffer(256)
    user32.GetWindowTextW(h, buf, 256)
    if 'Welcome' in buf.value:
        print('Closing welcome dialog')
        user32.PostMessageW(h, 0x0010, 0, 0) # WM_CLOSE
        time.sleep(1)

# Now trigger Options -> Terrain Settings:
# Command ID or Menu click
# Let's see what happens if we find the main window and click Options -> Terrain Settings:
# Or we can post WM_COMMAND with the Terrain Settings menu ID!
# What is the menu ID for Terrain Settings?
# Let's find menuStrip1 on main form:
main_h = None
for h in hwnds:
    buf = ctypes.create_unicode_buffer(256)
    user32.GetWindowTextW(h, buf, 256)
    if 'CityScape' in buf.value:
        main_h = h
        break

print('Main window:', hex(main_h) if main_h else 'None')

# Read initial dimensions
app = read_u32(base + 0x02CF2118)
scene = read_u32(app + 0x7c)
terrain = read_u32(scene + 0x48)
gg = read_u32(terrain + 0x54)
ts = read_u32(scene + 0x64)

print(f'Before resize:')
print(f'  TerrainSettings size: {read_f32(ts+0xa8)} x {read_f32(ts+0xac)}')
print(f'  Boundary extent:      {read_f32(ts+0x48+0x3c)} x {read_f32(ts+0x48+0x40)}')
print(f'  Board extent:         {read_f32(gg+0x74+0x3c)} x {read_f32(gg+0x74+0x40)}')

proc.kill()
