using System;
using System.Collections;
using System.Collections.Generic;
using AnimateToUnity.Utility;
using UnityEngine;

namespace AnimateToUnity
{
	public sealed class AnRootManager : AnMonoSingleton<AnRootManager>
	{
		public string UnityVersion
		{
			get
			{
				return this._unityVersion;
			}
		}

		public string DeviceModel
		{
			get
			{
				return this._deviceModel;
			}
		}

		public AnUIManager UIManager
		{
			get
			{
				return this._uiManager;
			}
		}

		public Hashtable PlaneShadarTable
		{
			get
			{
				return this._planeShaderTable;
			}
		}

		public Hashtable PlaneA8ShadarTable
		{
			get
			{
				return this._planeA8ShaderTable;
			}
		}

		public Hashtable PlaneNoAlphaTexShadarTable
		{
			get
			{
				return this._planeNoAlphaTexShaderTable;
			}
		}

		public string LocalizeTarget
		{
			get
			{
				return this._localizeTarget;
			}
		}

		public Hashtable FontShadarTable
		{
			get
			{
				return this._fontShaderTable;
			}
		}

		public Hashtable FontIconShadarTable
		{
			get
			{
				return this._fontIconShaderTable;
			}
		}

		public AnGlobalData GlobalData
		{
			get
			{
				return this._globalData;
			}
		}

		public bool ExistGlobalData
		{
			get
			{
				return this._existGlobalData;
			}
		}

		public float ScreenWidth
		{
			get
			{
				return this._screenWidth;
			}
		}

		public float ScreenHeight
		{
			get
			{
				return this._screenHeight;
			}
		}

		public float CurrentDeltaTime
		{
			get
			{
				return this._currentDeltaTime;
			}
		}

		public float CurrentTime
		{
			get
			{
				return this._currentTime;
			}
		}

		public float PrevTime
		{
			get
			{
				return this._prevTime;
			}
		}

		public bool UseDebugComponent
		{
			get
			{
				return this._useDebugComponent;
			}
		}

		public bool UseDebugLog
		{
			get
			{
				return this._useDebugLog;
			}
		}

		public List<int> LayerBitFlagList
		{
			get
			{
				return this._layerBitFlagList;
			}
		}

		public List<string> LayerNameList
		{
			get
			{
				return this._layerNameList;
			}
		}

		public List<int> ActiveLayerBitFlagList
		{
			get
			{
				return this._activeLayerBitFlagList;
			}
		}

		public List<int> SortingLayerIndexList
		{
			get
			{
				return this._sortingLayerIndexList;
			}
		}

		public List<string> SortingLayerNameList
		{
			get
			{
				return this._sortingLayerNameList;
			}
		}

		public float ScreenRate
		{
			get
			{
				return this._screenRate;
			}
			set
			{
				this._screenRate = value;
			}
		}

		private void Update()
		{
			this._currentTargetFrameRate = Application.targetFrameRate;
			if ((float)this._currentTargetFrameRate != this._prevTargetFrameRate)
			{
				this._currentOneFrameTime = 1f / (float)this._currentTargetFrameRate;
			}
			this._currentTime = Time.realtimeSinceStartup;
			if (Time.deltaTime <= 1E-45f)
			{
				this._currentDeltaTime = (this._currentTime - this._prevTime) * this._customTimeScale;
			}
			else
			{
				this._currentDeltaTime = Time.deltaTime;
			}
			this._UpdateScreenSize();
			if (this._rootList != null)
			{
				for (int i = 0; i < this._rootList.Count; i++)
				{
					this._rootList[i]._UpdateRoot(true);
				}
			}
			this._uiManager._Update();
			this._prevTime = this._currentTime;
			this._prevTargetFrameRate = (float)this._currentTargetFrameRate;
		}

		public override void _OnInitialize()
		{
			base._OnInitialize();
			AnLog._Log(AnLogTypes.Initialize, AnLogColorTypes.color_aaaaaaff, AnLogTitleTypes.FlRootManager, base.gameObject);
			base.name = "_FlRootManager";
			this._unityVersion = Application.unityVersion.ToLower();
			this._deviceModel = SystemInfo.deviceModel.ToLower();
			this._uiManager = new AnUIManager();
			this._uiManager._Initilaize();
			if (this._planeShaderVariantTables == null)
			{
				this._planeShaderVariantTables = new Dictionary<AnShaderVariantTypes, Dictionary<Shader, Shader>>();
			}
			this._customTimeScale = 1f;
			this._prevTime = 0f;
			this._horizontalAxisNameList = new List<string>();
			this._horizontalAxisNameList.Add("Horizontal");
			this._verticalAxisNameList = new List<string>();
			this._verticalAxisNameList.Add("Vertical");
			this._subumitButtonNameList = new List<string>();
			this._subumitButtonNameList.Add("Submit");
			this._cancelButtonNameList = new List<string>();
			this._cancelButtonNameList.Add("Cancel");
			this._CreatePlaneMainShaderTable();
			this._CreatePlaneA8ShaderTable();
			this._CreatePlaneNoTexAlphaShaderTable();
			this._LoadGlobalData();
			this._InitializeScreenSize();
			this._UpdateScreenSize();
			this._UpdateLayerTable();
			this._UpdateSortingLayerTable();
			this._OptimizeActiveLayerTable();
			this._LoadEditorSetting();
		}

		public override void _OnLoaded()
		{
			base._OnLoaded();
			AnLog._Log(AnLogTypes.Loaded, AnLogColorTypes.color_aaaaaaff, AnLogTitleTypes.FlRootManager, base.gameObject);
			this.OptimizeAll();
		}

		public override void _OnFinalize()
		{
			base._OnFinalize();
			AnLog._Log(AnLogTypes.Finalize, AnLogColorTypes.color_aaaaaaff, AnLogTitleTypes.FlRootManager, base.gameObject);
		}

		public void OptimizeAll()
		{
			this._UpdateScreenSize();
			this._UpdateLayerTable();
			this._UpdateSortingLayerTable();
			this._OptimizeRootList();
			this._OptimizeRootParameterTable();
			this._OptimizeMeshParameterTable();
			this._OptimizeActiveLayerTable();
			this._uiManager._OptimizeAll();
		}

		public void _AddRoot(AnRoot target)
		{
			if (this._rootTable == null)
			{
				this._rootTable = new Hashtable();
			}
			if (this._rootList == null)
			{
				this._rootList = new List<AnRoot>();
			}
			if (this._ExistRoot(target))
			{
				return;
			}
			this._rootList.Add(target);
			this._rootTable.Add(target, target);
			this._AddActiveLayerTable(target.gameObject);
		}

		public void _RemoveRoot(AnRoot target)
		{
			if (this._rootTable == null)
			{
				this._rootTable = new Hashtable();
			}
			if (this._rootList == null)
			{
				this._rootList = new List<AnRoot>();
			}
			if (!this._ExistRoot(target))
			{
				return;
			}
			this._rootList.Remove(target);
			this._rootTable.Remove(target);
		}

		private bool _ExistRoot(AnRoot target)
		{
			return this._rootTable.ContainsKey(target);
		}

