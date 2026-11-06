import os
import re

inc_dir = "src/palNativeCore/include"
src_dir = "src/palNativeCore/src"

headers = [f for f in os.listdir(inc_dir) if f.endswith(".hpp") or f.endswith(".h")]
cpps = [f for f in os.listdir(src_dir) if f.endswith(".cpp")]

print(f"Native C++ Core Subsystems ({len(headers)} Headers, {len(cpps)} Implementations):")
print("="*70)

for h in sorted(headers):
    h_path = os.path.join(inc_dir, h)
    cpp_name = os.path.splitext(h)[0] + ".cpp"
    cpp_path = os.path.join(src_dir, cpp_name)
    has_cpp = os.path.exists(cpp_path)
    
    with open(h_path, "r", encoding="utf-8", errors="ignore") as f:
        h_content = f.read()
    
    classes = re.findall(r"class\s+([A-Za-z0-9_]+)", h_content)
    structs = re.findall(r"struct\s+([A-Za-z0-9_]+)", h_content)
    
    cpp_funcs = 0
    if has_cpp:
        with open(cpp_path, "r", encoding="utf-8", errors="ignore") as f:
            cpp_content = f.read()
        cpp_funcs = len(re.findall(r"\n[A-Za-z0-9_:<>\*&\s]+\s+[A-Za-z0-9_]+::[A-Za-z0-9_]+\s*\(", cpp_content))
        
    print(f"Header: {h:<30} | Impl: {cpp_name:<30} | Classes: {len(classes)} | Structs: {len(structs)} | Methods Implemented: {cpp_funcs}")
