# CityScape Open Source (1.8 Decompiled Edition - Modernized)

[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen.svg)](#building-the-project)
[![Target](https://img.shields.io/badge/.NET%20Framework-4.8-blue.svg)](#requirements)
[![Graphics Backends](https://img.shields.io/badge/Graphics-DirectX%2012%20%7C%20Vulkan%201.3%20%7C%20DirectX%209-orange.svg)](#modern-3d-graphics-architecture)
[![Architectures](https://img.shields.io/badge/Architectures-x64%20%7C%20ARM64%20%7C%20x86-purple.svg)](#multi-architecture-targets)
[![License](https://img.shields.io/badge/License-MIT%20%2F%20Academic%20Research-yellow.svg)](LICENSE)
[![Reverse Engineered](https://img.shields.io/badge/Decompiled%20With-REA%20%26%20ILSpy-purple.svg)](docs/REVERSE_ENGINEERING_REPORT.md)

Open-source reconstruction, modernization, and preservation of **PixelActive CityScape 1.8** (Build 1.8.3.34904, Autodesk Promo Edition) — the legendary procedural 3D urban generation engine acquired by Autodesk in 2010.

> [!NOTE]
> **Academic & Hobby Project Notice**: This project is a non-commercial educational, hobby, and academic research preservation endeavor. It is designed for studying procedural geometry generation, urban synthesis algorithms, and real-time terrain rendering architectures.

---

## 🇹🇷 Türkçe Özet

Bu depo, PixelActive / Autodesk tarafından 2010 yılında geliştirilen **CityScape 1.8** kentsel modelleme ve 3B şehir simülasyonu yazılımının tersine mühendislik ile açık kaynak hale getirilmiş, modernize edilmiş ve derlenebilir sürümünü içerir:
- **REA (Reverse Engineer Anything)** ve **ILSpy** kullanılarak tüm .NET kütüphaneleri ve arayüz modülleri decompile edilip modernize edilmiştir.
- **Gerçek 3B Motor (`CityScape.exe` - 49.07 MB)**:
  - PixelActive'in orijinal tam 3B motorunu, Direct3D 9 donanımsal çizim hattını, prosedürel yol ağlarını, arazi LOD sistemini ve COLLADA entegrasyonunu içerir.
  - Decompile edilip .NET 4.8'e modernize edilen açık kaynak kütüphanelerimizle (`palDotNet.dll`, `edEditorDotNet.dll`, `ktKitDotNet.dll`, `gisImageryDotNet.dll`) %100 uyumlu olarak çalışır.
- **GIS & OpenStreetMap Gerçek Dünya İçe Aktarıcısı (Real-World OSM Ingestion)**:
  - Tek tıkla gerçek dünya şehirlerini (Kadıköy, Manhattan, Berlin veya özel koordinatlar) Overpass API üzerinden indirip anında 3B yollara, kavşaklara ve binalara dönüştürür.
  - Yüksek kaliteli çevrimdışı önbellek desteğiyle ağ bağlantısı olmasa bile anında milisaniyeler içinde kusursuz 3B kentsel geometri üretir.
  - Etkileşimli `GIS -> OpenStreetMap 3D City Importer` penceresi (Ctrl+I) veya CLI (`--import-osm=kadikoy --export=kadikoy.glb`).
- **Saf C# / .NET Bağımsız 3B Motoru (Option 1 - 100% Pure C# Standalone Architecture)**:
  - Eski 49 MB kapalı kaynak C++ D3D9 ikilisine (`CityScape.exe`) hiçbir bağımlılık duymadan çalışan; x64, ARM64 ve AnyCPU üzerinde tam bağımsız yerel C# 3B kentsel motoru (`CityScape_Managed.exe`, `CityScape_x64.exe`, `CityScape_ARM64.exe`).
  - CDLOD arazi motoru, çok iş parçacıklı yükseklik taraması, prosedürel spline yol/kavşak geometrisi, bina üretimi, Yale yıldız kubbesi, otonom trafik, göl/okyanus dalgaları, elektrik şebekesi, sokak mobilyaları, viyadük sütunları, PBR materyalleri, birinci şahıs yürüme kamerası, geri al/yinele ve dinamik hava durumu sistemlerinin tümünü %100 açık kaynak C# olarak icra eder.
- **Modern 3B Dışa Aktarıcılar (Modern 3D Exporters)**:
  - **Khronos glTF 2.0 / GLB Binary**: Blender, Unreal Engine 5, Unity ve Three.js için tam ikili BIN/JSON tamponları.
  - **Pixar USD / USDA**: Pixar Universal Scene Description ve NVIDIA Omniverse uyumluluğu.
  - **Wavefront OBJ + PBR MTL**: Geleneksel 3B yazılımlar için normal ve roughness/metallic destekli materyaller.
- **Saf C++20 Çekirdek & Evrensel Eklentiler (Universal C++20 Core, UE5 & Blender)**:
  - **Blender 4.x / 5 Eklentisi (`dist/CityScape_Blender_Addon.zip`)**: Blender 3D Viewport içine yerleşen, tek tıkla gerçek zamanlı prosedürel yol ve şehir üreten resmi eklenti paketi.
  - **Unreal Engine 5 Plugini (`dist/CityScape_UE5_Plugin.zip`)**: UE5 projelerine doğrudan sürüklenebilen `.uplugin`, `ACityScapeRoadActor` ve Nanite uyumlu C++ bileşenleri.
  - **Evrensel C++20 Motoru (`CityScapeCore.dll` / `.lib`)**: Bağımsız, sıfır GC yükü ve AVX2 hızında çalışan saf C++20 kentsel motoru (`src/palNativeCore/`).
- **Otomatik Taşınabilir Paketleme (`package.bat` / `tools/package.ps1`)**:
  - `dist/CityScape_1.8_Legacy_x86_Portable.zip` (Tam 49 MB D3D9 Motorlu Sürüm)
  - `dist/CityScape_1.8_Modern_x64_Portable.zip`
  - `dist/CityScape_1.8_Modern_ARM64_Portable.zip`
  - `dist/CityScape_1.8_Decompiled_Edition_Complete.zip`
  - `dist/CityScape_Blender_Addon.zip` (Blender 4.x/5 Eklenti Arşivi)
  - `dist/CityScape_UE5_Plugin.zip` (Unreal Engine 5 Eklenti Paketi)
  - SHA-256 kriptografik doğrulama defteri (`dist/SHA256SUMS.txt`).

---

## 🏛️ Architecture & Modules

```
c:\cityscape-open
├── CityScape.sln                 # Classic Visual Studio Solution
├── CityScape.slnx                # Modern Visual Studio / .NET 10 Solution
├── build.ps1 / build.bat         # Automated build engine (x86, x64, ARM64)
├── package.ps1 / package.bat     # Portable distribution packager & SHA-256 ledger
├── test.bat                      # 21-suite automated verification runner (100% Green)
├── src/
│   ├── CityScape/                # Application Entry Point & Modern Launchers (x86, x64, ARM64)
│   ├── palDotNet/                # PixelActive Core Library
│   │   └── palModernEngine/      # Modern 3D Engine (DX12, Vulkan, PBR, CDLOD, Exporters)
│   ├── edEditorDotNet/           # CityScape Editor Suite (Roads, Buildings, Lots, Weather, Terrain)
│   ├── ktKitDotNet/              # Architectural Kit & Template System
│   ├── gisImageryDotNet/         # Satellite & GIS Imagery Streamer
│   ├── BugReporter/              # Diagnostics & Crash Reporter
│   └── CityScape_exe/            # Native / C++/CLI Engine Core (3,295 decompiled C# types)
├── lib/                          # Shipped runtime libraries (DirectX 9, FreeImage, Collada, etc.)
├── assets/                       # Shipped shaders, sky/star catalogs, textures
├── Worlds/                       # Sample 3D worlds, terrains (.cst), heightmaps, props
├── dist/                         # Staged portable zip releases and SHA256SUMS.txt
├── docs/                         # Architecture, file formats, and reverse engineering report
└── tools/                        # Automation & verification tools (.csp, .cst, .parc inspector)
```

---

## ⚡ Quick Start & Building

### 1. Requirements
* Windows 10 / 11 (x64 or ARM64)
* .NET SDK & .NET Framework 4.8 Developer Pack
* Visual Studio 2022 / 2026 or MSBuild Tools
* Python 3.10+ (for utility tools and test runner)

### 2. Build All Targets with One Click
```cmd
build.bat
```
Or with PowerShell:
```powershell
.\build.ps1
```
All assemblies and multi-architecture targets (`CityScape_x64.exe`, `CityScape_ARM64.exe`, `CityScape_Managed.exe`, `CityScape.exe`) will compile with 0 errors and 0 warnings.

### 3. Package Portable Distribution Archives
```cmd
package.bat
```
Generates 4 self-contained standalone `.zip` distribution packages with SHA-256 hashes in `dist/`.

### 4. Run Full 31-Suite Verification
```cmd
test.bat
```
Validates staged distribution binaries, CLI flags, PE hardware targets, CSP world parsing, PARC asset extraction, FormApp runtime lifecycle, deep subsystem assertions, live GUI windows, portable zip distributions, massive unlocked terrain extents, multi-backend stress & memory leak benchmark, decompiled 3D geometry pipeline with 6 exporters, real-world GIS OpenStreetMap city ingestion (Kadıköy, Manhattan, Berlin), interactive road gizmo spline generation with lossless `.csp` project persistence, procedural lot parceling & building generation with 4 roof types, atmospheric sky system with Yale star catalog & solar ephemeris, multi-agent autonomous traffic simulation with PixelActive XML export, procedural street trees and seasonal foliage, parametric water bodies (oceans, lakes, rivers) with wave dynamics, roadside highway guardrails and lot perimeter fences, physics collision AABB/OBB volumes with PixelActive Section 22.2.3 CSV metadata export, high-voltage pylon powerline grids, roadside urban props/furniture, 3D tape measure & slope angle HUD, real-time DemRaster terrain sculpting brushes, declarative urban scripting engine, bridge piers & elevated viaduct columns, PBR material surface palettes, first-person pedestrian walk & vehicle drive camera modes, undo/redo command history stack, and dynamic weather with exponential height fog & rain precipitation.

---

## 🏢 Prosedürel Bina Üretici & Parselleme (Lots & Buildings - Bölüm 7 & 12)

* **Yol Kenarı Arsa Parselleme (`Ctrl+L` / `[🏠 Parselle & Doldur]` Butonu)**:
  - Çizilen yol şebekesi boyunca yol marjlarını otomatik olarak arsa parsellerine (Urban Lots) böler.
  - İmar çekme mesafelerini (setbacks) hesaplayarak yapılaşmaya uygun taban alanı poligonlarını çıkarır.
* **4 Farklı Çatı Mimarisi (Procedural Roofs)**:
  - 🏢 **Düz Çatı / Teras & Parapet (Flat Roof + HVAC)**: 0.8m parapet bordürü ve çatı üzeri mekanik klima ünitesi kutusu.
  - 🏠 **Beşik Çatı (Gable Roof)**: Tepe mahya çizgili eğimli çift kanat çatı ve üçgen alın duvarları.
  - 🏛️ **Kırma Çatı (Hip Roof)**: Dört yöne eğimli piramidal kırma çatı ve saçaklar.
  - 🏰 **Mansart Çatı (Mansard Roof)**: Klasik Fransız dik alt eğim (65°) ve üst düz seyir terası.
* **İnteraktif Viewport Bina Gizmosu (`Ctrl+B` / `[🏢 Bina Ekle]` Butonu)**:
  - Viewport üzerinde tıklayarak anında bina yerleştirin.
  - Seçili binanın tepesindeki camgöbeği oku yukarı/aşağı sürükleyerek kat sayısını ve yüksekliği anlık olarak değiştirin.
  - `R` tuşuyla çatıyı, `T` tuşuyla stili değiştirin. `Delete` ile binayı kaldırın.

---

## ☀️ Astronomik Gökyüzü, Yale Yıldız Kataloğu & Gece/Gündüz (Bölüm 16)

* **Yale Bright Star Catalog (9.110 Yıldız)**:
  - Harvard Revised kataloğundan $V \le 5.5$ görünür parlaklıktaki 2.887 yıldız J2000 gök koordinatlarında parse edilir.
  - Tayf türlerine göre (O/B mavi, A beyaz, G sarı, K/M kızıl) gece gökyüzünde parıldayan otantik yıldız kubbesi.
* **Güneş Efemerisi & Atmosfer Renkleri**:
  - Şehrin enlem/boylamına, yılın gününe ve saate göre güneşin gökyüzündeki yükseklik (elevation) ve azimut açılarını hesaplar.
  - Şafak (06:00), Öğle (12:00), Altın Saat / Gün Batımı (18:30), Alacakaranlık (20:30) ve Gece (00:00) tonları.
* **Gerçek Zamanlı Gün/Gece Döngüsü (`RoadToolbar` [⏱️ Döngü] Butonu)**:
  - 1x'ten 1800x'e kadar hızlandırılmış gün/gece simülasyonu.
  - Gece olduğunda binaların cephelerinde sıcak akkor ve floresan aydınlatmalı pencereler otomatik olarak yanar.

---

## 🚗 Otonom Trafik ve Araç Simülasyonu (Bölüm 15 & 22)

* **Çok Şeritli Şerit Grafı (Lane Network Graph)**:
  - Otoyollar, bulvarlar ve caddeler için sağdan akan trafik kuralına uygun şeritler ve yönlü koridorlar üretir.
* **Otonom Araç Ajanları (Autonomous Vehicle Agents)**:
  - Sedan, Sarı Taksi, SUV, Kırmızı Belediye Otobüsü, Kamyonet ve Spor Araç modelleri.
  - **IDM (Intelligent Driver Model)**: Öndeki araçla güvenli takip mesafesini korur, hızlanır ve yavaşlar.
* **Kavşak Sinyalizasyonu & Gece Farları**:
  - 4 fazlı otomatik trafik ışıkları (Yeşil 15s -> Sarı 3s -> Kırmızı 12s) ile kavşak geçiş kontrolü.
  - Gece olduğunda araçların farları otomatik açılır ve asfalt üzerine 3B ışık huzmeleri yansıtılır.
  - Fren lambaları yavaşlama anında parlak kırmızı LED olarak yanar.
* **PixelActive XML Trafik Formatı (`--export-traffic=traffic.xml`)**:
  - Tüm şeritleri, kavşakları ve araç verilerini CityScape Kullanım Kılavuzu Bölüm 22 standardındaki XML formatında kaydeder.

---

## 🌲 Bitki Örtüsü & Ağaç Araçları (Foliage Tools - Bölüm 13)

* **Yol Boyu Cadde Ağaçları (`[🌲 Ağaç]` Butonu / Menü `&Foliage` -> `Plant Trees along Roads`)**:
  - Şehirdeki tüm yollar ve bulvarlar boyunca kaldırım sınırına paralel, eşit aralıklı (15m) cadde ağaçları diker.
  - Yolun sağ ve sol kenarları için otomatik ofset koridorları ve arazi yükseklik uyumu.
* **6 Farklı Ağaç Türü (Procedural Tree Species)**:
  - 🌳 **Londra Çınarı (London Plane)**: Geniş gölgeli, yaprak döken klasik metropol ağacı.
  - 🍁 **Akçaağaç (Maple)**: Canlı kehribar ve turuncu sonbahar tonları.
  - 🌲 **Akdeniz Çamı (Mediterranean Pine)**: Şemsiye formunda yüksek gövdeli tepe tacı.
  - 🌲 **İtalyan Servisi (Italian Cypress)**: İnce, sütun şeklinde dik tepe ve zarif silüet.
  - 🌴 **Hurma Palmiyesi (Date Palm)**: Sahil bulvarları ve vaha peyzajı için tropikal tepe tacı.
  - 🌸 **Kiraz Çiçeği (Cherry Blossom / Sakura)**: İlkbaharda pembe çiçek açan estetik park ağacı.
* **Park Korulukları & Bahçe Yeşillendirme**:
  - **Park Grove**: Belirlenen merkez ve yarıçapta radyal kümelenmiş doğal orman/koruluk üretimi.
  - **Lot Gardens**: Parsellenen binaların ön ve arka bahçelerine otomatik peyzaj bitkilendirmesi.
* **4 Mevsim Renk Paleti (Dynamic Seasons)**:
  - `Yaz (Summer)` -> `Sonbahar (Autumn)` -> `Kış (Winter - Çıplak/Donmuş)` -> `İlkbahar (Spring - Sakura Çiçeklenmesi)`.
  - Gün/Gece döngüsüne bağlı olarak gece saatlerinde yaprak ve gövde tonları otomatik loşlaşır.

---

## 🌊 Su Kütleleri, Göller, Nehirler ve Sonsuz Okyanus (Water Bodies - Bölüm 14)

* **Sonsuz Okyanus Ufuk Düzlemi (Infinite Ocean Plane)**:
  - `[🌊 Su]` Butonu / Menü `&Water` -> `Create Ocean Plane`.
  - Şehrin kıyısında ufka kadar uzanan, derinlik gradyanlı deniz yüzeyi.
* **Organik Şehir Gölleri & Nehir Koridorları**:
  - **Urban Lake**: Doğal sinüzoidal kıvrımlı kıyı poligonlarına sahip park gölleri.
  - **River Corridor**: Spline düğümleri boyunca sağ ve sol kıyı şeritleri hesaplanan, genişliği ayarlanabilir nehir ve kentsel kanallar.
* **Dinamik Dalga Simülasyonu & 3B Mesh Üretimi**:
  - Gerçek zamanlı dalga fazı ilerlemesi (`simTimer` ile periyodik dalga animasyonu).
  - Güneş doğrultusuna göre su yüzeyinde dinamik speküler yansıma (specular highlights) ve kıyı köpük sınır çizgileri.
  - Dışa aktarma için otomatik su yüzeyi 3B mesh üçgenlemesi (glTF, USD, OBJ).

---

## 🛡️ Çitler & Otoyol Bariyerleri (Fences & Barriers - Bölüm 11)

* **Otoyol Çelik Çarpışma Bariyerleri (`[🛡️ Bariyer]` Butonu / Menü `&Barriers` -> `Highway Guardrails`)**:
  - Otoyollar ve viyadükler boyunca yol omzuna paralel çelik W-Kiriş (W-Beam) çift raylı koruma bariyerleri.
* **Beton Jersey Bariyerleri (Concrete Jersey Median Barriers)**:
  - Çok şeritli bölünmüş yollarda karşı şeride geçişi engelleyen eğimli beton bariyer blokları.
* **Şehir İçi Korkuluklar & Arsa Ferforje Çitleri**:
  - **Wrought Iron Fence**: Parsellenmiş binaların sınırlarında dekoratif döküm ferforje parmaklıklar.
  - **Pedestrian Railing**: Yaya kaldırımları ve nehir boyu rıhtımlarda güvenli antrasit korkuluklar.
  - **Timber Paling Fence**: Müstakil bahçeler için doğal ahşap çitler.
* **3B Derinlik Sıralı Viewport Çizimi**:
  - 3B dikmeler (posts), çift yatay raylar ve arazi eğimine tam uyumlu 3B poligon hatları.

---

## ⚡ Fizik Çarpışma Hacimleri & Soyut Meta Veri Araçları (Physics & Metadata - Bölüm 17, 18 & 22)

* **Bina ve Yol Fizik Çarpışma Hacimleri (AABB / OBB Volumes)**:
  - Tüm binalar ve yol segmentleri için otomatik eksenel sınır kutusu (AABB) ve yönlü hacimler (OBB) üretir.
  - Menü `&Physics` -> `Rebuild Physics Colliders` veya arayüzden anında tetikleme.
* **3B Işın Çarpışma Tespiti (Raycast Hit Detection)**:
  - Kameradan veya otonom araçlardan fırlatılan ışınların binalara ve yol gövdelerine çarpma noktasını, mesafesini ve yüzey normal vektörünü milisaniyeler içinde hesaplar.
* **Tip Korumalı Soyut Meta Veri Etiketleme (Abstract Meta Data)**:
  - Şehirdeki herhangi bir nesneye `String`, `Integer`, `Float`, `Vector2D`, `Vector3D` tipinde nitelikler ekleme:
    - İmar Kodu (`ZoningCode`: C-3 Commercial), Kat Sayısı (`FloorCount`: 8), Gayrimenkul Değeri (`EstimatedValueUSD`: $14.5M), GPS Koordinatları (`GPS_Coordinates`: 40.7128, -74.0060).
* **PixelActive CSV Meta Veri Dışa Aktarma (`--export-metadata=city_metadata.csv`)**:
  - CityScape Kullanım Kılavuzu Bölüm 22.2.3 standardına uygun `ObjectId,PropertyName,DataType,Value` formatında tam CSV dışa aktarımı.

---

## ⚡ Elektrik Direkleri & Katenary Kablo Şebekesi (Power Lines & Pylon Grid - Bölüm 13)

* **Yol Kenarı Ahşap & Beton Elektrik Direkleri (Utility Poles)**:
  - Yollar ve caddeler boyunca istenen aralıkta (varsayılan 40m) ve yol omzu ofsetinde otomatik direk yerleşimi.
  - 3B travers (crossarm) kolları, porselen izolatör montaj noktaları ve yönlü rotasyon desteği.
* **Yüksek Gerilim Çelik Kafes İletim Kuleleri (Steel Lattice Transmission Towers)**:
  - Şehir dışı arazilerden şehre enerji taşıyan 28m yüksekliğinde çift katmanlı 6 iletkenli çelik kafes kule güzergahları (`AddCustomRoute`).
* **Fiziksel Katenary Yerçekimi Sarkma Eğrisi (Physical Catenary Cable Sag Simulation)**:
  - İki pilon arasındaki iletken telleri yerçekimi sarkma formülüyle ($y(t) = y_{\text{linear}} - 4 \cdot \text{sag} \cdot t(1-t)$) mikrosaniyeler içinde gerçekçi parabolik sarkma ile hesaplar.
* **3B Viewport Çizimi & Kalıcı Proje Entegrasyonu**:
  - Direkler, traversler, izolatörler ve derinlik sıralı asılı teller 3B viewport'ta çizilir ve `.csp` / `.csp.json` projelerine kaydedilir.

---

## 🛋️ Kentsel Donatı Elemanları & Sokak Mobilyaları (Street Props & Furniture - Bölüm 10)

* **Otomatik Cadde Donatı Üretimi (Roadside Furniture Ingestion)**:
  - Yol kaldırımları boyunca akıllı algoritmayla kentsel elemanlar yerleştirir:
    - 💡 **Sokak Lambaları (StreetLights)**: Çift yönlü dengeli aydınlatma direkleri.
    - 🛑 **Trafik Levhaları (Traffic Signs)**: Kavşak öncesi DUR (Stop) ve hız sınırı (Speed Limit) levhaları.
    - 🚒 **Yangın Hidrantları (Fire Hydrants)**: Standart kentsel aralıklarla kırmızı yangın hidrantları.
    - 🪑 **Park Bankları (Park Benches)** & 🚌 **Otobüs Durakları (Bus Shelters)** & 🗑️ **Çöp Kutuları (Trash Bins)**.
* **Gece Zemin Aydınlatma Projeksiyonu (Nighttime Ground Light Cones)**:
  - Gece modunda veya alacakaranlıkta sokak lambaları kaldırım ve asfalt üzerinde 3B aydınlatma konisi ve yumuşak elips ışık lekesi oluşturur.

---

## 📏 3B Cetvel, Eğim & Alan Ölçüm Araçları (Measure Tools - Bölüm 14)

* **3B Doğrudan & Yatay Mesafe Ölçümü (3D Tape Measure)**:
  - Viewport üzerinde iki nokta seçerek 3B Öklid mesafesi ($d_{3D}$), yatay izdüşüm mesafesi ($d_h$) ve dikey kot farkını ($\Delta y$) milimetrik hassasiyetle ölçün.
* **Yol Eğimi & Derece Hesaplayıcı (Slope Grade % & Angle)**:
  - Rampaların ve yol segmentlerinin eğim yüzdesini ($\text{Grade } \% = \frac{\Delta y}{d_h} \times 100$) ve eğim açısını ($\theta^\circ$) anında hesaplar.
* **Yüzölçümü (Alan) & Çevre Ölçümü (Shoelace Polygon Area & Perimeter)**:
  - Seçilen noktalarla çevrelenen arsa, parsel veya göl alanını Shoelace (Gauss) formülüyle $m^2$ ve hektar ($ha$) cinsinden hesaplar.
* **İnteraktif 3B HUD Bilgi Rozeti (Interactive HUD Badge Overlay)**:
  - Canlı sarı kesikli ölçüm çizgileri, köşe işaretçileri ve modern yarı saydam karanlık HUD rozeti ile tüm ölçüm verilerini 3B sahne üzerinde gösterir.

---

## ⛰️ Coğrafya & Arazi Şekillendirme Fırçaları (Terrain Sculpting Brushes - Bölüm 5 & 9)

* **Gerçek Zamanlı 256x256 Yazılabilir Sayısal Yükseklik Modeli (DemRasterData Grid)**:
  - `TerrainSurfaceData.ActiveDemRaster` üzerinden doğrudan bellekte canlı arazi deformasyonu.
* **5 Çeşit Şekillendirme Fırçası**:
  - 🔺 **Yükselt (Raise)**: Arazi yüzeyini radyal pürüzsüz geçişle tepe ve dağlara dönüştürür.
  - 🔻 **Alçalt (Lower)**: Çukurlar, hendekler ve vadi yatakları oyar.
  - 🏖️ **Plato Düzleştir (Flatten Plateau)**: Belirtilen hedef kotta (Target Elevation) düz inşaat parselleri ve teraslar açar.
  - 🌊 **Yumuşat (Smooth)**: 3x3 kutu filtresi ile sarp eğimleri yumuşatır ve pürüzleri giderir.
  - 🎲 **Gürültü / Engebe (Noise)**: Doğal kayalık yüzeyler ve mikro kabartılar üretir.
* **Bilineer Yüzey Enterpolasyonu & Anında Sınır Güncellemesi**:
  - Şekillendirilen hücreler anında `RecalculateBounds()` ve bilineer enterpolasyon ile güncellenir; yollar ve binalar yeni kotları anında algılar.

---

## 📜 Kentsel Betik Motoru & Otomasyon Konsolu (Urban Scripting Engine - Bölüm 19)

* **Bildirime Dayalı Şehir Betik Dili (Declarative Text Scripting)**:
  - Tek bir metin dosyasıyla sıfırdan devasa şehirleri otomatik inşa edin:
    - `sculpt <mode> <x> <z> <radius> <strength>`: Araziyi fırçala.
    - `road <name> <width> <lanes> <x1,z1> <x2,z2> ...`: Yol şeridi döşe.
    - `building <name> <floors> <cx> <cz> <roofType>`: Bina dik.
    - `foliage <spacing> [species]`: Cadde ağaçlandırması yap.
    - `powerline [type] [spacing]`: Elektrik şebekesi kur.
    - `props [lightSpacing] [benchSpacing]`: Kentsel donatıları diz.
    - `water <ocean|lake> [elevation]`: Su yüzeyi oluştur.
    - `barrier <style> [offset]`: Oto korkuluk bariyerleri çek.
    - `sky <HH:MM>`: Gün saatini ve güneş açısını ayarla.
    - `clear`: Tüm sahneyi temizle.
* **Komut Satırı & Menü Desteği**:
  - `CityScape.exe --run-script=city_build.txt` ile GUI açılmadan veya arayüzdeki `&Scripting -> Run Script File...` menüsünden doğrudan çalıştırılabilir.

---

## 🌉 Köprü Ayakları & Viyadük Sütunları (Bridge Piers & Overpass Viaducts - Bölüm 6.6)

* **Otomatik Yükseltilmiş Yol Desteği (Elevated Road Clearance Detection)**:
  - Yol güvertesi kotu ile altındaki zemin kotu farkı $\ge 2.0\text{m}$ olduğunda viyadük ve köprü ayağı sistemini otomatik devreye sokar.
* **Parametrik Beton Ayak Tipleri & Başlık Kirişleri (Crosshead Caps)**:
  - **Single Pillar (Tekli Masif Kolon)**: Dar caddeler ve tek şeritli rampalar için merkezi betonarme gövde.
  - **Twin Pillar + Crosshead (Çiftli Kolon & Başlık Kirişi)**: Çok şeritli otoyollar ve viyadükler için çift kolonlu taşıyıcı travers başlık.
* **Derinlik Sıralı 3B Çizim & Kalıcı Proje Kaydı**:
  - Köprü ayakları ve viyadük başlıkları arazinin engebesine tam uyum sağlayarak zemine oturur, derinlik sıralı olarak 3B sahnede çizilir ve `.csp` / `.csp.json` formatında saklanır.

---

## 🎨 PBR Yüzey & Materyal Paleti (PBR Material Palette - Bölüm 9)

* **17 Standart Kentsel PBR Materyal Profili**:
  - 🛣️ **Yol & Kaplama**: `asphalt_fresh`, `asphalt_weathered`, `asphalt_highway`, `concrete_road`, `cobblestone_heritage`, `gravel_rural`.
  - 🚶 **Kaldırım**: `concrete_pavement`, `brick_paver_red`, `granite_slab`.
  - 🏢 **Bina Cephesi**: `facade_glass_tower`, `facade_concrete_panel`, `facade_brick_brownstone`, `facade_modern_composite`.
  - 🏠 **Çatı**: `roof_terracotta_tile`, `roof_metal_seam`, `roof_bitumen_flat`.
  - 🌉 **Altyapı**: `concrete_infrastructure`.
* **Fiziksel Özellikler & Metrik UV Ölçekleme**:
  - Her profil renk, pürüzlülük (roughness), metaliklik (metallic) ve metrik UV ölçekleme çarpanlarına sahiptir; glTF 2.0, USD ve OBJ materyallerine kayıpsız aktarılır.

---

## 🚶 Birinci Şahıs Yürüme & Sürüş Kamerası (First-Person Walk & Drive - Bölüm 2 & 3)

* **Yaya Göz Hizası Dolaşım (First-Person Walkthrough)**:
  - İnsan göz hizasında ($1.75\text{m}$) `W, A, S, D` klavye kontrolleri ve fare bakış açısı (pitch/yaw) ile şehir sokaklarında yürüme deneyimi.
  - Arazi rölyefini ve tepe eğimlerini anlık olarak takip eden araziye yapışma mekanizması (`EvaluateElevation`).
* **Araç Kokpiti & Takip Kamerası (Vehicle Drive Mode)**:
  - Trafikteki otonom araçlardan birini seçerek araç üstü kokpit veya 3. şahıs dinamik takip kamerasına geçiş.
* **Kamera Modları Arasında Anında Geçiş**:
  - Menü `&Camera` veya klavye kısayollarıyla `Orbit CAD`, `FirstPersonWalk` ve `FirstPersonDrive` modları arasında geçiş.

---

## ↩️ Geri Al / Yinele Komut Yığını (Undo / Redo History - Bölüm 2 & 4)

* **Tersine Çevrilebilir Komut Deseni (Reversible Command Pattern)**:
  - `AddRoadAction`, `AddBuildingAction`, `SculptTerrainAction` ve `CompositeBatchAction` komutları.
* **Sınırsız İşlem Geçmişi (Unlimited History Stack)**:
  - `Ctrl+Z` ile son eklenen yolları, binaları veya arazi değişikliklerini geri alma.
  - `Ctrl+Y` ile geri alınan işlemleri ileri sararak yeniden uygulama.

---

## 🌧️ Dinamik Hava Durumu, Sis & Yağmur Simülasyonu (Weather, Fog & Rain - Bölüm 16)

* **5 Atmosferik Hava Durumu Durumu (Weather State Machine)**:
  - ☀️ **ClearSunny**: Kristal netliğinde gökyüzü, yüksek görüş mesafesi.
  - ⛅ **Overcast**: Yumuşak bulutlu gökyüzü ve dengeli dağınık aydınlatma.
  - 🌫️ **MorningFog**: Yoğun sabah sisi, sınırlı görüş ve atmosferik gizem.
  - 🌦️ **LightRain**: Hafif çiseleyen yağmur, nemli asfalt parlaklığı.
  - ⛈️ **HeavyRain / Storm**: Fırtına, yoğun düşen yağmur çizgileri, rüzgar sürüklemesi ve ıslak yol yansımaları.
* **Beer-Lambert Üstel Derinlik & Kot Sisi (Exponential Height/Depth Fog)**:
  - $f = 1 - e^{-\text{density} \cdot (d - d_0)}$ formülüyle mesafeye ve irtifaya bağlı otantik sis yoğunluğu.
  - Ufuk çizgisi rengini Yale yıldız kataloğu ve güneş efemerisinden dinamik olarak devralır.
* **Düşen Yağmur Damlası Parçacıkları (Precipitation Streak Simulation)**:
  - 3B kamera çevresinde simüle edilen dinamik düşen yağmur çizgileri ve ıslak asfalt yansıma çarpanı.

---

## 🛣️ İnteraktif Yol Çizim Aracı & Viewport Gizmo (Interactive Road Tool)

CityScape 1.8 artık 3B Viewport üzerinde fareyle tıklanarak gerçek zamanlı yol ve kavşak çizmeyi tam olarak destekler:

* **Yol Çizimi (`Ctrl+R` / `[🛣️ Yol Çiz]` Butonu)**:
  - Viewport üzerinde zemin veya arazi yüzeyine sol tıklayarak yol düğümleri (knots) ekleyin.
  - Canlı kauçuk çizgi (rubberband line) ve anlık metraj göstergesi ile mesafeyi takip edin.
  - Çizimi bitirmek için `Enter` tuşuna basın veya çift tıklayın. İptal için `Esc`.
* **Düğüm & Teğet Düzenleme (`Ctrl+E` / `[📐 Düğüm Düzenle]` Butonu)**:
  - Herhangi bir düğüme tıkladığınızda 3B öteleme gizmosu (Kırmızı X ekseni, Yeşil Y yükseklik, Mavi Z ekseni) ve Catmull-Rom/Hermite eğrilik kolları açılır.
  - Düğümleri sürükleyerek yolu kıvırabilir veya teğet kollarını çekerek viraj açısını ayarlayabilirsiniz.
  - İstenmeyen düğümleri `Delete` tuşuyla veya `[🗑️ Sil]` butonuyla kaldırabilirsiniz.
* **Otomatik Çok Kollu Kavşak Çözücü (`Ctrl+J` / `[🔀 Kavşak Bağla]` Butonu)**:
  - Yakınlaşan veya kesişen yol şeritlerini otomatik kümeleyip pürüzsüz çokgen kavşak göbeği (intersection junction mesh) üretir.
* **Yol Enkesit Profilleri (Cross-Section Profiles)**:
  - Araç çubuğundaki profiller menüsünden dilediğiniz yol tipini seçebilirsiniz:
    - 🏎️ **Otoyol (Highway)**: 22m genişlik, 6 şerit, çift yönlü refüj.
    - 🏙️ **Ana Bulvar (Boulevard)**: 14m genişlik, 4 şerit, bordür ve kaldırımlar.
    - 🚗 **Şehir İçi Cadde (Street)**: 10m genişlik, 2 şerit, bordür ve kaldırımlar.
    - 🏡 **Mahalle Sokağı (Residential)**: 7m genişlik, 2 şerit.
    - 🚶 **Yaya & Bisiklet Yolu (Pedestrian)**: 4m genişlik.

---

## 💾 Proje Kaydetme ve Açma (.csp / .csp.json)

Tasarımlarınızı, arazi ayarlarınızı, yollarınızı, binalarınızı ve kamera açınızı kalıcı hale getirin:

* **Kaydetme (`Ctrl+S` / `[💾 Kaydet]` Butonu)**:
  - Dosyayı hem orijinal PixelActive ikili `.csp` başlığıyla (`"<PixelActive CityScape 1.8.3.34904 [Autodesk Promo]> project bin ver 1"` + 16-bayt GUID) hem de temiz `.csp.json` olarak kaydeder.
  - Mevcut `tools/csp_reader.py` aracıyla ve orijinal CityScape ikili denetleyicileriyle %100 uyumludur.
* **Açma (`Ctrl+O` / `[📂 Aç]` Butonu)**:
  - Kaydedilmiş `.csp` veya `.csp.json` projelerini yükler.
  - Araziyi, tüm binaları, yol düğümlerini, teğet eğrilerini, şerit profillerini ve kamera perspektifini kayıpsız olarak yeniden oluşturur.

---

## 🌍 Gerçek Şehir GIS & OpenStreetMap İçe Aktarma (OSM 3D Importer)

* **Menü & Kısayol (`Ctrl+I` / `[🌍 OSM Şehir]` Butonu)**:
  - Açılan diyalog penceresinden hazır şehir önayarlarını seçebilir veya özel enlem/boylam sınır kutusu (bounding box) girebilirsiniz.
  - **Kadıköy, İstanbul**: Moda, Rıhtım, Bahariye caddeleri, 4.200+ gerçek bina, 83 km yol ağı.
  - **Manhattan, New York**: Midtown, Times Square, Empire State, gökdelenler ve bulvarlar.
  - **Berlin Mitte, Almanya**: Brandenburg Kapısı, Unter den Linden, müzeler ve tarihi yapılar.
  - İçe aktarılan modeller anında 3B yollara, kaldırımlara, binalara ve su yüzeylerine dönüştürülüp glTF 2.0 (GLB), Pixar USD veya Wavefront OBJ olarak dışa aktarılabilir.

---

## 🎮 Çalıştırma & Kullanım (Execution & Usage)

```cmd
# 1. Ana ve Tam Fonksiyonel 3B Şehir Motoru (Direct3D 9, Yol & Bina Çizimi, Arazi)
CityScape.exe

# Gerçek bir şehri doğrudan OSM üzerinden indirip 3B'ye dönüştürmek ve glTF/USD/OBJ olarak dışa aktarmak için:
CityScape.exe --import-osm=kadikoy --export=kadikoy.glb
CityScape.exe --import-osm=manhattan --export=manhattan.usda
CityScape.exe --import-osm=berlin --export=berlin.obj

# Belirli bir dünyayı doğrudan açmak için:
CityScape.exe Worlds\sample.csp

# 2. Yönetilen Test / Hata Ayıklama Kabuğu (.NET Diagnostik & Layout İncelemesi)
CityScape_Managed.exe --diagnostics
CityScape_x64.exe --diagnostics
```

---

## 🛠️ Python Utilities & Tools

Inspect any CityScape binary file format directly:

```powershell
# Inspect project (.csp) or terrain (.cst) files
python tools/csp_reader.py Worlds/

# Inspect packed asset archive (.parc)
python tools/parc_tool.py assets/assets.parc
```

---

## 📖 Documentation
* [Architecture Guide](docs/ARCHITECTURE.md)
* [Binary File Formats (.csp, .cst, .parc)](docs/FILE_FORMATS.md)
* [Reverse Engineering & Decompilation Report](docs/REVERSE_ENGINEERING_REPORT.md)
* [Original QuickStart Guide (PDF)](docs/QuickStart.pdf)
* [Original User Manual (PDF)](docs/UserManual.pdf)

---

## 📄 License & Academic Hobby Disclaimer
This project is released under the [MIT License](LICENSE) strictly as an open-source educational, hobby, and academic research preservation effort. All original trademarks and product names belong to their respective owners (PixelActive Inc. / Autodesk Inc.).