		private void _OptimizeRootList()
		{
			if (this._rootTable == null)
			{
				this._rootTable = new Hashtable();
			}
			if (this._rootList == null)
			{
				this._rootList = new List<AnRoot>();
			}
			if (this._tempRootList == null)
			{
				this._tempRootList = new List<AnRoot>();
			}
			this._tempRootList.Clear();
			for (int i = 0; i < this._rootList.Count; i++)
			{
				AnRoot anRoot = this._rootList[i];
				if (!(anRoot == null) && !(anRoot.gameObject == null))
				{
					this._tempRootList.Add(anRoot);
				}
			}
			this._rootList.Clear();
			this._rootTable.Clear();
			for (int j = 0; j < this._tempRootList.Count; j++)
			{
				AnRoot anRoot2 = this._tempRootList[j];
				this._rootList.Add(anRoot2);
				this._rootTable.Add(anRoot2, anRoot2);
			}
			this._tempRootList.Clear();
		}

		public AnRootParameter _GetRootParameter(AnRootParameter rootParameter)
		{
			if (this._rootParameterTable == null)
			{
				this._rootParameterTable = new Hashtable();
			}
			if (this._rootParameterTable.ContainsKey(rootParameter.ID))
			{
				AnRootParameter anRootParameter = this._rootParameterTable[rootParameter.ID] as AnRootParameter;
				if (anRootParameter != null)
				{
					return anRootParameter;
				}
				this._rootParameterTable.Remove(rootParameter.ID);
			}
			this._rootParameterTable.Add(rootParameter.ID, rootParameter);
			rootParameter._Initialize();
			return rootParameter;
		}

		private void _OptimizeRootParameterTable()
		{
			if (this._rootTable == null)
			{
				return;
			}
			if (this._rootParameterTable == null)
			{
				return;
			}
			if (this._tempRootParameterList == null)
			{
				this._tempRootParameterList = new List<AnRootParameter>();
			}
			this._tempRootParameterList.Clear();
			for (int i = 0; i < this._rootList.Count; i++)
			{
				if (!(this._rootList[i] == null) && !(this._rootList[i].Parameter == null))
				{
					bool flag = false;
					for (int j = 0; j < this._tempRootParameterList.Count; j++)
					{
						if (this._tempRootParameterList[j] == this._rootList[i].Parameter)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this._tempRootParameterList.Add(this._rootList[i].Parameter);
					}
				}
			}
			this._rootParameterTable.Clear();
			for (int k = 0; k < this._tempRootParameterList.Count; k++)
			{
				if (!this._rootParameterTable.ContainsKey(this._tempRootParameterList[k].ID))
				{
					this._rootParameterTable.Add(this._tempRootParameterList[k].ID, this._tempRootParameterList[k]);
				}
			}
			this._tempRootParameterList.Clear();
		}

		public AnMeshParameter _GetMeshParameter(AnMeshParameter meshParameter)
		{
			if (this._meshParameterTable == null)
			{
				this._meshParameterTable = new Hashtable();
			}
			if (this._meshParameterTable.ContainsKey(meshParameter.ID))
			{
				AnMeshParameter anMeshParameter = this._meshParameterTable[meshParameter.ID] as AnMeshParameter;
				if (anMeshParameter != null)
				{
					return anMeshParameter;
				}
				this._meshParameterTable.Remove(meshParameter.ID);
			}
			this._meshParameterTable.Add(meshParameter.ID, meshParameter);
			meshParameter._Initialize();
			return meshParameter;
		}

		private void _OptimizeMeshParameterTable()
		{
			if (this._rootTable == null)
			{
				return;
			}
			if (this._meshParameterTable == null)
			{
				return;
			}
			if (this._tempMeshParameterList == null)
			{
				this._tempMeshParameterList = new List<AnMeshParameter>();
			}
			this._tempMeshParameterList.Clear();
			for (int i = 0; i < this._rootList.Count; i++)
			{
				AnRoot anRoot = this._rootList[i];
				if (!(anRoot == null) && anRoot.MeshParameterGroup != null && anRoot.MeshParameterGroup.MeshParameterList != null)
				{
					for (int j = 0; j < anRoot.MeshParameterGroup._meshParameterList.Count; j++)
					{
						AnMeshParameter anMeshParameter = anRoot.MeshParameterGroup._meshParameterList[j];
						if (!(anMeshParameter == null))
						{
							bool flag = false;
							for (int k = 0; k < this._tempMeshParameterList.Count; k++)
							{
								if (this._tempMeshParameterList[k] == anMeshParameter)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								this._tempMeshParameterList.Add(anMeshParameter);
							}
						}
					}
				}
			}
			this._meshParameterTable.Clear();
			for (int l = 0; l < this._tempMeshParameterList.Count; l++)
			{
				this._GetMeshParameter(this._tempMeshParameterList[l]);
			}
			this._tempMeshParameterList.Clear();
		}

		private void _CreatePlaneMainShaderTable()
		{
			if (this._planeShaderTable != null)
			{
				return;
			}
			this._planeShaderTable = new Hashtable();
			this._planeShaderPathList = new List<string>();
			this._planeShaderPathList.Add(AnValue.ShaderNormalPath);
			this._planeShaderPathList.Add(AnValue.ShaderAddPath);
			this._planeShaderPathList.Add(AnValue.ShaderSubPath);
			this._planeShaderPathList.Add(AnValue.ShaderMultiplyPath);
			this._planeShaderPathList.Add(AnValue.ShaderHardLightPath);
			this._planeShaderPathList.Add(AnValue.ShaderInvertPath);
			this._planeShaderPathList.Add(AnValue.ShaderOpaquePath);
			this._planeShaderPathList.Add(AnValue.ShaderGrayscalePath);
			this._planeShaderPathList.Add(AnValue.ShaderMaskPath);
			this._planeShaderPathList.Add(AnValue.ShaderAlphaMaskPath);
			this._planeShaderPathList.Add(AnValue.ShaderAlphaMaskMultiplyPath);
			this._planeShaderPathList.Add(AnValue.ShaderStencilMaskPath);
			this._planeShaderPathList.Add(AnValue.ShaderStencilAlphaMaskPath);
			this._planeShaderPathList.Add(AnValue.ShaderObjectMaskPath);
			this._planeShaderPathList.Add(AnValue.ShaderObjectAlphaMaskPath);
			this._planeShaderPathList.Add(AnValue.ShaderNormal3DPath);
			this._planeShaderPathList.Add(AnValue.ShaderAdd3DPath);
			this._planeShaderPathList.Add(AnValue.ShaderNormalBlurPath);
			this._planeShaderPathList.Add(AnValue.ShaderAddBlurPath);
			this._planeShaderPathList.Add(AnValue.ShaderMultiplyBlurPath);
			this._planeShaderPathList.Add(AnValue.ShaderGrayscaleBlurPath);
			this._planeShaderPathList.Add(AnValue.ShaderNormalHorizontalFadePath);
			this._planeShaderPathList.Add(AnValue.ShaderNormalVerticalFadePath);
			this._planeShaderTypeList = new List<AnShaderTypes>();
			this._planeShaderTypeList.Add(AnShaderTypes.Normal);
			this._planeShaderTypeList.Add(AnShaderTypes.Add);
			this._planeShaderTypeList.Add(AnShaderTypes.Sub);
			this._planeShaderTypeList.Add(AnShaderTypes.Multiply);
			this._planeShaderTypeList.Add(AnShaderTypes.HardLight);
			this._planeShaderTypeList.Add(AnShaderTypes.Invert);
			this._planeShaderTypeList.Add(AnShaderTypes.Opaque);
			this._planeShaderTypeList.Add(AnShaderTypes.Grayscale);
			this._planeShaderTypeList.Add(AnShaderTypes.Mask);
			this._planeShaderTypeList.Add(AnShaderTypes.AlphaMask);
			this._planeShaderTypeList.Add(AnShaderTypes.AlphaMaskMultiply);
			this._planeShaderTypeList.Add(AnShaderTypes.StencilMask);
			this._planeShaderTypeList.Add(AnShaderTypes.StencilAlphaMask);
			this._planeShaderTypeList.Add(AnShaderTypes.ObjectMask);
			this._planeShaderTypeList.Add(AnShaderTypes.ObjectAlphaMask);
			this._planeShaderTypeList.Add(AnShaderTypes.Normal3D);
			this._planeShaderTypeList.Add(AnShaderTypes.Add3D);
			this._planeShaderTypeList.Add(AnShaderTypes.NormalBlur);
			this._planeShaderTypeList.Add(AnShaderTypes.AddBlur);
			this._planeShaderTypeList.Add(AnShaderTypes.MultiplyBlur);
			this._planeShaderTypeList.Add(AnShaderTypes.GrayscaleBlur);
			this._planeShaderTypeList.Add(AnShaderTypes.NormalHorizontalFade);
			this._planeShaderTypeList.Add(AnShaderTypes.NormalVerticalFade);
			for (int i = 0; i < this._planeShaderPathList.Count; i++)
			{
				Shader shader = Shader.Find(this._planeShaderPathList[i]);
				this._planeShaderTable.Add(this._planeShaderTypeList[i], shader);
				this.AddPlaneAlphaFadeShaderTable(this._planeShaderPathList[i], shader);
			}
		}

