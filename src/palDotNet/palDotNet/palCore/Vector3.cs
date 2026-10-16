using System;

namespace palDotNet.palCore;

public struct Vector3(float theX, float theY, float theZ) : IEquatable<Vector3>
{
	public float x = theX;

	public float y = theY;

	public float z = theZ;

	public bool Equals(Vector3 that)
	{
		if (x == that.x && y == that.y)
		{
			return z == that.z;
		}
		return false;
	}

	public override string ToString()
	{
		return x + "," + y + "," + z;
	}
}
