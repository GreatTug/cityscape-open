import os
from collections import defaultdict

def count_lines(filepath):
    try:
        with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
            lines = f.readlines()
            total = len(lines)
            blank = sum(1 for l in lines if not l.strip())
            comment = sum(1 for l in lines if l.strip().startswith('//') or l.strip().startswith('/*') or l.strip().startswith('*'))
            code = total - blank - comment
            return total, code
    except Exception:
        return 0, 0

def analyze():
    # Directories
    csharp_dirs = [
        'src/palDotNet',
        'src/edEditorDotNet',
        'src/ktKitDotNet',
        'src/gisImageryDotNet',
        'src/CityScape'
    ]
    cpp_dirs = [
        'src/palNativeCore'
    ]

    cs_stats = defaultdict(lambda: {'files': 0, 'total': 0, 'code': 0})
    cpp_stats = defaultdict(lambda: {'files': 0, 'total': 0, 'code': 0})

    for d in csharp_dirs:
        for root, dirs, files in os.walk(d):
            for file in files:
                if file.endswith('.cs'):
                    p = os.path.join(root, file)
                    t, c = count_lines(p)
                    # top category
                    rel = os.path.relpath(p, 'src')
                    top = rel.split(os.sep)[0]
                    cs_stats[top]['files'] += 1
                    cs_stats[top]['total'] += t
                    cs_stats[top]['code'] += c

    for d in cpp_dirs:
        for root, dirs, files in os.walk(d):
            for file in files:
                if file.endswith(('.cpp', '.hpp', '.h', '.cxx')):
                    p = os.path.join(root, file)
                    t, c = count_lines(p)
                    rel = os.path.relpath(p, 'src/palNativeCore')
                    sub = rel.split(os.sep)[0]
                    cpp_stats[sub]['files'] += 1
                    cpp_stats[sub]['total'] += t
                    cpp_stats[sub]['code'] += c

    total_cs_code = sum(v['code'] for v in cs_stats.values())
    total_cs_lines = sum(v['total'] for v in cs_stats.values())
    total_cs_files = sum(v['files'] for v in cs_stats.values())

    total_cpp_code = sum(v['code'] for v in cpp_stats.values())
    total_cpp_lines = sum(v['total'] for v in cpp_stats.values())
    total_cpp_files = sum(v['files'] for v in cpp_stats.values())

    print("=== C# CODEBASE STATS ===")
    for k, v in sorted(cs_stats.items()):
        print(f"  {k:20s}: {v['files']:3d} files, {v['total']:6d} total lines, {v['code']:6d} code lines")
    print(f"Total C#: {total_cs_files} files, {total_cs_lines} total lines, {total_cs_code} code lines\n")

    print("=== C++ NATIVE CORE STATS ===")
    for k, v in sorted(cpp_stats.items()):
        print(f"  {k:20s}: {v['files']:3d} files, {v['total']:6d} total lines, {v['code']:6d} code lines")
    print(f"Total C++: {total_cpp_files} files, {total_cpp_lines} total lines, {total_cpp_code} code lines\n")

    grand_total_code = total_cs_code + total_cpp_code
    cpp_pct = (total_cpp_code / grand_total_code) * 100
    cs_remaining_pct = (total_cs_code / grand_total_code) * 100

    print(f"Grand Total Code Lines: {grand_total_code}")
    print(f"C++ Share: {total_cpp_code} lines ({cpp_pct:.2f}%)")
    print(f"C# Share (C++'a Çevrilmemiş): {total_cs_code} lines ({cs_remaining_pct:.2f}%)")

if __name__ == '__main__':
    analyze()
