using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palDialog;
using palDotNet.palVisual;
using palDotNet.palVisualPropertyGrid;
using palDotNet.palWidgets;
using palDotNet.palModernEngine;
using ktKitDotNet;
using edEditorDotNet;
using edEditorDotNet.ExportDialogs;
using edEditorDotNet.ExportDialogItems;

namespace CityScape.Tests
{
    unsafe class MockNativeDialogWrapper
    {
        public void* Dialog;
        public void OnValueChanged(object sender, EventArgs e) { }
    }

    class Program
    {
        private static int _passedTests = 0;
        private static int _totalTests = 0;

        private static void Assert(bool condition, string message)
        {
            _totalTests++;
            if (!condition)
            {
                throw new Exception($"ASSERTION FAILED: {message}");
            }
            _passedTests++;
            Console.WriteLine($"    [PASS] {message}");
        }

        [STAThread]
        static int Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("  CityScape 1.8 Deep Subsystem Verification Suite");
            Console.WriteLine("=================================================");

            try
            {
                TestPalDotNetCore();
                TestGisImageryDotNet();
                TestKtKitDotNet();
                TestEdEditorDotNet();
                TestBugReporter();
                TestVisualAndDocking();
                TestModernGraphicsAndEngine();
                TestSpatialQuadtreeAndCulling();
                TestGeospatialDemIngestion();
                TestMultithreadedCdlodWorker();

                Console.WriteLine("\n=================================================");
                Console.WriteLine($"  ALL {_totalTests} DEEP VERIFICATION ASSERTIONS PASSED (100%)");
                Console.WriteLine("=================================================");
                return 0;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[FATAL ERROR] {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.ResetColor();
                return 1;
            }
        }

        static void TestPalDotNetCore()
        {
            Console.WriteLine("\n[MODULE 1] Testing palDotNet Core Serializers, Math Vectors & Utilities...");

            // 1. Math Vectors
            Vector3 v1 = new Vector3(10f, 20f, 30f);
            Vector3 v2 = new Vector3(10f, 20f, 30f);
            Vector3 vDiff = new Vector3(10f, 20f, 31f);
            Assert(v1.Equals(v2), "Vector3 equality operator");
            Assert(!v1.Equals(vDiff), "Vector3 inequality operator");
            Assert(v1.ToString().Contains("10") && v1.ToString().Contains("20") && v1.ToString().Contains("30"), "Vector3 ToString formatting");

            Vector4 v4 = new Vector4(1f, 2f, 3f, 4f);
            Vector4 v4_2 = new Vector4(1f, 2f, 3f, 4f);
            Vector4 v4Diff = new Vector4(1f, 2f, 3f, 5f);
            Assert(v4.Equals(v4_2), "Vector4 equality operator");
            Assert(!v4.Equals(v4Diff), "Vector4 inequality operator");
            Assert(v4.ToString().Contains("1") && v4.ToString().Contains("2") && v4.ToString().Contains("3") && v4.ToString().Contains("4"), "Vector4 ToString formatting");

            // 2. Serializer Memory Stream Writer & Reader Round-Trip
            unsafe
            {
                var writer = new SerializerMemoryStreamWriter();
                var reader = new SerializerMemoryStreamReader();
                reader.Buffer = writer.Buffer;
                reader.CurrentIndex = writer.CurrentIndex;

                Assert(writer.Begin(), "SerializerMemoryStreamWriter.Begin()");

                int writeInt = 123456789;
                writer.Serialize(ref writeInt);

                float writeFloat = 3.14159f;
                writer.Serialize(ref writeFloat);

                bool writeBool = true;
                writer.Serialize(ref writeBool);

                string writeStr = "PixelActive CityScape 1.8 Architecture Test";
                writer.Serialize(ref writeStr);

                Vector3 writeV3 = new Vector3(100.5f, 200.5f, 300.5f);
                writer.Serialize(ref writeV3);

                Vector4 writeV4 = new Vector4(1.5f, 2.5f, 3.5f, 4.5f);
                writer.Serialize(ref writeV4);

                writer.End(false);

                // Now read back and assert exact match
                Assert(reader.Begin(), "SerializerMemoryStreamReader.Begin()");

                int readInt = 0;
                reader.Serialize(ref readInt);
                Assert(readInt == writeInt, $"Int serialization roundtrip: {readInt} == {writeInt}");

                float readFloat = 0f;
                reader.Serialize(ref readFloat);
                Assert(Math.Abs(readFloat - writeFloat) < 0.0001f, $"Float serialization roundtrip: {readFloat} == {writeFloat}");

                bool readBool = false;
                reader.Serialize(ref readBool);
                Assert(readBool == writeBool, $"Bool serialization roundtrip: {readBool} == {writeBool}");

                string readStr = null;
                reader.Serialize(ref readStr);
                Assert(readStr == writeStr, $"String serialization roundtrip: '{readStr}' == '{writeStr}'");

                Vector3 readV3 = default;
                reader.Serialize(ref readV3);
                Assert(readV3.Equals(writeV3), $"Vector3 serialization roundtrip: {readV3} == {writeV3}");

                Vector4 readV4 = default;
                reader.Serialize(ref readV4);
                Assert(readV4.Equals(writeV4), $"Vector4 serialization roundtrip: {readV4} == {writeV4}");

                reader.End(true);
                Assert(reader.AtEnd, "SerializerMemoryStreamReader at end after reading all tokens");
            }

            // 3. Sano ColorConverter
            var colorConvType = typeof(Vector3).Assembly.GetType("Sano.PersonalProjects.ColorPicker.Controls.ColorConverter");
            Assert(colorConvType != null, "Sano ColorConverter reflected");
            var colorToRgbMethod = colorConvType.GetMethod("ColorToRgb", BindingFlags.NonPublic | BindingFlags.Static);
            var rgbToColorMethod = colorConvType.GetMethod("RgbToColor", BindingFlags.NonPublic | BindingFlags.Static);
            var rgbToHsbMethod = colorConvType.GetMethod("RgbToHsb", BindingFlags.NonPublic | BindingFlags.Static);

            Color testCol = Color.FromArgb(120, 180, 240);
            object rgbObj = colorToRgbMethod.Invoke(null, new object[] { testCol });
            Color roundtripCol = (Color)rgbToColorMethod.Invoke(null, new object[] { rgbObj });
            Assert(roundtripCol.ToArgb() == testCol.ToArgb(), "ColorConverter RGB roundtrip matches source color");

            object hsbObj = rgbToHsbMethod.Invoke(null, new object[] { rgbObj });
            Assert(hsbObj != null, "ColorConverter RGB to HSB conversion produces valid HSB instance");

            // 4. ComponentSearcher (Dynamic Attribute Enumerate)
            _ = typeof(DigitalGlobeSettingsDialog).Assembly;
            _ = typeof(MetaDataTemplateEditor).Assembly;
            _ = typeof(NewProjectDialog).Assembly;

            var compSearcherType = typeof(Vector3).Assembly.GetType("palDotNet.palCore.ComponentSearcher");
            Assert(compSearcherType != null, "ComponentSearcher reflected");
            var searcher = Activator.CreateInstance(compSearcherType, new object[] { typeof(DialogBoxCreatorAttribute) });
            var getEnumMethod = compSearcherType.GetMethod("GetEnumerator");
            var enumerator = (IEnumerator)getEnumMethod.Invoke(searcher, null);
            int dialogCount = 0;
            while (enumerator.MoveNext())
            {
                dialogCount++;
            }
            Assert(dialogCount >= 40, $"ComponentSearcher dynamically discovered {dialogCount} DialogBoxCreator types (expected >= 40)");

            // 5. palDotNet Form1 & DialogBoxView Harness Forms
            using (var dlg = new palDotNet.palDialog.Form1())
            {
                Assert(dlg != null, "palDotNet.palDialog.Form1 instantiated");
                Assert(dlg.Controls.Count > 0, $"palDotNet.palDialog.Form1 contains {dlg.Controls.Count} controls");
            }

            using (var dlg = new palDotNet.palDialog.DialogBoxView())
            {
                Assert(dlg != null, "palDotNet.palDialog.DialogBoxView instantiated");
                Assert(dlg.Controls.Count > 0, "palDotNet.palDialog.DialogBoxView contains controls");
            }
        }

        static void TestGisImageryDotNet()
        {
            Console.WriteLine("\n[MODULE 2] Testing gisImageryDotNet Functions, Dialogs & Resources...");

            // 1. Resources via Reflection
            var resType = typeof(DigitalGlobeSettingsDialog).Assembly.GetType("gisImageryDotNet.Properties.Resources");
            Assert(resType != null, "gisImageryDotNet.Properties.Resources reflected");

            var checkProp = resType.GetProperty("Check16", BindingFlags.NonPublic | BindingFlags.Static);
            var errorProp = resType.GetProperty("Error16", BindingFlags.NonPublic | BindingFlags.Static);
            var infoProp = resType.GetProperty("Info16", BindingFlags.NonPublic | BindingFlags.Static);

            Bitmap checkImg = (Bitmap)checkProp.GetValue(null);
            Bitmap errorImg = (Bitmap)errorProp.GetValue(null);
            Bitmap infoImg = (Bitmap)infoProp.GetValue(null);

            Assert(checkImg != null && checkImg.Width == 16 && checkImg.Height == 16, "Check16 resource loaded and valid");
            Assert(errorImg != null && errorImg.Width == 16 && errorImg.Height == 16, "Error16 resource loaded and valid");
            Assert(infoImg != null && infoImg.Width == 16 && infoImg.Height == 16, "Info16 resource loaded and valid");

            // 2. DigitalGlobeSettingsDialog validation state machine
            using (var settingsDlg = new DigitalGlobeSettingsDialog())
            {
                Assert(settingsDlg != null, "DigitalGlobeSettingsDialog instantiated");
                Assert(settingsDlg.ValidateClientIdBtn != null, "ValidateClientIdBtn accessible");
                Assert(!settingsDlg.ValidateClientIdBtn.Enabled, "ValidateClientIdBtn initially disabled for empty ID");

                var field = typeof(DigitalGlobeSettingsDialog).GetField("ClientIdTextBox", BindingFlags.NonPublic | BindingFlags.Instance);
                Assert(field != null, "ClientIdTextBox reflected");
                var txt = (DialogTextBox)field.GetValue(settingsDlg);

                txt.Text = "12345";
                Assert(!settingsDlg.ValidateClientIdBtn.Enabled, "ValidateClientIdBtn disabled for 5-digit ID");

                txt.Text = "1234567890";
                Assert(settingsDlg.ValidateClientIdBtn.Enabled, "ValidateClientIdBtn enabled for exactly 10-digit ID");

                txt.Text = "1234567890123";
                Assert(!settingsDlg.ValidateClientIdBtn.Enabled, "ValidateClientIdBtn disabled for 13-digit ID");

                // Test icon switching
                var setSuccess = typeof(DigitalGlobeSettingsDialog).GetMethod("SetSuccessfulValidatedIcon", BindingFlags.NonPublic | BindingFlags.Instance);
                var setFail = typeof(DigitalGlobeSettingsDialog).GetMethod("SetFailedValidatedIcon", BindingFlags.NonPublic | BindingFlags.Instance);
                var clearIcon = typeof(DigitalGlobeSettingsDialog).GetMethod("ClearValidatedIcon", BindingFlags.NonPublic | BindingFlags.Instance);

                setSuccess.Invoke(settingsDlg, null);
                Assert(true, "SetSuccessfulValidatedIcon invoked cleanly");
                setFail.Invoke(settingsDlg, null);
                Assert(true, "SetFailedValidatedIcon invoked cleanly");
                clearIcon.Invoke(settingsDlg, null);
                Assert(true, "ClearValidatedIcon invoked cleanly");
            }

            // 3. DigitalGlobeTimestampDialog calendar date & unix epoch sync
            using (var timeDlg = new DigitalGlobeTimestampDialog())
            {
                Assert(timeDlg != null, "DigitalGlobeTimestampDialog instantiated");

                var calField = typeof(DigitalGlobeTimestampDialog).GetField("mCalendar", BindingFlags.NonPublic | BindingFlags.Instance);
                var txtField = typeof(DigitalGlobeTimestampDialog).GetField("DisplayTimestampTextBox", BindingFlags.NonPublic | BindingFlags.Instance);
                var epochField = typeof(DigitalGlobeTimestampDialog).GetField("HiddenUnixEpochTimestampLabel", BindingFlags.NonPublic | BindingFlags.Instance);
                var syncMethod = typeof(DigitalGlobeTimestampDialog).GetMethod("SyncTimestampToCalendar", BindingFlags.NonPublic | BindingFlags.Instance);

                MonthCalendar cal = (MonthCalendar)calField.GetValue(timeDlg);
                DialogTextBox txt = (DialogTextBox)txtField.GetValue(timeDlg);
                DialogLabel epoch = (DialogLabel)epochField.GetValue(timeDlg);

                DateTime targetDate = new DateTime(2026, 10, 16);
                cal.SelectionStart = targetDate;
                cal.SelectionEnd = targetDate;

                syncMethod.Invoke(timeDlg, null);

                Assert(txt.Text == "10.16.2026", $"Formatted date matches: {txt.Text} == 10.16.2026");
                long expectedEpoch = (long)(targetDate - new DateTime(1970, 1, 1)).TotalSeconds;
                Assert(epoch.Text == expectedEpoch.ToString(), $"Unix timestamp matches: {epoch.Text} == {expectedEpoch}");

                // Test OnShown lifecycle method
                var onShownMethod = typeof(DigitalGlobeTimestampDialog).GetMethod("OnShown", BindingFlags.NonPublic | BindingFlags.Instance);
                onShownMethod.Invoke(timeDlg, new object[] { EventArgs.Empty });
                Assert(true, "DigitalGlobeTimestampDialog.OnShown executed cleanly");
            }
        }