		public Shader _GetPlaneMainShader(AnShaderTypes targetShader = AnShaderTypes.Normal)
		{
			this._CreatePlaneMainShaderTable();
			if (this._planeShaderTable.ContainsKey(targetShader))
			{
				return this._planeShaderTable[targetShader] as Shader;
			}
			return this._planeShaderTable[AnShaderTypes.Normal] as Shader;
		}

		private void _CreatePlaneA8ShaderTable()
		{
			if (this._planeA8ShaderTable != null)
			{
				return;
			}
			if (this._planeShaderPathList == null)
			{
				return;
			}
			if (this._planeShaderTypeList == null)
			{
				return;
			}
			this._planeA8ShaderTable = new Hashtable();
			List<string> list = new List<string>();
			List<AnShaderTypes> list2 = new List<AnShaderTypes>();
			for (int i = 0; i < this._planeShaderPathList.Count; i++)
			{
				list.Add(this._planeShaderPathList[i].Replace(AnValue.ShaderMainString, AnValue.ShaderA8String));
				list2.Add(this._planeShaderTypeList[i]);
			}
			int j = 0;
			while (j < list.Count)
			{
				Shader shader = Shader.Find(list[j]);
				if (!(shader == null))
				{
					goto IL_00AF;
				}
				shader = Shader.Find(list[0]);
				if (!(shader == null))
				{
					goto IL_00AF;
				}
				IL_00C8:
				j++;
				continue;
				IL_00AF:
				this._planeA8ShaderTable.Add(list2[j], shader);
				goto IL_00C8;
			}
		}

		public Shader _GetPlaneA8Shader(AnShaderTypes targetShader = AnShaderTypes.Normal)
		{
			this._CreatePlaneA8ShaderTable();
			if (this._planeA8ShaderTable.ContainsKey(targetShader))
			{
				return this._planeA8ShaderTable[targetShader] as Shader;
			}
			return this._planeA8ShaderTable[AnShaderTypes.Normal] as Shader;
		}

		private void _CreatePlaneNoTexAlphaShaderTable()
		{
			if (this._planeNoAlphaTexShaderTable != null)
			{
				return;
			}
			if (this._planeShaderPathList == null)
			{
				return;
			}
			if (this._planeShaderTypeList == null)
			{
				return;
			}
			this._planeNoAlphaTexShaderTable = new Hashtable();
			List<string> list = new List<string>();
			List<AnShaderTypes> list2 = new List<AnShaderTypes>();
			for (int i = 0; i < this._planeShaderPathList.Count; i++)
			{
				list.Add(this._planeShaderPathList[i].Replace(AnValue.ShaderMainString, AnValue.ShaderNoTexAlphaString));
				list2.Add(this._planeShaderTypeList[i]);
			}
			int j = 0;
			while (j < list.Count)
			{
				Shader shader = Shader.Find(list[j]);
				if (!(shader == null))
				{
					goto IL_00AF;
				}
				shader = Shader.Find(list[0]);
				if (!(shader == null))
				{
					goto IL_00AF;
				}
				IL_00D7:
				j++;
				continue;
				IL_00AF:
				this._planeNoAlphaTexShaderTable.Add(list2[j], shader);
				this.AddPlaneAlphaFadeShaderTable(list[j], shader);
				goto IL_00D7;
			}
		}

		public Shader _GetPlaneNoTexAlphaShader(AnShaderTypes targetShader = AnShaderTypes.Normal)
		{
			this._CreatePlaneNoTexAlphaShaderTable();
			if (this._planeNoAlphaTexShaderTable.ContainsKey(targetShader))
			{
				return this._planeNoAlphaTexShaderTable[targetShader] as Shader;
			}
			return this._planeNoAlphaTexShaderTable[AnShaderTypes.Normal] as Shader;
		}

		private void _CreateTextMainShaderTable()
		{
			if (this._fontShaderTable != null)
			{
				return;
			}
			this._fontShaderTable = new Hashtable();
			List<string> list = new List<string>();
			list.Add(AnValue.ShaderTextNormalPath);
			list.Add(AnValue.ShaderTextAddPath);
			list.Add(AnValue.ShaderTextMultiplyPath);
			list.Add(AnValue.ShaderTextGrayscalePath);
			list.Add(AnValue.ShaderTextStencilAlphaMaskPath);
			list.Add(AnValue.ShaderTextNormalGradationPath);
			list.Add(AnValue.ShaderTextAddGradationPath);
			list.Add(AnValue.ShaderTextMultiplyGradationPath);
			list.Add(AnValue.ShaderTextGrayscaleGradationPath);
			List<AnShaderTypes> list2 = new List<AnShaderTypes>();
			list2.Add(AnShaderTypes.Normal);
			list2.Add(AnShaderTypes.Add);
			list2.Add(AnShaderTypes.Multiply);
			list2.Add(AnShaderTypes.Grayscale);
			list2.Add(AnShaderTypes.StencilAlphaMask);
			list2.Add(AnShaderTypes.NormalGradation);
			list2.Add(AnShaderTypes.AddGradation);
			list2.Add(AnShaderTypes.MultiplyGradation);
			list2.Add(AnShaderTypes.GrayscaleGradation);
			for (int i = 0; i < list.Count; i++)
			{
				Shader shader = Shader.Find(list[i]);
				this._fontShaderTable.Add(list2[i], shader);
			}
		}

		public Shader _GetTextMainShader(AnShaderTypes targetShader = AnShaderTypes.Normal)
		{
			this._CreateTextMainShaderTable();
			if (this._fontShaderTable.ContainsKey(targetShader))
			{
				return this._fontShaderTable[targetShader] as Shader;
			}
			return this._fontShaderTable[AnShaderTypes.Normal] as Shader;
		}

