using System;

namespace palDotNet.palCore;

public struct Vector4(float theX, float theY, float theZ, float theW) : IEquatable<Vector4>
{
	public float x = theX;

	public float y = theY;

	public float z = theZ;

	public float w = theW;

	public bool Equals(Vector4 that)
	{
		if (x == that.x && y == that.y && z == that.z)
		{
			return w == that.w;
		}
		return false;
	}

	public override string ToString()
	{
		return x + "," + y + "," + z + "," + w;
	}
}
