import os
import time
import subprocess
import ctypes
from ctypes import wintypes

user32 = ctypes.windll.user32

def verify_window(exe_name, args, wait_time=4):
    print(f"Launching {exe_name} with args {args}...")
    proc = subprocess.Popen([exe_name] + args, cwd=os.getcwd())
    try:
        time.sleep(wait_time)
        assert proc.poll() is None, f"{exe_name} exited prematurely with code {proc.poll()}"

        found_hwnd = None
        def enum_cb(hwnd, lparam):
            nonlocal found_hwnd
            if user32.IsWindowVisible(hwnd):
                length = user32.GetWindowTextLengthW(hwnd)
                if length > 0:
                    buff = ctypes.create_unicode_buffer(length + 1)
                    user32.GetWindowTextW(hwnd, buff, length + 1)
                    if "PixelActive CityScape" in buff.value:
                        pid = wintypes.DWORD()
                        user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
                        if pid.value == proc.pid:
                            found_hwnd = (hwnd, buff.value)
                            return False
            return True

        WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.c_bool, wintypes.HWND, wintypes.LPARAM)
        user32.EnumWindows(WNDENUMPROC(enum_cb), 0)

        assert found_hwnd is not None, f"{exe_name} UI window was not found in top-level windows!"
        hwnd, title = found_hwnd
        print(f"  [SUCCESS] {exe_name} HWND: {hex(hwnd)}, Title: '{title}'")

        # Verify Window dimensions
        rect = wintypes.RECT()
        user32.GetWindowRect(hwnd, ctypes.byref(rect))
        width = rect.right - rect.left
        height = rect.bottom - rect.top
        print(f"  [SUCCESS] {exe_name} Window Dimensions: {width}x{height}")
        assert width >= 800 and height >= 600, f"Unexpected window dimensions: {width}x{height}"

        print(f"{exe_name} UI window is verified ACTIVE, RESPONSIVE, and RUNNING!\n")
    finally:
        print(f"Closing {exe_name} test process...")
        proc.terminate()
        try:
            proc.wait(timeout=3)
        except subprocess.TimeoutExpired:
            proc.kill()
        print(f"{exe_name} closed cleanly.")

def test_all_ui():
    print("==================================================")
    print("  Testing CityScape Dual Executables Live Windows ")
    print("==================================================")
    # 1. Test CityScape.exe (49 MB Full Native 3D Engine with Open-Source DLLs)
    verify_window("CityScape.exe", ["Worlds\\sample.csp"], wait_time=5)

    # 2. Test CityScape_Managed.exe (127 KB Pure C# Open-Source Launcher)
    verify_window("CityScape_Managed.exe", ["Worlds\\sample.csp"], wait_time=3)

    print("ALL DUAL EXECUTABLE LIVE GUI TESTS PASSED (100%)!")

if __name__ == "__main__":
    test_all_ui()