		private void _CreateTextIconShaderTable()
		{
			if (this._fontIconShaderTable != null)
			{
				return;
			}
			this._fontIconShaderTable = new Hashtable();
			List<string> list = new List<string>();
			list.Add(AnValue.ShaderTextIconNormalPath);
			list.Add(AnValue.ShaderTextIconAddPath);
			list.Add(AnValue.ShaderTextIconMultiplyPath);
			list.Add(AnValue.ShaderTextIconGrayscalePath);
			List<AnShaderTypes> list2 = new List<AnShaderTypes>();
			list2.Add(AnShaderTypes.Normal);
			list2.Add(AnShaderTypes.Add);
			list2.Add(AnShaderTypes.Multiply);
			list2.Add(AnShaderTypes.Grayscale);
			for (int i = 0; i < list.Count; i++)
			{
				Shader shader = Shader.Find(list[i]);
				this._fontIconShaderTable.Add(list2[i], shader);
			}
		}

		public Shader _GetTextIconShader(AnShaderTypes targetShader = AnShaderTypes.Normal)
		{
			this._CreateTextIconShaderTable();
			if (this._fontIconShaderTable.ContainsKey(targetShader))
			{
				return this._fontIconShaderTable[targetShader] as Shader;
			}
			return this._fontIconShaderTable[AnShaderTypes.Normal] as Shader;
		}

		private void AddPlaneAlphaFadeShaderTable(string baseShaderName, Shader key)
		{
			this.AddShaderVariantTable(AnShaderVariantTypes.HorizontalFade, key, baseShaderName + AnValue.ShaderHorizontalFadeString);
			this.AddShaderVariantTable(AnShaderVariantTypes.VerticalFade, key, baseShaderName + AnValue.ShaderVerticalFadeString);
		}

		private void AddShaderVariantTable(AnShaderVariantTypes variantType, Shader key, string shaderName)
		{
			Shader shader = Shader.Find(shaderName);
			if (shader == null)
			{
				return;
			}
			Dictionary<Shader, Shader> dictionary;
			if (!this._planeShaderVariantTables.TryGetValue(variantType, out dictionary))
			{
				dictionary = new Dictionary<Shader, Shader>();
				this._planeShaderVariantTables.Add(variantType, dictionary);
			}
			dictionary.Add(key, shader);
		}

		public bool TryGetPlaneVariantShader(AnShaderVariantTypes variantType, Shader target, out Shader shader)
		{
			shader = target;
			Dictionary<Shader, Shader> dictionary;
			if (!this._planeShaderVariantTables.TryGetValue(variantType, out dictionary))
			{
				return false;
			}
			Shader shader2;
			if (!dictionary.TryGetValue(target, out shader2))
			{
				return false;
			}
			shader = shader2;
			return true;
		}

		public Font _GetFont(string fontName, bool fromCommon)
		{
			if (fontName == null)
			{
				return null;
			}
			if (!this._existGlobalData)
			{
				return null;
			}
			Font font;
			if (fromCommon)
			{
				font = this._globalData._GetFontFromCommon(fontName);
			}
			else
			{
				font = this._globalData._GetFont(fontName);
			}
			if (font == null)
			{
				return null;
			}
			return font;
		}

		public bool _GetFontMaterial(Hashtable hashTable, string fontName, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, bool useCommon, ref Material fontMaterial)
		{
			if (!this._existGlobalData)
			{
				return false;
			}
			this._CreateFontMaterial(hashTable, fontName, shaderType, stencilRef, baseStencilRef, stencilCompareFunc, useCommon, ref fontMaterial);
			return !(fontMaterial == null);
		}

		private void _CreateFontMaterial(Hashtable hashTable, string fontName, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, bool useCommon, ref Material fontMaterial)
		{
			if (!this._existGlobalData)
			{
				return;
			}
			if (this._fontMaterialTable == null)
			{
				this._fontMaterialTable = new Hashtable();
			}
			Font font;
			if (useCommon)
			{
				font = this._globalData._GetFontFromCommon(fontName);
			}
			else
			{
				font = this._globalData._GetFont(fontName);
			}
			if (font == null)
			{
				return;
			}
			AnStencilCompareFuncTypes stencilCompareType = AnUtilityMaterial.GetStencilCompareType(shaderType, stencilRef, baseStencilRef, stencilCompareFunc);
			string text = string.Concat(new string[]
			{
				fontName,
				"_",
				font.name,
				"_",
				AnUtilityMaterial.GetMaterialKey(shaderType, stencilRef, stencilCompareType)
			});
			AnRootManager.SharedMaterialInfo sharedMaterialInfo;
			if (this._fontMaterialTable.ContainsKey(text))
			{
				sharedMaterialInfo = this._fontMaterialTable[text] as AnRootManager.SharedMaterialInfo;
				fontMaterial = sharedMaterialInfo._material;
				if (fontMaterial != null)
				{
					if (!hashTable.ContainsKey(text))
					{
						sharedMaterialInfo._refCount++;
						hashTable.Add(text, text);
					}
					return;
				}
				this._fontMaterialTable.Remove(text);
			}
			sharedMaterialInfo = new AnRootManager.SharedMaterialInfo
			{
				_refCount = 1,
				_material = new Material(font.material)
			};
			fontMaterial = sharedMaterialInfo._material;
			fontMaterial.name = text;
			fontMaterial.shader = this._GetTextMainShader(shaderType);
			fontMaterial.SetFloat(AnValue.ShaderParamStencilRef, (float)stencilRef);
			fontMaterial.SetFloat(AnValue.ShaderParamStencilComp, (float)stencilCompareType);
			this._fontMaterialTable.Add(text, sharedMaterialInfo);
			hashTable.Add(text, fontMaterial);
		}

		public bool _CloneFontMaterial(Hashtable hashTable, Material baseFontMaterial, string id, ref Material fontMaterial)
		{
			if (baseFontMaterial == null)
			{
				return false;
			}
			if (this._fontMaterialTable == null)
			{
				this._fontMaterialTable = new Hashtable();
			}
			string text = baseFontMaterial.name + AnValue.CloneString + id;
			AnRootManager.SharedMaterialInfo sharedMaterialInfo;
			if (this._fontMaterialTable.ContainsKey(text))
			{
				sharedMaterialInfo = this._fontMaterialTable[text] as AnRootManager.SharedMaterialInfo;
				fontMaterial = sharedMaterialInfo._material;
				if (fontMaterial != null)
				{
					if (!hashTable.ContainsKey(text))
					{
						sharedMaterialInfo._refCount++;
						hashTable.Add(text, fontMaterial);
					}
					return true;
				}
				this._fontMaterialTable.Remove(text);
			}
			sharedMaterialInfo = new AnRootManager.SharedMaterialInfo
			{
				_refCount = 1,
				_material = new Material(baseFontMaterial)
			};
			fontMaterial = sharedMaterialInfo._material;
			fontMaterial.name = text;
			this._fontMaterialTable.Add(text, sharedMaterialInfo);
			hashTable.Add(text, fontMaterial);
			return true;
		}

		public void RemoveFontMaterial(string materialKey)
		{
			if (this._fontMaterialTable == null)
			{
				return;
			}
			if (!this._fontMaterialTable.ContainsKey(materialKey))
			{
				return;
			}
			AnRootManager.SharedMaterialInfo sharedMaterialInfo = this._fontMaterialTable[materialKey] as AnRootManager.SharedMaterialInfo;
			sharedMaterialInfo._refCount--;
			if (sharedMaterialInfo._refCount <= 0)
			{
				global::UnityEngine.Object.Destroy(sharedMaterialInfo._material);
				this._fontMaterialTable.Remove(materialKey);
			}
		}

		public void AddFont(Font font)
		{
			if (!this._existGlobalData)
			{
				return;
			}
			this._globalData._AddFontToAddFontTable(font);
		}