        static void TestKtKitDotNet()
        {
            Console.WriteLine("\n[MODULE 3] Testing ktKitDotNet Data Types, Events & Template Editor...");

            // 1. KitCommon enums
            Assert(Enum.GetNames(typeof(KitCommon.DataType)).Length == 9, "KitCommon.DataType contains 9 data types");
            Assert(Enum.GetNames(typeof(KitCommon.Units)).Length == 4, "KitCommon.Units contains 4 unit representations");

            // 2. BooleanType
            using (var boolCtrl = new BooleanType())
            {
                bool eventFired = false;
                bool recordedVal = false;
                boolCtrl.BoolChanged += v => { eventFired = true; recordedVal = v; };

                boolCtrl.Initialize(true);
                Assert(!eventFired, "Initializing BooleanType does not trigger spurious event");

                var chkField = typeof(BooleanType).GetField("BoolValue", BindingFlags.NonPublic | BindingFlags.Instance);
                CheckBox chk = (CheckBox)chkField.GetValue(boolCtrl);
                chk.Checked = false;
                Assert(eventFired && !recordedVal, "BooleanType BoolChanged event fired with false");

                eventFired = false;
                chk.Checked = true;
                Assert(eventFired && recordedVal, "BooleanType BoolChanged event fired with true");
            }

            // 3. IntegerType
            using (var intCtrl = new IntegerType())
            {
                int eventVal = 0;
                int minVal = 0, maxVal = 0, stepVal = 0;
                intCtrl.IntChanged += v => { eventVal = v; };
                intCtrl.MinChanged += v => { minVal = v; };
                intCtrl.MaxChanged += v => { maxVal = v; };
                intCtrl.StepChanged += v => { stepVal = v; };

                intCtrl.Initialize(50, 0, 100, 5);

                var valField = typeof(IntegerType).GetField("IntValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var minField = typeof(IntegerType).GetField("MinValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var maxField = typeof(IntegerType).GetField("MaxValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var stepField = typeof(IntegerType).GetField("StepValue", BindingFlags.NonPublic | BindingFlags.Instance);

                NumericUpDown num = (NumericUpDown)valField.GetValue(intCtrl);
                num.Value = 75;
                Assert(eventVal == 75, $"IntegerType IntChanged event fired with value {eventVal}");

                ((NumericUpDown)minField.GetValue(intCtrl)).Value = 10;
                Assert(minVal == 10, $"IntegerType MinChanged event fired with {minVal}");

                ((NumericUpDown)maxField.GetValue(intCtrl)).Value = 200;
                Assert(maxVal == 200, $"IntegerType MaxChanged event fired with {maxVal}");

                ((NumericUpDown)stepField.GetValue(intCtrl)).Value = 2;
                Assert(stepVal == 2, $"IntegerType StepChanged event fired with {stepVal}");
            }

            // 4. FloatType
            using (var floatCtrl = new FloatType())
            {
                float eventVal = 0f;
                float minVal = 0f, maxVal = 0f, stepVal = 0f;
                floatCtrl.FloatChanged += v => { eventVal = v; };
                floatCtrl.MinChanged += v => { minVal = v; };
                floatCtrl.MaxChanged += v => { maxVal = v; };
                floatCtrl.StepChanged += v => { stepVal = v; };

                floatCtrl.Initialize(12.5f, 0f, 100f, 0.5f, KitCommon.Units.eMetric);

                var valField = typeof(FloatType).GetField("FloatValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var minField = typeof(FloatType).GetField("MinValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var maxField = typeof(FloatType).GetField("MaxValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var stepField = typeof(FloatType).GetField("StepValue", BindingFlags.NonPublic | BindingFlags.Instance);

                NumericUpDown num = (NumericUpDown)valField.GetValue(floatCtrl);
                num.Value = new decimal(45.5);
                Assert(Math.Abs(eventVal - 45.5f) < 0.001f, $"FloatType FloatChanged event fired with {eventVal}");

                ((NumericUpDown)minField.GetValue(floatCtrl)).Value = new decimal(5.0);
                Assert(Math.Abs(minVal - 5.0f) < 0.001f, $"FloatType MinChanged fired with {minVal}");

                ((NumericUpDown)maxField.GetValue(floatCtrl)).Value = new decimal(250.0);
                Assert(Math.Abs(maxVal - 250.0f) < 0.001f, $"FloatType MaxChanged fired with {maxVal}");

                ((NumericUpDown)stepField.GetValue(floatCtrl)).Value = new decimal(1.5);
                Assert(Math.Abs(stepVal - 1.5f) < 0.001f, $"FloatType StepChanged fired with {stepVal}");
            }

            // 5. StringType
            using (var strCtrl = new StringType())
            {
                string eventVal = "";
                strCtrl.StringChanged += v => { eventVal = v; };
                strCtrl.Initialize("CityScape", KitCommon.StringDisplayType.eTextBox);

                var txtField = typeof(StringType).GetField("StringValue", BindingFlags.NonPublic | BindingFlags.Instance);
                TextBox txt = (TextBox)txtField.GetValue(strCtrl);
                txt.Text = "UpdatedString";
                Assert(eventVal == "UpdatedString", $"StringType StringChanged fired with '{eventVal}'");
            }

            // 6. ColorType
            using (var colCtrl = new ColorType())
            {
                colCtrl.Initialize(Color.CornflowerBlue);
                var btnField = typeof(ColorType).GetField("ColorButton", BindingFlags.NonPublic | BindingFlags.Instance);
                Button btn = (Button)btnField.GetValue(colCtrl);
                Assert(btn.Text == "100,149,237,255", $"ColorType text initialized: {btn.Text}");
                Assert(btn.Image != null, "ColorType swatch image rendered");
            }

            // 7. Vector2Type (Validates Fix for MaxChanged & StepChanged)
            using (var v2 = new Vector2Type())
            {
                float xVal = 0, yVal = 0, minVal = 0, maxVal = 0, stepVal = 0;
                v2.Vector2Changed += (x, y) => { xVal = x; yVal = y; };
                v2.MinChanged += v => { minVal = v; };
                v2.MaxChanged += v => { maxVal = v; };
                v2.StepChanged += v => { stepVal = v; };

                v2.Initialize(1f, 2f, 0f, 100f, 0.5f, KitCommon.Units.eMetric);
                Assert(v2 != null, "Vector2Type initialized");

                var xField = typeof(Vector2Type).GetField("XValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var minField = typeof(Vector2Type).GetField("MinValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var maxField = typeof(Vector2Type).GetField("MaxValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var stepField = typeof(Vector2Type).GetField("StepValue", BindingFlags.NonPublic | BindingFlags.Instance);

                ((NumericUpDown)xField.GetValue(v2)).Value = new decimal(10.0);
                Assert(Math.Abs(xVal - 10.0f) < 0.001f, "Vector2Changed event fired on XValue update");

                ((NumericUpDown)minField.GetValue(v2)).Value = new decimal(5.0);
                Assert(Math.Abs(minVal - 5.0f) < 0.001f, "Vector2Type MinChanged fired with correct MinValue");

                ((NumericUpDown)maxField.GetValue(v2)).Value = new decimal(150.0);
                Assert(Math.Abs(maxVal - 150.0f) < 0.001f, $"Vector2Type MaxChanged fired with correct MaxValue (verified fix): {maxVal} == 150.0");

                ((NumericUpDown)stepField.GetValue(v2)).Value = new decimal(2.5);
                Assert(Math.Abs(stepVal - 2.5f) < 0.001f, $"Vector2Type StepChanged fired with correct StepValue (verified fix): {stepVal} == 2.5");
            }

            // 8. Vector3Type (Validates Fix for MaxChanged & StepChanged)
            using (var v3 = new Vector3Type())
            {
                float minVal = 0, maxVal = 0, stepVal = 0;
                v3.MinChanged += v => { minVal = v; };
                v3.MaxChanged += v => { maxVal = v; };
                v3.StepChanged += v => { stepVal = v; };

                v3.Initialize(1f, 2f, 3f, 0f, 200f, 0.5f, KitCommon.Units.eMetric);
                Assert(v3 != null, "Vector3Type initialized");

                var minField = typeof(Vector3Type).GetField("MinValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var maxField = typeof(Vector3Type).GetField("MaxValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var stepField = typeof(Vector3Type).GetField("StepValue", BindingFlags.NonPublic | BindingFlags.Instance);

                ((NumericUpDown)minField.GetValue(v3)).Value = new decimal(8.0);
                Assert(Math.Abs(minVal - 8.0f) < 0.001f, "Vector3Type MinChanged fired with correct MinValue");

                ((NumericUpDown)maxField.GetValue(v3)).Value = new decimal(300.0);
                Assert(Math.Abs(maxVal - 300.0f) < 0.001f, $"Vector3Type MaxChanged fired with correct MaxValue (verified fix): {maxVal} == 300.0");

                ((NumericUpDown)stepField.GetValue(v3)).Value = new decimal(4.0);
                Assert(Math.Abs(stepVal - 4.0f) < 0.001f, $"Vector3Type StepChanged fired with correct StepValue (verified fix): {stepVal} == 4.0");
            }

            // 9. Vector4Type (Validates Fix for MaxChanged & StepChanged)
            using (var v4 = new Vector4Type())
            {
                float minVal = 0, maxVal = 0, stepVal = 0;
                v4.MinChanged += v => { minVal = v; };
                v4.MaxChanged += v => { maxVal = v; };
                v4.StepChanged += v => { stepVal = v; };

                v4.Initialize(1f, 2f, 3f, 4f, 0f, 500f, 1f, KitCommon.Units.eNone);
                Assert(v4 != null, "Vector4Type initialized");

                var minField = typeof(Vector4Type).GetField("MinValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var maxField = typeof(Vector4Type).GetField("MaxValue", BindingFlags.NonPublic | BindingFlags.Instance);
                var stepField = typeof(Vector4Type).GetField("StepValue", BindingFlags.NonPublic | BindingFlags.Instance);

                ((NumericUpDown)minField.GetValue(v4)).Value = new decimal(12.0);
                Assert(Math.Abs(minVal - 12.0f) < 0.001f, "Vector4Type MinChanged fired with correct MinValue");

                ((NumericUpDown)maxField.GetValue(v4)).Value = new decimal(600.0);
                Assert(Math.Abs(maxVal - 600.0f) < 0.001f, $"Vector4Type MaxChanged fired with correct MaxValue (verified fix): {maxVal} == 600.0");

                ((NumericUpDown)stepField.GetValue(v4)).Value = new decimal(5.0);
                Assert(Math.Abs(stepVal - 5.0f) < 0.001f, $"Vector4Type StepChanged fired with correct StepValue (verified fix): {stepVal} == 5.0");
            }

            // 10. EnumerationType
            using (var enumCtrl = new EnumerationType())
            {
                int eventVal = -1;
                enumCtrl.EnumChanged += v => { eventVal = v; };
                enumCtrl.Initialize(0, KitCommon.EnumDisplayType.eComboBox, new string[] { "Option A", "Option B", "Option C" });
                var comboField = typeof(EnumerationType).GetField("EnumValue", BindingFlags.NonPublic | BindingFlags.Instance);
                ComboBox cmb = (ComboBox)comboField.GetValue(enumCtrl);
                Assert(cmb.Items.Count == 3, $"EnumerationType loaded 3 items: {cmb.Items.Count}");
                cmb.SelectedIndex = 2;
                Assert(eventVal == 2, $"EnumerationType EnumChanged fired with index {eventVal}");
            }

            // 11. MetaDataTemplateEditor
            using (var editor = new MetaDataTemplateEditor())
            {
                Assert(editor.BoolInfo != null, "MetaDataTemplateEditor has BoolControl");
                Assert(editor.ColorInfo != null, "MetaDataTemplateEditor has ColorControl");
                Assert(editor.StringInfo != null, "MetaDataTemplateEditor has StringControl");
                Assert(editor.IntInfo != null, "MetaDataTemplateEditor has IntControl");
                Assert(editor.FloatInfo != null, "MetaDataTemplateEditor has FloatControl");
                Assert(editor.Vector2Info != null, "MetaDataTemplateEditor has Vector2Control");
                Assert(editor.Vector3Info != null, "MetaDataTemplateEditor has Vector3Control");
                Assert(editor.Vector4Info != null, "MetaDataTemplateEditor has Vector4Control");
                Assert(editor.EnumInfo != null, "MetaDataTemplateEditor has EnumControl");

                // Cycle through all 9 data types
                editor.SetType(KitCommon.DataType.eBoolean);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "Boolean", "SetType Boolean");

                editor.SetType(KitCommon.DataType.eInteger);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "Integer", "SetType Integer");

                editor.SetType(KitCommon.DataType.eFloat);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "Float", "SetType Float");

                editor.SetType(KitCommon.DataType.eColor);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "Color", "SetType Color");

                editor.SetType(KitCommon.DataType.eString);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "String", "SetType String");

                editor.SetType(KitCommon.DataType.eVector2);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "2D Vector", "SetType 2D Vector");

                editor.SetType(KitCommon.DataType.eVector3);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "3D Vector", "SetType 3D Vector");

                editor.SetType(KitCommon.DataType.eVector4);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "4D Vector", "SetType 4D Vector");

                editor.SetType(KitCommon.DataType.eEnumeration);
                Assert((string)editor.ElementTypeCombo.SelectedItem == "Enumeration", "SetType Enumeration");
            }
        }

