using System;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnCustomMeshInfoParameter
	{
		public AnMeshParameter MeshParameter
		{
			get
			{
				return this._meshParameter;
			}
			set
			{
				this._meshParameter = value;
			}
		}

		public string TextureName
		{
			get
			{
				return this._textureName;
			}
			set
			{
				this._textureName = value;
			}
		}

		public AnPrimitiveMeshTypes PrimitiveMeshType
		{
			get
			{
				return this._primitiveMeshType;
			}
			set
			{
				this._primitiveMeshType = value;
			}
		}

		public Mesh CustomMesh
		{
			get
			{
				return this._customMesh;
			}
			set
			{
				this._customMesh = value;
			}
		}

		public Texture TextureColor
		{
			get
			{
				return this._textureColor;
			}
			set
			{
				this._textureColor = value;
			}
		}

		public Texture TextureAlpha
		{
			get
			{
				return this._textureAlpha;
			}
			set
			{
				this._textureAlpha = value;
			}
		}

		public bool KeepMeshSize
		{
			get
			{
				return this._keepMeshSize;
			}
			set
			{
				this._keepMeshSize = value;
			}
		}

		public bool KeepMeshAspect
		{
			get
			{
				return this._keepMeshAspect;
			}
			set
			{
				this._keepMeshAspect = value;
			}
		}

		public bool CullingOn
		{
			get
			{
				return this._cullingOn;
			}
			set
			{
				this._cullingOn = value;
			}
		}

		public bool InvertNormal
		{
			get
			{
				return this._invertNormal;
			}
			set
			{
				this._invertNormal = value;
			}
		}

		public Vector3 PositionOffset
		{
			get
			{
				return this._positionOffset;
			}
			set
			{
				this._positionOffset = value;
			}
		}

		public Vector3 RotateOffset
		{
			get
			{
				return this._rotateOffset;
			}
			set
			{
				this._rotateOffset = value;
			}
		}

		public Vector3 ScaleOffset
		{
			get
			{
				return this._scaleOffset;
			}
			set
			{
				this._scaleOffset = value;
			}
		}

		public Vector2 UVPositionOffset
		{
			get
			{
				return this._uvPositionOffset;
			}
			set
			{
				this._uvPositionOffset = value;
			}
		}

		public Vector2 UVScaleOffset
		{
			get
			{
				return this._uvScaleOffset;
			}
			set
			{
				this._uvScaleOffset = value;
			}
		}

		public float MarginTop
		{
			get
			{
				return this._marginTop;
			}
			set
			{
				this._marginTop = value;
			}
		}

		public float MarginButtom
		{
			get
			{
				return this._marginButtom;
			}
			set
			{
				this._marginButtom = value;
			}
		}

		public float MarginLeft
		{
			get
			{
				return this._marginLeft;
			}
			set
			{
				this._marginLeft = value;
			}
		}

		public float MarginRight
		{
			get
			{
				return this._marginRight;
			}
			set
			{
				this._marginRight = value;
			}
		}

		[NonSerialized]
		private AnMeshParameter _meshParameter;

		public string _textureName;

		public AnPrimitiveMeshTypes _primitiveMeshType;

		public Mesh _customMesh;

		public Texture _textureColor;

		public Texture _textureAlpha;

		public Vector3 _positionOffset = Vector3.zero;

		public Vector3 _rotateOffset = Vector3.zero;

		public Vector3 _scaleOffset = Vector3.zero;

		public Vector2 _uvPositionOffset = Vector3.zero;

		public Vector2 _uvScaleOffset = Vector3.zero;

		public bool _cullingOn;

		public bool _invertNormal;

		public bool _keepMeshSize;

		public bool _keepMeshAspect;

		public float _marginTop;

		public float _marginButtom;

		public float _marginRight;

		public float _marginLeft;
	}
}