		public void _CloneTextIconMaterialList(Hashtable hashTable, string id, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, ref Material[] materialList)
		{
			if (!this._existGlobalData)
			{
				if (materialList == null)
				{
					materialList = new Material[1];
				}
				return;
			}
			if (materialList == null)
			{
				materialList = new Material[AnMonoSingleton<AnRootManager>.Instance.GlobalData.FontIconParameterList.Count + 1];
			}
			this._tempMaterial00 = null;
			this._tempMaterial01 = null;
			for (int i = 0; i < this._globalData.FontIconParameterList.Count; i++)
			{
				AnFontIconParameter anFontIconParameter = this._globalData.FontIconParameterList[i];
				if (anFontIconParameter == null)
				{
					return;
				}
				if (anFontIconParameter.ColorTexture == null)
				{
					return;
				}
				if (anFontIconParameter.AlphaTexture == null)
				{
					return;
				}
				this._GetFontIconMaterial(hashTable, anFontIconParameter.ColorTexture.name, shaderType, stencilRef, baseStencilRef, stencilCompareFunc, ref this._tempMaterial00);
				this._CloneFontIconMaterial(hashTable, this._tempMaterial00, id, ref this._tempMaterial01);
				materialList[i + 1] = this._tempMaterial01;
			}
			this._tempMaterial00 = null;
			this._tempMaterial01 = null;
		}

		private bool _GetFontIconMaterial(Hashtable hashTable, string fontIconName, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, ref Material fontIconMaterial)
		{
			if (fontIconName == null)
			{
				return false;
			}
			if (!this._existGlobalData)
			{
				return false;
			}
			this._CreateFontIconMaterial(hashTable, fontIconName, shaderType, stencilRef, baseStencilRef, stencilCompareFunc, ref fontIconMaterial);
			return !(fontIconMaterial == null);
		}

		private void _CreateFontIconMaterial(Hashtable hashTable, string fontIconName, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, ref Material fontIconMaterial)
		{
			if (this._fontIconMaterialTable == null)
			{
				this._fontIconMaterialTable = new Hashtable();
			}
			if (!this._existGlobalData)
			{
				return;
			}
			AnFontIconParameter anFontIconParameter = this._globalData._GetFontIconParameter(fontIconName);
			if (anFontIconParameter == null)
			{
				return;
			}
			if (anFontIconParameter.ColorTexture == null)
			{
				return;
			}
			if (anFontIconParameter.AlphaTexture == null)
			{
				return;
			}
			AnStencilCompareFuncTypes stencilCompareType = AnUtilityMaterial.GetStencilCompareType(shaderType, stencilRef, baseStencilRef, stencilCompareFunc);
			string text = fontIconName + "_" + AnUtilityMaterial.GetMaterialKey(shaderType, stencilRef, stencilCompareType);
			AnRootManager.SharedMaterialInfo sharedMaterialInfo;
			if (this._fontIconMaterialTable.ContainsKey(text))
			{
				sharedMaterialInfo = this._fontIconMaterialTable[text] as AnRootManager.SharedMaterialInfo;
				fontIconMaterial = sharedMaterialInfo._material;
				if (fontIconMaterial != null)
				{
					if (!hashTable.ContainsKey(text))
					{
						sharedMaterialInfo._refCount++;
						hashTable.Add(text, fontIconMaterial);
					}
					return;
				}
				this._fontIconMaterialTable.Remove(text);
			}
			sharedMaterialInfo = new AnRootManager.SharedMaterialInfo
			{
				_refCount = 1,
				_material = new Material(this._GetTextIconShader(shaderType))
			};
			fontIconMaterial = sharedMaterialInfo._material;
			fontIconMaterial.name = text;
			fontIconMaterial.SetTexture(AnValue.ShaderParamMainTex, anFontIconParameter.ColorTexture);
			fontIconMaterial.SetTexture(AnValue.ShaderParamAlphaTex, anFontIconParameter.AlphaTexture);
			fontIconMaterial.SetFloat(AnValue.ShaderParamStencilRef, (float)stencilRef);
			fontIconMaterial.SetFloat(AnValue.ShaderParamStencilComp, (float)stencilCompareType);
			this._fontIconMaterialTable.Add(text, sharedMaterialInfo);
			hashTable.Add(text, fontIconMaterial);
		}

		private bool _CloneFontIconMaterial(Hashtable hashTable, Material baseFontIconMaterial, string id, ref Material fontIconMaterial)
		{
			if (baseFontIconMaterial == null)
			{
				return false;
			}
			if (this._fontIconMaterialTable == null)
			{
				this._fontIconMaterialTable = new Hashtable();
			}
			string text = baseFontIconMaterial.name + AnValue.CloneString + id;
			AnRootManager.SharedMaterialInfo sharedMaterialInfo;
			if (this._fontIconMaterialTable.ContainsKey(text))
			{
				sharedMaterialInfo = this._fontIconMaterialTable[text] as AnRootManager.SharedMaterialInfo;
				fontIconMaterial = sharedMaterialInfo._material;
				if (fontIconMaterial != null)
				{
					if (!hashTable.ContainsKey(text))
					{
						sharedMaterialInfo._refCount++;
						hashTable.Add(text, fontIconMaterial);
					}
					return true;
				}
				this._fontIconMaterialTable.Remove(text);
			}
			sharedMaterialInfo = new AnRootManager.SharedMaterialInfo
			{
				_refCount = 1,
				_material = new Material(baseFontIconMaterial)
			};
			fontIconMaterial = sharedMaterialInfo._material;
			fontIconMaterial.name = text;
			this._fontIconMaterialTable.Add(text, fontIconMaterial);
			hashTable.Add(text, fontIconMaterial);
			return true;
		}

		public void RemoveFontIconMaterial(string materialKey)
		{
			if (this._fontIconMaterialTable == null)
			{
				return;
			}
			if (!this._fontIconMaterialTable.ContainsKey(materialKey))
			{
				return;
			}
			AnRootManager.SharedMaterialInfo sharedMaterialInfo = this._fontIconMaterialTable[materialKey] as AnRootManager.SharedMaterialInfo;
			sharedMaterialInfo._refCount--;
			if (sharedMaterialInfo._refCount <= 0)
			{
				global::UnityEngine.Object.Destroy(sharedMaterialInfo._material);
				this._fontIconMaterialTable.Remove(materialKey);
			}
		}

		private void _LoadGlobalData()
		{
			this._existGlobalData = false;
			AnGlobalDataMediator anGlobalDataMediator = Resources.Load<AnGlobalDataMediator>(AnValue.GlobalDataMediatorPath);
			if (anGlobalDataMediator == null)
			{
				return;
			}
			this._globalData = anGlobalDataMediator.GlobalData;
			if (this._globalData == null)
			{
				return;
			}
			this._existGlobalData = true;
			this._globalData._Initialize();
		}

		public void SetLocalizeTaget(string localizeTarget)
		{
			this._localizeTarget = localizeTarget;
			if (!this._existGlobalData)
			{
				return;
			}
			this._globalData._UpdateFontTable();
		}

		private void _InitializeScreenSize()
		{
			this._prevScreenWidth = float.MinValue;
			this._prevScreenHeight = float.MinValue;
			this._targetScreenSizeParameter = null;
			if (this._existGlobalData)
			{
				this._targetScreenSizeParameter = this._globalData._GetScreenSizeParameter(this._deviceModel);
			}
		}

