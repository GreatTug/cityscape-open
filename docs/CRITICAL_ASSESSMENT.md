# PixelActive CityScape 1.8 — Eleştirel Mimari ve Mühendislik Raporu (Critical Assessment)

> **Belge Amacı**: Bu rapor, CityScape 1.8 Açık Kaynak ve Dijital Koruma Projesi'nin mevcut durumunu pembe tablolardan arındırarak, **tamamen tarafsız, teknik ve eleştirel bir gözle** değerlendirmektedir. Projeyi devralacak geliştiricilerin veya topluluğun karşılaşacağı yapısal engeller, potansiyel çökme senaryoları, güvenlik riskleri ve mimari borçlar aşağıda detaylandırılmıştır.

---

## 1. Mimari Gerçeklik: "Açık Kaynak mı, Yoksa Yamalı Bir İkili (Binary) mi?"

### Mevcut Durum Özeti
Proje bugün iki belirgin katmana ayrılmıştır:
1. **Yönetilen C# / .NET 4.8 Katmanı (%100 Açık Kaynak)**:
   - `palDotNet`, `edEditorDotNet`, `ktKitDotNet`, `gisImageryDotNet` ve `BugReporter`.
   - Tüm GUI, docking pencereleri, parametre denetleyicileri, araç çubukları ve layout motoru C# kaynak kodundan MSBuild ile sıfırdan derlenmektedir.
2. **Yerel Çekirdek Motoru (49 MB Monolitik x86 PE32 Binary - Kapalı Kaynak)**:
   - `CityScape.exe` orijinal bir C++/CLI karma derlemesidir (mixed-mode assembly).
   - Render döngüsü (DirectX 9), fizik motoru (NVIDIA PhysX 2.8.3), yol oluşturucu, arazi tesselasyonu, spline matematiği ve dosya I/O rutinleri bu 49 MB'lık ikili içinde yaşamaktadır.

### Eleştiri ve Açıklar
- **Proje Henüz %100 C++ Kaynak Kodlu Değildir**:
  Motorun kendisi C++ derleyicisi ile yeniden derlenmemektedir. Yapılan işlem, 2010 tarihli özgün ikili dosyanın bayt düzeyinde cerrahi olarak yamalanmasıdır (`tools/apply_native_patch.py`). 
- **Sökülen 22 Algoritma Henüz Entegre Edilmemiştir**:
  Tersine mühendislikle Ghidra üzerinden decompile edilen 22 kritik C++ algoritması (`Hermite Splines`, `Catmull-Rom`, `Terrain Subdivider`, `Quadtree LOD`, `Fresnel Refraction`, `Ray-AABB` vb.) şu anda `tools/reverse_engineering/decompiled_math_engine_complete.txt` dosyasında birer referans metin olarak durmaktadır. Bu algoritmalar henüz bağımsız bir `CityScapeNative.dll` haline getirilip C# katmanına P/Invoke ile bağlanmamış veya motora enjekte edilmemiştir.

---

## 2. Taşınabilirlik ve Farklı Bilgisayarlarda Karşılaşılabilecek Riskler

### Risk 1: Vulkan 1.3 / DXVK Bağımlılığı ve Grafik Çökmesi
- **Sorun**: Varsayılan olarak projeye entegre edilen `d3d9.dll` (DXVK 2.3+), modern sistemlerde inanılmaz bir FPS artışı sağlasa da **Khronos Vulkan 1.3** donanım desteği gerektirir.
- **Patlama Senaryosu**:
  - Intel UHD 620 öncesi eski entegre GPU'lar (Haswell, Sandy Bridge),
  - Eski AMD/NVIDIA grafik kartları,
  - Sanal makineler (VMware, VirtualBox, Proxmox, Hyper-V),
  - Windows RDP (Uzak Masaüstü) oturumları.
  Bu ortamlarda CityScape başlatıldığında DXVK Vulkan başlatamadığı için uygulama sessizce çöker (Silent Crash) veya `dxvk: Failed to initialize Vulkan` hatası verir.
- **Çözüm**: Pakete eklenen `switch_graphics.bat` ile anında `directx` (dgVoodoo2 - DirectX 11/12) veya `native` (Windows D3D9) moduna geçilmelidir. Ancak son kullanıcının bunu kendisinin yapması gerekmektedir; otomatik donanım algılamalı bir C# bootstrap başlatıcısı henüz yazılmamıştır.

### Risk 2: Antivirüs ve Windows Defender Heuristik Yanılgıları (False Positives)
- **Sorun**:
  1. `CityScape.exe` PE başlıkları (LAA bayrağı, opcode atlamaları) disk üzerinde değiştirilmektedir.
  2. Uygulama klasörüne doğrudan `d3d9.dll` yerleştirilmiştir (Klasik DLL Search-Order Hijacking modeli).
  3. İkili dosyalarda geçerli bir Authenticode dijital imzası bulunmamaktadır.
- **Patlama Senaryosu**: Bazı kurumsal sistemlerde veya Windows Defender'ın "Bulut Tabanlı Koruma" modunda, bu dosya kombinasyonu `Trojan:Win32/Wacatac.B!ml` veya `Heur.BPE` (Bypass/Packing Exploit) heuristik tespitiyle anında karantinaya alınabilir.
- **Çözüm**: Dağıtım paketlerine SHA-256 doğrulama belgeleri eklenmeli ve gelecekte disk üzeri PE yaması yerine bellek içi hooking (MinHook tabanlı temiz bir DLL enjektörü) mimarisine geçilmelidir.

