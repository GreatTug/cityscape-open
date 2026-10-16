using System.Globalization;
using System.Threading;

namespace palDotNet.palCore;

internal class Thread
{
	public readonly System.Threading.Thread Inst;

	public Thread(ThreadStart threadStart)
	{
		Inst = new System.Threading.Thread(threadStart);
		Inst.CurrentCulture = new CultureInfo("en-US");
		Inst.CurrentUICulture = new CultureInfo("en-US");
	}
}