		private void _UpdateScreenSize()
		{
			this._screenSizeChangeFlag = false;
			this._screenWidth = (float)Screen.width;
			this._screenHeight = (float)Screen.height;
			if (this._screenWidth == this._prevScreenWidth && this._screenHeight == this._prevScreenHeight)
			{
				return;
			}
			this._displayWidth = (float)Display.displays[0].systemWidth;
			this._displayHeight = (float)Display.displays[0].systemHeight;
			this._screenSafeArea = SafeAreaResolver.SafeArea;
			this._currentScreenAspect = this._screenWidth / this._screenHeight;
			this._prevScreenWidth = this._screenWidth;
			this._prevScreenHeight = this._screenHeight;
			this._screenTopMarginPercent = 0f;
			this._screenBottomMarginPercent = 0f;
			this._screenLeftMarginPercent = 0f;
			this._screenRightMarginPercent = 0f;
			this._screenMaxWideSize.x = 1000f;
			this._screenMaxWideSize.y = 1f;
			this._screenMaxNarrowSize.x = 1f;
			this._screenMaxNarrowSize.y = 1000f;
			if (this._screenSafeArea.x > 0f || this._screenSafeArea.xMax < this._displayWidth)
			{
				this._screenLeftMarginPercent = this._screenSafeArea.x / this._displayWidth;
				this._screenRightMarginPercent = (this._displayWidth - this._screenSafeArea.xMax) / this._displayWidth;
			}
			if (this._screenSafeArea.y > 0f || this._screenSafeArea.yMax < this._displayHeight)
			{
				this._screenTopMarginPercent = (this._displayHeight - this._screenSafeArea.yMax) / this._displayHeight;
				this._screenBottomMarginPercent = this._screenSafeArea.y / this._displayHeight;
			}
			if (this._targetScreenSizeParameter != null)
			{
				if (this._targetScreenSizeParameter.ScreenSize.x > 0f)
				{
					this._screenLeftMarginPercent = this._targetScreenSizeParameter.LeftMargin / this._targetScreenSizeParameter.ScreenSize.x;
					this._screenRightMarginPercent = this._targetScreenSizeParameter.RightMargin / this._targetScreenSizeParameter.ScreenSize.x;
				}
				if (this._targetScreenSizeParameter.ScreenSize.y > 0f)
				{
					this._screenTopMarginPercent = this._targetScreenSizeParameter.TopMargin / this._targetScreenSizeParameter.ScreenSize.y;
					this._screenBottomMarginPercent = this._targetScreenSizeParameter.BottomMargin / this._targetScreenSizeParameter.ScreenSize.y;
				}
				if (this._targetScreenSizeParameter.MaxWideSize.x > 0f && this._targetScreenSizeParameter.MaxWideSize.y > 0f)
				{
					this._screenMaxWideSize.x = this._targetScreenSizeParameter.MaxWideSize.x;
					this._screenMaxWideSize.y = this._targetScreenSizeParameter.MaxWideSize.y;
				}
				if (this._targetScreenSizeParameter.MaxNarrowSize.x > 0f && this._targetScreenSizeParameter.MaxNarrowSize.y > 0f)
				{
					this._screenMaxNarrowSize.x = this._targetScreenSizeParameter.MaxNarrowSize.x;
					this._screenMaxNarrowSize.y = this._targetScreenSizeParameter.MaxNarrowSize.y;
				}
			}
			this._screenMaxWideAspect = this._screenMaxWideSize.x / this._screenMaxWideSize.y;
			this._screenMaxNarrowAspect = this._screenMaxNarrowSize.x / this._screenMaxNarrowSize.y;
			this._screenSizeChangeFlag = true;
		}

		public T _GetFlBaseFromGameObject<T>(GameObject targetObject) where T : AnBase
		{
			if (this._rootTable == null)
			{
				return default(T);
			}
			foreach (object obj in this._rootTable.Values)
			{
				AnRoot anRoot = (AnRoot)obj;
				if (anRoot.DataTable.ContainsKey(targetObject))
				{
					return anRoot.DataTable[targetObject] as T;
				}
			}
			return default(T);
		}

		public int _GetUpdateGroup(int interval)
		{
			if (interval == 0)
			{
				return 0;
			}
			List<int> list = new List<int>();
			for (int i = 0; i < interval + 1; i++)
			{
				list.Add(0);
			}
			for (int j = 0; j < this._rootList.Count; j++)
			{
				AnRoot anRoot = this._rootList[j];
				if (!(anRoot == null) && !(anRoot.gameObject == null) && anRoot.UpdateInterval == interval)
				{
					List<int> list2 = list;
					int num = anRoot.UpdateGroup % (interval + 1);
					list2[num]++;
				}
			}
			int num2 = 0;
			int num3 = int.MaxValue;
			for (int k = 0; k < list.Count; k++)
			{
				if (list[k] < num3)
				{
					num2 = k;
					num3 = list[k];
				}
			}
			return num2;
		}

		public void SetCustomTimeScale(float timeScale)
		{
			this._customTimeScale = timeScale;
		}

		public float _GetDefaultLongTouchTime()
		{
			if (this._existGlobalData)
			{
				return this._globalData.DefaultLongTouchTime;
			}
			return 1f;
		}

		public float _GetKeyInputChangeStartDelayTime()
		{
			if (this._existGlobalData)
			{
				return this._globalData.KeyInputChangeStartDelayTime;
			}
			return 0.3f;
		}

		public float _GetKeyInputChangeDelayTime()
		{
			if (this._existGlobalData)
			{
				return this._globalData.KeyInputChangeDelayTime;
			}
			return 0.1f;
		}

		public float _GetRayInputSubmitDelay()
		{
			if (this._existGlobalData)
			{
				return this._globalData.RayInputSubmitDelay;
			}
			return 3f;
		}

		public List<string> _GetHorizontalAxisNameList(int playerIndex)
		{
			if (this._existGlobalData)
			{
				AnPlayerSetting anPlayerSetting = this._globalData._GetPlayerSetting(playerIndex);
				if (anPlayerSetting != null)
				{
					return anPlayerSetting.RuntimeKeyInputHorizontalNameList;
				}
			}
			return this._horizontalAxisNameList;
		}

		public List<string> _GetVerticalAxisNameList(int playerIndex)
		{
			if (this._existGlobalData)
			{
				AnPlayerSetting anPlayerSetting = this._globalData._GetPlayerSetting(playerIndex);
				if (anPlayerSetting != null)
				{
					return anPlayerSetting.RuntimeKeyInputVerticalNameList;
				}
			}
			return this._verticalAxisNameList;
		}

		public List<string> _GetSubmitButtonNameList(int playerIndex)
		{
			if (this._existGlobalData)
			{
				AnPlayerSetting anPlayerSetting = this._globalData._GetPlayerSetting(playerIndex);
				if (anPlayerSetting != null)
				{
					return anPlayerSetting.RuntimeKeyInputSubmitNameList;
				}
			}
			return this._subumitButtonNameList;
		}

		public List<string> _GetCancelButtonNameList(int playerIndex)
		{
			if (this._existGlobalData)
			{
				AnPlayerSetting anPlayerSetting = this._globalData._GetPlayerSetting(playerIndex);
				if (anPlayerSetting != null)
				{
					return anPlayerSetting.RuntimeKeyInputCancelNameList;
				}
			}
			return this._cancelButtonNameList;
		}

		public int _GetTextSortOderRoundValue()
		{
			if (!this._existGlobalData)
			{
				return 200;
			}
			return this._globalData.TextSortOderRoundValue;
		}