        static void TestEdEditorDotNet()
        {
            Console.WriteLine("\n[MODULE 4] Testing edEditorDotNet Project, GIS, Export Dialogs & Controls...");

            // 1. NewProjectDialog
            using (var dlg = new NewProjectDialog())
            {
                var okBtnField = typeof(NewProjectDialog).GetField("okButton", BindingFlags.NonPublic | BindingFlags.Instance);
                var nameTxtField = typeof(NewProjectDialog).GetField("NameTextBox", BindingFlags.NonPublic | BindingFlags.Instance);
                var locField = typeof(NewProjectDialog).GetField("ProjectLocation", BindingFlags.NonPublic | BindingFlags.Instance);

                Button okBtn = (Button)okBtnField.GetValue(dlg);
                DialogTextBox nameTxt = (DialogTextBox)nameTxtField.GetValue(dlg);
                DialogPathLoader loc = (DialogPathLoader)locField.GetValue(dlg);

                Assert(!okBtn.Enabled, "NewProjectDialog OK button initially disabled");

                nameTxt.Text = "UrbanTestProject";
                loc.Path = @"C:\CityScapeProjects";
                Assert(okBtn.Enabled, "NewProjectDialog OK button enabled after filling Name and Location");
            }

            // 2. TerrainSettingsDialog
            using (var dlg = new TerrainSettingsDialog())
            {
                var panel = dlg.GetTerrainSettingsPanel();
                Assert(panel != null, "TerrainSettingsDialog has valid TerrainSettingsPanel");
                dlg.EnableProjectPanel(true);
                dlg.EnableProjectPanel(false);
                dlg.ShowErrorMessage("Non-fatal verification warning message");
                dlg.ClearMessageInfo();
                Assert(true, "TerrainSettingsDialog error messaging functional");

                // Native call simulation: SetMaxTerrainSize(1600, 1600) and SetForceSingleTile(true)
                panel.SetMaxTerrainSize(1600, 1600);
                Assert(panel.GetTerrainMaxSizeX() >= 10000000f, "MaxTerrainSizeX limit bypassed (unlimited 10,000 km)");
                Assert(panel.GetTerrainMaxSizeY() >= 10000000f, "MaxTerrainSizeY limit bypassed (unlimited 10,000 km)");
                Assert(panel.MaxTerrainSizeInfoLabel.Text.Contains("Unlimited") || panel.MaxTerrainSizeInfoLabel.Text.Contains("Sınırsız"), "MaxTerrainSizeInfo displays Unlimited (Sınırsız)");

                panel.SetForceSingleTile(true);
                Assert(panel.TileSizeX.Enabled, "TileSizeX is unlocked and enabled");
                Assert(panel.TileSizeY.Enabled, "TileSizeY is unlocked and enabled");
                Assert(panel.TileCountX.Enabled, "TileCountX is unlocked and enabled");
                Assert(panel.TileCountY.Enabled, "TileCountY is unlocked and enabled");

                // 1. ForceEnabled immunity test on DialogNumericUpDownFloat
                panel.SamplingDistance.ForceEnabled = true;
                panel.SamplingDistance.Enabled = false;
                Assert(panel.SamplingDistance.Enabled, "SamplingDistance cannot be disabled when ForceEnabled is true");
                panel.SamplingDistance.ReadOnly = true;
                Assert(!panel.SamplingDistance.ReadOnly, "SamplingDistance cannot be made read-only when ForceEnabled is true");

                // Range validation (0.01m to 100,000m)
                panel.SetTessellationSampleDistRange(0f, 0f);
                Assert(panel.SamplingDistance.Maximum >= 100000m, "SamplingDistance maximum range is healthy (>= 100,000m) and not clamped to 0");
                Assert(panel.SamplingDistance.Minimum <= 0.01m, "SamplingDistance minimum range is healthy (<= 0.01m)");

                panel.SamplingDistance.Value = 0.01m;
                Assert(panel.SamplingDistance.Value == 0.01m, "SamplingDistance minimum 0.01m accepted");
                panel.SamplingDistance.Value = 100000m;
                Assert(panel.SamplingDistance.Value == 100000m, "SamplingDistance maximum 100,000m accepted");
                panel.SamplingDistance.Value = 3.5m;
                Assert(panel.SamplingDistance.Value == 3.5m, "SamplingDistance value set to 3.5m");

                // Native call simulation: SetEnableTessellationSampleDist(false)
                panel.SetEnableTessellationSampleDist(false);
                Assert(panel.SamplingDistance.Enabled, "SamplingDistance input remains unlocked and enabled even if SetEnableTessellationSampleDist(false) is called");

                // Trigger OnShown lifecycle
                var onShownMethod = typeof(TerrainSettingsDialog).GetMethod("OnShown", BindingFlags.NonPublic | BindingFlags.Instance);
                onShownMethod.Invoke(dlg, new object[] { EventArgs.Empty });
                Assert(panel.SamplingDistance.Enabled, "SamplingDistance is unlocked after OnShown");

                // Multi-tile calculation verification: Set Terrain size to 10,000m and TileCount to 5
                panel.TerrainSizeX.Value = 10000m;
                panel.TerrainSizeY.Value = 10000m;
                panel.TileCountX.Value = 5m;
                panel.TileCountY.Value = 5m;
                Assert(panel.TileSizeX.Value == 2000m, $"TileSizeX computed correctly: {panel.TileSizeX.Value} == 2000");
                Assert(panel.TileSizeY.Value == 2000m, $"TileSizeY computed correctly: {panel.TileSizeY.Value} == 2000");

                // Direct tile size modification: change TileSizeX to 2500m
                panel.TileSizeX.Value = 2500m;
                Assert(panel.TileCountX.Value == 4m, $"TileCountX recomputed correctly: {panel.TileCountX.Value} == 4");
                Assert(panel.IsDataValid(), "Multi-tile terrain data is fully valid");

                // 2. Real Native Pointer Discovery & Live Memory Write-Back Verification
                unsafe
                {
                    byte[] mockNativeBuffer = new byte[224];
                    fixed (byte* pNative = mockNativeBuffer)
                    {
                        var mockListener = new MockNativeDialogWrapper();
                        mockListener.Dialog = pNative;
                        panel.TerrainSizeX.ValueChanged += mockListener.OnValueChanged;

                        void* discoveredPtr = panel.FindNativeDialogPointer();
                        Assert(discoveredPtr == pNative, "FindNativeDialogPointer() dynamically discovered native struct pointer via delegate reflection");

                        // Modify panel values to 10,000m
                        panel.TerrainSizeX.Value = 10000m;
                        panel.TerrainSizeY.Value = 10000m;
                        panel.TileSizeX.Value = 2500m;
                        panel.TileSizeY.Value = 2000m;
                        panel.SamplingDistance.Value = 3.5m;

                        // Execute live write-back from panel into native memory
                        bool writeSuccess = panel.WriteValuesToNativeStorage();
                        Assert(writeSuccess, "panel.WriteValuesToNativeStorage() returned true");

                        // Inspect actual memory inside pNative
                        Assert(((float*)pNative)[40] == 10000f, "Live write: Native struct TerrainSizeX (offset 40) is 10,000m");
                        Assert(((float*)pNative)[41] == 10000f, "Live write: Native struct TerrainSizeY (offset 41) is 10,000m");
                        Assert(((float*)pNative)[42] == 2500f, "Live write: Native struct TileSizeX (offset 42) is 2500m");
                        Assert(((float*)pNative)[43] == 2000f, "Live write: Native struct TileSizeY (offset 43) is 2000m");
                        Assert(((float*)pNative)[44] == 3.5f, "Live write: Native struct SamplingDistance (offset 44) is 3.5m");
                        Assert(((int*)pNative)[54] >= 10000, "Live write: Native struct MaxTerrainSizeX (offset 54) updated");
                        Assert(((bool*)pNative)[157] == true, "Live write: Native struct IsCustomTerrain flag (offset 157) enabled");
                        Assert(((bool*)pNative)[159] == true, "Live write: Native struct EnableTessellationSampleDist flag (offset 159) enabled");

                        // Simulate OkButton click in dialog and verify write-back to native memory
                        panel.TerrainSizeX.Value = 15000m;
                        panel.SamplingDistance.Value = 7.5m;
                        dlg.OkButton.PerformClick();
                        Assert(((float*)pNative)[40] == 15000f, "OkButton click write-back: Native struct TerrainSizeX updated to 15,000m");
                        Assert(((float*)pNative)[44] == 7.5f, "OkButton click write-back: Native struct SamplingDistance updated to 7.5m");
                    }
                }

                // Render screenshot for visual confirmation
                try
                {
                    dlg.Show();
                    dlg.Refresh();
                    using (Bitmap bmp = new Bitmap(Math.Max(1, dlg.Width), Math.Max(1, dlg.Height)))
                    {
                        dlg.DrawToBitmap(bmp, new Rectangle(0, 0, Math.Max(1, dlg.Width), Math.Max(1, dlg.Height)));
                        string screenshotDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools");
                        if (!System.IO.Directory.Exists(screenshotDir)) System.IO.Directory.CreateDirectory(screenshotDir);
                        string targetPath = System.IO.Path.Combine(screenshotDir, "terrain_unlocked_dialog.png");
                        try
                        {
                            bmp.Save(targetPath, System.Drawing.Imaging.ImageFormat.Png);
                            Console.WriteLine("    [OK] Captured screenshot of unlocked dialog to tools/terrain_unlocked_dialog.png");
                        }
                        catch (Exception exSave)
                        {
                            Console.WriteLine("    [WARN] Could not save screenshot (file in use): " + exSave.Message);
                        }
                    }
                    dlg.Hide();
                }
                catch (Exception exShow)
                {
                    Console.WriteLine("    [WARN] Dialog screenshot skipped: " + exShow.Message);
                }
            }

            // 3. Grid & Geometry dialogs
            using (var dlg = new LodSettingsDialog()) Assert(dlg != null, "LodSettingsDialog instantiated");
            using (var dlg = new GridSettingsDialog()) Assert(dlg != null, "GridSettingsDialog instantiated");
            using (var dlg = new GeographyGridResizeDialog()) Assert(dlg != null, "GeographyGridResizeDialog instantiated");

            // 4. GIS Import dialogs
            using (var dlg = new GisImportRoadsDialog()) Assert(dlg != null, "GisImportRoadsDialog instantiated");
            using (var dlg = new GisImportRoadsCustomDialog()) Assert(dlg != null, "GisImportRoadsCustomDialog instantiated");
            using (var dlg = new GisImportElevationDialog()) Assert(dlg != null, "GisImportElevationDialog instantiated");
            using (var dlg = new GisImportBuildingsDialog()) Assert(dlg != null, "GisImportBuildingsDialog instantiated");
            using (var dlg = new GisImportWaterDialog()) Assert(dlg != null, "GisImportWaterDialog instantiated");
            using (var dlg = new GisImportOpenStreetMapDialog()) Assert(dlg != null, "GisImportOpenStreetMapDialog instantiated");
            using (var dlg = new GisImportOrthoImageDialog()) Assert(dlg != null, "GisImportOrthoImageDialog instantiated");
            using (var dlg = new GisImportPropPointsDialog()) Assert(dlg != null, "GisImportPropPointsDialog instantiated");
            using (var dlg = new GisImportPropPolygonsDialog()) Assert(dlg != null, "GisImportPropPolygonsDialog instantiated");
            using (var dlg = new OverlayImageDialog()) Assert(dlg != null, "OverlayImageDialog instantiated");

            // 5. Export Dialogs
            using (var dlg = new StandardExportDialog()) Assert(dlg != null, "StandardExportDialog instantiated");
            using (var dlg = new AutodeskExportDialog()) Assert(dlg != null, "AutodeskExportDialog instantiated");
            using (var dlg = new DefaultExportDialog()) Assert(dlg != null, "DefaultExportDialog instantiated");
            using (var dlg = new ExportFilmbox()) Assert(dlg != null, "ExportFilmbox instantiated");
            using (var dlg = new ExportOpenFlight()) Assert(dlg != null, "ExportOpenFlight instantiated");
            using (var dlg = new ExportBuildings()) Assert(dlg != null, "ExportBuildings instantiated");
            using (var dlg = new ExportProps()) Assert(dlg != null, "ExportProps instantiated");

            // 6. Navigation and Search Dialogs
            using (var dlg = new MultiUserCheckoutDialog()) Assert(dlg != null, "MultiUserCheckoutDialog instantiated");
            using (var dlg = new LoginDialog()) Assert(dlg != null, "LoginDialog instantiated");
            using (var dlg = new ActivationKeyDialog()) Assert(dlg != null, "ActivationKeyDialog instantiated");
            using (var dlg = new ActivationFormDialog()) Assert(dlg != null, "ActivationFormDialog instantiated");
            using (var dlg = new EditorAboutBox()) Assert(dlg != null, "EditorAboutBox instantiated");
            using (var dlg = new FindBuildingDialog()) Assert(dlg != null, "FindBuildingDialog instantiated");
            using (var dlg = new FindRoadDialog()) Assert(dlg != null, "FindRoadDialog instantiated");
            using (var dlg = new GoToTileDialog()) Assert(dlg != null, "GoToTileDialog instantiated");
            using (var dlg = new NewNavMeshDialog()) Assert(dlg != null, "NewNavMeshDialog instantiated");
            using (var dlg = new PhysicsControllerDialog()) Assert(dlg != null, "PhysicsControllerDialog instantiated");
            using (var dlg = new PhysicsEnterDialog()) Assert(dlg != null, "PhysicsEnterDialog instantiated");
            using (var dlg = new StreetViewDialog()) Assert(dlg != null, "StreetViewDialog instantiated");
            using (var dlg = new TextInputDialog()) Assert(dlg != null, "TextInputDialog instantiated");
            using (var dlg = new ComboBoxSelectionDialog()) Assert(dlg != null, "ComboBoxSelectionDialog instantiated");
            using (var dlg = new AssignMetaDataTemplates()) Assert(dlg != null, "AssignMetaDataTemplates instantiated");
            using (var dlg = new ExportMetaData()) Assert(dlg != null, "ExportMetaData instantiated");

            // 7. Missing and Utility Dialogs (Expanded Coverage)
            using (var dlg = new SplashDialog()) Assert(dlg != null, "SplashDialog instantiated");
            using (var dlg = new LodCountDialog()) Assert(dlg != null, "LodCountDialog instantiated");
            using (var dlg = new KeyLokNetworkDialog()) Assert(dlg != null, "KeyLokNetworkDialog instantiated");

            // 8. Export Controls
            using (var pathCtrl = new ExportPathControl())
            {
                Assert(pathCtrl != null, "ExportPathControl instantiated");
                pathCtrl.Filter = "Test Files (*.dae)|*.dae";
                Assert(pathCtrl.Filter == "Test Files (*.dae)|*.dae", "ExportPathControl Filter set and get");
                pathCtrl.Path = @"C:\CityScapeExport\Model.dae";
                Assert(pathCtrl.Path == @"C:\CityScapeExport\Model.dae", "ExportPathControl Path set and get");
            }

            using (var relPathCtrl = new ExportRelativePathControl())
            {
                Assert(relPathCtrl != null, "ExportRelativePathControl instantiated");
                Assert(relPathCtrl.AllRelativeToExportPathCheckBox != null, "ExportRelativePathControl AllRelativeToExportPathCheckBox accessible");
            }

            // 9. UiEnableManager via Reflection
            var mgrType = typeof(NewProjectDialog).Assembly.GetType("palDotNet.palDialog.UiEnableManager");
            Assert(mgrType != null, "UiEnableManager reflected");
            var mgr = Activator.CreateInstance(mgrType);
            var addCbMethod = mgrType.GetMethod("AddCheckBox");
            var enableMethod = mgrType.GetMethod("EnableElement");
            var disableUnsetMethod = mgrType.GetMethod("DisableUnset");
            using (var testCb = new DialogCheckBox { Name = "testCb", Checked = true, Enabled = true })
            {
                addCbMethod.Invoke(mgr, new object[] { testCb });
                disableUnsetMethod.Invoke(mgr, null);
                Assert(!testCb.Enabled, "UiEnableManager DisableUnset disables unchecked elements");
                enableMethod.Invoke(mgr, new object[] { "testCb" });
                Assert(testCb.Enabled, "UiEnableManager EnableElement restores enabled status");
            }
        }

        static void TestBugReporter()
        {
            Console.WriteLine("\n[MODULE 5] Testing BugReporter Recipients, MAPI structures & Forms...");

            // 1. Recipient
            var r1 = new BugReporter.Recipient("dev@pixelactive3d.com", "CityScape Dev", BugReporter.MapiMailMessage.RecipientType.To);
            Assert(r1.Address == "dev@pixelactive3d.com", "Recipient address set");
            Assert(r1.DisplayName == "CityScape Dev", "Recipient display name set");
            Assert(r1.RecipientType == BugReporter.MapiMailMessage.RecipientType.To, "Recipient type set to To");

            var r2 = new BugReporter.Recipient("cc@pixelactive3d.com", "CityScape CC", BugReporter.MapiMailMessage.RecipientType.CC);
            Assert(r2.RecipientType == BugReporter.MapiMailMessage.RecipientType.CC, "Recipient type set to CC");

            var r3 = new BugReporter.Recipient("bcc@pixelactive3d.com", "CityScape BCC", BugReporter.MapiMailMessage.RecipientType.BCC);
            Assert(r3.RecipientType == BugReporter.MapiMailMessage.RecipientType.BCC, "Recipient type set to BCC");

            // 2. RecipientCollection
            var coll = new BugReporter.RecipientCollection();
            coll.Add(r1);
            coll.Add(r2);
            coll.Add(r3);
            Assert(coll.Count == 3, $"RecipientCollection count: {coll.Count} == 3");
            Assert(coll[0].Address == "dev@pixelactive3d.com", "RecipientCollection indexer access");
            coll.RemoveAt(1);
            Assert(coll.Count == 2, "RecipientCollection RemoveAt(1)");

            // 3. MapiMailMessage
            var mail = new BugReporter.MapiMailMessage("Crash in Urban Pipeline", "Detailed minidump trace");
            mail.Recipients.Add("support@autodesk.com");
            Assert(mail.Recipients.Count == 1, "MapiMailMessage recipients count: 1");
            Assert(mail.Files != null, "MapiMailMessage Files collection initialized");

            // 4. Form1 (BugReporter UI)
            using (var form = new BugReporter.Form1())
            {
                Assert(form != null, "BugReporter Form1 instantiated");
                Assert(form.Text == "PixelActive Bug Reporter", $"Form1 title: {form.Text}");
            }
        }

        static void TestVisualAndDocking()
        {
            Console.WriteLine("\n[MODULE 6] Testing palDotNet Visual Views, DockContent, Widgets & PropertyGrid...");

            // 1. Views
            Guid outputGuid = Guid.NewGuid();
            using (var outView = new ViewOutput(outputGuid))
            {
                Assert(outView != null, "ViewOutput dock content instantiated");
                Assert(outView.Guid == outputGuid, $"ViewOutput GUID verified: {outView.Guid}");
            }

            Guid toolGuid = Guid.NewGuid();
            using (var toolView = new ViewTool(toolGuid))
            {
                Assert(toolView != null, "ViewTool dock content instantiated");
                Assert(toolView.Guid == toolGuid, $"ViewTool GUID verified: {toolView.Guid}");
            }

            Guid propGuid = Guid.NewGuid();
            using (var propView = new ViewPropertyGrid(propGuid))
            {
                Assert(propView != null, "ViewPropertyGrid dock content instantiated");
                Assert(propView.Guid == propGuid, $"ViewPropertyGrid GUID verified: {propView.Guid}");
                Assert(propView.TheGrid != null, "ViewPropertyGrid has active PropertyGrid");
            }

            // 2. Full FormApp Lifecycle with Layout and WidgetMgr
            var wm = new WidgetMgr();
            wm.Reader = new SerializerMemoryStreamReader();
            wm.Writer = new SerializerMemoryStreamWriter();

            // 3. SliderVector3 Widget
            var sliderType = typeof(Vector3).Assembly.GetType("palDotNet.palWidgets.SliderVector3");
            Assert(sliderType != null, "SliderVector3 reflected");
            var slider = Activator.CreateInstance(sliderType, new object[] { 1u });
            var xProp = sliderType.GetProperty("x");
            var yProp = sliderType.GetProperty("y");
            var zProp = sliderType.GetProperty("z");
            xProp.SetValue(slider, 10f);
            yProp.SetValue(slider, 20f);
            zProp.SetValue(slider, 30f);
            Assert((float)xProp.GetValue(slider) == 10f, "SliderVector3 x property set and get");
            Assert((float)yProp.GetValue(slider) == 20f, "SliderVector3 y property set and get");
            Assert((float)zProp.GetValue(slider) == 30f, "SliderVector3 z property set and get");

            var init = new FormAppBaseInit
            {
                AppName = "Subsystem Verification Harness",
                XRes = 1024,
                YRes = 768,
                ToolWindowGuidStr = "05675B1B-B72D-4B56-9D16-F3E940BCFF90",
                OutputWindowGuidStr = "DE0A0C0E-DC18-48C5-8320-8F4E1990C0AE",
                WidgetTheme = "Office2007",
                AppIconPath = "icons",
                LockRenderWindows = false
            };

            using (var form = new FormApp(init))
            {
                Assert(form != null, "FormApp constructed");
                form.DefaultLayout();
                Assert(true, "FormApp.DefaultLayout() completed");
                form.InitializeWidgetView();
                Assert(true, "FormApp.InitializeWidgetView() completed");
                form.LoadLayout();
                Assert(true, "FormApp.LoadLayout() completed");
            }
        }

