import sys
import pkg_resources

print("Python:", sys.executable)
installed = sorted([f"{p.key}=={p.version}" for p in pkg_resources.working_set])
print(f"Installed packages ({len(installed)}):")
for pkg in installed:
    print(" ", pkg)