		public AnFontLocalizeParameter _GetFontLocalizeParam(string fontName, bool useCommon)
		{
			if (!this._existGlobalData)
			{
				return null;
			}
			AnFontLocalizeParameter anFontLocalizeParameter;
			if (useCommon)
			{
				anFontLocalizeParameter = this._globalData._GetFontLocalizeParamFromCommon(fontName);
			}
			else
			{
				anFontLocalizeParameter = this._globalData._GetFontLocalizeParam(fontName);
			}
			return anFontLocalizeParameter;
		}

		public int _GetTextOutlineQualityForMinFontSize(AnFontLocalizeParameter _localizeParam)
		{
			if (!this._existGlobalData)
			{
				return 20;
			}
			if (_localizeParam == null)
			{
				if (this._globalData.TextOutlineQualityForMinFontSize <= 0)
				{
					return 20;
				}
				return this._globalData.TextOutlineQualityForMinFontSize;
			}
			else
			{
				if (_localizeParam.TextOutlineQualityForMinFontSize <= 0)
				{
					return this._globalData.TextOutlineQualityForMinFontSize;
				}
				return _localizeParam.TextOutlineQualityForMinFontSize;
			}
		}

		public int _GetTextOutlineQualityMinFontSize(AnFontLocalizeParameter _localizeParam)
		{
			if (!this._existGlobalData)
			{
				return 50;
			}
			if (_localizeParam == null)
			{
				if (this._globalData.TextOutlineQualityMinFontSize <= 0)
				{
					return 50;
				}
				return this._globalData.TextOutlineQualityMinFontSize;
			}
			else
			{
				if (_localizeParam.TextOutlineQualityMinFontSize <= 0)
				{
					return this._globalData.TextOutlineQualityMinFontSize;
				}
				return _localizeParam.TextOutlineQualityMinFontSize;
			}
		}

		public int _GetTextOutlineQualityForMinOffset(AnFontLocalizeParameter _localizeParam)
		{
			if (!this._existGlobalData)
			{
				return 16;
			}
			if (_localizeParam == null)
			{
				if (this._globalData.TextOutlineQualityForMinOffset <= 0)
				{
					return 16;
				}
				return this._globalData.TextOutlineQualityForMinOffset;
			}
			else
			{
				if (_localizeParam.TextOutlineQualityForMinOffset <= 0)
				{
					return this._globalData.TextOutlineQualityForMinOffset;
				}
				return _localizeParam.TextOutlineQualityForMinOffset;
			}
		}

		public float _GetTextOutlineQualityMinOffset(AnFontLocalizeParameter _localizeParam)
		{
			if (!this._existGlobalData)
			{
				return 5f;
			}
			if (_localizeParam == null)
			{
				if (this._globalData.TextOutlineQualityMinOffset <= 0)
				{
					return 5f;
				}
				return (float)this._globalData.TextOutlineQualityMinOffset;
			}
			else
			{
				if (_localizeParam.TextOutlineQualityMinOffset <= 0f)
				{
					return (float)this._globalData.TextOutlineQualityMinOffset;
				}
				return _localizeParam.TextOutlineQualityMinOffset;
			}
		}

		public int _GetStencilMaskInterval()
		{
			if (!this._existGlobalData)
			{
				return 3;
			}
			return this._globalData.StencilMaskInterval;
		}

		public float _GetBaseScreenWidth()
		{
			if (!this._existGlobalData)
			{
				return 1920f;
			}
			return this._globalData.BaseScreenWidth;
		}

		public float _GetScrollStartPixel()
		{
			if (!this._existGlobalData)
			{
				return 5f;
			}
			return this._globalData.ScrollStartPixel;
		}

		public float _GetScrollSpeedValue()
		{
			if (!this._existGlobalData)
			{
				return 0.02f;
			}
			return this._globalData.ScrollSpeedValue;
		}

		public float _GetScrollAccelValue()
		{
			if (!this._existGlobalData)
			{
				return 0.05f;
			}
			return this._globalData.ScrollAccelValue;
		}

		public float _GetScrollIncrementValue()
		{
			if (!this._existGlobalData)
			{
				return 3f;
			}
			return this._globalData.ScrollIncrementValue;
		}

		private void _UpdateLayerTable()
		{
			if (this._layerTableByBitFlagKey == null)
			{
				this._layerTableByBitFlagKey = new Hashtable();
			}
			if (this._layerTableByNameKey == null)
			{
				this._layerTableByNameKey = new Hashtable();
			}
			if (this._layerBitFlagList == null)
			{
				this._layerBitFlagList = new List<int>();
			}
			if (this._layerNameList == null)
			{
				this._layerNameList = new List<string>();
			}
			this._layerTableByBitFlagKey.Clear();
			this._layerTableByNameKey.Clear();
			this._layerBitFlagList.Clear();
			this._layerNameList.Clear();
			for (int i = 0; i < this._maxLayerCount; i++)
			{
				string text = LayerMask.LayerToName(i);
				if (text != null && !(text == AnValue.TextEmpty) && !this._layerTableByNameKey.Contains(text))
				{
					int num = 1 << i;
					this._layerTableByNameKey.Add(text, num);
					this._layerTableByBitFlagKey.Add(num, text);
					this._layerBitFlagList.Add(num);
					this._layerNameList.Add(text);
				}
			}
		}

		public int _GetLayerBitFlag(string layerName)
		{
			if (!this._layerTableByNameKey.Contains(layerName))
			{
				return -1;
			}
			return (int)this._layerTableByNameKey[layerName];
		}

		public string _GetLayerName(int layerBitFlag)
		{
			if (!this._layerTableByBitFlagKey.Contains(layerBitFlag))
			{
				return null;
			}
			return (string)this._layerTableByBitFlagKey[layerBitFlag];
		}

		private void _AddActiveLayerTable(GameObject gameObject)
		{
			if (this._activeLayerBitFlagList == null)
			{
				this._activeLayerBitFlagList = new List<int>();
			}
			if (this._activeLayerTableByBitFlagKey == null)
			{
				this._activeLayerTableByBitFlagKey = new Hashtable();
			}
			int num = 1 << gameObject.layer;
			if (this._activeLayerTableByBitFlagKey.ContainsKey(num))
			{
				return;
			}
			string text = this._GetLayerName(num);
			this._activeLayerTableByBitFlagKey.Add(num, text);
			this._activeLayerBitFlagList.Add(num);
		}

		private void _OptimizeActiveLayerTable()
		{
			if (this._activeLayerTableByBitFlagKey == null)
			{
				this._activeLayerTableByBitFlagKey = new Hashtable();
			}
			if (this._activeLayerBitFlagList == null)
			{
				this._activeLayerBitFlagList = new List<int>();
			}
			this._activeLayerTableByBitFlagKey.Clear();
			this._activeLayerBitFlagList.Clear();
			if (this._rootList != null)
			{
				for (int i = 0; i < this._rootList.Count; i++)
				{
					this._AddActiveLayerTable(this._rootList[i].gameObject);
				}
			}
		}

		private void _UpdateSortingLayerTable()
		{
			if (this._sortingLayerTableByIndexKey == null)
			{
				this._sortingLayerTableByIndexKey = new Hashtable();
			}
			if (this._sortingLayerTableByNameKey == null)
			{
				this._sortingLayerTableByNameKey = new Hashtable();
			}
			if (this._sortingLayerIndexList == null)
			{
				this._sortingLayerIndexList = new List<int>();
			}
			if (this._sortingLayerNameList == null)
			{
				this._sortingLayerNameList = new List<string>();
			}
			this._sortingLayerTableByIndexKey.Clear();
			this._sortingLayerTableByNameKey.Clear();
			this._sortingLayerIndexList.Clear();
			this._sortingLayerNameList.Clear();
			for (int i = 0; i < SortingLayer.layers.Length; i++)
			{
				SortingLayer sortingLayer = SortingLayer.layers[i];
				if (!this._sortingLayerTableByNameKey.Contains(sortingLayer.name))
				{
					this._sortingLayerTableByIndexKey.Add(i, sortingLayer.name);
					this._sortingLayerTableByNameKey.Add(sortingLayer.name, i);
					this._sortingLayerIndexList.Add(i);
					this._sortingLayerNameList.Add(sortingLayer.name);
				}
			}
		}

