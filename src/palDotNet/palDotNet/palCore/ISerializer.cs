using palDotNet.palWidgets;

namespace palDotNet.palCore;

public interface ISerializer
{
	bool AtEnd { get; }

	bool Reading { get; }

	bool Writing { get; }

	int EnterCount { get; }

	void Serialize(ref char inst);

	void Serialize(ref int inst);

	void Serialize(ref uint inst);

	void Serialize(ref long inst);

	void Serialize(ref ulong inst);

	void Serialize(ref bool inst);

	void Serialize(ref float inst);

	void Serialize(ref Vector3 inst);

	void Serialize(ref Vector4 inst);

	void Serialize(ref string inst);

	void SerializeArString(ref string inst);

	void Serialize(ref WidgetBase inst);

	void Serialize(ref Bank inst);

	void Serialize(ref byte[] bytes, int count);

	bool Begin();

	void End(bool clearIndex);
}
