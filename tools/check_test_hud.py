with open("tests/test_suite.py", "r", encoding="utf-8") as f:
    txt = f.read()

for term in ["DrawHud", "RMB: Place", "Road Tool [", "hudBrush", "Telemetry HUD"]:
    if term in txt:
        print(f"Found '{term}' in test_suite.py")
    else:
        print(f"'{term}' NOT in test_suite.py")