		public int _GetSortingLayerIndex(string sortingLayerName)
		{
			if (!this._sortingLayerTableByNameKey.Contains(sortingLayerName))
			{
				return -1;
			}
			return (int)this._sortingLayerTableByNameKey[sortingLayerName];
		}

		public string _GetSortingLayerName(int sortingLayerIndex)
		{
			if (!this._sortingLayerTableByIndexKey.Contains(sortingLayerIndex))
			{
				return null;
			}
			return (string)this._sortingLayerTableByIndexKey[sortingLayerIndex];
		}

		public void AddSharedMaterial(string materialKey, Material material)
		{
			if (material == null)
			{
				return;
			}
			if (this._sharedMaterialTable == null)
			{
				this._sharedMaterialTable = new Hashtable();
			}
			if (this._sharedMaterialTable.ContainsKey(materialKey))
			{
				(this._sharedMaterialTable[materialKey] as AnRootManager.SharedMaterialInfo)._refCount++;
				return;
			}
			this._sharedMaterialTable[materialKey] = new AnRootManager.SharedMaterialInfo
			{
				_refCount = 1,
				_material = material
			};
		}

		public void RemoveSharedMaterial(string materialKey)
		{
			if (this._sharedMaterialTable == null)
			{
				return;
			}
			if (!this._sharedMaterialTable.ContainsKey(materialKey))
			{
				return;
			}
			AnRootManager.SharedMaterialInfo sharedMaterialInfo = this._sharedMaterialTable[materialKey] as AnRootManager.SharedMaterialInfo;
			sharedMaterialInfo._refCount--;
			if (sharedMaterialInfo._refCount <= 0)
			{
				global::UnityEngine.Object.DestroyImmediate(sharedMaterialInfo._material);
				this._sharedMaterialTable.Remove(materialKey);
			}
		}

		public List<float[]> _GetGaussianBlurValue(int quality, int precision)
		{
			if (this._gaussianBlurValueTable == null)
			{
				this._gaussianBlurValueTable = new Hashtable();
			}
			AnUtilityValue.LimitValue(ref quality, 1, 3);
			AnUtilityValue.LimitValue(ref precision, 1, 3);
			int num = quality * 10 + precision;
			if (this._gaussianBlurValueTable.ContainsKey(num))
			{
				return this._gaussianBlurValueTable[num] as List<float[]>;
			}
			List<float[]> list = new List<float[]>();
			float[] array = null;
			float[] array2 = null;
			float[] array3 = null;
			float num2 = 0f;
			if (quality == 1)
			{
				num2 = 1f;
			}
			else if (quality == 2)
			{
				num2 = 1.2f;
			}
			else if (quality == 3)
			{
				num2 = 1.4f;
			}
			else if (quality == 4)
			{
				num2 = 1.6f;
			}
			else if (quality == 5)
			{
				num2 = 1.8f;
			}
			else if (quality == 6)
			{
				num2 = 2f;
			}
			else if (quality == 7)
			{
				num2 = 2f;
			}
			AnUtilityMaterial.ComputeGaussianBlurList((float)quality, num2, precision, ref array, ref array2, ref array3);
			list.Add(array);
			list.Add(array2);
			list.Add(array3);
			this._gaussianBlurValueTable.Add(num, list);
			return list;
		}

		public void _LoadEditorSetting()
		{
		}

		private void _UpdateDebugComponent()
		{
		}

		private string _unityVersion;

		private string _deviceModel;

		private AnUIManager _uiManager;

		private List<AnRoot> _rootList;

		private Hashtable _rootTable;

		private List<AnRoot> _tempRootList;

		private Hashtable _rootParameterTable;

		private List<AnRootParameter> _tempRootParameterList;

		private Hashtable _meshParameterTable;

		private List<AnMeshParameter> _tempMeshParameterList;

		private Hashtable _planeShaderTable;

		private List<string> _planeShaderPathList;

		private List<AnShaderTypes> _planeShaderTypeList;

		private Hashtable _planeA8ShaderTable;

		private Hashtable _planeNoAlphaTexShaderTable;

		private Dictionary<AnShaderVariantTypes, Dictionary<Shader, Shader>> _planeShaderVariantTables;

		private string _localizeTarget;

		private Hashtable _fontShaderTable;

		private Hashtable _fontIconShaderTable;

		private Hashtable _fontMaterialTable;

		private Hashtable _fontIconMaterialTable;

		private Material _tempMaterial00;

		private Material _tempMaterial01;

		private int _maxLayerCount = 32;

		private List<int> _layerBitFlagList;

		private List<string> _layerNameList;

		private Hashtable _layerTableByBitFlagKey;

		private Hashtable _layerTableByNameKey;

		private List<int> _activeLayerBitFlagList;

		private Hashtable _activeLayerTableByBitFlagKey;

		private List<int> _sortingLayerIndexList;

		private List<string> _sortingLayerNameList;

		private Hashtable _sortingLayerTableByIndexKey;

		private Hashtable _sortingLayerTableByNameKey;

		private Hashtable _gaussianBlurValueTable;

		private Hashtable _sharedMaterialTable;

		private AnGlobalData _globalData;

		private bool _existGlobalData;

		[HideInInspector]
		public float _screenWidth;

		[HideInInspector]
		public float _screenHeight;

		[HideInInspector]
		public float _displayWidth;

		[HideInInspector]
		public float _displayHeight;

		[HideInInspector]
		public Rect _screenSafeArea = Rect.zero;

		[HideInInspector]
		public float _screenTopMarginPercent;

		[HideInInspector]
		public float _screenBottomMarginPercent;

		[HideInInspector]
		public float _screenLeftMarginPercent;

		[HideInInspector]
		public float _screenRightMarginPercent;

		[HideInInspector]
		public Vector2 _screenMaxWideSize = Vector2.zero;

		[HideInInspector]
		public float _screenMaxWideAspect;

		[HideInInspector]
		public Vector2 _screenMaxNarrowSize = Vector2.zero;

		[HideInInspector]
		public float _screenMaxNarrowAspect;

		[HideInInspector]
		public AnScreenSizeParameter _targetScreenSizeParameter;

		private float _prevScreenWidth;

		private float _prevScreenHeight;

		[HideInInspector]
		public float _currentScreenAspect;

		[HideInInspector]
		public bool _screenSizeChangeFlag;

		[HideInInspector]
		public float _currentTime;

		private float _prevTime;

		[HideInInspector]
		public float _currentOneFrameTime;

		private int _currentTargetFrameRate;

		private float _prevTargetFrameRate = -2.1474836E+09f;

		[HideInInspector]
		public float _currentDeltaTime;

		private float _customTimeScale;

		private List<string> _horizontalAxisNameList;

		private List<string> _verticalAxisNameList;

		private List<string> _subumitButtonNameList;

		private List<string> _cancelButtonNameList;

		private bool _useDebugComponent;

		private bool _useDebugLog;

		private float _screenRate = 1f;

		private class SharedMaterialInfo
		{
			public int _refCount;

			public Material _material;
		}
	}
}
