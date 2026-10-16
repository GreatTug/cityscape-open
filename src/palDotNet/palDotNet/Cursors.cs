using System.Windows.Forms;

namespace palDotNet;

public static class Cursors
{
	public enum EnumCursor
	{
		eAppStarting,
		eArrow,
		eCross,
		eDefault,
		eHand,
		eHelp,
		eHSplit,
		eIBeam,
		eNo,
		eNoMove2D,
		eNoMoveHoriz,
		eNoMoveVert,
		ePanEast,
		ePanNE,
		ePanNorth,
		ePanNW,
		ePanSE,
		ePanSouth,
		ePanSW,
		ePanWest,
		eSizeAll,
		eSizeNESW,
		eSizeNS,
		eSizeNWSE,
		eSizeWE,
		eUpArrow,
		eVSplit,
		eWaitCursor,
		ePanHand,
		ePanHandDown,
		eZoomIn,
		eZoomOut
	}

	public static void SetCurrent(Control control, EnumCursor cursor)
	{
		switch (cursor)
		{
		case EnumCursor.eAppStarting:
			control.Cursor = System.Windows.Forms.Cursors.AppStarting;
			break;
		case EnumCursor.eArrow:
			control.Cursor = System.Windows.Forms.Cursors.Arrow;
			break;
		case EnumCursor.eCross:
			control.Cursor = System.Windows.Forms.Cursors.Cross;
			break;
		case EnumCursor.eDefault:
			control.Cursor = System.Windows.Forms.Cursors.Default;
			break;
		case EnumCursor.eHand:
			control.Cursor = System.Windows.Forms.Cursors.Hand;
			break;
		case EnumCursor.eHelp:
			control.Cursor = System.Windows.Forms.Cursors.Help;
			break;
		case EnumCursor.eHSplit:
			control.Cursor = System.Windows.Forms.Cursors.HSplit;
			break;
		case EnumCursor.eIBeam:
			control.Cursor = System.Windows.Forms.Cursors.IBeam;
			break;
		case EnumCursor.eNo:
			control.Cursor = System.Windows.Forms.Cursors.No;
			break;
		case EnumCursor.eNoMove2D:
			control.Cursor = System.Windows.Forms.Cursors.NoMove2D;
			break;
		case EnumCursor.eNoMoveHoriz:
			control.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
			break;
		case EnumCursor.eNoMoveVert:
			control.Cursor = System.Windows.Forms.Cursors.NoMoveVert;
			break;
		case EnumCursor.ePanEast:
			control.Cursor = System.Windows.Forms.Cursors.PanEast;
			break;
		case EnumCursor.ePanNE:
			control.Cursor = System.Windows.Forms.Cursors.PanNE;
			break;
		case EnumCursor.ePanNorth:
			control.Cursor = System.Windows.Forms.Cursors.PanNorth;
			break;
		case EnumCursor.ePanNW:
			control.Cursor = System.Windows.Forms.Cursors.PanNW;
			break;
		case EnumCursor.ePanSE:
			control.Cursor = System.Windows.Forms.Cursors.PanSE;
			break;
		case EnumCursor.ePanSouth:
			control.Cursor = System.Windows.Forms.Cursors.PanSouth;
			break;
		case EnumCursor.ePanSW:
			control.Cursor = System.Windows.Forms.Cursors.PanSW;
			break;
		case EnumCursor.ePanWest:
			control.Cursor = System.Windows.Forms.Cursors.PanWest;
			break;
		case EnumCursor.eSizeAll:
			control.Cursor = System.Windows.Forms.Cursors.SizeAll;
			break;
		case EnumCursor.eSizeNESW:
			control.Cursor = System.Windows.Forms.Cursors.SizeNESW;
			break;
		case EnumCursor.eSizeNS:
			control.Cursor = System.Windows.Forms.Cursors.SizeNS;
			break;
		case EnumCursor.eSizeNWSE:
			control.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			break;
		case EnumCursor.eSizeWE:
			control.Cursor = System.Windows.Forms.Cursors.SizeWE;
			break;
		case EnumCursor.eUpArrow:
			control.Cursor = System.Windows.Forms.Cursors.UpArrow;
			break;
		case EnumCursor.eVSplit:
			control.Cursor = System.Windows.Forms.Cursors.VSplit;
			break;
		case EnumCursor.eWaitCursor:
			control.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			break;
		case EnumCursor.ePanHand:
			control.Cursor = new Cursor(typeof(Cursors), "Cursors.pan.cur");
			break;
		case EnumCursor.ePanHandDown:
			control.Cursor = new Cursor(typeof(Cursors), "Cursors.pandown.cur");
			break;
		case EnumCursor.eZoomIn:
			control.Cursor = new Cursor(typeof(Cursors), "Cursors.zoomin.cur");
			break;
		case EnumCursor.eZoomOut:
			control.Cursor = new Cursor(typeof(Cursors), "Cursors.zoomout.cur");
			break;
		}
	}
}