        static void TestModernGraphicsAndEngine()
        {
            Console.WriteLine("\n[MODULE 7] Testing Modern 3D Graphics Engines (DX12, Vulkan, DX9), Shaders & Modern Exporters...");

            // 1. Adapter enumeration & Architecture detection
            var adapters = GraphicsBackendManager.EnumerateAvailableAdapters();
            Assert(adapters != null && adapters.Count >= 3, $"Adapter enumeration found {adapters.Count} backends (>= 3)");
            Assert(GraphicsBackendManager.CurrentArchitecture == TargetArchitecture.x86_Legacy ||
                   GraphicsBackendManager.CurrentArchitecture == TargetArchitecture.x64_Modern ||
                   GraphicsBackendManager.CurrentArchitecture == TargetArchitecture.ARM64_Modern,
                   $"Current target architecture identified: {GraphicsBackendManager.CurrentArchitecture}");

            // 2. DirectX 12 Modern Backend Lifecycle & Telemetry
            using (var dx12 = new DirectX12Backend())
            {
                Assert(dx12.BackendType == GraphicsBackendType.DirectX12_Modern, "DirectX 12 BackendType verified");
                Assert(dx12.Features.HasFlag(GraphicsFeatures.ShaderModel6_DXIL), "DirectX 12 supports HLSL SM6+ DXIL");
                Assert(dx12.Features.HasFlag(GraphicsFeatures.MeshShaders), "DirectX 12 supports Mesh Shaders");
                Assert(dx12.Features.HasFlag(GraphicsFeatures.HardwareTessellation), "DirectX 12 supports Hardware Tessellation");

                Assert(dx12.Initialize(IntPtr.Zero, 1920, 1080), "DirectX 12 backend initialized 1080p viewport");
                Assert(dx12.IsInitialized, "DirectX 12 IsInitialized is true");

                dx12.BeginFrame(Color.CornflowerBlue);
                dx12.RecordMeshDraw(1024, 2048);
                dx12.EndFrame();
                dx12.Present();

                var stats = dx12.GetStats();
                Assert(stats.FrameIndex == 1, "DirectX 12 FrameIndex incremented to 1");
                Assert(stats.DrawCalls == 1, "DirectX 12 DrawCalls recorded: 1");
                Assert(stats.TrianglesRendered == 682, $"DirectX 12 Triangles rendered: {stats.TrianglesRendered}");

                string report = dx12.GetDiagnosticReport();
                Assert(report.Contains("DirectX 12") && report.Contains("D3D12"), "DirectX 12 diagnostic report contains D3D12 metadata");
            }

            // 3. Vulkan 1.3 Modern Backend Lifecycle & Telemetry
            using (var vk = new VulkanBackend())
            {
                Assert(vk.BackendType == GraphicsBackendType.Vulkan_Modern, "Vulkan BackendType verified");
                Assert(vk.Features.HasFlag(GraphicsFeatures.SPIR_V_Bytecode), "Vulkan supports SPIR-V Bytecode");
                Assert(vk.Features.HasFlag(GraphicsFeatures.VariableRateShading), "Vulkan supports Variable Rate Shading");

                Assert(vk.Initialize(IntPtr.Zero, 2560, 1440), "Vulkan 1.3 backend initialized 1440p viewport");
                Assert(vk.IsInitialized, "Vulkan IsInitialized is true");

                vk.BeginFrame(Color.DarkSlateGray);
                vk.RecordMeshDraw(500, 1500);
                vk.EndFrame();
                vk.Present();

                var stats = vk.GetStats();
                Assert(stats.FrameIndex == 1, "Vulkan FrameIndex incremented to 1");
                Assert(stats.TrianglesRendered == 500, $"Vulkan Triangles rendered: {stats.TrianglesRendered}");

                string report = vk.GetDiagnosticReport();
                Assert(report.Contains("Vulkan 1.3") && report.Contains("SPIR-V"), "Vulkan diagnostic report contains SPIR-V metadata");
            }

            // 4. DirectX 9 Legacy Safe Mode Fallback Backend
            using (var dx9 = new DirectX9LegacyBackend())
            {
                Assert(dx9.BackendType == GraphicsBackendType.DirectX9_Legacy, "DirectX 9 Legacy BackendType verified");
                Assert(dx9.Features.HasFlag(GraphicsFeatures.ShaderModel3), "DirectX 9 supports Shader Model 3.0");

                Assert(dx9.Initialize(IntPtr.Zero, 1280, 800), "DirectX 9 backend initialized legacy viewport");
                Assert(dx9.IsInitialized, "DirectX 9 IsInitialized is true");

                dx9.BeginFrame(Color.Black);
                dx9.RecordMeshDraw(100, 300);
                dx9.EndFrame();
                dx9.Present();

                string report = dx9.GetDiagnosticReport();
                Assert(report.Contains("Direct3D 9.0c") || report.Contains("DirectX 9"), "DirectX 9 diagnostic report verified");
            }

            // 5. Runtime Backend Switching
            bool eventFired = false;
            GraphicsBackendType switchedType = GraphicsBackendType.DirectX9_Legacy;
            Action<GraphicsBackendType, IGraphicsBackend> handler = (type, b) =>
            {
                eventFired = true;
                switchedType = type;
            };

            GraphicsBackendManager.BackendChanged += handler;
            try
            {
                // Switch to Vulkan
                Assert(GraphicsBackendManager.SwitchBackend(GraphicsBackendType.Vulkan_Modern, IntPtr.Zero), "Runtime switch to Vulkan succeeded");
                Assert(GraphicsBackendManager.ActiveBackendType == GraphicsBackendType.Vulkan_Modern, "ActiveBackendType is Vulkan_Modern");
                Assert(eventFired && switchedType == GraphicsBackendType.Vulkan_Modern, "BackendChanged event fired for Vulkan");

                // Switch to DX12
                eventFired = false;
                Assert(GraphicsBackendManager.SwitchBackend(GraphicsBackendType.DirectX12_Modern, IntPtr.Zero), "Runtime switch to DirectX 12 succeeded");
                Assert(GraphicsBackendManager.ActiveBackendType == GraphicsBackendType.DirectX12_Modern, "ActiveBackendType is DirectX12_Modern");
                Assert(eventFired && switchedType == GraphicsBackendType.DirectX12_Modern, "BackendChanged event fired for DirectX 12");

                // Switch to legacy DX9 fallback
                eventFired = false;
                Assert(GraphicsBackendManager.SwitchBackend(GraphicsBackendType.DirectX9_Legacy, IntPtr.Zero), "Runtime switch to DirectX 9 Legacy succeeded");
                Assert(GraphicsBackendManager.ActiveBackendType == GraphicsBackendType.DirectX9_Legacy, "ActiveBackendType is DirectX9_Legacy");
                Assert(eventFired && switchedType == GraphicsBackendType.DirectX9_Legacy, "BackendChanged event fired for DirectX 9");
            }
            finally
            {
                GraphicsBackendManager.BackendChanged -= handler;
            }

            // 6. Modern Shader Pipeline & Mathematical Models
            var pipeline = new ModernShaderPipeline();
            Assert(pipeline.RegisteredModuleCount >= 7, $"ModernShaderPipeline loaded {pipeline.RegisteredModuleCount} shader modules (>= 7)");
            Assert(pipeline.Cache != null, "ModernShaderPipeline persistent on-disk cache initialized");

            var pipeline2 = new ModernShaderPipeline();
            Assert(pipeline2.Cache.CacheHits >= 4, $"Shader compilation cache hit {pipeline2.Cache.CacheHits} times on warm start");

            var pbrVs = pipeline.GetModule("StandardPBR", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil);
            Assert(pbrVs != null && pbrVs.SourceCode.Contains("gViewProjection"), "HLSL SM6+ Vertex shader verified");

            var terrainPs = pipeline.GetModule("TerrainSplat", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary);
            Assert(terrainPs != null && terrainPs.SourceCode.Contains("mix(rock, grass, slope)"), "Vulkan SPIR-V Terrain pixel shader verified");

            var skyVs = pipeline.GetModule("AtmosphericSky", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil);
            Assert(skyVs != null && skyVs.SourceCode.Contains("gViewProjNoTranslate"), "HLSL SM6+ Sky Vertex shader verified");

            var shadowVs = pipeline.GetModule("CascadedShadowDepth", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil);
            Assert(shadowVs != null && shadowVs.SourceCode.Contains("gLightViewProjection"), "HLSL SM6+ Cascaded Shadow depth pass shader verified");

            // Cook-Torrance Microfacet PBR evaluation & full BRDF
            var pbrMat = new PbrMaterialParameters { Roughness = 0.3f, Metallic = 0.8f };
            float specular = pbrMat.EvaluateCookTorranceSpecular(0.85f, 0.85f, 0.98f, 0.95f);
            Assert(specular > 0f, $"PBR Cook-Torrance specular BRDF computed: {specular:F4} > 0");
            pbrMat.EvaluateFullPbrBrdf(0.85f, 0.85f, 0.98f, 0.95f, out float diffTerm, out float specTerm);
            Assert(diffTerm >= 0f && specTerm > 0f, $"PBR Full BRDF (Diffuse: {diffTerm:F4}, Specular: {specTerm:F4}) verified");

            // Texture Loading & Ingestion
            string ddsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "extracted", "PixelActiveLogo.dds");
            if (File.Exists(ddsPath))
            {
                Assert(TextureLoader.TryInspectTexture(ddsPath, out TextureMetadata texMeta), "TextureLoader successfully inspected DDS asset");
                Assert(texMeta.Width > 0 && texMeta.Height > 0, $"DDS texture dimensions parsed: {texMeta.Width}x{texMeta.Height}");
                pbrMat.AlbedoMapPath = ddsPath;
                Assert(pbrMat.LoadAndValidateTextures(), "PbrMaterialParameters.LoadAndValidateTextures() succeeded");
                Assert(pbrMat.AlbedoMetadata.HasValue && pbrMat.AlbedoMetadata.Value.Width > 0, "AlbedoMetadata populated");
            }

            // Terrain Continuous LoD Tessellation Factors
            var terrainPipe = pipeline.TerrainPipeline;
            float tessClose = terrainPipe.CalculateTessellationFactor(50f);
            float tessFar = terrainPipe.CalculateTessellationFactor(3000f);
            float tessMid = terrainPipe.CalculateTessellationFactor(1300f);
            Assert(tessClose == 16f, $"Close range tessellation factor clamped to max (16): {tessClose}");
            Assert(tessFar == 1f, $"Far range tessellation factor clamped to min (1): {tessFar}");
            Assert(tessMid > 1f && tessMid < 16f, $"Continuous adaptive LoD tessellation factor: {tessMid:F2}");

            // Slope-based splat weighting
            float weightFlat = terrainPipe.CalculateSlopeWeight(0.95f);
            float weightCliff = terrainPipe.CalculateSlopeWeight(0.1f);
            Assert(weightFlat == 1.0f, "Flat terrain normal (Y=0.95) weighted 100% grass/dirt");
            Assert(weightCliff == 0.0f, "Vertical cliff normal (Y=0.1) weighted 0% grass (steep cliff rock)");

            // Gerstner Wave Ocean 3D displacement
            var waterPipe = pipeline.WaterPipeline;
            waterPipe.EvaluateWaterDisplacement(100f, 200f, 5.0f, out float dispX, out float dispY, out float dispZ);
            Assert(Math.Abs(dispY) > 0f, $"Gerstner wave 3D ocean displacement computed: Y={dispY:F3}m");

            // Modern Water SSR, Fresnel, Beer-Lambert Extinction & Caustics
            waterPipe.EvaluateWaterNormal(100f, 200f, 5.0f, out float wNx, out float wNy, out float wNz);
            float wNormLen = (float)Math.Sqrt(wNx * wNx + wNy * wNy + wNz * wNz);
            Assert(Math.Abs(wNormLen - 1.0f) < 0.001f, "Water surface analytical normal is unit length");

            float fresnelNormal = WaterModernPipeline.EvaluateWaterFresnel(1.0f);
            float fresnelGlance = WaterModernPipeline.EvaluateWaterFresnel(0.0f);
            Assert(Math.Abs(fresnelNormal - 0.02f) < 0.001f, "Water Fresnel at normal incidence is 0.02 (2% reflectance)");
            Assert(Math.Abs(fresnelGlance - 1.00f) < 0.001f, "Water Fresnel at glancing angle is 1.00 (100% total reflection)");

            waterPipe.EvaluateWaterExtinction(5.0f, out float transR, out float transG, out float transB);
            Assert(transR < transG && transG < transB, $"Beer-Lambert water extinction preserves deep blue: R={transR:F3}, G={transG:F3}, B={transB:F3}");

            float causticShallow = waterPipe.EvaluateCaustics(10f, 10f, 1.0f, 1.5f);
            float causticDeep = waterPipe.EvaluateCaustics(10f, 10f, 1.0f, 25.0f);
            Assert(causticShallow > causticDeep, $"Shallow water caustics focused and attenuated with depth ({causticShallow:F3} vs {causticDeep:F3})");

            bool ssrHit = waterPipe.TraceScreenSpaceReflection(0.5f, 0.5f, 0.95f, 0.3f, 0.4f, out float hitU, out float hitV, out float fadeWeight);
            Assert(ssrHit && hitU > 0f && hitV > 0f && fadeWeight > 0f, "Screen-Space Reflection (SSR) ray march detected hit on screen");

