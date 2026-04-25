using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnMeshInfoParameterGroup
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

		public string TextureSetName
		{
			get
			{
				return this._textureSetName;
			}
			set
			{
				this._textureSetName = value;
			}
		}

		public Texture TextureSetColor
		{
			get
			{
				return this._textureSetColor;
			}
			set
			{
				this._textureSetColor = value;
			}
		}

		public Texture TextureSetAlpha
		{
			get
			{
				return this._textureSetAlpha;
			}
			set
			{
				this._textureSetAlpha = value;
			}
		}

		public Vector2 TextureSetSize
		{
			get
			{
				return this._textureSetSize;
			}
			set
			{
				this._textureSetSize = value;
			}
		}

		public AnColorTextureFormatTypes TextureSetColorFormat
		{
			get
			{
				return this._textureSetColorFormat;
			}
			set
			{
				this._textureSetColorFormat = value;
			}
		}

		public AnAlphaTextureFormatTypes TextureSetAlphaFormat
		{
			get
			{
				return this._textureSetAlphaFormat;
			}
			set
			{
				this._textureSetAlphaFormat = value;
			}
		}

		public AnTextureCombinationTypes TextureCombinationType
		{
			get
			{
				return this._textureCombinationType;
			}
			set
			{
				this._textureCombinationType = value;
			}
		}

		public List<AnMeshInfoParameter> MeshInfoParameterList
		{
			get
			{
				return this._meshInfoParameterList;
			}
			set
			{
				this._meshInfoParameterList = value;
			}
		}

		public void _Initialize()
		{
			this._meshInfoParameterTable = new Hashtable();
			this._materialTable = new Hashtable();
			foreach (AnMeshInfoParameter anMeshInfoParameter in this._meshInfoParameterList)
			{
				if (!this._meshInfoParameterTable.ContainsKey(anMeshInfoParameter.FixTextureName))
				{
					anMeshInfoParameter.MeshInfoParameterGroup = this;
					anMeshInfoParameter._Initialize();
					this._meshInfoParameterTable.Add(anMeshInfoParameter.FixTextureName, anMeshInfoParameter);
				}
			}
		}

		public bool _CreateMesh(string textureName, List<Mesh> meshList, bool useCustomMesh, ref AnMeshInfoParameter meshInfo, ref Mesh mesh)
		{
			if (!this._meshInfoParameterTable.ContainsKey(textureName))
			{
				return false;
			}
			meshInfo = this._meshInfoParameterTable[textureName] as AnMeshInfoParameter;
			mesh = meshInfo._CreateMesh(meshList, useCustomMesh);
			if (mesh == null)
			{
				meshInfo = null;
				mesh = null;
				return false;
			}
			return true;
		}

		public bool _GetMaterial(string textureName, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, bool useCustomMesh, ref Material material)
		{
			if (!this._meshInfoParameterTable.ContainsKey(textureName))
			{
				return false;
			}
			if (useCustomMesh)
			{
				AnCustomMeshInfoParameter anCustomMeshInfoParameter = this._meshParameter._GetCustomMeshInfoParam(textureName);
				if (anCustomMeshInfoParameter != null)
				{
					Texture texture = this._textureSetColor;
					Texture texture2 = this._textureSetAlpha;
					if (anCustomMeshInfoParameter.TextureColor != null)
					{
						texture = anCustomMeshInfoParameter.TextureColor;
						texture2 = anCustomMeshInfoParameter.TextureAlpha;
					}
					AnShaderTypes anShaderTypes = shaderType;
					if (anCustomMeshInfoParameter.CullingOn)
					{
						if (shaderType == AnShaderTypes.Add)
						{
							anShaderTypes = AnShaderTypes.Add3D;
						}
						else
						{
							anShaderTypes = AnShaderTypes.Normal3D;
						}
					}
					this._CreateCustomMeshMaterial(texture, texture2, anShaderTypes, stencilRef, baseStencilRef, stencilCompareFunc, ref material);
				}
				else
				{
					this._CreateMaterial(shaderType, stencilRef, baseStencilRef, stencilCompareFunc, ref material);
				}
			}
			else
			{
				this._CreateMaterial(shaderType, stencilRef, baseStencilRef, stencilCompareFunc, ref material);
			}
			return !(material == null);
		}

		private void _CreateMaterial(AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, ref Material material)
		{
			if (this._materialTable == null)
			{
				this._materialTable = new Hashtable();
			}
			AnStencilCompareFuncTypes stencilCompareType = AnUtilityMaterial.GetStencilCompareType(shaderType, stencilRef, baseStencilRef, stencilCompareFunc);
			string materialKey = AnUtilityMaterial.GetMaterialKey(shaderType, stencilRef, stencilCompareType);
			if (this._materialTable.ContainsKey(materialKey))
			{
				material = this._materialTable[materialKey] as Material;
				if (material != null)
				{
					return;
				}
				this._materialTable.Remove(materialKey);
			}
			Shader shader;
			if (Application.isPlaying)
			{
				if (!AnMonoSingleton<AnRootManager>.Instance.PlaneShadarTable.ContainsKey(shaderType))
				{
					material = null;
					return;
				}
				if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGBA)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneNoAlphaTexShadarTable[shaderType] as Shader;
				}
				else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGB_And_AlphaR)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneShadarTable[shaderType] as Shader;
				}
				else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGB_And_AlphaA)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneA8ShadarTable[shaderType] as Shader;
				}
				else if (this._textureSetColor != null && this._textureSetAlpha == null)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneNoAlphaTexShadarTable[shaderType] as Shader;
				}
				else if (this._textureSetAlphaFormat == AnAlphaTextureFormatTypes.A8Bit)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneA8ShadarTable[shaderType] as Shader;
				}
				else
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneShadarTable[shaderType] as Shader;
				}
			}
			else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGBA)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath.Replace(AnValue.ShaderMainString, AnValue.ShaderNoTexAlphaString));
			}
			else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGB_And_AlphaR)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath);
			}
			else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGB_And_AlphaA)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath.Replace(AnValue.ShaderMainString, AnValue.ShaderA8String));
			}
			else if (this._textureSetColor != null && this._textureSetAlpha == null)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath.Replace(AnValue.ShaderMainString, AnValue.ShaderNoTexAlphaString));
			}
			else if (this._textureSetAlphaFormat == AnAlphaTextureFormatTypes.A8Bit)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath.Replace(AnValue.ShaderMainString, AnValue.ShaderA8String));
			}
			else
			{
				shader = Shader.Find(AnValue.ShaderEditorPath);
			}
			material = new Material(shader);
			material.name = this._textureSetName + "_" + materialKey;
			material.mainTexture = this._textureSetColor;
			material.SetTexture(AnValue.ShaderParamAlphaTex, this._textureSetAlpha);
			material.SetVector(AnValue.ShaderParamUVColorInfo, AnValue.UVInfoDefaultValue);
			material.SetVector(AnValue.ShaderParamUVAlphaInfo, AnValue.UVInfoDefaultValue);
			material.SetFloat(AnValue.ShaderParamStencilRef, (float)stencilRef);
			material.SetFloat(AnValue.ShaderParamStencilComp, (float)stencilCompareType);
			this._materialTable.Add(materialKey, material);
		}

		private void _CreateCustomMeshMaterial(Texture colorTexture, Texture alphaTexture, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, ref Material material)
		{
			if (colorTexture == null)
			{
				return;
			}
			if (this._materialTable == null)
			{
				this._materialTable = new Hashtable();
			}
			AnStencilCompareFuncTypes stencilCompareType = AnUtilityMaterial.GetStencilCompareType(shaderType, stencilRef, baseStencilRef, stencilCompareFunc);
			string text = AnUtilityMaterial.GetMaterialKey(shaderType, stencilRef, stencilCompareType) + AnValue.CustomTextureString + colorTexture.name;
			if (this._materialTable.ContainsKey(text))
			{
				material = this._materialTable[text] as Material;
				if (material != null)
				{
					return;
				}
				this._materialTable.Remove(text);
			}
			Shader shader;
			if (Application.isPlaying)
			{
				if (!AnMonoSingleton<AnRootManager>.Instance.PlaneShadarTable.ContainsKey(shaderType))
				{
					material = null;
					return;
				}
				if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGBA)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneNoAlphaTexShadarTable[shaderType] as Shader;
				}
				else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGB_And_AlphaR)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneShadarTable[shaderType] as Shader;
				}
				else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGB_And_AlphaA)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneA8ShadarTable[shaderType] as Shader;
				}
				else if (this._textureSetColor != null && this._textureSetAlpha == null)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneNoAlphaTexShadarTable[shaderType] as Shader;
				}
				else if (this._textureSetAlphaFormat == AnAlphaTextureFormatTypes.A8Bit)
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneA8ShadarTable[shaderType] as Shader;
				}
				else
				{
					shader = AnMonoSingleton<AnRootManager>.Instance.PlaneShadarTable[shaderType] as Shader;
				}
			}
			else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGBA)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath.Replace(AnValue.ShaderMainString, AnValue.ShaderNoTexAlphaString));
			}
			else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGB_And_AlphaR)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath);
			}
			else if (this._textureCombinationType == AnTextureCombinationTypes.ColorRGB_And_AlphaA)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath.Replace(AnValue.ShaderMainString, AnValue.ShaderA8String));
			}
			else if (this._textureSetColor != null && this._textureSetAlpha == null)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath.Replace(AnValue.ShaderMainString, AnValue.ShaderNoTexAlphaString));
			}
			else if (this._textureSetAlphaFormat == AnAlphaTextureFormatTypes.A8Bit)
			{
				shader = Shader.Find(AnValue.ShaderEditorPath.Replace(AnValue.ShaderMainString, AnValue.ShaderA8String));
			}
			else
			{
				shader = Shader.Find(AnValue.ShaderEditorPath);
			}
			material = new Material(shader);
			material.name = this._textureSetName + "_" + text;
			material.mainTexture = colorTexture;
			material.SetTexture(AnValue.ShaderParamAlphaTex, alphaTexture);
			material.SetVector(AnValue.ShaderParamUVColorInfo, AnValue.UVInfoDefaultValue);
			material.SetVector(AnValue.ShaderParamUVAlphaInfo, AnValue.UVInfoDefaultValue);
			material.SetFloat(AnValue.ShaderParamStencilRef, (float)stencilRef);
			material.SetFloat(AnValue.ShaderParamStencilComp, (float)stencilCompareType);
			this._materialTable.Add(text, material);
		}

		public void _Destroy()
		{
			if (this._materialTable != null)
			{
				foreach (object obj in this._materialTable.Values)
				{
					Material material = (Material)obj;
					if (!(material == null) && !Application.isPlaying)
					{
						global::UnityEngine.Object.DestroyImmediate(material);
					}
				}
			}
			if (this._meshInfoParameterList != null)
			{
				int count = this._meshInfoParameterList.Count;
				for (int i = 0; i < count; i++)
				{
					if (this._meshInfoParameterList[i] != null)
					{
						this._meshInfoParameterList[i]._Destroy();
					}
				}
			}
		}

		public bool _SearchMesh(string textureName, ref AnMeshInfoParameter meshInfoParam)
		{
			if (!Application.isPlaying)
			{
				foreach (AnMeshInfoParameter anMeshInfoParameter in this._meshInfoParameterList)
				{
					if (anMeshInfoParameter.FixTextureName == textureName)
					{
						meshInfoParam = anMeshInfoParameter;
						return true;
					}
				}
				return false;
			}
			if (this._meshInfoParameterTable.ContainsKey(textureName))
			{
				meshInfoParam = this._meshInfoParameterTable[textureName] as AnMeshInfoParameter;
				AnMeshInfoParameter anMeshInfoParameter2 = meshInfoParam;
				return true;
			}
			return true;
		}

		[NonSerialized]
		private AnMeshParameter _meshParameter;

		public string _textureSetName;

		public Vector2 _textureSetSize = Vector2.zero;

		public Texture _textureSetColor;

		public Texture _textureSetAlpha;

		public AnColorTextureFormatTypes _textureSetColorFormat;

		public AnAlphaTextureFormatTypes _textureSetAlphaFormat;

		public AnTextureCombinationTypes _textureCombinationType;

		public List<AnMeshInfoParameter> _meshInfoParameterList;

		private Hashtable _meshInfoParameterTable;

		private Hashtable _materialTable;
	}
}
