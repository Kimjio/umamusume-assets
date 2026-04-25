using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnUtilityVector
	{
		public static bool IsSameVector(Vector3 src, Vector3 dst)
		{
			return src.x == dst.x && src.y == dst.y && src.z == dst.z;
		}

		public static void MultiplyVector2(ref Vector2 src, Vector2 dst)
		{
			src.x *= dst.x;
			src.y *= dst.y;
		}

		public static void MultiplyVector3(ref Vector3 src, Vector3 dst)
		{
			src.x *= dst.x;
			src.y *= dst.y;
			src.z *= dst.z;
		}

		public static void GetFixScreenPosition(Vector3 screenPosition, ref Vector3 target)
		{
			target.x = (screenPosition.x - AnMonoSingleton<AnRootManager>.Instance.ScreenWidth * 0.5f) / AnMonoSingleton<AnRootManager>.Instance.ScreenWidth * AnMonoSingleton<AnRootManager>.Instance._GetBaseScreenWidth();
			target.y = (screenPosition.y - AnMonoSingleton<AnRootManager>.Instance.ScreenHeight * 0.5f) / AnMonoSingleton<AnRootManager>.Instance.ScreenWidth * AnMonoSingleton<AnRootManager>.Instance._GetBaseScreenWidth();
			target.z = 0f;
		}

		public static void GetScreenPosition(Vector3 fixScreenPosition, ref Vector3 target)
		{
			target.x = fixScreenPosition.x * AnMonoSingleton<AnRootManager>.Instance.ScreenWidth / AnMonoSingleton<AnRootManager>.Instance._GetBaseScreenWidth() + AnMonoSingleton<AnRootManager>.Instance.ScreenWidth * 0.5f;
			target.y = fixScreenPosition.y * AnMonoSingleton<AnRootManager>.Instance.ScreenWidth / AnMonoSingleton<AnRootManager>.Instance._GetBaseScreenWidth() + AnMonoSingleton<AnRootManager>.Instance.ScreenHeight * 0.5f;
			target.z = 0f;
		}

		public static Vector3 GetWorldPositionFromScreen(Vector3 screenPosition, Camera camera)
		{
			if (camera == null)
			{
				return Vector3.zero;
			}
			return camera.ScreenToWorldPoint(screenPosition);
		}

		public static void GetWorldPositionFromScreen(Vector3 screenPosition, Camera camera, ref Vector3 worldPosition)
		{
			worldPosition.x = 0f;
			worldPosition.y = 0f;
			worldPosition.z = 0f;
			if (camera == null)
			{
				return;
			}
			worldPosition = camera.ScreenToWorldPoint(screenPosition);
		}

		public static Vector2 Rotate2DPosition(Vector2 positon, Vector2 centerPosition, float degree)
		{
			float num = degree * 0.017453292f;
			float num2 = Mathf.Sin(num);
			float num3 = Mathf.Cos(num);
			positon -= centerPosition;
			positon.x = positon.x * num3 - positon.y * num2;
			positon.y = positon.x * num2 + positon.y * num3;
			positon += centerPosition;
			return positon;
		}

		public static void GetOrthogonalProjectionVector(Vector3 firstVector, Vector3 secondVector, ref Vector3 result)
		{
			result.x = 0f;
			result.y = 0f;
			result.z = 0f;
			float magnitude = firstVector.magnitude;
			if (magnitude == 0f)
			{
				return;
			}
			float num = Vector3.Dot(firstVector, secondVector);
			result = num / magnitude * firstVector;
		}
	}
}
