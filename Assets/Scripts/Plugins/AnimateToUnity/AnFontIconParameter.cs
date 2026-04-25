using System;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnFontIconParameter
	{
		public Texture ColorTexture
		{
			get
			{
				return this._colorTexture;
			}
		}

		public Texture AlphaTexture
		{
			get
			{
				return this._alphaTexture;
			}
		}

		[SerializeField]
		private Texture _colorTexture;

		[SerializeField]
		private Texture _alphaTexture;
	}
}