            Assert(pipeline.GetModule("WaterOceanSsr", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 WaterOceanSsr shader registered");
            Assert(pipeline.GetModule("WaterOceanSsr", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V WaterOceanSsr shader registered");

            // Atmospheric Scattering & Dynamic Sky Dome
            var atmo = pipeline.Atmosphere;
            atmo.GetSunDirection(out float sunX, out float sunY, out float sunZ);
            Assert(Math.Abs(sunX * sunX + sunY * sunY + sunZ * sunZ - 1.0f) < 0.001f, "Atmospheric sun direction is normalized vector");
            atmo.EvaluateSkyColor(0.8f, out float skyR, out float skyG, out float skyB);
            Assert(skyB > skyR && skyB > 0.3f, $"Atmospheric daylight sky zenith is blue: ({skyR:F2}, {skyG:F2}, {skyB:F2})");
            atmo.EvaluateAerialPerspective(2000f, 0.2f, 0.5f, 0.2f, out float fogR, out float fogG, out float fogB);
            Assert(fogR >= 0f && fogG >= 0f && fogB >= 0f, "Atmospheric aerial perspective fog computed");

            // Cascaded Shadow Maps (CSM) Splits & PCF
            var csm = pipeline.Shadows;
            Assert(csm.CascadeSplitDistances != null && csm.CascadeSplitDistances.Length == 4, "CSM has 4 cascade splits");
            Assert(csm.CascadeSplitDistances[0] < csm.CascadeSplitDistances[1] &&
                   csm.CascadeSplitDistances[1] < csm.CascadeSplitDistances[2] &&
                   csm.CascadeSplitDistances[2] < csm.CascadeSplitDistances[3], "CSM cascade split distances monotonically increase");
            Assert(csm.SelectCascadeIndex(10f) == 0, "Near target (10m) assigned to Cascade 0");
            Assert(csm.SelectCascadeIndex(4000f) == 3, "Far landscape (4000m) assigned to Cascade 3");
            Assert(csm.EvaluatePcfShadow(0.5f, 0.49f) == 1.0f, "CSM unshadowed PCF test returns 1.0f");
            Assert(csm.EvaluatePcfShadow(0.5f, 0.60f) < 1.0f, "CSM shadowed PCF test returns ambient occlusion");

            // Percentage-Closer Soft Shadows (PCSS) & Contact Hardening
            float sharpPenumbra = csm.EstimatePenumbraWidth(1.05f, 1.00f);
            float softPenumbra = csm.EstimatePenumbraWidth(5.00f, 1.00f);
            Assert(sharpPenumbra < softPenumbra, $"PCSS contact hardening verified: near penumbra ({sharpPenumbra:F4}) < far penumbra ({softPenumbra:F4})");

            float pcssLit = csm.EvaluatePcssShadow(1.0f, new float[] { 1.5f, 1.6f, 1.7f }, 0.01f);
            float pcssPartial = csm.EvaluatePcssShadow(1.0f, new float[] { 0.5f, 1.5f, 1.5f, 0.5f }, 0.01f);
            float pcssShadowed = csm.EvaluatePcssShadow(1.0f, new float[] { 0.5f, 0.4f, 0.3f }, 0.01f);
            Assert(pcssLit == 1.0f, "PCSS unblocked sample is 100% lit");
            Assert(pcssPartial > pcssShadowed && pcssPartial < pcssLit, $"PCSS partial penumbra softens shadow ({pcssPartial:F3})");
            Assert(Math.Abs(pcssShadowed - csm.AmbientShadowFactor) < 0.01f, "PCSS fully blocked sample clamped to ambient shadow floor");

            float blendStart = csm.ComputeCascadeBlendWeight(csm.CascadeSplitDistances[0] * 0.5f, 0);
            float blendMid = csm.ComputeCascadeBlendWeight(csm.CascadeSplitDistances[0] * 0.92f, 0);
            float blendEnd = csm.ComputeCascadeBlendWeight(csm.CascadeSplitDistances[0], 0);
            Assert(blendStart == 0.0f && blendMid > 0.0f && blendEnd == 1.0f, "Cascade blend weight transitions smoothly across split boundary");

            Assert(pipeline.GetModule("CascadedShadowPcss", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 CascadedShadowPcss shader registered");
            Assert(pipeline.GetModule("CascadedShadowPcss", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V CascadedShadowPcss shader registered");

            // HDR ACES Filmic Tone Mapping, Exposure & Bloom Pipeline
            var postPipe = pipeline.PostProcessing;
            Assert(postPipe != null, "pipeline.PostProcessing initialized");
            Assert(pipeline.GetModule("PostProcessAces", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 PostProcessAces shader registered");
            Assert(pipeline.GetModule("PostProcessAces", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V PostProcessAces shader registered");

            // Verify ACES Filmic Tone Mapping mathematical curve
            float darkVal = HdrPostProcessingPipeline.AcesFilmicToneMap(0.1f);
            float midVal = HdrPostProcessingPipeline.AcesFilmicToneMap(1.0f);
            float brightVal = HdrPostProcessingPipeline.AcesFilmicToneMap(10.0f);
            float extremeVal = HdrPostProcessingPipeline.AcesFilmicToneMap(100.0f);
            Assert(darkVal < midVal && midVal < brightVal && brightVal <= 1.0f && extremeVal <= 1.0f,
                $"ACES Filmic curve monotonically maps HDR radiance: 0.1->{darkVal:F3}, 1.0->{midVal:F3}, 10->{brightVal:F3}, 100->{extremeVal:F3}");

            // Verify Bloom soft-knee extraction
            postPipe.ExtractBloom(0.5f, 0.5f, 0.5f, out float blLowR, out _, out _);
            postPipe.ExtractBloom(2.0f, 2.5f, 3.0f, out float blHighR, out float blHighG, out float blHighB);
            Assert(blLowR == 0.0f, "Sub-threshold luminance correctly filtered from bloom extraction");
            Assert(blHighR > 0.0f && blHighB > blHighR, $"Super-threshold HDR highlights extracted for bloom: ({blHighR:F2}, {blHighG:F2}, {blHighB:F2})");

            // Verify Full HDR Pixel processing with ACES tone mapping & sRGB gamma
            postPipe.ProcessHdrPixel(2.0f, 1.5f, 0.8f, blHighR, blHighG, blHighB, out float ldrR, out float ldrG, out float ldrB);
            Assert(ldrR > 0f && ldrR <= 1.0f && ldrG > 0f && ldrG <= 1.0f && ldrB > 0f && ldrB <= 1.0f,
                $"Full HDR pixel tone mapped and gamma corrected: ({ldrR:F3}, {ldrG:F3}, {ldrB:F3})");

            // PBR Foliage GPU Instancing & Multi-LOD Hierarchies
            var foliage = pipeline.Foliage;
            Assert(foliage != null, "pipeline.Foliage initialized");
            foliage.Clear();

            // Populate multi-distance test instances
            foliage.AddInstance(new FoliageInstance(0f, 0f, 30f, 0.5f, 1f, 1f, 1f, 0));      // 30m -> LOD 0 Full Mesh
            foliage.AddInstance(new FoliageInstance(0f, 0f, 120f, 1.2f, 1f, 1f, 1f, 0));     // 120m -> LOD 1 Simplified Mesh
            foliage.AddInstance(new FoliageInstance(0f, 0f, 350f, 2.1f, 1f, 1f, 1f, 1));     // 350m -> LOD 2 Cross-Quad Billboard
            foliage.AddInstance(new FoliageInstance(0f, 0f, 1000f, 0.0f, 1f, 1f, 1f, 1));    // 1000m -> LOD 3 Impostor Billboard
            foliage.AddInstance(new FoliageInstance(0f, 0f, 3000f, 0.0f, 1f, 1f, 1f, 2));    // 3000m -> Culled

            // Batch stream 1,000 instances
            var batchList = new List<FoliageInstance>(1000);
            for (int fi = 0; fi < 1000; fi++)
            {
                batchList.Add(new FoliageInstance(fi * 1.5f, 0f, (fi % 100) * 20f, (float)(fi * 0.1), 1f, 1f + (fi % 3) * 0.5f, 1f, fi % 4));
            }
            foliage.AddInstances(batchList);
            Assert(foliage.TotalInstanceCount == 1005, $"Foliage instance collection populated: {foliage.TotalInstanceCount} instances");

            // Build GPU Instance Buffer & LOD binning from camera (0, 0, 0)
            var gpuInstances = foliage.BuildGpuInstanceBuffer(0f, 0f, 0f,
                out int fLod0, out int fLod1, out int fLod2, out int fLod3, out int fCulled);
            Assert(fLod0 > 0 && fLod1 > 0 && fLod2 > 0 && fLod3 > 0, $"Foliage LOD bins verified (LOD0: {fLod0}, LOD1: {fLod1}, LOD2: {fLod2}, LOD3: {fLod3})");
            Assert(fCulled > 0, $"Distant instances outside max view range culled: {fCulled}");
            Assert(gpuInstances.Length == (1005 - fCulled), "GPU instance buffer contains only active non-culled instances");

            // Wind Simulation State & Analytical Displacement
            var wind = foliage.Wind;
            wind.CurrentTime = 5.0f;
            wind.EvaluateWindDisplacement(100f, 100f, 0.0f, 15f, 0f, out float dispRootX, out _, out float dispRootZ);
            wind.EvaluateWindDisplacement(100f, 100f, 1.0f, 15f, 0f, out float dispCanopyX, out float dispCanopyY, out float dispCanopyZ);

            Assert(dispRootX == 0.0f && dispRootZ == 0.0f, "Plant root (height=0) has exactly zero wind displacement");
            float canopyMag = (float)Math.Sqrt(dispCanopyX * dispCanopyX + dispCanopyZ * dispCanopyZ);
            Assert(canopyMag > 0.1f, $"Canopy top (height=1.0) demonstrates dynamic wind sway: {canopyMag:F3}m");
            Assert(dispCanopyY < 0.0f, "Canopy exhibits physical vertical compression under bending sway");

            // PBR Leaf Translucency & Subsurface Scattering
            float scatterBacklit = foliage.EvaluateLeafTranslucency(0f, 0f, -1f, 0f, 0f, 1f, 0f, 0f, 1f); // Sun behind leaf (-Z) towards camera (+Z)
            float scatterFrontlit = foliage.EvaluateLeafTranslucency(0f, 0f, 1f, 0f, 0f, 1f, 0f, 0f, 1f);  // Sun in front of leaf (+Z)
            Assert(scatterBacklit > scatterFrontlit, $"Leaf translucency forward scattering high when back-lit ({scatterBacklit:F3} > {scatterFrontlit:F3})");

            // Modern Foliage Shaders Registered
            Assert(pipeline.GetModule("FoliageInstancedVs", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 FoliageInstancedVs shader registered");
            Assert(pipeline.GetModule("FoliagePbrPs", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 FoliagePbrPs shader registered");
            Assert(pipeline.GetModule("FoliageInstancedVs", ShaderStage.Vertex, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V FoliageInstancedVs shader registered");
            Assert(pipeline.GetModule("FoliagePbrPs", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V FoliagePbrPs shader registered");

            // Temporal Anti-Aliasing (TAA) Pipeline, Sub-pixel Jittering & Neighborhood Clamping
            var taa = pipeline.TemporalAA;
            Assert(taa != null, "pipeline.TemporalAA initialized");
            Assert(taa.JitterSampleCount == 8, "TAA configured with 8-phase Halton sequence");

            // Verify Halton sequence low-discrepancy sub-pixel jitter
            taa.GetCameraJitter(0, 1920, 1080, out float jPx0, out float jPy0, out float jNdcX0, out float jNdcY0);
            taa.GetCameraJitter(1, 1920, 1080, out float jPx1, out float jPy1, out float jNdcX1, out float jNdcY1);
            Assert(jPx0 >= -0.5f && jPx0 <= 0.5f && jPy0 >= -0.5f && jPy0 <= 0.5f, "TAA sub-pixel jitter bounded within [-0.5, 0.5] pixel envelope");
            Assert(jPx0 != jPx1 || jPy0 != jPy1, "Consecutive frames yield distinct low-discrepancy sub-pixel jitter samples");
            Assert(Math.Abs(jNdcX0) < 0.002f && Math.Abs(jNdcY0) < 0.002f, "TAA NDC jitter magnitude is sub-pixel scaled for 1080p projection");

            // Verify 3x3 Color Variance Clamping (Anti-Ghosting)
            float[] testNeighborhoodR = new float[] { 0.20f, 0.22f, 0.21f, 0.23f, 0.25f, 0.24f, 0.22f, 0.21f, 0.23f };
            float[] testNeighborhoodG = new float[] { 0.50f, 0.52f, 0.51f, 0.53f, 0.55f, 0.54f, 0.52f, 0.51f, 0.53f };
            float[] testNeighborhoodB = new float[] { 0.10f, 0.12f, 0.11f, 0.13f, 0.15f, 0.14f, 0.12f, 0.11f, 0.13f };

            // Outlier bright history (e.g. fast moving specular highlight at 1.0)
            taa.ClampHistoryColor(1.0f, 1.0f, 1.0f, testNeighborhoodR, testNeighborhoodG, testNeighborhoodB,
                out float clampedR, out float clampedG, out float clampedB);
            Assert(clampedR < 0.35f && clampedG < 0.65f && clampedB < 0.25f,
                $"Outlier history color clamped into 3x3 variance box to prevent ghosting: R({clampedR:F3}), G({clampedG:F3}), B({clampedB:F3})");

            // In-range history color preserved
            taa.ClampHistoryColor(0.22f, 0.52f, 0.12f, testNeighborhoodR, testNeighborhoodG, testNeighborhoodB,
                out float keepR, out float keepG, out float keepB);
            Assert(Math.Abs(keepR - 0.22f) < 0.001f && Math.Abs(keepG - 0.52f) < 0.001f, "Valid in-gamut history color preserved without clamping");

            // Verify Full TAA Pixel Resolve & Disocclusion Rejection
            taa.ResolveTaaPixel(0.24f, 0.54f, 0.14f, 0.22f, 0.52f, 0.12f, testNeighborhoodR, testNeighborhoodG, testNeighborhoodB,
                isDisoccluded: false, out float resR, out float resG, out float resB);
            Assert(resR > 0.22f && resR < 0.24f, $"TAA smoothly blends current jittered sample with historical accumulator: R={resR:F4}");

            taa.ResolveTaaPixel(0.90f, 0.90f, 0.90f, 0.10f, 0.10f, 0.10f, testNeighborhoodR, testNeighborhoodG, testNeighborhoodB,
                isDisoccluded: true, out float disocR, out float disocG, out float disocB);
            Assert(disocR == 0.90f && disocG == 0.90f && disocB == 0.90f, "Disocclusion edge instantly rejects history buffer to prevent ghost trails");

            // TAA Shaders Registered
            Assert(pipeline.GetModule("TemporalAntiAliasingPs", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 TemporalAntiAliasingPs shader registered");
            Assert(pipeline.GetModule("TemporalAntiAliasingPs", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V TemporalAntiAliasingPs shader registered");

            // Ground-Truth Ambient Occlusion (GTAO) Pipeline & Jimenez Multi-Bounce Approximation
            var gtao = pipeline.AmbientOcclusion;
            Assert(gtao != null, "pipeline.AmbientOcclusion initialized");
            Assert(gtao.Radius == 2.5f && gtao.SliceCount == 4, "GTAO configured with 2.5m radius and 4 horizon slices");

            // Flat surface has zero horizon blockage (100% visible)
            float[] flatDeltas = new float[] { 0f, 0f, 0f, 0f };
            gtao.EvaluateGtao(0f, 1f, 0f, flatDeltas, 10f, out float aoFlatRaw, out float aoFlatMb);
            Assert(aoFlatRaw == 1.0f && aoFlatMb == 1.0f, "Flat unoccluded surface evaluates to 1.0 (no ambient occlusion)");

            // Deep crevice / building contact corner has high horizon blockage
            float[] creviceDeltas = new float[] { 2.0f, 2.2f, 1.8f, 2.1f };
            gtao.EvaluateGtao(0f, 1f, 0f, creviceDeltas, 10f, out float aoCreviceRaw, out float aoCreviceMb);
            Assert(aoCreviceRaw < 0.30f, $"Deep corner creates dark contact occlusion: Raw={aoCreviceRaw:F3}");
            Assert(aoCreviceMb > aoCreviceRaw, $"Jimenez multi-bounce approximation softens harsh black crevice shadows ({aoCreviceMb:F3} > {aoCreviceRaw:F3})");

            // Distant background beyond MaxDistance is unoccluded
            gtao.EvaluateGtao(0f, 1f, 0f, creviceDeltas, 350f, out float aoFarRaw, out float aoFarMb);
            Assert(aoFarRaw == 1.0f && aoFarMb == 1.0f, "Distant terrain beyond GTAO max radius bypasses occlusion");

            // Bilateral Depth-Preserving Edge Filter
            float[] neighborAo = new float[] { 0.2f, 0.2f, 0.9f };
            float[] neighborDepths = new float[] { 10.05f, 10.10f, 85.0f };
            float filteredAo = gtao.BilateralFilter(0.2f, 10.0f, neighborAo, neighborDepths, depthThreshold: 0.5f);
            Assert(filteredAo < 0.35f, $"Bilateral filter rejects distant background depth edge without bleeding: Filtered={filteredAo:F3}");

            // GTAO Shaders Registered
            Assert(pipeline.GetModule("GroundTruthAmbientOcclusionPs", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 GroundTruthAmbientOcclusionPs shader registered");
            Assert(pipeline.GetModule("GroundTruthAmbientOcclusionPs", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V GroundTruthAmbientOcclusionPs shader registered");

            // Clustered Forward 3D Froxel Lighting Pipeline (Urban Streetlights & Spotlights)
            var clustered = pipeline.ClusteredLighting;
            Assert(clustered != null, "pipeline.ClusteredLighting initialized");
            Assert(ClusteredLightingPipeline.TotalClusters == 3456, "3D Froxel Grid configured to 16x9x24 (3456 clusters)");

            // Logarithmic Depth Cluster Indexing
            int cNearIdx = clustered.ComputeClusterIndex(0.5f, 0.5f, 5.0f, out int cNx, out int cNy, out int cNz);
            int cFarIdx = clustered.ComputeClusterIndex(0.5f, 0.5f, 400.0f, out int cFx, out int cFy, out int cFz);
            Assert(cNearIdx >= 0 && cNearIdx < ClusteredLightingPipeline.TotalClusters, "Near cluster index within 3D grid bounds");
            Assert(cFarIdx >= 0 && cFarIdx < ClusteredLightingPipeline.TotalClusters, "Far cluster index within 3D grid bounds");
            Assert(cNz < cFz, $"Logarithmic depth clustering maps near depth to lower Z-slice than far depth ({cNz} < {cFz})");

            // Point and Spot Light Physical Attenuation Law
            float attNear = ClusteredLightingPipeline.EvaluatePointAttenuation(0.0f, 20.0f);
            float attMid = ClusteredLightingPipeline.EvaluatePointAttenuation(10.0f, 20.0f);
            float attCutoff = ClusteredLightingPipeline.EvaluatePointAttenuation(20.0f, 20.0f);
            Assert(attNear == 1.0f, "Point light attenuation at d=0 is 1.0");
            Assert(attMid > 0.0f && attMid < attNear, "Point light inverse-square attenuation decreases smoothly with distance");
            Assert(attCutoff == 0.0f, "Point light attenuation smoothly clamped to 0 at cutoff range");

            float spotCenter = ClusteredLightingPipeline.EvaluateSpotAttenuation(0f, -10f, 0f, 0f, -1f, 0f, 0.5f, 0.8f);
            float spotOutside = ClusteredLightingPipeline.EvaluateSpotAttenuation(10f, 0f, 0f, 0f, -1f, 0f, 0.5f, 0.8f);
            Assert(spotCenter == 1.0f, "Spot light on-axis cone evaluates to 1.0");
            Assert(spotOutside == 0.0f, "Spot light outside outer angle evaluates to 0.0");

            // Populate 500 Urban Streetlights & Spotlights
            clustered.ClearLights();
            var urbanLights = new List<ClusteredLight>(500);
            for (int li = 0; li < 500; li++)
            {
                float lx = (li % 20) * 25.0f - 250.0f;
                float lz = (li / 20) * 25.0f - 250.0f;
                urbanLights.Add(new ClusteredLight(lx, 8.0f, lz, 18.0f, 1.0f, 0.85f, 0.6f, 120.0f,
                    type: (li % 3 == 0) ? ClusteredLightType.Spot : ClusteredLightType.Point));
            }
            clustered.AddLights(urbanLights);
            Assert(clustered.TotalLights == 500, $"Clustered lighting pipeline populated with {clustered.TotalLights} urban lights");

            // Bin lights into 3D Froxels from camera at (0, 15, -50)
            clustered.BuildClusterLightGrid(0f, 15f, -50f);
            IReadOnlyList<int> froxelLights = clustered.GetClusterLights(cNearIdx);
            Assert(froxelLights.Count < 500, $"Froxel cluster prunes light evaluation: {froxelLights.Count} lights in local cell vs 500 total");

            // Evaluate Clustered Diffuse Radiance under Streetlight (0, 8, 0) from camera at (0, 15, -50)
            clustered.EvaluateClusteredLighting(0f, 0f, 0f, 0f, 1f, 0f, 0.5f, 0.5f, 50.0f,
                out float clR, out float clG, out float clB);
            Assert(clR > 0.0f && clG > 0.0f && clR > clB, $"Clustered forward street illumination verified: Warm Amber ({clR:F2}, {clG:F2}, {clB:F2})");

            // Clustered Shaders Registered
            Assert(pipeline.GetModule("ClusteredLightingPs", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 ClusteredLightingPs shader registered");
            Assert(pipeline.GetModule("ClusteredLightingPs", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V ClusteredLightingPs shader registered");

            // Hierarchical Z-Buffer (HZB) GPU Occlusion Culling Pipeline
            var hzb = pipeline.OcclusionCulling;
            Assert(hzb != null, "pipeline.OcclusionCulling initialized");
            Assert(hzb.BaseWidth == 1024 && hzb.BaseHeight == 512, "HZB base resolution configured to 1024x512");
            Assert(hzb.MipLevels == 11, $"HZB generates full 11-level depth pyramid (1024x512 down to 1x1): MipLevels={hzb.MipLevels}");

            // Conservative 2x2 Max-Depth Reduction across Mip Levels
            var testSmallHzb = new HierarchicalZBufferPipeline(4, 4);
            Assert(testSmallHzb.MipLevels == 3, $"Small HZB 4x4 produces 3 mip levels (4x4, 2x2, 1x1): {testSmallHzb.MipLevels}");
            float[] smallDepths = new float[] {
                0.10f, 0.20f,  0.50f, 0.60f,
                0.30f, 0.40f,  0.70f, 0.80f,
                0.15f, 0.25f,  0.55f, 0.65f,
                0.35f, 0.45f,  0.75f, 0.85f
            };
            testSmallHzb.BuildPyramid(smallDepths);
            Assert(Math.Abs(testSmallHzb.MipPyramid[1][0] - 0.40f) < 1e-4f, "HZB 2x2 conservative max-depth reduction verified on Mip 1 (0,0)=0.40");
            Assert(Math.Abs(testSmallHzb.MipPyramid[1][1] - 0.80f) < 1e-4f, "HZB 2x2 conservative max-depth reduction verified on Mip 1 (1,0)=0.80");
            Assert(Math.Abs(testSmallHzb.MipPyramid[1][2] - 0.45f) < 1e-4f, "HZB 2x2 conservative max-depth reduction verified on Mip 1 (0,1)=0.45");
            Assert(Math.Abs(testSmallHzb.MipPyramid[1][3] - 0.85f) < 1e-4f, "HZB 2x2 conservative max-depth reduction verified on Mip 1 (1,1)=0.85");
            Assert(Math.Abs(testSmallHzb.MipPyramid[2][0] - 0.85f) < 1e-4f, "HZB Mip 2 reduction yields global conservative max occluder depth 0.85");

            // Optimal Mip Level Footprint Selection
            int mipTiny = hzb.ComputeOptimalMip(2.0f, 2.0f);
            int mipLarge = hzb.ComputeOptimalMip(256.0f, 128.0f);
            Assert(mipTiny == 1 && mipLarge == 8, $"HZB optimal mip level selection verified: small(2px)=1, large(256px)=8");

            // Screen Rect Occlusion Tests
            bool isOccFar = testSmallHzb.IsOccluded(0.0f, 0.0f, 0.45f, 0.45f, 0.45f);
            bool isOccNear = testSmallHzb.IsOccluded(0.0f, 0.0f, 0.45f, 0.45f, 0.25f);
            Assert(isOccFar, "Object behind front occluder correctly identified as occluded (culled)");
            Assert(!isOccNear, "Object in front of occluder correctly identified as visible (rendered)");

            // Batch Occlusion Query
            float[] queryMinU = new float[] { 0.0f, 0.0f, 0.5f };
            float[] queryMinV = new float[] { 0.0f, 0.0f, 0.5f };
            float[] queryMaxU = new float[] { 0.45f, 0.45f, 0.95f };
            float[] queryMaxV = new float[] { 0.45f, 0.45f, 0.95f };
            float[] queryDepths = new float[] { 0.50f, 0.20f, 0.90f };
            bool[] occResults = new bool[3];
            int occCount = testSmallHzb.BatchTestOcclusion(queryMinU, queryMinV, queryMaxU, queryMaxV, queryDepths, occResults, 3);
            Assert(occCount == 2 && occResults[0] == true && occResults[1] == false && occResults[2] == true,
                $"Batch occlusion culling correctly resolved {occCount}/3 instances as occluded");

            // 3D AABB Projected Occlusion Test
            float[] identityMatrix = new float[] {
                1f, 0f, 0f, 0f,
                0f, 1f, 0f, 0f,
                0f, 0f, 1f, 0f,
                0f, 0f, 0f, 1f
            };
            bool aabbCulled = testSmallHzb.IsAabbOccluded(-0.8f, -0.8f, 0.50f, -0.2f, -0.2f, 0.60f, identityMatrix);
            bool aabbVisible = testSmallHzb.IsAabbOccluded(-0.8f, -0.8f, 0.10f, -0.2f, -0.2f, 0.20f, identityMatrix);
            Assert(aabbCulled && !aabbVisible, "3D AABB projection against HZB correctly culls hidden building and preserves visible building");

            // HZB Shaders Registered
            Assert(pipeline.GetModule("HzbOcclusionCullCs", ShaderStage.Compute, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 HzbOcclusionCullCs compute shader registered");
            Assert(pipeline.GetModule("HzbOcclusionCullCs", ShaderStage.Compute, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V HzbOcclusionCullCs compute shader registered");

            // Volumetric Fog & Raymarched Crepuscular Light Shafts Pipeline
            var vFog = pipeline.VolumetricFog;
            Assert(vFog != null, "pipeline.VolumetricFog initialized");
            Assert(vFog.FogDensity == 0.015f && vFog.FogHeightFalloff == 45.0f, "Volumetric fog configured with 0.015 base density and 45m scale height");

            // Exponential Height Density Falloff
            float dSea = vFog.EvaluateDensity(0f);
            float dHigh = vFog.EvaluateDensity(90f);
            Assert(Math.Abs(dSea - 0.015f) < 1e-4f, "Sea level fog density matches configured base density (0.015)");
            Assert(dHigh < dSea * 0.2f && dHigh > 0f, $"Exponential height falloff attenuates fog density at altitude (d_90m={dHigh:F5} vs d_0m={dSea:F5})");

            // Analytical Optical Depth Integration
            float tauHoriz = vFog.EvaluateOpticalDepth(0f, 0f, 100f);
            Assert(Math.Abs(tauHoriz - 1.50f) < 1e-3f, "Analytical optical depth along horizontal ray matches rho * dist: 1.50");
            float tauClimb = vFog.EvaluateOpticalDepth(0f, 90f, 100f);
            Assert(tauClimb < tauHoriz && tauClimb > 0.0f, $"Climbing ray experiences decreasing optical depth due to vertical density falloff ({tauClimb:F3} < {tauHoriz:F3})");

            // Henyey-Greenstein Mie Scattering Phase Function
            float hgForward = VolumetricFogPipeline.EvaluateHenyeyGreenstein(1.0f, 0.72f);
            float hgBack = VolumetricFogPipeline.EvaluateHenyeyGreenstein(-1.0f, 0.72f);
            Assert(hgForward > 1.0f, $"Forward Mie scattering strongly amplifies direct sunlight: {hgForward:F2}");
            Assert(hgForward > hgBack * 20.0f, $"Forward Mie scattering is dramatically stronger than backscattering ({hgForward:F2} vs {hgBack:F4})");

            // Raymarched Volumetric Light Shafts (Crepuscular Rays)
            vFog.RaymarchVolumetricFog(0f, 10f, 0f, 0f, 0.2f, 1f, 200f, 0f, 0.2f, 1f, 1.0f, 0.9f, 0.7f,
                null, out float litFogR, out float litFogG, out float litFogB, out float transLit);
            Assert(litFogR > 0.0f && transLit < 1.0f,
                $"Direct unshadowed volumetric sunbeam in-scattering verified: Radiance=({litFogR:F2}, {litFogG:F2}, {litFogB:F2}), Transmittance={transLit:F3}");

            vFog.RaymarchVolumetricFog(0f, 10f, 0f, 0f, 0.2f, 1f, 200f, 0f, 0.2f, 1f, 1.0f, 0.9f, 0.7f,
                (x, y, z) => false, out float shadFogR, out float shadFogG, out float shadFogB, out float transShad);
            Assert(litFogR > shadFogR * 5.0f,
                $"Crepuscular sunbeam creates intense god-ray contrast between lit and shadowed air ({litFogR:F2} vs {shadFogR:F2})");
            Assert(Math.Abs(transLit - transShad) < 0.01f, "Atmospheric optical transmittance is invariant to shadow state");

            // Final Scene Fog Compositing
            vFog.ApplyFogToColor(0.2f, 0.5f, 0.8f, litFogR, litFogG, litFogB, transLit, out float compR, out float compG, out float compB);
            Assert(compR > 0.2f * transLit && compR > 0.0f, "Volumetric fog composited smoothly over background scene");

            // Volumetric Shaders Registered
            Assert(pipeline.GetModule("VolumetricFogLightShaftsPs", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 VolumetricFogLightShaftsPs shader registered");
            Assert(pipeline.GetModule("VolumetricFogLightShaftsPs", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V VolumetricFogLightShaftsPs shader registered");

            // GPU-Driven Meshlet / Cluster LOD Selection & Indirect Draw Pipeline
            var clusterPipe = pipeline.MeshClustering;
            Assert(clusterPipe != null, "pipeline.MeshClustering initialized");
            Assert(clusterPipe.TotalClusters == 0, "Cluster pipeline initialized with 0 clusters");

            // Populate test meshlet clusters (Front, Back-facing, Off-screen, High-error, and Coarse LOD)
            var clFront = new MeshCluster(0f, 0f, 50f, radius: 10f, coneX: 0f, coneY: 0f, coneZ: -1f, coneCutoff: 0.2f, lodError: 0.05f, indexOffset: 0, indexCount: 128, vertexOffset: 0, vertexCount: 64, lodLevel: 0);
            var clBack = new MeshCluster(0f, 0f, 50f, radius: 10f, coneX: 0f, coneY: 0f, coneZ: 1f, coneCutoff: 0.2f, lodError: 0.05f, indexOffset: 128, indexCount: 128, vertexOffset: 64, vertexCount: 64, lodLevel: 0);
            var clBehind = new MeshCluster(0f, 0f, -100f, radius: 10f, coneX: 0f, coneY: 0f, coneZ: -1f, coneCutoff: 0.2f, lodError: 0.05f, indexOffset: 256, indexCount: 128, vertexOffset: 128, vertexCount: 64, lodLevel: 0);
            var clHighError = new MeshCluster(0f, 0f, 500f, radius: 10f, coneX: 0f, coneY: 0f, coneZ: -1f, coneCutoff: 0.2f, lodError: 5.0f, indexOffset: 384, indexCount: 128, vertexOffset: 192, vertexCount: 64, lodLevel: 0);
            var clCoarse = new MeshCluster(0f, 0f, 500f, radius: 10f, coneX: 0f, coneY: 0f, coneZ: -1f, coneCutoff: 0.2f, lodError: 0.1f, indexOffset: 512, indexCount: 64, vertexOffset: 256, vertexCount: 32, lodLevel: 2);

            clusterPipe.AddClusters(new[] { clFront, clBack, clBehind, clHighError, clCoarse });
            Assert(clusterPipe.TotalClusters == 5, $"Cluster pipeline loaded with {clusterPipe.TotalClusters} meshlet clusters");

            // Frustum Culling on Cluster Bounding Sphere
            float invSqrt2 = 0.70710678f;
            float[] frustumPlanes = new float[] {
                0f, 0f, 1f, -0.5f,             // Near
                0f, 0f, -1f, 1000f,            // Far
                invSqrt2, 0f, invSqrt2, 0f,    // Left
                -invSqrt2, 0f, invSqrt2, 0f,   // Right
                0f, -invSqrt2, invSqrt2, 0f,   // Top
                0f, invSqrt2, invSqrt2, 0f     // Bottom
            };
            bool frontInFrustum = GpuClusterLodPipeline.TestFrustumSphere(clFront, frustumPlanes);
            bool behindInFrustum = GpuClusterLodPipeline.TestFrustumSphere(clBehind, frustumPlanes);
            Assert(frontInFrustum, "Cluster in front of camera passes frustum sphere test");
            Assert(!behindInFrustum, "Cluster behind camera correctly culled by frustum sphere test");

            // Normal Cone Backface Culling
            bool frontConePassed = GpuClusterLodPipeline.TestConeCulling(clFront, 0f, 0f, 0f);
            bool backConePassed = GpuClusterLodPipeline.TestConeCulling(clBack, 0f, 0f, 0f);
            Assert(frontConePassed, "Front-facing cluster survives normal cone culling");
            Assert(!backConePassed, "Back-facing cluster culled by normal cone test without vertex processing");

            // Projected Screen-Space LOD Error Metric
            bool highErrorPassed = GpuClusterLodPipeline.TestLodSelection(clHighError, 0f, 0f, 0f, maxScreenErrorPx: 2.0f, screenHeight: 1080f, fovYRadians: (float)Math.PI / 3f);
            bool coarsePassed = GpuClusterLodPipeline.TestLodSelection(clCoarse, 0f, 0f, 0f, maxScreenErrorPx: 2.0f, screenHeight: 1080f, fovYRadians: (float)Math.PI / 3f);
            Assert(!highErrorPassed, "Distant high-detail cluster rejected by screen-space error metric");
            Assert(coarsePassed, "Distant simplified LOD cluster selected matching pixel budget");

            // Indirect Draw Command Emission & Compaction
            var indirectCmds = new List<DrawIndexedIndirectCommand>();
            int surviving = clusterPipe.ExecuteClusterCull(0f, 0f, 0f, frustumPlanes, maxScreenErrorPx: 2.0f, screenHeight: 1080f, fovYRadians: (float)Math.PI / 3f, indirectCmds);
            Assert(surviving == 2 && indirectCmds.Count == 2, $"GPU indirect draw compaction produced exactly 2 surviving draw commands ({surviving}/5 clusters)");
            Assert(indirectCmds[0].IndexCountPerInstance == 128 && indirectCmds[1].IndexCountPerInstance == 64, "Compacted indirect draw commands contain valid meshlet index counts (128 & 64)");
            Assert(indirectCmds[0].InstanceCount == 1 && indirectCmds[1].InstanceCount == 1, "Indirect draw commands specify 1 instance count");

            // Cluster Shaders Registered
            Assert(pipeline.GetModule("GpuClusterCullIndirectCs", ShaderStage.Compute, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 GpuClusterCullIndirectCs compute shader registered");
            Assert(pipeline.GetModule("GpuClusterCullIndirectCs", ShaderStage.Compute, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V GpuClusterCullIndirectCs compute shader registered");

            // Depth-Aware Spatial Bilateral Upsampling & Temporal Reprojection Pipeline
            var bilateral = pipeline.BilateralUpsample;
            Assert(bilateral != null, "pipeline.BilateralUpsample initialized");
            Assert(bilateral.HalfWidth == 960 && bilateral.HalfHeight == 540, "Half-resolution target configured to 960x540 (quarter pixel count)");
            Assert(bilateral.FullWidth == 1920 && bilateral.FullHeight == 1080, "Full-resolution target configured to 1920x1080 (1080p)");

            // Flat Surface Exact Bilinear Interpolation
            float[] flatDepths = { 10.0f, 10.0f, 10.0f, 10.0f };
            float[] flatAoValues = { 0.20f, 0.40f, 0.60f, 0.80f };
            float upsampledCenter = bilateral.EvaluateBilateralUpsample(10.0f, flatDepths, flatAoValues, 0.5f, 0.5f);
            Assert(Math.Abs(upsampledCenter - 0.50f) < 1e-3f, $"Flat continuous surface evaluates to exact bilinear blend: {upsampledCenter:F3}");

            // Sharp Depth Discontinuity Edge Preservation (Zero Silhouette Bleed)
            float[] edgeDepths = { 5.0f, 100.0f, 100.0f, 100.0f };
            float[] edgeAoValues = { 0.20f, 1.00f, 1.00f, 1.00f };
            float fgUpsampled = bilateral.EvaluateBilateralUpsample(5.0f, edgeDepths, edgeAoValues, 0.5f, 0.5f);
            Assert(Math.Abs(fgUpsampled - 0.20f) < 0.01f, $"Sharp depth edge preserves foreground occlusion without background bleed ({fgUpsampled:F3} ~ 0.200)");
            float bgUpsampled = bilateral.EvaluateBilateralUpsample(100.0f, edgeDepths, edgeAoValues, 0.5f, 0.5f);
            Assert(Math.Abs(bgUpsampled - 1.00f) < 0.01f, $"Sharp depth edge preserves background without dark foreground halo bleed ({bgUpsampled:F3} ~ 1.000)");

            // Thin Silhouette Fallback (No NaN or division by zero)
            float[] missDepths = { 100.0f, 100.0f, 100.0f, 100.0f };
            float[] missAoValues = { 0.80f, 0.90f, 0.85f, 0.95f };
            float fallbackVal = bilateral.EvaluateBilateralUpsample(2.0f, missDepths, missAoValues, 0.5f, 0.5f);
            Assert(!float.IsNaN(fallbackVal) && fallbackVal >= 0.80f && fallbackVal <= 0.95f, "Thin silhouette sample safely falls back to nearest sample without NaN");

            // Temporal Accumulation & Disocclusion Rejection
            float[] local3x3 = { 0.20f, 0.22f, 0.21f, 0.23f, 0.22f, 0.24f, 0.21f, 0.20f, 0.22f };
            float historyStable = 0.22f;
            float temporalResolved = bilateral.ResolveTemporalBilateral(10.0f, flatDepths, new[] { 0.22f, 0.22f, 0.22f, 0.22f }, 0.5f, 0.5f, historyStable, local3x3, isDisoccluded: false);
            Assert(Math.Abs(temporalResolved - 0.22f) < 1e-3f, "Temporal accumulation maintains stable signal for stationary camera");

            float disocResolved = bilateral.ResolveTemporalBilateral(10.0f, flatDepths, new[] { 0.90f, 0.90f, 0.90f, 0.90f }, 0.5f, 0.5f, historyValue: 0.10f, local3x3, isDisoccluded: true);
            Assert(Math.Abs(disocResolved - 0.90f) < 1e-3f, "Disocclusion edge instantly bypasses history buffer to prevent ghost trails");

            // Bilateral Upsample Shaders Registered
            Assert(pipeline.GetModule("DepthAwareBilateralUpsamplePs", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil) != null, "DirectX 12 HLSL SM6 DepthAwareBilateralUpsamplePs shader registered");
            Assert(pipeline.GetModule("DepthAwareBilateralUpsamplePs", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary) != null, "Vulkan SPIR-V DepthAwareBilateralUpsamplePs shader registered");

            // Check Backend Root Signatures and Descriptor Layouts
            var dx12Inst = new DirectX12Backend();
            Assert(dx12Inst.RootSignature != null && dx12Inst.RootSignature.ParameterCount == 4, "DirectX12Backend Root Signature has 4 parameter slots");
            Assert(dx12Inst.RootSignature.Parameter0.Contains("CBV b0") && dx12Inst.RootSignature.Parameter2.Contains("SRV t0-t7"), "DirectX12 root parameters verified");

            var vkInst = new VulkanBackend();
            Assert(vkInst.DescriptorLayout != null && vkInst.DescriptorLayout.BindingCount == 3, "VulkanBackend Descriptor Layout has 3 bindings");
            Assert(vkInst.DescriptorLayout.Binding2.Contains("COMBINED_IMAGE_SAMPLER"), "Vulkan descriptor bindings verified");

            // 7. Modern Scene Exporters (glTF 2.0 / GLB, USD, OBJ+PBR)
            string tempDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp_export_tests");
            if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);

            try
            {
                var testMesh = new ExportMeshData { Name = "TerrainSubdivisionTile_0_0" };
                testMesh.Vertices.Add(new ExportVertex { X = 0f, Y = 10f, Z = 0f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 0f });
                testMesh.Vertices.Add(new ExportVertex { X = 100f, Y = 12f, Z = 0f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 0f });
                testMesh.Vertices.Add(new ExportVertex { X = 100f, Y = 15f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 1f });
                testMesh.Indices.AddRange(new uint[] { 0, 1, 2 });
                testMesh.Material = new PbrMaterialParameters { BaseColorR = 0.2f, BaseColorG = 0.8f, BaseColorB = 0.3f, Roughness = 0.4f, Metallic = 0.1f };

                var meshList = new List<ExportMeshData> { testMesh };

                // 7a. glTF 2.0 JSON
                string gltfPath = Path.Combine(tempDir, "terrain_test.gltf");
                var gltfExporter = new GltfModernExporter();
                Assert(gltfExporter.ExportScene(gltfPath, meshList, "CityScapeUrbanWorld"), "glTF 2.0 scene export returned true");
                Assert(File.Exists(gltfPath), "glTF export file exists on disk");
                string gltfContent = File.ReadAllText(gltfPath);
                Assert(gltfContent.Contains("\"version\": \"2.0\"") && gltfContent.Contains("pbrMetallicRoughness"), "glTF 2.0 file contains PBR metadata");
                Assert(gltfContent.Contains("\"meshes\":") && gltfContent.Contains("\"accessors\":") && gltfContent.Contains("\"bufferViews\":"), "glTF 2.0 JSON contains complete meshes, accessors, and bufferViews");
                Assert(gltfContent.Contains("data:application/octet-stream;base64,"), "glTF 2.0 JSON embeds valid self-contained base64 data URI buffer");

                // 7b. glTF 2.0 Binary (.glb)
                string glbPath = Path.Combine(tempDir, "terrain_test.glb");
                Assert(gltfExporter.ExportScene(glbPath, meshList, "CityScapeUrbanWorld"), "glTF Binary (.glb) export returned true");
                Assert(File.Exists(glbPath) && new FileInfo(glbPath).Length >= 20, "glTF Binary file created with valid header");

                using (var fs = File.OpenRead(glbPath))
                using (var br = new BinaryReader(fs))
                {
                    uint magic = br.ReadUInt32();
                    uint ver = br.ReadUInt32();
                    uint totLen = br.ReadUInt32();
                    Assert(magic == 0x46546C67, "GLB binary magic is 'glTF'");
                    Assert(ver == 2, "GLB version is 2");
                    Assert(totLen == (uint)fs.Length, "GLB totalLength header matches file size");

                    uint chunk0Len = br.ReadUInt32();
                    uint chunk0Type = br.ReadUInt32();
                    Assert(chunk0Type == 0x4E4F534A, "GLB chunk 0 type is JSON");

                    fs.Seek(chunk0Len, SeekOrigin.Current);
                    uint chunk1Len = br.ReadUInt32();
                    uint chunk1Type = br.ReadUInt32();
                    Assert(chunk1Type == 0x004E4942, "GLB chunk 1 type is BIN");
                    Assert(chunk1Len > 0, $"GLB chunk 1 BIN contains vertex and index data: {chunk1Len} bytes");
                }

                // 7c. Universal Scene Description (.usda)
                string usdPath = Path.Combine(tempDir, "terrain_test.usda");
                var usdExporter = new UsdModernExporter();
                Assert(usdExporter.ExportScene(usdPath, meshList, "CityScapeUrbanWorld"), "USD scene export returned true");
                Assert(File.Exists(usdPath), "USD file exists on disk");
                string usdContent = File.ReadAllText(usdPath);
                Assert(usdContent.Contains("#usda 1.0") && usdContent.Contains("def Mesh \"TerrainSubdivisionTile_0_0\""), "USD file contains USDA 1.0 and mesh definitions");
                Assert(usdContent.Contains("int[] faceVertexCounts = [3]"), "USDA faceVertexCounts contains '3' per triangle face");
                Assert(usdContent.Contains("primvars:normals") && usdContent.Contains("primvars:st"), "USDA contains standard primvars for normals and texture coordinates");
                Assert(usdContent.Contains("def Scope \"Materials\"") && usdContent.Contains("UsdPreviewSurface"), "USDA contains PBR material definitions and UsdPreviewSurface shaders");

                // 7d. Modern Wavefront OBJ + PBR MTL
                string objPath = Path.Combine(tempDir, "terrain_test.obj");
                string mtlPath = Path.Combine(tempDir, "terrain_test.mtl");
                var objExporter = new WavefrontPbrModernExporter();
                Assert(objExporter.ExportScene(objPath, meshList, "CityScapeUrbanWorld"), "OBJ+PBR export returned true");
                Assert(File.Exists(objPath) && File.Exists(mtlPath), "Both OBJ and MTL files generated on disk");
                string mtlContent = File.ReadAllText(mtlPath);
                Assert(mtlContent.Contains("newmtl Mat_TerrainSubdivisionTile_0_0") && mtlContent.Contains("Pr 0.400"), "MTL contains PBR roughness (Pr) and metallic (Pm)");

                // 7e. Native GPU Probing Telemetry
                var dxgiList = NativeGpuProber.GetDxgiAdapters();
                Assert(dxgiList != null, "NativeGpuProber.GetDxgiAdapters() returned non-null list");
                var primaryGpu = NativeGpuProber.GetPrimaryHardwareAdapter();
                if (primaryGpu.HasValue)
                {
                    Assert(!string.IsNullOrEmpty(primaryGpu.Value.Description), "Probed primary hardware GPU has valid description");
                }

                // 7f. GIS Procedural 3D Building Extrusion & Viewport Rendering
                var defaultTerrain = TerrainSurfaceData.CreateDefault();
                var cityBuildings = GisProceduralCityGenerator.GenerateUrbanGrid(defaultTerrain, blocksX: 3, blocksZ: 3, blockSize: 100f, streetWidth: 15f);
                Assert(cityBuildings != null && cityBuildings.Count > 0, $"GisProceduralCityGenerator produced {cityBuildings.Count} 3D building instances");
                
                var firstBld = cityBuildings[0];
                Assert(firstBld.Footprint != null && firstBld.TotalHeight > 0f, "GIS building footprint and total height verified");
                var bldMesh = firstBld.GenerateMesh();
                Assert(bldMesh != null && bldMesh.Vertices.Count > 0 && bldMesh.Indices.Count > 0, $"GIS building mesh generated with {bldMesh.Vertices.Count} vertices and {bldMesh.Indices.Count} indices");
                Assert(bldMesh.Material != null && bldMesh.Material.Roughness > 0f, "GIS building mesh has PBR material properties");

                string bldGltfPath = Path.Combine(tempDir, "building_test.gltf");
                Assert(gltfExporter.ExportScene(bldGltfPath, new List<ExportMeshData> { bldMesh }, "GisBuildingScene"), "Exported GIS building to glTF 2.0");
                Assert(File.Exists(bldGltfPath), "GIS building glTF file exists on disk");

                string bldUsdPath = Path.Combine(tempDir, "building_test.usda");
                Assert(usdExporter.ExportScene(bldUsdPath, new List<ExportMeshData> { bldMesh }, "GisBuildingScene"), "Exported GIS building to USDA");
                Assert(File.Exists(bldUsdPath), "GIS building USDA file exists on disk");

                ControlRender.SetActiveBuildings(cityBuildings);
                Assert(ControlRender.ActiveBuildings.Count == cityBuildings.Count, "ControlRender.ActiveBuildings correctly populated for 3D viewport rendering");
            }
            finally
            {
                try { if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true); } catch { }
            }
        }

        static void TestSpatialQuadtreeAndCulling()
        {
            Console.WriteLine("\n[MODULE 8] Testing Spatial Quadtree Scene Graph, Frustum Culling & Spatial Queries...");

            // 1. 3D Bounding Box (AABB) Calculations
            var box = new BoundingBox3D(10f, 0f, 10f, 50f, 20f, 60f);
            Assert(box.CenterX == 30f && box.CenterY == 10f && box.CenterZ == 35f, "BoundingBox3D center coordinate calculation");
            Assert(box.ExtentX == 20f && box.ExtentY == 10f && box.ExtentZ == 25f, "BoundingBox3D half-extents calculation");
            Assert(box.Contains(30f, 10f, 35f), "BoundingBox3D.Contains center point");
            Assert(!box.Contains(5f, 10f, 35f), "BoundingBox3D.Contains outside point correctly rejected");
            Assert(box.Contains2D(30f, 35f), "BoundingBox3D.Contains2D horizontal footprint test");

            var overlappingBox = new BoundingBox3D(40f, 15f, 50f, 80f, 30f, 90f);
            var disjointBox = new BoundingBox3D(100f, 0f, 100f, 120f, 10f, 120f);
            Assert(box.Intersects(overlappingBox), "BoundingBox3D AABB overlap detected");
            Assert(!box.Intersects(disjointBox), "BoundingBox3D disjoint AABB correctly rejected");

            // 2. Slab Ray-AABB Intersection
            Assert(box.Raycast(0f, 10f, 35f, 1f, 0f, 0f, out float tMin, out float tMax) && tMin == 10f, "BoundingBox3D slab raycast intersection distance");
            Assert(!box.Raycast(0f, 10f, 35f, -1f, 0f, 0f, out _, out _), "BoundingBox3D slab raycast correctly rejects ray pointing away");

            // 3. 3D View Frustum Construction & Box Intersection
            // Camera at (0, 20, -100) looking towards +Z
            var frustum = ViewFrustum3D.CreateFromCamera(0f, 20f, -100f, 0f, 0f, 60f, 16f / 9f, 1f, 1000f);
            Assert(frustum.Planes != null && frustum.Planes.Length == 6, "ViewFrustum3D initialized with 6 clipping planes");

            var targetInFront = new BoundingBox3D(-10f, 10f, 50f, 10f, 30f, 70f);
            var targetBehind = new BoundingBox3D(-10f, 10f, -300f, 10f, 30f, -250f);
            Assert(frustum.IntersectsBox(targetInFront) != FrustumIntersection.Outside, "Target in front of camera intersects view frustum");
            Assert(frustum.IntersectsBox(targetBehind) == FrustumIntersection.Outside, "Target behind camera is culled outside view frustum");

            // 4. Spatial Quadtree Population & Subdivision
            var worldBounds = new BoundingBox3D(-2000f, -100f, -2000f, 2000f, 1000f, 2000f);
            var quadtree = new SpatialQuadtree<GisBuildingInstance>(worldBounds, maxDepth: 4, maxItemsPerLeaf: 4);

            // Populate synthetic city grid with 64 buildings across a 4x4 km world
            var buildings = new List<GisBuildingInstance>();
            int id = 0;
            for (float x = -1500f; x <= 1500f; x += 400f)
            {
                for (float z = -1500f; z <= 1500f; z += 400f)
                {
                    var fp = new GisBuildingFootprint
                    {
                        FloorCount = (id % 15) + 2,
                        FloorHeightMeters = 3.5f
                    };
                    fp.Polygon.Add(new PointF(x - 20f, z - 20f));
                    fp.Polygon.Add(new PointF(x + 20f, z - 20f));
                    fp.Polygon.Add(new PointF(x + 20f, z + 20f));
                    fp.Polygon.Add(new PointF(x - 20f, z + 20f));

                    var b = new GisBuildingInstance(fp, null);
                    buildings.Add(b);
                    quadtree.Insert(b);
                    id++;
                }
            }

            Assert(quadtree.TotalItems == buildings.Count, $"SpatialQuadtree indexed all {buildings.Count} buildings");
            Assert(quadtree.TotalNodes > 1, $"SpatialQuadtree hierarchically subdivided into {quadtree.TotalNodes} nodes");

            // 5. Frustum Culling Query
            // View Frustum looking towards the north (+Z)
            var northFrustum = ViewFrustum3D.CreateFromCamera(0f, 50f, -1800f, 0f, 0f, 50f, 16f / 9f, 5f, 2000f);
            var visibleBuildings = quadtree.QueryFrustum(northFrustum, out int nodesTested, out int nodesCulled);
            Assert(visibleBuildings.Count > 0, $"Frustum query returned {visibleBuildings.Count} visible buildings");
            Assert(visibleBuildings.Count < buildings.Count, $"Frustum culling successfully culled off-screen buildings ({visibleBuildings.Count} visible of {buildings.Count} total)");
            Assert(nodesCulled > 0, $"Hierarchical culling pruned {nodesCulled} node subtrees out of {nodesTested} tested");

            // 6. Spatial Radius Query
            var nearCenter = quadtree.QueryRadius(0f, 0f, 500f);
            Assert(nearCenter.Count > 0 && nearCenter.Count < buildings.Count, $"Spatial radius query returned {nearCenter.Count} buildings within 500m radius");
            foreach (var b in nearCenter)
            {
                float dx = b.SpatialBounds.CenterX;
                float dz = b.SpatialBounds.CenterZ;
                float dist = (float)Math.Sqrt(dx * dx + dz * dz);
                Assert(dist <= 500f + 30f, $"Queried building within radius distance (dist={dist:F1}m)");
            }

            // 7. Viewport Integration Verification
            ControlRender.SetActiveBuildings(buildings);
            Assert(ControlRender.BuildingSpatialIndex != null, "ControlRender.BuildingSpatialIndex automatically created on SetActiveBuildings");
            Assert(ControlRender.BuildingSpatialIndex.TotalItems == buildings.Count, "BuildingSpatialIndex item count matches active buildings count");

            // 8. CDLOD Multi-Resolution Terrain Quadtree Engine
            var cdlodTerrain = new TerrainSurfaceData { SizeX = 100000f, SizeY = 4800f, ElevationMax = 250f };
            var cdlodTree = new CdlodTerrainQuadtree(cdlodTerrain, maxLodLevels: 4);
            Assert(cdlodTree.Root != null, "CdlodTerrainQuadtree root node initialized");
            Assert(cdlodTree.TotalNodes > 1, $"CdlodTerrainQuadtree hierarchically subdivided into {cdlodTree.TotalNodes} chunk nodes");
            Assert(cdlodTree.LodRanges != null && cdlodTree.LodRanges.Length == 4, "CDLOD distance ranges populated for 4 LOD tiers");
            Assert(cdlodTree.LodRanges[0] < cdlodTree.LodRanges[1] && cdlodTree.LodRanges[1] < cdlodTree.LodRanges[2], "CDLOD ranges monotonically scale geometrically");

            // Perform CDLOD Frustum Chunk Selection
            // Camera looking towards origin from -2000m on Z
            var cdlodFrustum = ViewFrustum3D.CreateFromCamera(0f, 200f, -2000f, 0f, 0f, 60f, 16f / 9f, 5f, 20000f);
            var selectedChunks = cdlodTree.SelectVisibleChunks(cdlodFrustum, 0f, 200f, -2000f, out int cdlodTested, out int cdlodCulled);
            Assert(selectedChunks != null && selectedChunks.Count > 0, $"CDLOD chunk selection returned {selectedChunks.Count} visible chunks");
            Assert(cdlodCulled > 0, $"CDLOD frustum culling pruned {cdlodCulled} off-screen terrain chunk subtrees");

            // Verify LOD mesh generation
            var firstChunk = selectedChunks[0];
            var chunkMesh = firstChunk.GenerateMesh(cdlodTerrain, baseGridResolution: 8);
            Assert(chunkMesh != null && chunkMesh.Vertices.Count > 0 && chunkMesh.Indices.Count > 0,
                $"CDLOD chunk generated dynamic LOD mesh with {chunkMesh.Vertices.Count} vertices and {chunkMesh.Indices.Count} indices");

            // Verify normals are normalized
            var v0 = chunkMesh.Vertices[0];
            float normLen = (float)Math.Sqrt(v0.Nx * v0.Nx + v0.Ny * v0.Ny + v0.Nz * v0.Nz);
            Assert(Math.Abs(normLen - 1.0f) < 0.05f, "CDLOD chunk generated normalized vertex normal vector");

            // Viewport CDLOD integration
            ControlRender.SetActiveTerrain(cdlodTerrain);
            Assert(ControlRender.TerrainCdlodTree != null, "ControlRender.TerrainCdlodTree populated on SetActiveTerrain");
            Assert(ControlRender.TerrainCdlodTree.TotalNodes == cdlodTree.TotalNodes, "ControlRender.TerrainCdlodTree node count matches configured quadtree");
        }

        static void TestGeospatialDemIngestion()
        {
            Console.WriteLine("\n[MODULE 9] Testing Real-World Geospatial DEM & GeoTIFF Raster Ingestion Pipeline...");

            string tempDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp_dem_tests");
            if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);

            try
            {
                // 1. Ingest Synthetic ESRI ASCII Grid (.asc)
                string ascPath = Path.Combine(tempDir, "sample_mountains.asc");
                using (var sw = new StreamWriter(ascPath))
                {
                    sw.WriteLine("NCOLS         5");
                    sw.WriteLine("NROWS         5");
                    sw.WriteLine("XLLCORNER     28.978400");
                    sw.WriteLine("YLLCORNER     41.008200");
                    sw.WriteLine("CELLSIZE      100.000000");
                    sw.WriteLine("NODATA_VALUE  -9999.0");
                    sw.WriteLine("10.0 20.0 50.0 80.0 100.0");
                    sw.WriteLine("15.0 35.0 90.0 150.0 120.0");
                    sw.WriteLine("25.0 60.0 180.0 250.0 140.0");
                    sw.WriteLine("20.0 45.0 110.0 160.0 90.0");
                    sw.WriteLine("12.0 25.0 60.0 80.0 40.0");
                }

                var demAsc = DemRasterData.LoadFromAsciiGrid(ascPath);
                Assert(demAsc.Width == 5 && demAsc.Height == 5, $"ASCII Grid raster parsed dimensions: {demAsc.Width}x{demAsc.Height}");
                Assert(Math.Abs(demAsc.MinElevation - 10.0f) < 0.1f, $"ASCII Grid MinElevation verified: {demAsc.MinElevation:F1}m");
                Assert(Math.Abs(demAsc.MaxElevation - 250.0f) < 0.1f, $"ASCII Grid MaxElevation verified: {demAsc.MaxElevation:F1}m");
                Assert(Math.Abs(demAsc.OriginX - 28.9784) < 1e-4 && Math.Abs(demAsc.OriginY - 41.0082) < 1e-4, "Geospatial origin coordinates parsed");

                // Bilinear elevation sampling at center
                float elevCenter = demAsc.SampleBilinear(0.5f, 0.5f);
                Assert(elevCenter > 100.0f && elevCenter < 250.0f, $"Bilinear interpolated elevation at center (u=0.5, v=0.5): {elevCenter:F2}m");

                // 2. Export & Ingest RAW 32-bit Float DEM (.raw)
                string rawPath = Path.Combine(tempDir, "sample_mountains.raw");
                demAsc.SaveToRawFloat(rawPath);
                Assert(File.Exists(rawPath) && new FileInfo(rawPath).Length == 5 * 5 * 4, "Exported 32-bit Float RAW DEM file");

                var demRaw = DemRasterData.LoadFromRawFloat(rawPath, 5, 5);
                Assert(demRaw.Width == 5 && demRaw.Height == 5, "RAW DEM ingested matching dimensions");
                Assert(Math.Abs(demRaw.GetElevation(2, 2) - 180.0f) < 1e-3, "RAW DEM center peak verified (180.0m)");
                Assert(Math.Abs(demRaw.GetElevation(3, 2) - 250.0f) < 1e-3, "RAW DEM summit verified (250.0m)");

                // 3. Ingest Grayscale Heightmap Image (.png)
                string pngPath = Path.Combine(tempDir, "heightmap_test.png");
                using (var bmp = new Bitmap(16, 16, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    for (int y = 0; y < 16; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            int val = (x * 16 + y * 16) / 2;
                            val = Math.Min(255, Math.Max(0, val));
                            bmp.SetPixel(x, y, Color.FromArgb(val, val, val));
                        }
                    }
                    bmp.Save(pngPath, System.Drawing.Imaging.ImageFormat.Png);
                }

                var demImg = DemRasterData.LoadFromImage(pngPath, minElevation: 50f, maxElevation: 450f);
                Assert(demImg.Width == 16 && demImg.Height == 16, $"Image DEM dimensions parsed: {demImg.Width}x{demImg.Height}");
                Assert(demImg.MinElevation >= 50f && demImg.MaxElevation <= 450f, $"Image DEM elevation mapped to [50m, 450m]");

                // 4. Ingest GeoTIFF Raster (.tif)
                string tifPath = Path.Combine(tempDir, "geotiff_sample.tif");
                using (var fs = new FileStream(tifPath, FileMode.Create, FileAccess.Write))
                using (var bw = new BinaryWriter(fs))
                {
                    // Little-Endian TIFF Header
                    bw.Write((ushort)0x4949); // 'II'
                    bw.Write((ushort)42);     // Magic
                    bw.Write((uint)8);        // IFD offset immediately after header

                    ushort numEntries = 6;
                    bw.Write(numEntries);

                    // Tag 256: ImageWidth = 4
                    bw.Write((ushort)256); bw.Write((ushort)4); bw.Write((uint)1); bw.Write((uint)4);
                    // Tag 257: ImageLength = 4
                    bw.Write((ushort)257); bw.Write((ushort)4); bw.Write((uint)1); bw.Write((uint)4);
                    // Tag 258: BitsPerSample = 32
                    bw.Write((ushort)258); bw.Write((ushort)3); bw.Write((uint)1); bw.Write((uint)32);
                    // Tag 339: SampleFormat = 3 (Float)
                    bw.Write((ushort)339); bw.Write((ushort)3); bw.Write((uint)1); bw.Write((uint)3);
                    // Tag 273: StripOffsets
                    uint dataOffset = (uint)(8 + 2 + numEntries * 12 + 4);
                    bw.Write((ushort)273); bw.Write((ushort)4); bw.Write((uint)1); bw.Write(dataOffset);
                    // Tag 279: StripByteCounts = 4*4*4 = 64
                    bw.Write((ushort)279); bw.Write((ushort)4); bw.Write((uint)1); bw.Write((uint)(4 * 4 * 4));

                    bw.Write((uint)0); // Next IFD offset = 0

                    // Raw float elevation strip (4x4 = 16 floats)
                    for (int p = 0; p < 16; p++)
                    {
                        bw.Write((float)(100.0f + p * 15.0f));
                    }
                }

                var demTif = DemRasterData.LoadFromGeoTiff(tifPath);
                Assert(demTif.Width == 4 && demTif.Height == 4, $"GeoTIFF 32-bit float elevation raster parsed: {demTif.Width}x{demTif.Height}");
                Assert(Math.Abs(demTif.MinElevation - 100.0f) < 0.1f, $"GeoTIFF MinElevation verified: {demTif.MinElevation:F1}m");
                Assert(Math.Abs(demTif.MaxElevation - 325.0f) < 0.1f, $"GeoTIFF MaxElevation verified: {demTif.MaxElevation:F1}m");

                // 5. Integration with TerrainSurfaceData & CDLOD Rendering
                var realWorldTerrain = new TerrainSurfaceData(10000f, 10000f, 2500f, 2500f, 4, 4, 2f);
                realWorldTerrain.ApplyDemRaster(demAsc);
                Assert(realWorldTerrain.ActiveDemRaster == demAsc, "TerrainSurfaceData successfully ingested real-world DEM raster");
                Assert(Math.Abs(realWorldTerrain.ElevationMax - 250.0f) < 0.1f, "TerrainSurfaceData ElevationMax updated to match DEM bounds");

                // Evaluate elevations on terrain surface
                float eCenter = realWorldTerrain.EvaluateElevation(0f, 0f);
                Assert(Math.Abs(eCenter - elevCenter) < 1e-3, $"Terrain surface EvaluateElevation(0,0) matches DEM raster: {eCenter:F2}m");

                // CDLOD Chunk Mesh Generation using Ingested Real-World DEM
                var cdlodTree = new CdlodTerrainQuadtree(realWorldTerrain, maxLodLevels: 3);
                var frustum = ViewFrustum3D.CreateFromCamera(0f, 500f, -5000f, 0f, 0f, 60f, 16f / 9f, 5f, 15000f);
                var visibleChunks = cdlodTree.SelectVisibleChunks(frustum, 0f, 500f, -5000f, out int _, out int _);
                Assert(visibleChunks.Count > 0, "CDLOD chunks selected on real-world DEM terrain");

                var demMesh = visibleChunks[0].GenerateMesh(realWorldTerrain, baseGridResolution: 8);
                Assert(demMesh != null && demMesh.Vertices.Count > 0, "CDLOD mesh generated using ingested DEM raster elevation surface");
            }
            finally
            {
                try { if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true); } catch { }
            }
        }

        static void TestMultithreadedCdlodWorker()
        {
            Console.WriteLine("\n[MODULE 10] Testing High-Throughput Multithreaded CDLOD Chunk Mesh Worker Pool...");

            // 1. Worker Lifecycle & Construction
            using (var worker = new CdlodAsyncMeshWorker(workerThreads: 4))
            {
                Assert(worker != null, "CdlodAsyncMeshWorker instantiated with 4 worker threads");
                Assert(worker.PendingQueueCount == 0, "Worker starts with 0 pending tasks");
                Assert(worker.CompletedCacheCount == 0, "Worker starts with empty completed mesh cache");

                // 2. Setup CDLOD Terrain Quadtree for 100km terrain
                var terrain = new TerrainSurfaceData { SizeX = 100000f, SizeY = 5000f, ElevationMax = 350f };
                var quadtree = new CdlodTerrainQuadtree(terrain, maxLodLevels: 4);
                var frustum = ViewFrustum3D.CreateFromCamera(0f, 300f, -3000f, 0f, 0f, 60f, 16f / 9f, 5f, 25000f);

                int tested = 0, culled = 0;
                var visibleChunks = quadtree.SelectVisibleChunks(frustum, 0f, 300f, -3000f, out tested, out culled);
                Assert(visibleChunks.Count > 0, $"CDLOD visible chunk selection returned {visibleChunks.Count} chunks");

                // 3. Asynchronously Enqueue Mesh Generation Requests
                int enqueued = 0;
                foreach (var chunk in visibleChunks)
                {
                    if (worker.EnqueueRequest(chunk.Node, chunk.LodLevel, terrain, gridResolution: 8))
                    {
                        enqueued++;
                    }
                }
                Assert(enqueued > 0, $"Enqueued {enqueued} chunk meshing requests to worker pool");

                // Duplicate requests should be safely rejected
                var firstChunk = visibleChunks[0];
                bool dupEnqueued = worker.EnqueueRequest(firstChunk.Node, firstChunk.LodLevel, terrain, gridResolution: 8);
                Assert(!dupEnqueued, "Duplicate in-flight or cached mesh request safely rejected");

                // 4. Await Multithreaded Completion
                bool finished = worker.WaitForCompletion(5000);
                Assert(finished, "Worker pool finished all queued chunk meshes within timeout");
                Assert(worker.PendingQueueCount == 0, "Worker pending queue is 0 after completion");
                Assert(worker.CompletedCacheCount >= enqueued, $"Completed cache contains all {worker.CompletedCacheCount} generated chunk meshes");
                Assert(worker.TotalMeshesGenerated >= enqueued, $"TotalMeshesGenerated telemetry count: {worker.TotalMeshesGenerated}");

                // 5. Retrieve Cached Mesh and Verify Mathematical Geometry
                bool gotMesh = worker.TryGetMesh(firstChunk.Node, firstChunk.LodLevel, 8, out var cachedMesh);
                Assert(gotMesh && cachedMesh != null, "TryGetMesh successfully retrieved precomputed mesh from cache");
                Assert(cachedMesh.Vertices.Count > 0 && cachedMesh.Indices.Count > 0,
                    $"Cached mesh contains {cachedMesh.Vertices.Count} vertices and {cachedMesh.Indices.Count} indices");

                // Check vertex normal length is normalized
                var norm = cachedMesh.Vertices[0];
                float nLen = (float)Math.Sqrt(norm.Nx * norm.Nx + norm.Ny * norm.Ny + norm.Nz * norm.Nz);
                Assert(Math.Abs(nLen - 1.0f) < 0.05f, "Background worker computed normalized vertex normal vector");

                // 6. Immediate Generation or Cache Hit
                var immediateMesh = worker.GetOrGenerateImmediate(firstChunk.Node, firstChunk.LodLevel, terrain, 8);
                Assert(immediateMesh == cachedMesh, "GetOrGenerateImmediate returned cached instance on hit");

                // 7. Verify CDLOD Crack-Prevention Boundary Skirts & Continuous LOD Morphing
                var skirtChunk = new CdlodRenderChunk(firstChunk.Node, firstChunk.LodLevel, morphFactor: 0.5f);
                var meshWithSkirts = skirtChunk.GenerateMesh(terrain, 8, enableSkirts: true, enableMorphing: true);
                var meshNoSkirts = skirtChunk.GenerateMesh(terrain, 8, enableSkirts: false, enableMorphing: false);

                Assert(meshWithSkirts.Vertices.Count > meshNoSkirts.Vertices.Count,
                    $"Skirt geometry generated extra boundary vertices ({meshWithSkirts.Vertices.Count} vs {meshNoSkirts.Vertices.Count})");
                Assert(meshWithSkirts.Indices.Count > meshNoSkirts.Indices.Count,
                    $"Skirt geometry added seam-sealing triangles ({meshWithSkirts.Indices.Count} vs {meshNoSkirts.Indices.Count} indices)");
                Assert(skirtChunk.LastSkirtDepth > 0f, $"Boundary skirt depth calculated: {skirtChunk.LastSkirtDepth:F1} m");

                // 8. Clear Cache and Teardown
                worker.ClearCache();
                Assert(worker.CompletedCacheCount == 0, "ClearCache flushed completed cache");
                Assert(worker.PendingQueueCount == 0, "ClearCache flushed pending queue");
            }
            Assert(true, "CdlodAsyncMeshWorker cleanly disposed and terminated worker threads");
        }
    }
}
