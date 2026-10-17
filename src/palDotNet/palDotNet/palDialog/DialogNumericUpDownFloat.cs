using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogNumericUpDownFloat : DialogNumericUpDown, IDialogValue
{
	[DialogValueType(EnumDialogValueTypes.eFloat)]
	[ReadOnly(true)]
	public object DialogValue
	{
		get
		{
			return base.Value;
		}
		set
		{
			decimal val = Convert.ToDecimal(value);
			val = Math.Max(base.Minimum, val);
			val = Math.Min(base.Maximum, val);
			base.Value = val;
		}
	}

	private bool mForceEnabled = false;

	[DefaultValue(false)]
	public bool ForceEnabled
	{
		get => mForceEnabled;
		set
		{
			mForceEnabled = value;
			if (value)
			{
				base.Enabled = true;
				base.ReadOnly = false;
			}
		}
	}

	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
		if (mForceEnabled && !base.Enabled)
		{
			base.Enabled = true;
		}
	}

	public new bool Enabled
	{
		get => base.Enabled;
		set
		{
			if (mForceEnabled)
			{
				base.Enabled = true;
			}
			else
			{
				base.Enabled = value;
			}
		}
	}

	public new bool ReadOnly
	{
		get => base.ReadOnly;
		set
		{
			if (mForceEnabled)
			{
				base.ReadOnly = false;
			}
			else
			{
				base.ReadOnly = value;
			}
		}
	}

	public new decimal Minimum
	{
		get => base.Minimum;
		set
		{
			if (mForceEnabled && value <= 0m)
			{
				base.Minimum = 0.01m;
			}
			else
			{
				base.Minimum = value;
			}
		}
	}

	public new decimal Maximum
	{
		get => base.Maximum;
		set
		{
			if (mForceEnabled && value < 100000m)
			{
				base.Maximum = 100000m;
			}
			else
			{
				base.Maximum = value;
			}
		}
	}

	public override void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		string caption = "script command: " + Environment.NewLine + "'" + dialogName + " -" + varName + " <float>'";
		toolTip.SetToolTip(this, caption);
	}
}
