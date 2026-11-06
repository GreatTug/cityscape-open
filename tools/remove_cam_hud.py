path = r"src\palDotNet\palDotNet\palVisual\ControlRender.cs"
with open(path, "r", encoding="utf-8") as f:
    content = f.read()

target = """\t\t\t// Render Authentic Camera HUD Indicator (Chapter 2.2.2 & 3.2)
\t\t\tif (ActiveCamera.IsOrthographic)
\t\t\t{
\t\t\t\tusing (var font = new Font("Segoe UI", 9.0f, FontStyle.Bold))
\t\t\t\tusing (var brush = new SolidBrush(Color.FromArgb(220, 255, 235, 120)))
\t\t\t\t{
\t\t\t\t\tg.DrawString("\U0001F4F7 [ORTHOGRAPHIC TOP-DOWN CAMERA]  ALT+LMB: Pan | ALT+RMB/Wheel: Zoom | C: Perspective", font, brush, 10, 10);
\t\t\t\t}
\t\t\t}"""

if target in content:
    content = content.replace(target, "")
    with open(path, "w", encoding="utf-8") as f:
        f.write(content)
    print("Successfully removed Camera HUD from ControlRender.cs")
else:
    print("Target not found directly, checking CRLF vs LF")
    target_crlf = target.replace("\n", "\r\n")
    if target_crlf in content:
        content = content.replace(target_crlf, "")
        with open(path, "w", encoding="utf-8") as f:
            f.write(content)
        print("Successfully removed Camera HUD (CRLF) from ControlRender.cs")
    else:
        print("Still not found")
