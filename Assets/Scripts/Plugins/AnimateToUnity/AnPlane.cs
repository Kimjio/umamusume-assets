using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnPlane : AnObjectBase
	{
		public Texture OuterTextureColor
		{
			get
			{
				return this._outerTextureColor;
			}
		}

		public AnPlaneParameter PlaneParameter
		{
			get
			{
				return this._planeParam;
			}
		}

		public MeshRenderer MeshRenderer
		{
			get
			{
				return this._meshRenderer;
			}
		}

		public MeshFilter MeshFilter
		{
			get
			{
				return this._meshFilter;
			}
		}

		public Mesh[] MeshList
		{
			get
			{
				return this._meshList;
			}
		}

		public Vector2[] UVColorInfoList
		{
			get
			{
				return this._uvColorInfoList;
			}
		}

		public Vector2[] UVAlphaInfoList
		{
			get
			{
				return this._uvAlphaInfoList;
			}
		}

		public Color[] VertexColorList
		{
			get
			{
				return this._vertexColorList;
			}
		}

		public float FillValue
		{
			get
			{
				return this._currentFillValue;
			}
		}

		public AnFillType FillType
		{
			get
			{
				return this._fillType;
			}
		}

		public Material CurrentMaterial
		{
			get
			{
				return this._currentMaterial;
			}
		}

		public Material[] CurrentMaterialList
		{
			get
			{
				return this._currentMaterialList;
			}
		}

		public AnPlane(GameObject gameObject)
			: base(gameObject)
		{
		}

		public override void _CreateEditorData(AnObjectParameterBase parameter, AnMotion parentMotion)
		{
			base._CreateEditorData(parameter, parentMotion);
			this._planeParam = this._parameter as AnPlaneParameter;
			AnMeshInfoParameter anMeshInfoParameter = null;
			Mesh mesh = null;
			Material material = null;
			this._parentMotion.Root.MeshParameterGroup._CreateMesh(this._planeParam.TextureNameList[0], this._root.Parameter.UseCustomMesh, ref anMeshInfoParameter, ref mesh);
			this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[0], AnShaderTypes.Normal, 0, 0, AnStencilCompareFuncTypes.Disabled, this._root.Parameter.UseCustomMesh, ref material);
			this._meshRenderer = this._offsetObject.GetComponent<MeshRenderer>();
			this._meshFilter = this._offsetObject.GetComponent<MeshFilter>();
			this._meshRenderer.enabled = true;
			this._meshRenderer.material = material;
			this._meshFilter.mesh = mesh;
		}

		public override void _ApplyData(AnObjectParameterBase parameter, AnMotion parentMotion)
		{
			base._ApplyData(parameter, parentMotion);
			this._planeParam = this._parameter as AnPlaneParameter;
		}

		public override void _CreateData()
		{
			base._CreateData();
			this._CreateMeshAndMaterialList();
			if (!this._existMesh)
			{
				return;
			}
			this._CreateUVList();
			this._CreateVertexColorList();
			this._meshRenderer = this._parentMotion.Root.MeshRendererTable[this._offsetObject] as MeshRenderer;
			AnUtilityObject.SetMeshRendererDefaultValue(this._meshRenderer);
			this._meshFilter = this._parentMotion.Root.MeshFilterTable[this._offsetObject] as MeshFilter;
			this._outerTextureVertexPositionList = this._meshList[this._currentMeshIndex].vertices.Clone() as Vector3[];
			this._outerTextureKeepSize = false;
			this._root.SortOrderCount += this._root.SortOrderInterval;
			this._sortOrderIndex = this._root.SortOrderCount;
			this._root.SortOrderCountForDrawTextLater++;
			this._sortOrderIndexForDrawTextLater = this._root.SortOrderCountForDrawTextLater;
			this._fillType = AnFillType.None;
			this._changeFillValue = false;
			this._currentFillValue = 1f;
			this._prevFillValue = float.MinValue;
		}

		private void _CreateMeshAndMaterialList()
		{
			this._meshParamList = new AnMeshInfoParameter[this._planeParam.TextureNameList.Count];
			this._meshTypeList = new AnMeshTypes[this._planeParam.TextureNameList.Count];
			this._meshList = new Mesh[this._planeParam.TextureNameList.Count];
			this._vertexPositionListGroup = new Vector3[this._planeParam.TextureNameList.Count][];
			this._currentMaterialList = new Material[this._planeParam.TextureNameList.Count];
			this._currentMaterialIDList = new int[this._planeParam.TextureNameList.Count];
			this._isCurrentMaterialShared = false;
			this._existMesh = true;
			this._currentMeshIndex = 0;
			this._prevMeshIndex = int.MaxValue;
			this._prevMaterialID = 0;
			for (int i = 0; i < this._planeParam.TextureNameList.Count; i++)
			{
				AnMeshInfoParameter anMeshInfoParameter = null;
				Mesh mesh = null;
				if (!this._parentMotion.Root.MeshParameterGroup._CreateMesh(this._planeParam.TextureNameList[i], this._root.Parameter.UseCustomMesh, ref anMeshInfoParameter, ref mesh))
				{
					this._existMesh = false;
					return;
				}
				this._meshParamList[i] = anMeshInfoParameter;
				this._meshList[i] = mesh;
				this._meshList[i].bounds = new Bounds(Vector3.zero, new Vector3(this._parameter.Size.x * this._parameter.Scale.x, this._parameter.Size.y * this._parameter.Scale.y, 0.0001f));
				if (mesh.name.Contains(AnValue.NormalMeshString))
				{
					this._meshTypeList[i] = AnMeshTypes.Normal;
					this._vertexPositionListGroup[i] = anMeshInfoParameter.Vertices.Clone() as Vector3[];
				}
				else if (mesh.name.Contains(AnValue.NineSliceMeshString))
				{
					this._meshTypeList[i] = AnMeshTypes.NineSlice;
					this._vertexPositionListGroup[i] = anMeshInfoParameter.Vertices.Clone() as Vector3[];
				}
				else if (mesh.name.Contains(AnValue.CustomMeshString))
				{
					this._meshTypeList[i] = AnMeshTypes.Mesh;
					this._vertexPositionListGroup[i] = anMeshInfoParameter.CustomMeshVertices.Clone() as Vector3[];
				}
				else
				{
					this._meshTypeList[i] = AnMeshTypes.Normal;
					this._vertexPositionListGroup[i] = anMeshInfoParameter.Vertices.Clone() as Vector3[];
				}
			}
		}

		private void _CreateUVList()
		{
			this._uvChanged = false;
			this._uvColorInfoList = new Vector2[3];
			this._uvColorChanged = true;
			if (this._planeParam.UVColorList.Count != 3)
			{
				this._uvColorInfoList[0] = Vector2.one;
				this._uvColorInfoList[1] = Vector2.zero;
				this._uvColorInfoList[2] = Vector2.zero;
			}
			else
			{
				this._uvColorInfoList[0] = this._planeParam.UVColorList[0];
				this._uvColorInfoList[1] = this._planeParam.UVColorList[1];
				this._uvColorInfoList[2] = this._planeParam.UVColorList[2];
			}
			this._uvAlphaInfoList = new Vector2[3];
			this._uvAlphaChanged = false;
			if (this._planeParam.UVAlphaList.Count != 3)
			{
				this._uvAlphaInfoList[0] = Vector2.one;
				this._uvAlphaInfoList[1] = Vector2.zero;
				this._uvAlphaInfoList[2] = Vector2.zero;
				return;
			}
			this._uvAlphaInfoList[0] = this._planeParam.UVAlphaList[0];
			this._uvAlphaInfoList[1] = this._planeParam.UVAlphaList[1];
			this._uvAlphaInfoList[2] = this._planeParam.UVAlphaList[2];
		}

		private void _CreateVertexColorList()
		{
			if (this._meshTypeList[0] == AnMeshTypes.Normal || this._meshTypeList[0] == AnMeshTypes.NineSlice)
			{
				this._vertexColorList = AnUtilityColor.CreateColorList(this._meshList[0].colors.Length, new Color(1f, 1f, 1f, 1f));
				if (this._planeParam.VertexColorList.Count == 4)
				{
					Color[] array = this._planeParam.VertexColorList.ToArray();
					this._vertexColorList[0] = array[3];
					this._vertexColorList[1] = array[1];
					this._vertexColorList[2] = array[2];
					this._vertexColorList[3] = array[0];
				}
			}
			else
			{
				this._vertexColorList = new Color[this._meshList[0].colors.Length];
				for (int i = 0; i < this._meshList[0].colors.Length; i++)
				{
					this._vertexColorList[i] = this._meshList[0].colors[i];
				}
			}
			this._currentVertexColorList = new Color[this._meshList[0].colors.Length];
			this._currentVertexColorOffsetList0 = new Vector2[this._meshList[0].colors.Length];
			this._currentVertexColorOffsetList1 = new Vector2[this._meshList[0].colors.Length];
		}

		public override void _FixData()
		{
			if (!this._existMesh)
			{
				return;
			}
			base._FixData();
			this._UpdateMesh();
			this._SetMaterial();
		}

		private void _SetMaterial()
		{
			if (!this._existMesh)
			{
				return;
			}
			this._UpdateCurrentMaterialList();
			this._SetParameterToMaterial();
			this._UpdateCurrentMaterial();
			this._UpdateMeshRendererMaterial();
		}

		private void _SetParameterToMaterial()
		{
			if (!this._existMesh)
			{
				return;
			}
			this._SetUVParameter();
			this._SetBlurParameter();
		}

		private void _SetUVParameter()
		{
			if (!this._existMesh)
			{
				return;
			}
			this._uvChanged = false;
			if (this._isCurrentMaterialShared)
			{
				return;
			}
			if (!this._IsDefaultUVList(this._uvColorInfoList) || !this._IsDefaultUVList(this._uvAlphaInfoList))
			{
				this._uvChanged = true;
			}
			if (this._uvColorInfoList[2] != Vector2.zero || this._uvAlphaInfoList[2] != Vector2.zero)
			{
				this._uvChanged = true;
			}
			if (!this._meshParamList[this._currentMeshIndex].Rotated)
			{
				AnBase._tempVector4_0.x = this._uvColorInfoList[0].x;
				AnBase._tempVector4_0.y = this._uvColorInfoList[0].y;
				AnBase._tempVector4_0.z = this._uvColorInfoList[1].x;
				AnBase._tempVector4_0.w = this._uvColorInfoList[1].y;
				this._currentMaterial.SetVector(AnValue.ShaderParamUVColorInfo, AnBase._tempVector4_0);
				AnBase._tempVector4_0.x = this._uvAlphaInfoList[0].x;
				AnBase._tempVector4_0.y = this._uvAlphaInfoList[0].y;
				AnBase._tempVector4_0.z = this._uvAlphaInfoList[1].x;
				AnBase._tempVector4_0.w = this._uvAlphaInfoList[1].y;
				this._currentMaterial.SetVector(AnValue.ShaderParamUVAlphaInfo, AnBase._tempVector4_0);
			}
			AnBase._tempVector4_0.x = this._uvColorInfoList[0].x;
			AnBase._tempVector4_0.y = this._uvColorInfoList[0].y;
			AnBase._tempVector4_0.z = this._uvColorInfoList[1].x;
			AnBase._tempVector4_0.w = -this._uvColorInfoList[1].y;
			this._currentMaterial.SetVector(AnValue.ShaderParamUVColorInfo, AnBase._tempVector4_0);
			AnBase._tempVector4_0.x = this._uvAlphaInfoList[0].x;
			AnBase._tempVector4_0.y = this._uvAlphaInfoList[0].y;
			AnBase._tempVector4_0.z = this._uvAlphaInfoList[1].x;
			AnBase._tempVector4_0.w = -this._uvAlphaInfoList[1].y;
			this._currentMaterial.SetVector(AnValue.ShaderParamUVAlphaInfo, AnBase._tempVector4_0);
		}

		private void _SetBlurParameter()
		{
			if (!this._existMesh)
			{
				return;
			}
			if (this._currentBlurQuality <= 0)
			{
				return;
			}
			if (this._currentBlurPrecision <= 0)
			{
				return;
			}
			if (this._currentBlurValue.x == 0f && this._currentBlurValue.y == 0f)
			{
				return;
			}
			if (this._isCurrentMaterialShared)
			{
				return;
			}
			this._currentMaterial.SetInt(AnValue.ShaderParamBlurQuality, AnUtilityValue.GetLimitValue(this._currentBlurQuality, 0, 3));
			List<float[]> list = AnMonoSingleton<AnRootManager>.Instance._GetGaussianBlurValue(this._currentBlurQuality, this._currentBlurPrecision);
			if (list != null)
			{
				this._currentMaterial.SetFloatArray(AnValue.ShaderParamBlurOffsetListX, list[0]);
				this._currentMaterial.SetFloatArray(AnValue.ShaderParamBlurOffsetListY, list[1]);
				this._currentMaterial.SetFloatArray(AnValue.ShaderParamBlurWeightList, list[2]);
			}
		}

		private bool _IsDefaultUVList(Vector2[] uvList)
		{
			return this._existMesh && !(uvList[0] != Vector2.one) && !(uvList[1] != Vector2.zero) && !(uvList[2] != Vector2.zero);
		}

		public override void _UpdateFirst()
		{
			if (!this._existMesh)
			{
				return;
			}
			base._UpdateFirst();
			if (!this._visibleInHierarchy || !this._visibleByAlpha)
			{
				this._UpdateEnableRenderer(false);
				this._meshVisibleFlag = false;
				return;
			}
			if (!this._meshVisibleFlag)
			{
				this._UpdateEnableRenderer(true);
				this._colorChanged = true;
				this._colorOffsetChanged = true;
				if (!this._root._initializeFlag)
				{
					this._meshVisibleFlag = true;
				}
			}
			this._UpdatePlane();
		}

		public override void _UpdateSecond()
		{
			base._UpdateSecond();
			this._prevUvColorAnimation = this._currentUvColorAnimation;
			this._prevUvAlphaAnimation = this._currentUvAlphaAnimation;
			this._prevMeshIndex = this._currentMeshIndex;
			this._prevMaterialID = this._currentMaterialIDList[this._currentMeshIndex];
			this._prevOuterTextureType = this._outerTextureType;
			this._prevOuterTextureColorID = this._outerTextureColorID;
			this._prevOuterTextureAlphaID = this._outerTextureAlphaID;
		}

		protected override void _UpdateColor()
		{
			this._currentColor = this._baseColor;
			this._currentColorOffset = this._baseColorOffset;
			base._UpdateColor();
		}

		private void _UpdatePlane()
		{
			this._UpdateMeshIndex();
			this._UpdateCurrentMaterial();
			this._UpdateVertexColor();
			this._UpdateVertexPosition();
			this._UpdateUVParameter();
			this._UpdateBlurParameter();
			this._UpdateMeshRendererMaterial();
			this._UpdateMesh();
		}

		private void _UpdateMeshIndex()
		{
			if (!this._existMesh)
			{
				return;
			}
			if (this._planeParam._textureKeyParam._keyCount < 2)
			{
				this._currentMeshIndex = 0;
				return;
			}
			this._currentMeshIndex = (int)this._planeParam._textureKeyParam._GetValue(0f, this._parentMotion, ref this._meshKeyIndex);
			if (this._currentMeshIndex > this._meshList.Length - 1)
			{
				this._currentMeshIndex = this._meshList.Length - 1;
			}
		}

		private void _UpdateCurrentMaterialList()
		{
			if (!this._existMesh)
			{
				return;
			}
			bool flag = false;
			if (this._currentMaterialList[this._currentMeshIndex] == null)
			{
				flag = true;
			}
			else if (this._outerTextureType != this._prevOuterTextureType)
			{
				flag = true;
			}
			else if (this._stencilRef != this._prevStencilRef)
			{
				flag = true;
			}
			else if (this._stencilCompareFunc != this._prevStencilCompareFunc)
			{
				flag = true;
			}
			else if (this._outerTextureColorID != this._prevOuterTextureColorID)
			{
				flag = true;
			}
			else if (this._outerTextureAlphaID != this._prevOuterTextureAlphaID)
			{
				flag = true;
			}
			else if (this._currentBlurQuality != this._prevBlurQuality)
			{
				flag = true;
			}
			else if (this._currentBlurPrecision != this._prevBlurPrecision)
			{
				flag = true;
			}
			else if (this._currentBlurValue != this._prevBlurValue)
			{
				flag = true;
			}
			else if (!this._IsDefaultUVList(this._uvColorInfoList) || !this._IsDefaultUVList(this._uvAlphaInfoList))
			{
				flag = true;
			}
			else if (this._isGrayscale != this._prevIsGrayscale)
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			for (int i = 0; i < this._currentMaterialList.Length; i++)
			{
				bool flag2 = true;
				if (!this._IsDefaultUVList(this._uvColorInfoList) || !this._IsDefaultUVList(this._uvAlphaInfoList))
				{
					flag2 = false;
				}
				if (this._outerTextureType == AnTextureTypes.None)
				{
					this._tempMaterial = null;
					if (this._isGrayscale && (this._objectType == AnObjectTypes.Plane || this._objectType == AnObjectTypes.Opaque || this._objectType == AnObjectTypes.Object))
					{
						if (this._currentBlurQuality <= 0 || this._currentBlurPrecision <= 0 || (this._currentBlurValue.x == 0f && this._currentBlurValue.y == 0f))
						{
							this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.Grayscale, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
							flag2 = false;
						}
						else
						{
							this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.GrayscaleBlur, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
						}
					}
					else if (this._objectType == AnObjectTypes.Opaque)
					{
						this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.Opaque, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
					}
					else if (this._objectType == AnObjectTypes.Mask || this._objectType == AnObjectTypes.AlphaMask)
					{
						if (this._objectType == AnObjectTypes.Mask)
						{
							this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.Mask, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
						}
						else
						{
							this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.AlphaMask, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
						}
					}
					else if (this._objectType == AnObjectTypes.StencilMask || this._objectType == AnObjectTypes.StencilAlphaMask)
					{
						if (this._objectType == AnObjectTypes.StencilMask)
						{
							this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.StencilMask, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
						}
						else
						{
							this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.StencilAlphaMask, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
						}
					}
					else if (this._objectType == AnObjectTypes.ObjectMask || this._objectType == AnObjectTypes.ObjectAlphaMask)
					{
						if (this._objectType == AnObjectTypes.ObjectMask)
						{
							this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.ObjectMask, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
						}
						else
						{
							this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnShaderTypes.ObjectAlphaMask, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
						}
					}
					else if (this._currentBlurQuality <= 0 || this._currentBlurPrecision <= 0 || (this._currentBlurValue.x == 0f && this._currentBlurValue.y == 0f))
					{
						this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType), this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
					}
					else
					{
						this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType, true, false), this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
						flag2 = false;
					}
					if (!flag2)
					{
						this._parentMotion.Root._CloneMeshParameterGroupMaterial(this._tempMaterial, this._id, ref this._tempMaterial);
					}
				}
				else
				{
					flag2 = false;
					this._parentMotion.Root._GetMeshParameterGroupMaterial(this._planeParam.TextureNameList[i], AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType), this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._root.Parameter.UseCustomMesh, ref this._tempMaterial);
					this._parentMotion.Root._CloneMeshParameterGroupMaterial(this._tempMaterial, AnValue.OuterTextureString + this._id, ref this._tempMaterial);
					bool flag3 = true;
					if (this._currentBlurQuality <= 0 || this._currentBlurPrecision <= 0 || (this._currentBlurValue.x == 0f && this._currentBlurValue.y == 0f))
					{
						flag3 = false;
					}
					if (!this._isGrayscale)
					{
						if (this._outerTextureType == AnTextureTypes.ColorOnly)
						{
							this._tempMaterial.shader = AnMonoSingleton<AnRootManager>.Instance._GetPlaneNoTexAlphaShader(AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType, flag3, false));
						}
						else if (this._outerTextureType == AnTextureTypes.ColorAndAlpha)
						{
							if ((this._outerTextureAlpha as Texture2D).format == TextureFormat.Alpha8)
							{
								this._tempMaterial.shader = AnMonoSingleton<AnRootManager>.Instance._GetPlaneA8Shader(AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType, flag3, false));
							}
							else
							{
								this._tempMaterial.shader = AnMonoSingleton<AnRootManager>.Instance._GetPlaneMainShader(AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType, flag3, false));
							}
							this._tempMaterial.SetTexture(AnValue.ShaderParamAlphaTex, this._outerTextureAlpha);
						}
					}
					else
					{
						AnShaderTypes anShaderTypes = AnShaderTypes.Grayscale;
						if (flag3)
						{
							anShaderTypes = AnShaderTypes.GrayscaleBlur;
						}
						if (this._outerTextureType == AnTextureTypes.ColorOnly)
						{
							this._tempMaterial.shader = AnMonoSingleton<AnRootManager>.Instance._GetPlaneNoTexAlphaShader(anShaderTypes);
						}
						else
						{
							if ((this._outerTextureAlpha as Texture2D).format == TextureFormat.Alpha8)
							{
								this._tempMaterial.shader = AnMonoSingleton<AnRootManager>.Instance._GetPlaneA8Shader(anShaderTypes);
							}
							else
							{
								this._tempMaterial.shader = AnMonoSingleton<AnRootManager>.Instance._GetPlaneMainShader(anShaderTypes);
							}
							this._tempMaterial.SetTexture(AnValue.ShaderParamAlphaTex, this._outerTextureAlpha);
						}
					}
					this._tempMaterial.SetTexture(AnValue.ShaderParamMainTex, this._outerTextureColor);
				}
				if (!flag2)
				{
					this._tempMaterial.SetFloat(AnValue.ShaderParamStencilRef, (float)this._stencilRef);
					if (this._stencilRef == this._root.DefaultStencilRefOffset)
					{
						this._tempMaterial.SetFloat(AnValue.ShaderParamStencilComp, (float)this._stencilCompareFunc);
					}
					else
					{
						this._tempMaterial.SetFloat(AnValue.ShaderParamStencilComp, 3f);
					}
				}
				this._currentMaterialList[i] = this._tempMaterial;
				this._currentMaterialIDList[i] = this._tempMaterial.GetInstanceID();
				if (i == this._currentMeshIndex)
				{
					this._isCurrentMaterialShared = flag2;
				}
			}
			this._UpdateCurrentMaterial();
		}

		private void _UpdateCurrentMaterial()
		{
			this._currentMaterial = this._currentMaterialList[this._currentMeshIndex];
		}

		private void _UpdateVertexColor()
		{
			if (!this._existMesh)
			{
				return;
			}
			if (!this._colorChanged && !this._colorOffsetChanged)
			{
				return;
			}
			for (int i = 0; i < this._currentVertexColorList.Length; i++)
			{
				this._currentVertexColorList[i] = this._currentColor * this._vertexColorList[i];
				this._currentVertexColorOffsetList0[i] = new Vector2(this._currentColorOffset.r, this._currentColorOffset.g);
				this._currentVertexColorOffsetList1[i] = new Vector2(this._currentColorOffset.b, this._currentColorOffset.a);
			}
			if (this._colorChanged)
			{
				for (int j = 0; j < this._meshList.Length; j++)
				{
					this._meshList[j].colors = this._currentVertexColorList;
				}
			}
			if (this._colorOffsetChanged)
			{
				for (int k = 0; k < this._meshList.Length; k++)
				{
					this._meshList[k].uv2 = this._currentVertexColorOffsetList0;
					this._meshList[k].uv3 = this._currentVertexColorOffsetList1;
				}
			}
		}

		private void _UpdateVertexPosition()
		{
			if (!this._existMesh)
			{
				return;
			}
			if (this._parentMotion._root._initializeFlag)
			{
				this._positionOffsetChanged = true;
				this._scaleChanged = true;
				this._shearChanged = true;
				this._changeFillValue = true;
			}
			if (!this._shearChanged && !this._scaleChanged && !this._positionOffsetChanged && !this._changeFillValue)
			{
				return;
			}
			if (this._meshTypeList[0] == AnMeshTypes.Normal)
			{
				this._transform.localScale = Vector3.one;
				for (int i = 0; i < this._vertexPositionListGroup.Length; i++)
				{
					for (int j = 0; j < this._vertexPositionListGroup[i].Length; j++)
					{
						Vector3 vector;
						if (this._outerTextureType == AnTextureTypes.None)
						{
							vector = AnUtilityMesh.CalculateShearPosition(this._meshParamList[i]._baseMeshVertices[j], -this._currentPositionOffset, this._currentShearCosSin.x, this._currentShearCosSin.y, this._currentShearCosSin.z, this._currentShearCosSin.w, this._currentScale.x, this._currentScale.y);
						}
						else if (this._outerTextureKeepSize)
						{
							vector = AnUtilityMesh.CalculateShearPosition(this._meshParamList[i]._baseMeshVertices[j], -this._currentPositionOffset, this._currentShearCosSin.x, this._currentShearCosSin.y, this._currentShearCosSin.z, this._currentShearCosSin.w, this._currentScale.x, this._currentScale.y);
						}
						else
						{
							if (this._outerTextureKeepCenter)
							{
								vector = AnUtilityMesh.CalculateShearPosition(this._meshParamList[i]._baseMeshVertices[j], -this._currentPositionOffset, this._currentShearCosSin.x, this._currentShearCosSin.y, this._currentShearCosSin.z, this._currentShearCosSin.w, this._currentScale.x * this._outerTextureScale.x, this._currentScale.y * this._outerTextureScale.y);
							}
							else
							{
								vector = AnUtilityMesh.CalculateShearPosition(this._outerTextureVertexPositionList[j], -this._currentPositionOffset, this._currentShearCosSin.x, this._currentShearCosSin.y, this._currentShearCosSin.z, this._currentShearCosSin.w, this._currentScale.x, this._currentScale.y);
							}
							vector += this._outerTextureOffset;
						}
						this._vertexPositionListGroup[i][j] = vector;
					}
					this._meshList[i].vertices = this._vertexPositionListGroup[i];
					if (this._currentFillValue != this._prevFillValue)
					{
						AnUtilityMesh.FillPlane(this._meshList[i], this._currentFillValue, this._meshParamList[i], this._fillType);
					}
					this._meshList[i].RecalculateBounds();
				}
			}
			else if (this._meshTypeList[0] == AnMeshTypes.NineSlice)
			{
				if (this._scaleChanged)
				{
					this._transform.localScale = this._currentScale;
				}
				if (this._shearChanged)
				{
					this._transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, this._currentShear.y));
				}
				AnBase._tempVector3_0 = this._transform.localScale;
				AnBase._tempVector3_1 = AnBase._tempVector3_0;
				if (this._planeParam._fullNineSlice)
				{
					AnBase._tempVector3_1 = this._transform.lossyScale;
					if (AnBase._tempVector3_1.x != 0f)
					{
						AnBase._tempVector3_1.x = AnBase._tempVector3_1.x / this._root.transform.lossyScale.x;
					}
					else
					{
						AnBase._tempVector3_1.x = 0f;
					}
					if (AnBase._tempVector3_1.y != 0f)
					{
						AnBase._tempVector3_1.y = AnBase._tempVector3_1.y / this._root.transform.lossyScale.y;
					}
					else
					{
						AnBase._tempVector3_1.y = 0f;
					}
				}
				AnUtilityMesh.UpdateNinesliceVertexPositionList(this._meshParamList[0]._size, AnBase._tempVector3_1, this._meshParamList[0]._sliceRange, this._currentPositionOffset, ref this._vertexPositionListGroup[0]);
				if (AnBase._tempVector3_1.x != 0f)
				{
					AnBase._tempVector3_0.x = 1f / AnBase._tempVector3_1.x * AnBase._tempVector3_0.x;
				}
				else
				{
					AnBase._tempVector3_0.x = 0f;
				}
				if (AnBase._tempVector3_1.y != 0f)
				{
					AnBase._tempVector3_0.y = 1f / AnBase._tempVector3_1.y * AnBase._tempVector3_0.y;
				}
				else
				{
					AnBase._tempVector3_0.y = 0f;
				}
				AnBase._tempVector3_0.z = 1f;
				this._gameObject.transform.localScale = AnBase._tempVector3_0;
				this._meshList[0].vertices = this._vertexPositionListGroup[0];
				this._meshList[0].RecalculateBounds();
			}
			else if (this._meshTypeList[0] == AnMeshTypes.Mesh)
			{
				if (this._scaleChanged)
				{
					this._currentScale.z = (this._currentScale.x + this._currentScale.y) * 0.5f;
					this._transform.localScale = this._currentScale;
				}
				if (this._shearChanged)
				{
					this._transform.localRotation = Quaternion.Euler(new Vector3(this._currentRotate.x, this._currentRotate.y, this._currentRotate.z));
				}
				this._meshList[0].RecalculateBounds();
			}
			this._changeFillValue = false;
		}

		private void _UpdateUVParameter()
		{
			if (!this._existMesh)
			{
				return;
			}
			if (!this._uvChanged)
			{
				return;
			}
			if (this._timeModeType != AnTimeModeTypes.Sync)
			{
				this._currentUvColorAnimation -= this._uvColorInfoList[2] * this._parentMotion._root._deltaTime * this._parentMotion._motionSpeed;
				this._currentUvAlphaAnimation -= this._uvAlphaInfoList[2] * this._parentMotion._root._deltaTime * this._parentMotion._motionSpeed;
			}
			else
			{
				this._currentUvColorAnimation = -this._uvColorInfoList[2] * this._parentMotion._root._syncTime;
				this._currentUvAlphaAnimation = -this._uvAlphaInfoList[2] * this._parentMotion._root._syncTime;
			}
			if (this._currentUvColorAnimation != this._prevUvColorAnimation)
			{
				this._uvColorChanged = true;
			}
			if (this._currentUvAlphaAnimation != this._prevUvAlphaAnimation)
			{
				this._uvAlphaChanged = true;
			}
			if (this._uvColorChanged)
			{
				Vector2 vector = this._currentUvColorAnimation - this._uvColorInfoList[1];
				if (this._meshParamList[this._currentMeshIndex]._rotated)
				{
					vector = new Vector2(vector.y, -vector.x);
				}
				vector = AnUtilityMesh.FixUV(vector);
				AnBase._tempVector4_0.x = this._uvColorInfoList[0].x;
				AnBase._tempVector4_0.y = this._uvColorInfoList[0].y;
				AnBase._tempVector4_0.z = vector.x;
				AnBase._tempVector4_0.w = vector.y;
				this._currentMaterial.SetVector(AnValue.ShaderParamUVColorInfo, AnBase._tempVector4_0);
				this._uvColorChanged = false;
			}
			if (this._uvAlphaChanged)
			{
				Vector2 vector2 = this._currentUvAlphaAnimation - this._uvAlphaInfoList[1];
				if (this._meshParamList[this._currentMeshIndex]._rotated)
				{
					vector2 = new Vector2(vector2.y, -vector2.x);
				}
				vector2 = AnUtilityMesh.FixUV(vector2);
				AnBase._tempVector4_0.x = this._uvAlphaInfoList[0].x;
				AnBase._tempVector4_0.y = this._uvAlphaInfoList[0].y;
				AnBase._tempVector4_0.z = vector2.x;
				AnBase._tempVector4_0.w = vector2.y;
				this._currentMaterial.SetVector(AnValue.ShaderParamUVAlphaInfo, AnBase._tempVector4_0);
				this._uvAlphaChanged = false;
			}
		}

		private void _UpdateBlurParameter()
		{
			if (!this._existMesh)
			{
				return;
			}
			if (this._currentBlurQuality <= 0)
			{
				return;
			}
			if (this._currentBlurPrecision <= 0)
			{
				return;
			}
			if (this._currentBlurValue != this._prevBlurValue)
			{
				if (this._currentBlurValue.x == 0f && this._currentBlurValue.y == 0f)
				{
					this._SetMaterial();
				}
				else if (this._prevBlurValue.x == 0f && this._prevBlurValue.y == 0f)
				{
					this._SetMaterial();
				}
			}
			if (this._currentBlurValue.x == 0f && this._currentBlurValue.y == 0f)
			{
				return;
			}
			if (!this._blurChanged)
			{
				return;
			}
			float num = 0.2f;
			if (this._currentBlurQuality == 1)
			{
				num = 0.3f;
			}
			else if (this._currentBlurQuality == 2)
			{
				num = 0.25f;
			}
			else if (this._currentBlurQuality == 3)
			{
				num = 0.2f;
			}
			this._currentMaterial.SetFloat(AnValue.ShaderParamBlurOffsetX, this._currentBlurValue.x * num);
			this._currentMaterial.SetFloat(AnValue.ShaderParamBlurOffsetY, this._currentBlurValue.y * num);
		}

		private void _UpdateMeshRendererMaterial()
		{
			if (!this._existMesh)
			{
				return;
			}
			int num = this._currentMaterialIDList[this._currentMeshIndex];
			int prevMaterialID = this._prevMaterialID;
			this._meshRenderer.material = this._currentMaterial;
		}

		private void _UpdateMesh()
		{
			if (!this._existMesh)
			{
				return;
			}
			if (this._currentMeshIndex == this._prevMeshIndex)
			{
				return;
			}
			this._meshFilter.mesh = this._meshList[this._currentMeshIndex];
		}

		public override void _ResetTime()
		{
			base._ResetTime();
			this._meshKeyIndex = 0;
			this._prevUvColorAnimation = Vector2.one * float.MaxValue;
			this._prevUvAlphaAnimation = Vector2.one * float.MaxValue;
		}

		private void _UpdateEnableRenderer(bool enable)
		{
			if (!this._existMesh)
			{
				return;
			}
			if (this._parentMotion._root._initializeFlag)
			{
				enable = false;
			}
			if (enable)
			{
				if (!this._meshRenderer.enabled)
				{
					this._meshRenderer.enabled = true;
				}
				return;
			}
			if (this._meshRenderer.enabled)
			{
				this._meshRenderer.enabled = false;
			}
		}

		protected override void _UpdateSortOrder()
		{
			base._UpdateSortOrder();
			if (!this._existMesh)
			{
				return;
			}
			if (this._meshRenderer.sortingOrder != this._sortOrder)
			{
				this._meshRenderer.sortingOrder = this._sortOrder;
			}
		}

		protected override void _UpdateSortLayer()
		{
			base._UpdateSortLayer();
			if (!this._existMesh)
			{
				return;
			}
			if (this._meshRenderer.sortingLayerName != this._sortLayerName)
			{
				this._meshRenderer.sortingLayerName = this._sortLayerName;
			}
		}

		protected override void _UpdateStencilRefBase()
		{
			base._UpdateStencilRefBase();
			this._SetMaterial();
		}

		protected override void _UpdateStencilCompareFuncBase()
		{
			base._UpdateStencilCompareFuncBase();
			this._SetMaterial();
		}

		protected override void _SetGrayscaleBase(bool enable)
		{
			base._SetGrayscaleBase(enable);
			this._SetMaterial();
		}

		public void SetUV(Vector2 value, AnUVValueTypes target, bool alpha, bool updateMaterial = true)
		{
			if (!this._existMesh)
			{
				return;
			}
			if (alpha)
			{
				this._uvAlphaInfoList[(int)target] = value;
				this._uvAlphaChanged = true;
			}
			else
			{
				this._uvColorInfoList[(int)target] = value;
				this._uvColorChanged = true;
			}
			this._SetMaterial();
		}

		public void SetVertexColor(int index, Color color)
		{
			if (!this._existMesh)
			{
				return;
			}
			if (index == 0)
			{
				this._vertexColorList[3] = color;
			}
			else if (index == 1)
			{
				this._vertexColorList[1] = color;
			}
			else if (index == 2)
			{
				this._vertexColorList[2] = color;
			}
			else if (index == 3)
			{
				this._vertexColorList[0] = color;
			}
			this._colorChanged = true;
			this._UpdateVertexColor();
		}

		public void SetOuterTexture(Texture colorTexture, Texture alphaTexture, bool keepUV = false, bool keepSize = true, float width = 0f, float height = 0f, float offsetX = 0f, float offsetY = 0f, bool keepCenter = false, float uvSizeX = 1f, float uvSizeY = 1f, float uvOffsetX = 0f, float uvOffsetY = 0f, bool uvRotated = false)
		{
			if (!this._existMesh)
			{
				return;
			}
			this._prevOuterTextureColorID = -1;
			this._prevOuterTextureAlphaID = -1;
			if (colorTexture == null)
			{
				this._outerTextureType = AnTextureTypes.None;
				this._outerTextureColor = null;
				this._outerTextureAlpha = null;
				this._outerTextureColorID = 0;
				this._outerTextureAlphaID = 0;
				for (int i = 0; i < this._meshList.Length; i++)
				{
					this._meshList[i].uv = this._meshParamList[i]._GetUVList();
				}
				this._SetMaterial();
				return;
			}
			this._outerTextureColor = colorTexture;
			this._outerTextureAlpha = alphaTexture;
			this._outerTextureColorID = colorTexture.GetInstanceID();
			if (alphaTexture == null)
			{
				this._outerTextureAlphaID = 0;
				this._outerTextureType = AnTextureTypes.ColorOnly;
			}
			else
			{
				this._outerTextureAlphaID = alphaTexture.GetInstanceID();
				this._outerTextureType = AnTextureTypes.ColorAndAlpha;
			}
			if (!keepUV)
			{
				for (int j = 0; j < this._meshList.Length; j++)
				{
					if (uvRotated)
					{
						this._meshList[j].uv = new Vector2[]
						{
							new Vector2(uvOffsetX, uvOffsetY + uvSizeY),
							new Vector2(uvOffsetX + uvSizeX, uvOffsetY),
							new Vector2(uvOffsetX, uvOffsetY),
							new Vector2(uvOffsetX + uvSizeX, uvOffsetY + uvSizeY)
						};
					}
					else
					{
						this._meshList[j].uv = new Vector2[]
						{
							new Vector2(uvOffsetX, uvOffsetY),
							new Vector2(uvOffsetX + uvSizeX, uvOffsetY + uvSizeY),
							new Vector2(uvOffsetX + uvSizeX, uvOffsetY),
							new Vector2(uvOffsetX, uvOffsetY + uvSizeY)
						};
					}
				}
			}
			this._outerTextureKeepSize = keepSize;
			this._outerTextureKeepCenter = keepCenter;
			if (width == 0f)
			{
				width = (float)this._outerTextureColor.width;
			}
			if (height == 0f)
			{
				height = (float)this._outerTextureColor.height;
			}
			this._outerTextureScale = new Vector2(width / this._parameter.Size.x, height / this._parameter.Size.y);
			this._outerTextureOffset = new Vector3(offsetX, offsetY, 0f);
			this._outerTextureVertexPositionList[0] = new Vector3(-0.5f * width, -0.5f * height, 0f);
			this._outerTextureVertexPositionList[1] = new Vector3(0.5f * width, 0.5f * height, 0f);
			this._outerTextureVertexPositionList[2] = new Vector3(0.5f * width, -0.5f * height, 0f);
			this._outerTextureVertexPositionList[3] = new Vector3(-0.5f * width, 0.5f * height, 0f);
			this._shearChanged = true;
			this._UpdateVertexPosition();
			this._SetMaterial();
		}

		public void SetOuterTextureFromMeshParameter(string textureName, AnMeshParameter meshParameter, bool keepSize = true, float width = 0f, float height = 0f, float offsetX = 0f, float offsetY = 0f, bool keepCenter = false)
		{
			AnMeshInfoParameterGroup anMeshInfoParameterGroup = null;
			AnMeshInfoParameter anMeshInfoParameter = null;
			if (!meshParameter._SearchMesh(textureName, ref anMeshInfoParameterGroup, ref anMeshInfoParameter))
			{
				this.SetOuterTexture(null, null, false, true, 0f, 0f, 0f, 0f, false, 1f, 1f, 0f, 0f, false);
				return;
			}
			this.SetOuterTexture(anMeshInfoParameterGroup.TextureSetColor, anMeshInfoParameterGroup.TextureSetAlpha, false, keepSize, width, height, offsetX, offsetY, keepCenter, anMeshInfoParameter.UVSize.x, anMeshInfoParameter.UVSize.y, anMeshInfoParameter.UVOffset.x, anMeshInfoParameter.UVOffset.y, anMeshInfoParameter.Rotated);
		}

		public void SetFill(float fillValue, AnFillType fillType)
		{
			this._currentFillValue = fillValue;
			this._prevFillValue = float.MinValue;
			this._fillType = fillType;
			this._changeFillValue = true;
			this._UpdateVertexPosition();
		}

		public override void SetBlurQuality(int blurQuality, int blurPrecision, bool affectChildren)
		{
			base.SetBlurQuality(blurQuality, blurPrecision, affectChildren);
			this._SetMaterial();
		}

		public override void _UpdateScreenSize()
		{
			base._UpdateScreenSize();
			this._UpdateVertexPosition();
		}

		public void SetCurrentMaterials(Material[] materials)
		{
			this._currentMaterialList = materials;
		}

		private AnPlaneParameter _planeParam;

		private bool _existMesh;

		private bool _meshVisibleFlag;

		private MeshRenderer _meshRenderer;

		private MeshFilter _meshFilter;

		private AnMeshInfoParameter[] _meshParamList;

		private AnMeshTypes[] _meshTypeList;

		private Mesh[] _meshList;

		private int _currentMeshIndex;

		private int _prevMeshIndex = int.MaxValue;

		private int _meshKeyIndex;

		private Material _currentMaterial;

		private Material[] _currentMaterialList;

		private int[] _currentMaterialIDList;

		private bool _isCurrentMaterialShared = true;

		private Material _tempMaterial;

		private int _prevMaterialID = int.MaxValue;

		private Vector3[][] _vertexPositionListGroup;

		private Vector2[] _uvColorInfoList;

		private Vector2[] _uvAlphaInfoList;

		private Vector2 _currentUvColorAnimation = Vector2.zero;

		private Vector2 _currentUvAlphaAnimation = Vector2.zero;

		private Vector2 _prevUvColorAnimation = Vector2.zero;

		private Vector2 _prevUvAlphaAnimation = Vector2.zero;

		private bool _uvColorChanged;

		private bool _uvAlphaChanged;

		private bool _uvChanged;

		private Color[] _vertexColorList;

		private Color[] _currentVertexColorList;

		private Vector2[] _currentVertexColorOffsetList0;

		private Vector2[] _currentVertexColorOffsetList1;

		private Texture _outerTextureColor;

		private Texture _outerTextureAlpha;

		private int _outerTextureColorID;

		private int _outerTextureAlphaID;

		private int _prevOuterTextureColorID;

		private int _prevOuterTextureAlphaID;

		private AnTextureTypes _outerTextureType;

		private AnTextureTypes _prevOuterTextureType = AnTextureTypes.ColorAndAlpha;

		private Vector3[] _outerTextureVertexPositionList;

		private Vector2 _outerTextureScale = Vector2.zero;

		private Vector3 _outerTextureOffset = Vector2.zero;

		private bool _outerTextureKeepSize;

		private bool _outerTextureKeepCenter;

		private AnFillType _fillType = AnFillType.ButtomToTop;

		private float _currentFillValue = 1f;

		private float _prevFillValue;

		private bool _changeFillValue;
	}
}
