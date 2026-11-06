import os
import time
import subprocess
import struct
import ctypes
import sys
sys.path.append('.')
from scratch.get_base import get_module_base

kernel32 = ctypes.windll.kernel32
PROCESS_VM_READ = 0x0010
PROCESS_QUERY_INFORMATION = 0x0400

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

def inspect_file(target_arg):
    print(f"\n==================== Testing CityScape.exe with '{target_arg}' ====================")
    args = ["CityScape.exe"]
    if target_arg:
        args.append(target_arg)
    proc = subprocess.Popen(args, cwd=r"c:\cityscape-open")
    time.sleep(6)

    hproc = kernel32.OpenProcess(PROCESS_VM_READ | PROCESS_QUERY_INFORMATION, False, proc.pid)
    if not hproc:
        print(f"Failed to OpenProcess: {kernel32.GetLastError()}")
        proc.kill()
        return

    try:
        mod_base = get_module_base(hproc)
        if not mod_base:
            print("Could not get module base!")
            return

        def va(addr):
            return mod_base + (addr - 0x00400000)

        app_ptr = read_u32(hproc, va(0x30f2118))
        if not app_ptr:
            print("app_ptr is NULL")
            return

        scene_ptr = read_u32(hproc, app_ptr + 0x7c)
        if not scene_ptr:
            print("scene_ptr is NULL")
            return

        ts_ptr = read_u32(hproc, scene_ptr + 0x64)
        bgp_ext_x = bgp_ext_y = None
        if ts_ptr:
            ts_size_x = read_f32(hproc, ts_ptr + 0xa8)
            ts_size_y = read_f32(hproc, ts_ptr + 0xac)
            ts_sample_dist = read_f32(hproc, ts_ptr + 0x44)
            bgp_ext_x = read_f32(hproc, ts_ptr + 0x48 + 0x3c)
            bgp_ext_y = read_f32(hproc, ts_ptr + 0x48 + 0x40)
            print(f"  [TerrainSettings (ts_ptr={hex(ts_ptr)})]")
            print(f"    TerrainSizeX x Y       : {ts_size_x:.1f} x {ts_size_y:.1f} m")
            print(f"    SamplingDistance       : {ts_sample_dist:.2f} m")
            print(f"    Pink Boundary Extent   : {bgp_ext_x:.1f} x {bgp_ext_y:.1f} m")

        terrain_ptr = read_u32(hproc, scene_ptr + 0x48)
        if terrain_ptr:
            gg_ptr = read_u32(hproc, terrain_ptr + 0x54)
            if gg_ptr:
                gg_res = read_u32(hproc, gg_ptr + 0x70)
                gg_sample_dist = read_f32(hproc, gg_ptr + 0x6c)
                wgp_ext_x = read_f32(hproc, gg_ptr + 0x74 + 0x3c)
                wgp_ext_y = read_f32(hproc, gg_ptr + 0x74 + 0x40)
                print(f"  [GlobalGrid / White Surface (gg_ptr={hex(gg_ptr)})]")
                print(f"    GridRes                : {gg_res}")
                print(f"    SamplingDistance       : {gg_sample_dist:.2f} m")
                print(f"    White Surface Board Ext: {wgp_ext_x:.1f} x {wgp_ext_y:.1f} m")
                if bgp_ext_x and wgp_ext_x:
                    ratio_x = wgp_ext_x / bgp_ext_x * 100.0
                    ratio_y = wgp_ext_y / bgp_ext_y * 100.0
                    print(f"    Match with Boundary    : X={ratio_x:.1f}%, Y={ratio_y:.1f}%")

    finally:
        kernel32.CloseHandle(hproc)
        proc.terminate()
        try:
            proc.wait(timeout=3)
        except:
            proc.kill()

if __name__ == '__main__':
    inspect_file(r"testsaves\save1.cst")
    inspect_file(r"C:\Users\greattug\Documents\3ds Max 2026\test.csp")
