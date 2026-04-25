using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnMeshInfoParameter
	{
		public AnMeshInfoParameterGroup MeshInfoParameterGroup
		{
			get
			{
				return this._meshInfoParameterGroup;
			}
			set
			{
				this._meshInfoParameterGroup = value;
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

		public string FixTextureName
		{
			get
			{
				return this._fixTextureName;
			}
			set
			{
				this._fixTextureName = value;
			}
		}

		public Vector2 Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		public Vector2 Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		public Vector2 UVSize
		{
			get
			{
				return this._uvSize;
			}
			set
			{
				this._uvSize = value;
			}
		}

		public Vector2 UVOffset
		{
			get
			{
				return this._uvOffset;
			}
			set
			{
				this._uvOffset = value;
			}
		}

		public bool Rotated
		{
			get
			{
				return this._rotated;
			}
			set
			{
				this._rotated = value;
			}
		}

		public AnMeshTypes MeshType
		{
			get
			{
				return this._meshType;
			}
			set
			{
				this._meshType = value;
			}
		}

		public Vector4 SliceRange
		{
			get
			{
				return this._sliceRange;
			}
			set
			{
				this._sliceRange = value;
			}
		}

		public Vector3[] Vertices
		{
			get
			{
				return this._baseMeshVertices;
			}
			set
			{
				this._baseMeshVertices = value;
			}
		}

		public Vector3[] CustomMeshVertices
		{
			get
			{
				return this._baseCustomMeshVertices;
			}
			set
			{
				this._baseCustomMeshVertices = value;
			}
		}

		public void _Initialize()
		{
			this._baseMesh = null;
			this._baseCustomMesh = null;
		}

		public Mesh _CreateMesh(List<Mesh> meshList, bool useCustomMesh)
		{
			if (useCustomMesh)
			{
				AnCustomMeshInfoParameter anCustomMeshInfoParameter = this._meshInfoParameterGroup.MeshParameter._GetCustomMeshInfoParam(this._fixTextureName);
				if (anCustomMeshInfoParameter != null)
				{
					if (this._baseCustomMesh == null)
					{
						Mesh mesh;
						if (anCustomMeshInfoParameter.CustomMesh == null)
						{
							mesh = AnUtilityMesh.GetPrimitiveMesh(anCustomMeshInfoParameter.PrimitiveMeshType);
						}
						else
						{
							mesh = anCustomMeshInfoParameter.CustomMesh;
						}
						if (mesh != null)
						{
							this._baseCustomMesh = AnUtilityMesh.CloneMesh(mesh);
							AnUtilityMesh.FixMeshColorAndUV2AndUV3(this._baseCustomMesh);
							if (!anCustomMeshInfoParameter.KeepMeshSize)
							{
								AnUtilityMesh.FixMeshSize(this._baseCustomMesh, this._size, anCustomMeshInfoParameter.MarginTop, anCustomMeshInfoParameter.MarginButtom, anCustomMeshInfoParameter.MarginRight, anCustomMeshInfoParameter.MarginLeft, anCustomMeshInfoParameter.KeepMeshAspect);
							}
							AnUtilityMesh.OffsetMesh(this._baseCustomMesh, anCustomMeshInfoParameter.PositionOffset, anCustomMeshInfoParameter.RotateOffset, anCustomMeshInfoParameter.ScaleOffset);
							if (anCustomMeshInfoParameter.InvertNormal)
							{
								AnUtilityMesh.InvertMeshNormal(this._baseCustomMesh);
							}
							if (anCustomMeshInfoParameter.TextureColor == null)
							{
								AnUtilityMesh.FixMeshUV(this._baseCustomMesh, this._uvOffset, this._uvSize, this._rotated, this._meshInfoParameterGroup.TextureSetSize, this._size, anCustomMeshInfoParameter.MarginTop, anCustomMeshInfoParameter.MarginButtom, anCustomMeshInfoParameter.MarginRight, anCustomMeshInfoParameter.MarginLeft);
							}
							if (anCustomMeshInfoParameter.UVPositionOffset != Vector2.zero || anCustomMeshInfoParameter.UVScaleOffset != Vector2.zero)
							{
								AnUtilityMesh.OffsetMeshUV(this._baseCustomMesh, anCustomMeshInfoParameter.UVPositionOffset, anCustomMeshInfoParameter.UVScaleOffset + Vector2.one);
							}
							this._baseCustomMeshVertices = this._baseCustomMesh.vertices;
							meshList.Add(this._baseCustomMesh);
						}
					}
					if (this._baseCustomMesh != null)
					{
						Mesh mesh2 = AnUtilityMesh.CloneMesh(this._baseCustomMesh);
						Mesh mesh3 = mesh2;
						mesh3.name += AnValue.CustomMeshString;
						Mesh mesh4 = mesh2;
						mesh4.name += AnValue.CloneString;
						meshList.Add(mesh2);
						return mesh2;
					}
				}
			}
			if (this._baseMesh == null)
			{
				if (this._meshType == AnMeshTypes.NineSlice)
				{
					this._baseMesh = AnUtilityMesh.CreateNinesliceMesh(this._fixTextureName, this._size, Vector2.zero, this._uvOffset, this._uvSize, this._rotated, this._sliceRange);
				}
				if (this._baseMesh == null)
				{
					this._baseMesh = AnUtilityMesh.CreateMesh(this._fixTextureName, this._size, Vector2.zero, this._uvOffset, this._uvSize, this._rotated);
				}
				this._baseMeshVertices = this._baseMesh.vertices;
				meshList.Add(this._baseMesh);
			}
			Mesh mesh5 = AnUtilityMesh.CloneMesh(this._baseMesh);
			if (this._meshType == AnMeshTypes.NineSlice)
			{
				Mesh mesh6 = mesh5;
				mesh6.name += AnValue.NineSliceMeshString;
			}
			else
			{
				Mesh mesh7 = mesh5;
				mesh7.name += AnValue.NormalMeshString;
			}
			Mesh mesh8 = mesh5;
			mesh8.name += AnValue.CloneString;
			meshList.Add(mesh5);
			return mesh5;
		}

		public Vector2[] _GetUVList()
		{
			Vector2[] array = new Vector2[4];
			if (!this._rotated)
			{
				array[0] = new Vector2(0f, 0f) + this._uvOffset;
				array[1] = new Vector2(this._uvSize.x, this._uvSize.y) + this._uvOffset;
				array[2] = new Vector2(this._uvSize.x, 0f) + this._uvOffset;
				array[3] = new Vector2(0f, this._uvSize.y) + this._uvOffset;
			}
			else
			{
				array[0] = new Vector2(0f, this._uvSize.y) + this._uvOffset;
				array[1] = new Vector2(this._uvSize.x, 0f) + this._uvOffset;
				array[2] = new Vector2(0f, 0f) + this._uvOffset;
				array[3] = new Vector2(this._uvSize.x, this._uvSize.y) + this._uvOffset;
			}
			return array;
		}

		public void _Destroy()
		{
			if (Application.isPlaying)
			{
				if (this._baseMesh != null)
				{
					global::UnityEngine.Object.Destroy(this._baseMesh);
					this._baseMesh = null;
				}
				if (this._baseCustomMesh != null)
				{
					global::UnityEngine.Object.Destroy(this._baseCustomMesh);
					this._baseCustomMesh = null;
					return;
				}
			}
			else
			{
				if (this._baseMesh != null)
				{
					global::UnityEngine.Object.DestroyImmediate(this._baseMesh);
					this._baseMesh = null;
				}
				if (this._baseCustomMesh != null)
				{
					global::UnityEngine.Object.DestroyImmediate(this._baseCustomMesh);
					this._baseCustomMesh = null;
				}
			}
		}

		[NonSerialized]
		private AnMeshInfoParameterGroup _meshInfoParameterGroup;

		public string _textureName;

		public string _fixTextureName;

		public Vector2 _size = Vector2.zero;

		public Vector2 _offset = Vector2.zero;

		public Vector2 _uvSize = Vector2.zero;

		public Vector2 _uvOffset = Vector2.zero;

		public bool _rotated;

		public AnMeshTypes _meshType;

		public Vector4 _sliceRange;

		[NonSerialized]
		private Mesh _baseMesh;

		[NonSerialized]
		public Vector3[] _baseMeshVertices;

		[NonSerialized]
		private Mesh _baseCustomMesh;

		[NonSerialized]
		public Vector3[] _baseCustomMeshVertices;
	}
}
