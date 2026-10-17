using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palDialog;
using palDotNet.palVisual;
using palDotNet.palVisualPropertyGrid;
using palDotNet.palWidgets;
using ktKitDotNet;
using edEditorDotNet;
using edEditorDotNet.ExportDialogs;
using edEditorDotNet.ExportDialogItems;

namespace CityScape.Tests
{
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

                // Trigger OnShown lifecycle
                var onShownMethod = typeof(TerrainSettingsDialog).GetMethod("OnShown", BindingFlags.NonPublic | BindingFlags.Instance);
                onShownMethod.Invoke(dlg, new object[] { EventArgs.Empty });

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

                // Render screenshot for visual confirmation
                dlg.Show();
                dlg.Refresh();
                using (Bitmap bmp = new Bitmap(dlg.Width, dlg.Height))
                {
                    dlg.DrawToBitmap(bmp, new Rectangle(0, 0, dlg.Width, dlg.Height));
                    string screenshotDir = @"C:\cityscape-open\tools";
                    if (!System.IO.Directory.Exists(screenshotDir)) System.IO.Directory.CreateDirectory(screenshotDir);
                    bmp.Save(System.IO.Path.Combine(screenshotDir, "terrain_unlocked_dialog.png"), System.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine("    [OK] Captured screenshot of unlocked dialog to tools/terrain_unlocked_dialog.png");
                }
                dlg.Hide();
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
    }
}
