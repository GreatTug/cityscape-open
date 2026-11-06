import time
import subprocess
import struct
import ctypes
from ctypes import wintypes
import sys
sys.path.append('.')
from scratch.get_base import get_module_base

user32 = ctypes.windll.user32
kernel32 = ctypes.windll.kernel32

PROCESS_VM_READ = 0x0010
PROCESS_QUERY_INFORMATION = 0x0400
WM_CLOSE = 0x0010
WM_LBUTTONDOWN = 0x0201
WM_LBUTTONUP = 0x0202
MK_LBUTTON = 0x0001

def click_ctrl_at(hwnd, x, y):
    lparam = (y << 16) | x
    user32.SendMessageW(hwnd, WM_LBUTTONDOWN, MK_LBUTTON, lparam)
    time.sleep(0.05)
    user32.SendMessageW(hwnd, WM_LBUTTONUP, 0, lparam)
    time.sleep(0.2)

def read_mem(hproc, addr, size):
    buf = ctypes.create_string_buffer(size)
    bytes_read = ctypes.c_size_t()
    if kernel32.ReadProcessMemory(hproc, ctypes.c_void_p(addr), buf, size, ctypes.byref(bytes_read)):
        return buf.raw
    return None

def read_u32(hproc, addr):
    data = read_mem(hproc, addr, 4)
    if data and len(data) == 4:
        return struct.unpack('<I', data)[0]
    return None

def read_f32(hproc, addr):
    data = read_mem(hproc, addr, 4)
    if data and len(data) == 4:
        return struct.unpack('<f', data)[0]
    return None

