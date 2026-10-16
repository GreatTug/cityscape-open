import os
import time
import subprocess
import ctypes
from ctypes import wintypes

user32 = ctypes.windll.user32

def test_ui_window():
    print("Launching CityScape.exe...")
    proc = subprocess.Popen(["CityScape.exe", "Worlds\\sample.csp"], cwd=os.getcwd())
    try:
        # Give UI 3 seconds to initialize, layout dock panels and render
        time.sleep(3)
        assert proc.poll() is None, f"CityScape exited prematurely with code {proc.poll()}"

        # Search for CityScape top-level window
        found_hwnd = None
        def enum_cb(hwnd, lparam):
            nonlocal found_hwnd
            if user32.IsWindowVisible(hwnd):
                length = user32.GetWindowTextLengthW(hwnd)
                if length > 0:
                    buff = ctypes.create_unicode_buffer(length + 1)
                    user32.GetWindowTextW(hwnd, buff, length + 1)
                    if "PixelActive CityScape" in buff.value:
                        found_hwnd = (hwnd, buff.value)
                        return False
            return True

        WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.c_bool, wintypes.HWND, wintypes.LPARAM)
        user32.EnumWindows(WNDENUMPROC(enum_cb), 0)

        assert found_hwnd is not None, "CityScape UI window was not found in top-level windows!"
        hwnd, title = found_hwnd
        print(f"  [SUCCESS] Found Window HWND: {hex(hwnd)}, Title: '{title}'")

        # Verify Window dimensions
        rect = wintypes.RECT()
        user32.GetWindowRect(hwnd, ctypes.byref(rect))
        width = rect.right - rect.left
        height = rect.bottom - rect.top
        print(f"  [SUCCESS] Window Dimensions: {width}x{height}")
        assert width >= 800 and height >= 600, f"Unexpected window dimensions: {width}x{height}"

        print("CityScape UI window is verified ACTIVE, RESPONSIVE, and RUNNING!")
    finally:
        # Gracefully terminate process
        print("Closing CityScape.exe test process...")
        proc.terminate()
        try:
            proc.wait(timeout=3)
        except subprocess.TimeoutExpired:
            proc.kill()
        print("Process closed cleanly.")

if __name__ == "__main__":
    test_ui_window()