### Risk 3: 32-Bit (x86) Sanal Adres Alanı Tavanı (Out of Memory - OOM)
- **Sorun**: `CityScape.exe` mimari olarak bir 32-bit (x86) Windows uygulamasıdır. `IMAGE_FILE_LARGE_ADDRESS_AWARE` (LAA) yaması ile adres alanı 2GB'tan 4GB'a çıkarılmış olsa da:
  - 32-bit Windows kullanıcı alanı heap parçalanmasına (memory fragmentation) tabidir.
  - Uygulama pratikte **~3.2 GB - 3.5 GB RAM** kullanımına ulaştığında sanal bellek tükenecek ve `E_OUTOFMEMORY` / `std::bad_alloc` ile aniden çökecektir.
- **Patlama Senaryosu**: 1600m limiti 10.000.000m'ye çıkarılmış olsa bile; 10 km x 10 km devasa bir araziye yüz binlerce `.gob` ağaç modeli ve yüksek çözünürlüklü GIS ortofotoları yerleştirildiğinde bellek tavanına çarpacaktır.

### Risk 4: VC++ 2008 SxS Fusion Davranışı
- **Sorun**: `lib/Microsoft.VC90.CRT/` altına `msvcr90.dll`, `msvcp90.dll` ve manifest konulmuştur. Ancak Windows Side-by-Side (WinSxS) mimarisi bazı kısıtlı kurumsal Windows politikalarında yerel dizindeki SxS manifestlerini yoksayabilir.

---

## 3. Veri Biçimleri, Şifreleme ve Kilitlenme (Vendor Lock-in)

### Sorun: 72-Byte Magic Header ve OpenSSL RSA/AES Koruması
- `.csp` (CityScape Project) ve `.cst` (CityScape Template) dosyaları salt metin veya standart JSON değildir.
- Orijinal motor, sahneleri kaydederken eski OpenSSL 0.9.8 kütüphanesini kullanarak tescilli bir 72-byte başlık ve 256-byte RSA anahtar bloğu ile AES-256 şifrelemesi uygular.
- **Risk**: Python tarafında geliştirdiğimiz `tools/csp_reader.py` bu dosyaları başarıyla okuyup doğrulayabilmektedir. Ancak motorun kendi yerel C++ kayıt mekanizması hala eski şifreli yapıyı kullanmaktadır. Kullanıcı CityScape içinde bir sahne oluşturduğunda, bu sahneyi doğrudan Blender veya Unreal Engine gibi açık kaynak araçlara aktarmak için Python dönüştürücü köprüsünü kullanmak zorundadır.

---

## 4. Yasal Durum ve Fikri Mülkiyet (IP Reality Check)

- PixelActive Inc. 2010 yılında Autodesk tarafından satın alınmıştır.
- Motorun içinde Autodesk telif hakkı dizgileri, tanıtım materyalleri ve tescilli varlıklar (`.gob`, `.fxo`) bulunmaktadır.
- **Risk Analizi**: Bu projenin GitHub'da sıradan bir "Açık Kaynak Yazılım" gibi sunulması, Autodesk Hukuk Departmanı tarafından telif hakkı veya ticari marka şikayetine (DMCA Takedown) yol açabilir.
- **Stratejik Konumlandırma**:
  - Proje kesinlikle ticari olmayan bir **"Historical Software Preservation & Interoperability Research" (Tarihsel Yazılım Koruma ve Birlikte Çalışabilirlik Araştırması)** çerçevesinde sunulmalıdır.
  - Açık kaynak olan kısımlar (C# kodları, tersine mühendislik araçları, dokümantasyon) ile mülkiyete tabi olan ikili varlıklar açıkça ayrıştırılmalıdır.

---

## 5. Mühendislik Çözüm Yol Haritası (Remediation Roadmap)

Bu eleştirilerin ışığında, projenin gerçek anlamda sürdürülebilir bir açık kaynak simülasyon motoruna dönüşmesi için gereken 4 öncelikli teknik aşama:

```
[Aşama 1: Stabilite] ──> [Aşama 2: Detours Hooking] ──> [Aşama 3: C++20 Core] ──> [Aşama 4: glTF / 64-Bit]
(Tamamlandı)             (Planlanan)                    (Gelecek)                 (Vizyon)
```

1. **Aşama 1 (Tamamlandı)**:
   - C# GUI katmanının %100 açık kaynak derlenebilirliği sağlandı.
   - SxS CRT ve grafik seçici mekanizmaları (`switch_graphics.bat`, `Run_CityScape.bat`) eklendi.
2. **Aşama 2 (Öncelikli Sıradaki Adım - In-Memory Shim DLL)**:
   - `apply_native_patch.py` yerine, bellek içi MinHook veya Microsoft Detours kullanan `CityScapeShim.dll` yazılmalıdır.
   - Böylece `CityScape.exe` disk üzerinde hiç değiştirilmez, antivirüs false positive oranı sıfırlanır.
3. **Aşama 3 (C++20 Temiz Çekirdek Derlemesi)**:
   - `decompiled_math_engine_complete.txt` içindeki 22 algoritma modern C++20 standardında bir CMake projesine dönüştürülmeli (`src/CityScapeNativeCore/`).
   - C# katmanından doğrudan bu açık kaynak DLL çağrılmalıdır.
4. **Aşama 4 (Modern Standartlar & 64-Bit Gelecek)**:
   - Şifreli `.csp` yerine açık `glTF 2.0` ve `GeoJSON` veri aktarımı.
   - Motorun 64-bit (x64) mimarisine taşınarak 4GB bellek sınırının tamamen kaldırılması.