def test_new_terrain_flow(create_project_checked=False):
    print(f"\n==================== Testing New Terrain (CreateNewProject={create_project_checked}) ====================")
    proc = subprocess.Popen(["CityScape.exe"], cwd=r"c:\cityscape-open")
    time.sleep(6)
    try:
        main_hwnd = None
        welcome_hwnd = None
        def enum_wnds(hwnd, lparam):
            nonlocal main_hwnd, welcome_hwnd
            if user32.IsWindowVisible(hwnd):
                pid = wintypes.DWORD()
                user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
                if pid.value == proc.pid:
                    buff = ctypes.create_unicode_buffer(256)
                    user32.GetWindowTextW(hwnd, buff, 256)
                    if "PixelActive CityScape 1.8" in buff.value:
                        main_hwnd = hwnd
                    elif "Welcome" in buff.value:
                        welcome_hwnd = hwnd
            return True
        WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.c_bool, wintypes.HWND, wintypes.LPARAM)
        user32.EnumWindows(WNDENUMPROC(enum_wnds), 0)

        if welcome_hwnd:
            user32.PostMessageW(welcome_hwnd, WM_CLOSE, 0, 0)
            time.sleep(1.0)

        menu_hwnd = None
        def enum_child(hwnd, lparam):
            nonlocal menu_hwnd
            buff = ctypes.create_unicode_buffer(256)
            user32.GetWindowTextW(hwnd, buff, 256)
            if buff.value == "menuStrip1":
                menu_hwnd = hwnd
                return False
            return True
        WNDENUMCHILD = ctypes.WINFUNCTYPE(ctypes.c_bool, wintypes.HWND, wintypes.LPARAM)
        user32.EnumChildWindows(main_hwnd, WNDENUMCHILD(enum_child), 0)

        click_ctrl_at(menu_hwnd, 15, 10) # File
        time.sleep(0.8)

        dropdown_hwnd = None
        def enum_drop(hwnd, lparam):
            nonlocal dropdown_hwnd
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

        click_ctrl_at(dropdown_hwnd, 40, 15) # New Terrain...
        time.sleep(2.0)

        dlg_hwnd = None
        def enum_dlg(hwnd, lparam):
            nonlocal dlg_hwnd
            if user32.IsWindowVisible(hwnd):
                pid = wintypes.DWORD()
                user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
                if pid.value == proc.pid and hwnd != main_hwnd:
                    buff = ctypes.create_unicode_buffer(256)
                    user32.GetWindowTextW(hwnd, buff, 256)
                    if buff.value == "New Terrain":
                        dlg_hwnd = hwnd
                        return False
            return True
        user32.EnumWindows(WNDENUMPROC(enum_dlg), 0)

        if not dlg_hwnd:
            print("Could not find New Terrain dialog!")
            return

        chk_hwnd = None
        ok_hwnd = None
        def enum_c(hwnd, lparam):
            nonlocal chk_hwnd, ok_hwnd
            buff = ctypes.create_unicode_buffer(256)
            user32.GetWindowTextW(hwnd, buff, 256)
            cbuff = ctypes.create_unicode_buffer(256)
            user32.GetClassNameW(hwnd, cbuff, 256)
            if "Create new project" in buff.value:
                chk_hwnd = hwnd
            elif buff.value == "OK" and "BUTTON" in cbuff.value:
                ok_hwnd = hwnd
            return True
        user32.EnumChildWindows(dlg_hwnd, WNDENUMCHILD(enum_c), 0)

        # By default in user.config, TerrainSizeX and Y are already 2500!
        if not create_project_checked:
            # uncheck
            click_ctrl_at(chk_hwnd, 8, 8)
            time.sleep(0.5)

        # Click OK
        click_ctrl_at(ok_hwnd, 37, 11)
        time.sleep(1.0)

        # If create_project_checked is True, does a New Project dialog appear?
        if create_project_checked:
            time.sleep(1.0)
            subdlg_hwnd = None
            def enum_subdlg(hwnd, lparam):
                nonlocal subdlg_hwnd
                if user32.IsWindowVisible(hwnd):
                    pid = wintypes.DWORD()
                    user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
                    if pid.value == proc.pid and hwnd != main_hwnd:
                        buff = ctypes.create_unicode_buffer(256)
                        user32.GetWindowTextW(hwnd, buff, 256)
                        print(f"  Visible sub-dialog: '{buff.value}' (hwnd={hex(hwnd)})")
                        if "Project" in buff.value or "OK" in buff.value:
                            subdlg_hwnd = hwnd
                return True
            user32.EnumWindows(WNDENUMPROC(enum_subdlg), 0)

        time.sleep(5.0)

        # Inspect memory
        hproc = kernel32.OpenProcess(PROCESS_VM_READ | PROCESS_QUERY_INFORMATION, False, proc.pid)
        mod_base = get_module_base(hproc)
        def va(addr): return mod_base + (addr - 0x00400000)

        app_ptr = read_u32(hproc, va(0x30f2118))
        scene_ptr = read_u32(hproc, app_ptr + 0x7c) if app_ptr else None
        ts_ptr = read_u32(hproc, scene_ptr + 0x64) if scene_ptr else None
        terrain_ptr = read_u32(hproc, scene_ptr + 0x48) if scene_ptr else None
        gg_ptr = read_u32(hproc, terrain_ptr + 0x54) if terrain_ptr else None

        if ts_ptr:
            ts_size_x = read_f32(hproc, ts_ptr + 0xa8)
            ts_size_y = read_f32(hproc, ts_ptr + 0xac)
            bgp_ext_x = read_f32(hproc, ts_ptr + 0x48 + 0x3c)
            bgp_ext_y = read_f32(hproc, ts_ptr + 0x48 + 0x40)
            print(f"  TerrainSettings: Size={ts_size_x} x {ts_size_y} m | Boundary={bgp_ext_x} x {bgp_ext_y} m")
        if gg_ptr:
            wgp_ext_x = read_f32(hproc, gg_ptr + 0x74 + 0x3c)
            wgp_ext_y = read_f32(hproc, gg_ptr + 0x74 + 0x40)
            print(f"  GlobalGrid: Board Extent={wgp_ext_x} x {wgp_ext_y} m")
            if ts_ptr:
                print(f"  Ratio Board/Boundary: {wgp_ext_x/bgp_ext_x*100.0:.1f}%")

    finally:
        proc.terminate()
        try: proc.wait(timeout=3)
        except: proc.kill()

if __name__ == '__main__':
    test_new_terrain_flow(create_project_checked=False)
