using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnPlaneParameter : AnObjectParameterBase
	{
		public bool FullNineSlice
		{
			get
			{
				return this._fullNineSlice;
			}
			set
			{
				this._fullNineSlice = value;
			}
		}

		public List<string> TextureNameList
		{
			get
			{
				return this._textureNameList;
			}
			set
			{
				this._textureNameList = value;
			}
		}

		public List<Color> VertexColorList
		{
			get
			{
				return this._vertexColorList;
			}
			set
			{
				this._vertexColorList = value;
			}
		}

		public List<Vector2> UVColorList
		{
			get
			{
				return this._uvColorList;
			}
			set
			{
				this._uvColorList = value;
			}
		}

		public List<Vector2> UVAlphaList
		{
			get
			{
				return this._uvAlphaList;
			}
			set
			{
				this._uvAlphaList = value;
			}
		}

		public AnKeyParameter TextureKeyParam
		{
			get
			{
				return this._textureKeyParam;
			}
			set
			{
				this._textureKeyParam = value;
			}
		}

		public override void _Initialize()
		{
			base._Initialize();
			this._gameObjectName = AnValue.PlanePrefix + this._objectName;
			this._ExistAnimation(this._textureKeyParam);
		}

		public override void _CreateEditorData(AnMotion parentMotion)
		{
			base._CreateEditorData(parentMotion);
			if (this._targetGameObject == null)
			{
				return;
			}
			new AnPlane(this._targetGameObject)._CreateEditorData(this, parentMotion);
		}

		public override void _CreateHierarchy(AnRoot root, GameObject parentObject)
		{
			base._CreateHierarchy(root, parentObject);
			this._targetGameObject.name = AnValue.PlanePrefix + base.ObjectName;
			this._attachGameObject.AddComponent<MeshFilter>();
			this._attachGameObject.AddComponent<MeshRenderer>();
		}

		public override void _ApplyData(AnMotion parentMotion)
		{
			base._ApplyData(parentMotion);
			if (this._targetGameObject == null)
			{
				return;
			}
			AnPlane anPlane = new AnPlane(this._targetGameObject);
			anPlane._ApplyData(this, parentMotion);
			parentMotion.Root.ObjectList.Add(anPlane);
			parentMotion.Root.DataTable.Add(this._targetGameObject, anPlane);
			parentMotion.Root.DataList.Add(anPlane);
		}

		public List<string> _textureNameList;

		public bool _fullNineSlice;

		public List<Color> _vertexColorList;

		public List<Vector2> _uvColorList;

		public List<Vector2> _uvAlphaList;

		public AnKeyParameter _textureKeyParam;
	}
}
