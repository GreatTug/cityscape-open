import subprocess
import time
import ctypes
from ctypes import wintypes
import struct
import sys
sys.path.append('.')
from scratch.get_base import get_module_base

kernel32 = ctypes.windll.kernel32
user32 = ctypes.windll.user32

proc = subprocess.Popen(["CityScape.exe"], cwd=r"c:\cityscape-open")
time.sleep(6)

try:
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

    main_hwnd = None
    welcome_hwnd = None
    def enum_wnds(hwnd, lparam):
        global main_hwnd, welcome_hwnd
        if user32.IsWindowVisible(hwnd):
            pid = wintypes.DWORD()
            user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
            if pid.value == proc.pid:
                buff = ctypes.create_unicode_buffer(256)
                user32.GetWindowTextW(hwnd, buff, 256)
                if "CityScape" in buff.value:
                    main_hwnd = hwnd
                elif "Welcome" in buff.value:
                    welcome_hwnd = hwnd
        return True
    WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.c_bool, wintypes.HWND, wintypes.LPARAM)
    user32.EnumWindows(WNDENUMPROC(enum_wnds), 0)

    if welcome_hwnd:
        user32.PostMessageW(welcome_hwnd, 0x0010, 0, 0)
        time.sleep(1.0)

    # Find menuStrip1
    menu_hwnd = None
    def enum_child(hwnd, lparam):
        global menu_hwnd
        buff = ctypes.create_unicode_buffer(256)
        user32.GetWindowTextW(hwnd, buff, 256)
        if buff.value == "menuStrip1":
            menu_hwnd = hwnd
            return False
        return True
    WNDENUMCHILD = ctypes.WINFUNCTYPE(ctypes.c_bool, wintypes.HWND, wintypes.LPARAM)
    user32.EnumChildWindows(main_hwnd, WNDENUMCHILD(enum_child), 0)

    # Click Options
    lparam = (10 << 16) | 265
    user32.SendMessageW(menu_hwnd, 0x0201, 1, lparam)
    time.sleep(0.05)
    user32.SendMessageW(menu_hwnd, 0x0202, 0, lparam)
    time.sleep(0.8)

    dropdown_hwnd = None
    def enum_drop(hwnd, lparam):
        global dropdown_hwnd
        if user32.IsWindowVisible(hwnd):
            pid = wintypes.DWORD()
            user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
            if pid.value == proc.pid and hwnd != main_hwnd:
                cbuff = ctypes.create_unicode_buffer(256)
                user32.GetClassNameW(hwnd, cbuff, 256)
                if "20808" in cbuff.value:
                    dropdown_hwnd = hwnd
                    return False
        return True
    user32.EnumWindows(WNDENUMPROC(enum_drop), 0)

    lparam_item = (15 << 16) | 40
    user32.SendMessageW(dropdown_hwnd, 0x0201, 1, lparam_item)
    time.sleep(0.05)
    user32.SendMessageW(dropdown_hwnd, 0x0202, 0, lparam_item)
    time.sleep(1.5)

    dialog_hwnd = None
    def enum_dlg(hwnd, lparam):
        global dialog_hwnd
        if user32.IsWindowVisible(hwnd):
            pid = wintypes.DWORD()
            user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
            if pid.value == proc.pid:
                buff = ctypes.create_unicode_buffer(256)
                user32.GetWindowTextW(hwnd, buff, 256)
                if "Terrain Settings" in buff.value:
                    dialog_hwnd = hwnd
                    return False
        return True
    user32.EnumWindows(WNDENUMPROC(enum_dlg), 0)

    edits_with_pos = []
    ok_btn = None
    def enum_dlg_child(hwnd, lparam):
        global ok_btn
        cbuff = ctypes.create_unicode_buffer(256)
        tbuff = ctypes.create_unicode_buffer(256)
        user32.GetClassNameW(hwnd, cbuff, 256)
        user32.GetWindowTextW(hwnd, tbuff, 256)
        cls = cbuff.value
        txt = tbuff.value
        if "EDIT" in cls.upper():
            rc = wintypes.RECT()
            user32.GetWindowRect(hwnd, ctypes.byref(rc))
            edits_with_pos.append((hwnd, rc.left, rc.top))
        elif txt == "OK":
            ok_btn = hwnd
        return True
    user32.EnumChildWindows(dialog_hwnd, WNDENUMCHILD(enum_dlg_child), 0)

    edits_with_pos.sort(key=lambda item: (item[2], item[1]))
    # Filter only the left-side edits (x around 747)
    left_edits = [e for e in edits_with_pos if abs(e[1] - edits_with_pos[0][1]) < 30]

    # left_edits[0] = Terrain Size X
    # left_edits[1] = Terrain Size Y
    # left_edits[2] = Tile Size X
    # left_edits[3] = Tile Size Y
    edit_tsx = left_edits[0][0]
    edit_tsy = left_edits[1][0]
    edit_tilesx = left_edits[2][0]
    edit_tilesy = left_edits[3][0]

    VK_RETURN = 0x0D
    WM_KEYDOWN = 0x0100
    WM_CHAR = 0x0102
    WM_KEYUP = 0x0101
    WM_SETTEXT = 0x000C

    target_w = "4000"
    target_h = "2800"

    user32.SendMessageW(edit_tsx, WM_SETTEXT, 0, target_w)
    user32.SendMessageW(edit_tsx, WM_KEYDOWN, VK_RETURN, 0)
    user32.SendMessageW(edit_tsx, WM_CHAR, VK_RETURN, 0)
    user32.SendMessageW(edit_tsx, WM_KEYUP, VK_RETURN, 0)
    time.sleep(0.05)

    user32.SendMessageW(edit_tsy, WM_SETTEXT, 0, target_h)
    user32.SendMessageW(edit_tsy, WM_KEYDOWN, VK_RETURN, 0)
    user32.SendMessageW(edit_tsy, WM_CHAR, VK_RETURN, 0)
    user32.SendMessageW(edit_tsy, WM_KEYUP, VK_RETURN, 0)
    time.sleep(0.05)

    user32.SendMessageW(edit_tilesx, WM_SETTEXT, 0, target_w)
    user32.SendMessageW(edit_tilesx, WM_KEYDOWN, VK_RETURN, 0)
    user32.SendMessageW(edit_tilesx, WM_CHAR, VK_RETURN, 0)
    user32.SendMessageW(edit_tilesx, WM_KEYUP, VK_RETURN, 0)
    time.sleep(0.05)

    user32.SendMessageW(edit_tilesy, WM_SETTEXT, 0, target_h)
    user32.SendMessageW(edit_tilesy, WM_KEYDOWN, VK_RETURN, 0)
    user32.SendMessageW(edit_tilesy, WM_CHAR, VK_RETURN, 0)
    user32.SendMessageW(edit_tilesy, WM_KEYUP, VK_RETURN, 0)
    time.sleep(0.05)

    # Click OK
    user32.SendMessageW(ok_btn, 0x00F5, 0, 0)
    time.sleep(2.0)

    app = read_u32(base + 0x02CF2118)
    scene = read_u32(app + 0x7c)
    terrain = read_u32(scene + 0x48)
    gg = read_u32(terrain + 0x54)
    ts = read_u32(scene + 0x64)

    tsx = read_f32(ts + 0xa8)
    tsy = read_f32(ts + 0xac)
    bound_x = read_f32(ts + 0x84)
    bound_y = read_f32(ts + 0x88)
    board_x = read_f32(gg + 0xa8)
    board_y = read_f32(gg + 0xac)

    print("\n================ LIVE TERRAIN SETTINGS TEST RESULT ================")
    print(f"Requested Size:       {target_w}.0 m x {target_h}.0 m")
    print(f"TerrainSettings Size: {tsx:.1f} m x {tsy:.1f} m")
    print(f"Pink Boundary Extent: {bound_x:.1f} m x {bound_y:.1f} m")
    print(f"White Board Extent:   {board_x:.1f} m x {board_y:.1f} m")
    print(f"White / Boundary:     {board_x/bound_x*100:.1f}% x {board_y/bound_y*100:.1f}%")
    print("===================================================================\n")

finally:
    proc.kill()
