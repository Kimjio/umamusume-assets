using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnKeyParameter
	{
		public List<Vector2> KeyList
		{
			get
			{
				return this._keyList;
			}
			set
			{
				this._keyList = value;
			}
		}

		public float _GetValue(float original, AnMotion motion, ref int startIndex)
		{
			if (this._keyCount == 0)
			{
				return original;
			}
			if (motion._fixObjectTime <= this._keyList[0].x)
			{
				startIndex = 0;
				return this._keyList[0].y;
			}
			if (motion._fixObjectTime >= this._keyList[this._keyCount - 1].x)
			{
				startIndex = this._keyCount - 1;
				return this._keyList[this._keyCount - 1].y;
			}
			if (motion._currentTime < motion._prevTime)
			{
				startIndex = 0;
			}
			else if (motion._fixObjectTime < this._keyList[startIndex].x)
			{
				startIndex = 0;
			}
			else if (startIndex >= this._keyCount)
			{
				startIndex = 0;
			}
			else if (startIndex < 0)
			{
				startIndex = 0;
			}
			int num = 0;
			int num2 = startIndex;
			while (num2 < this._keyCount && motion._fixObjectTime >= this._keyList[num2].x)
			{
				num = num2;
				num2++;
			}
			startIndex = num;
			float num3 = this._keyList[num + 1].x - this._keyList[num].x;
			if (num3 < 0.001f)
			{
				return this._keyList[num + 1].y;
			}
			float num4 = motion._fixObjectTime - this._keyList[num].x;
			return this._keyList[num].y + (this._keyList[num + 1].y - this._keyList[num].y) * num4 / num3;
		}

		public List<Vector2> _keyList;

		[NonSerialized]
		public int _keyCount;
	}
}
