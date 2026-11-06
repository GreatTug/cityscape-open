# 🙏 Acknowledgements & Heartfelt Gratitude
### *Teşekkürler ve Saygı Bildirimi*

> **"If I have seen further, it is by standing on the shoulders of giants."**  
> — Sir Isaac Newton

This preservation and reverse-engineering project would not exist without the pioneering brilliance, decades of research, and extraordinary contributions of the engineers, scientists, and open-source creators acknowledged below.

---

## 🌟 The Original Creators of CityScape (PixelActive Inc.)

Our deepest admiration, respect, and gratitude go to the original founders and engineering team of **PixelActive Inc.** (Carlsbad, California, 2003–2010):

### 🎓 Steve Rotenberg
* **Co-Founder & Chief Scientist, PixelActive Inc.**  
* *Professor & Director of Computer Graphics Laboratory at the University of California, San Diego (UCSD).*  
Steve Rotenberg's pioneering contributions to procedural algorithms, vehicle dynamics, inverse kinematics, and real-time computational geometry provided the intellectual bedrock for CityScape. His mathematical elegance shines throughout the disassembled codebase—from the radial angular sorting of multi-arm intersections (`rdIntersection`) to the seamless Catmull-Rom tangent evaluations and CDLOD elevation systems.

### 💼 Dan Kagan
* **Co-Founder & Chief Executive Officer, PixelActive Inc.**  
Dan Kagan's visionary leadership recognized early on that urban environments for simulation, entertainment, and GIS should not be painstakingly placed by hand, but synthesized procedurally with real-time feedback. His vision catalyzed the entire procedural urban generation industry.

### 💻 The PixelActive Engineering & Graphics Team (2003–2010)
To all the software architects, 3D artists, physics programmers, and UI designers who built CityScape 1.8:
- You built a unified 52-byte vertex buffer pipeline (`0x34` stride) that was decades ahead of its time.
- You solved complex 4-slope parametric roof generation (Mansard, Hip, Gable, Flat) in real time when multi-core CPUs were just emerging.
- You created an intuitive, fluid docking workspace that remains a masterclass in desktop simulation UI design.
- **Thank you for creating an enduring masterpiece of interactive computer graphics.**

---

## 🏢 Autodesk, Inc.

We express our sincere appreciation to **Autodesk, Inc.** for:
- Acquiring PixelActive in November 2010 and integrating its procedural modeling paradigms into broader architectural and civil engineering platforms, including **Autodesk InfraWorks** and **3ds Max**.
- Continuing to push the boundaries of 3D computer graphics, architectural visualization, and digital city simulation worldwide for over four decades.
- Inspiring generations of computer graphics artists and engineers around the globe.

---

## 🛠️ The Open-Source & Emulation Community

This preservation edition would not run on modern 64-bit operating systems and modern graphics cards without the tireless work of open-source developers worldwide:

1. **Philip Rebohle & The DXVK Team**:
   - For developing **DXVK**, the world-class Vulkan-based Direct3D 9, 10, and 11 translation layer. Your work breathes new life into historical Direct3D 9 engines on modern NVIDIA RTX, AMD Radeon, and Intel Arc GPUs.
2. **Dege**:
   - For authoring **dgVoodoo2**, the indispensable DirectX 11/12 wrapper ensuring seamless compatibility on older hardware and virtualized environments.
3. **The ICSharpCode & ILSpy Team**:
   - For creating **ILSpy**, the preeminent open-source .NET decompiler that made the complete reconstruction and modernization of the managed C# layer (`palDotNet`, `edEditorDotNet`, `ktKitDotNet`, `gisImageryDotNet`, `BugReporter`) possible.
4. **National Security Agency (NSA) Research Directorate**:
   - For releasing **Ghidra**, the Software Reverse Engineering (SRE) framework used to audit, disassemble, and document the 22 core native mathematical routines.
5. **Weifen Luo & DockPanel Suite Contributors**:
   - For the robust docking window library that powers CityScape's authentic desktop workstation layout.
6. **FreeImage & DevIL Projects**:
   - For the open-source image loading libraries enabling texture manipulation across multiple legacy formats.

---

## 🏛️ Digital Preservation & Software Archaeology Initiatives

We draw continuous inspiration from organizations fighting against digital obsolescence:
- **The Internet Archive** (Brewster Kahle and team) — for defending the principle that digital history belongs to humanity.
- **The Software Preservation Network (SPN)** — for establishing best practices in software archival and legal fair use.
- **The Video Game History Foundation (VGHF)** — for pioneering efforts in preserving interactive digital media.

---

*Thank you to everyone who believes that code, like art and literature, should never be forgotten.*
