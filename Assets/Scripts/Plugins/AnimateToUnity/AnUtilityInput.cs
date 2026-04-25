using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnUtilityInput
	{
		public static AnUIInputDirectionTypes GetInputDirectionType(Vector3 direction, bool isEightDirection)
		{
			if (direction.sqrMagnitude < 0.001f)
			{
				return AnUIInputDirectionTypes.None;
			}
			direction.Normalize();
			float num = 0.7071f;
			if (isEightDirection)
			{
				num = 0.866f;
			}
			if (direction.y > num)
			{
				return AnUIInputDirectionTypes.Up;
			}
			if (direction.y < -num)
			{
				return AnUIInputDirectionTypes.Down;
			}
			if (direction.x < -num)
			{
				return AnUIInputDirectionTypes.Left;
			}
			if (direction.x > num)
			{
				return AnUIInputDirectionTypes.Right;
			}
			if (direction.y >= 0.5f && direction.y <= 0.866f && isEightDirection)
			{
				if (direction.x > 0f)
				{
					return AnUIInputDirectionTypes.UpperRight;
				}
				return AnUIInputDirectionTypes.UpperLeft;
			}
			else
			{
				if (direction.y > -0.5f || direction.y < -0.866f || !isEightDirection)
				{
					return AnUIInputDirectionTypes.None;
				}
				if (direction.x > 0f)
				{
					return AnUIInputDirectionTypes.DownRight;
				}
				return AnUIInputDirectionTypes.DownLeft;
			}
		}

		public static Vector3 GetInputDirectionVector(AnUIInputDirectionTypes directionType)
		{
			Vector3 zero = Vector3.zero;
			if (directionType == AnUIInputDirectionTypes.None)
			{
				return zero;
			}
			if (directionType == AnUIInputDirectionTypes.Up)
			{
				zero.y = 1f;
			}
			else if (directionType == AnUIInputDirectionTypes.Down)
			{
				zero.y = -1f;
			}
			else if (directionType == AnUIInputDirectionTypes.Left)
			{
				zero.x = -1f;
			}
			else if (directionType == AnUIInputDirectionTypes.Right)
			{
				zero.x = 1f;
			}
			else if (directionType == AnUIInputDirectionTypes.UpperLeft)
			{
				zero.x = -0.7071f;
				zero.y = 0.7071f;
			}
			else if (directionType == AnUIInputDirectionTypes.UpperRight)
			{
				zero.x = 0.7071f;
				zero.y = 0.7071f;
			}
			else if (directionType == AnUIInputDirectionTypes.DownLeft)
			{
				zero.x = -0.7071f;
				zero.y = -0.7071f;
			}
			else if (directionType == AnUIInputDirectionTypes.DownRight)
			{
				zero.x = 0.7071f;
				zero.y = -0.7071f;
			}
			return zero.normalized;
		}
	}
}
