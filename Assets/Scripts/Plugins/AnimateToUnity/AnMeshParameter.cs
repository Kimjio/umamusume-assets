using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [CreateAssetMenu(fileName = "AnMeshParameter", menuName = "AnScriptableObject/AnMeshParameter", order = 1)]
    public class AnMeshParameter : AnScriptableObject
    {
		public List<AnMeshInfoParameterGroup> MeshParameterGroupList
		{
			get
			{
				return this._meshParameterGroupList;
			}
			set
			{
				this._meshParameterGroupList = value;
			}
		}

		public List<AnCustomMeshInfoParameter> CustomMeshInfoParameterList
		{
			get
			{
				return this._customMeshInfoParameterList;
			}
			set
			{
				this._customMeshInfoParameterList = value;
			}
		}

		public void _Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			for (int i = 0; i < this._meshParameterGroupList.Count; i++)
			{
				this._meshParameterGroupList[i].MeshParameter = this;
				this._meshParameterGroupList[i]._Initialize();
			}
			this._customMeshInfoParameterTable = new Hashtable();
			for (int j = 0; j < this._customMeshInfoParameterList.Count; j++)
			{
				AnCustomMeshInfoParameter anCustomMeshInfoParameter = this._customMeshInfoParameterList[j];
				if (!this._customMeshInfoParameterTable.ContainsKey(anCustomMeshInfoParameter.TextureName))
				{
					anCustomMeshInfoParameter.MeshParameter = this;
					this._customMeshInfoParameterTable.Add(anCustomMeshInfoParameter.TextureName, anCustomMeshInfoParameter);
				}
			}
			this._initialized = true;
		}

		public bool _CreateMesh(string textureName, List<Mesh> meshList, bool useCustomMesh, ref AnMeshInfoParameter meshInfo, ref Mesh mesh)
		{
			for (int i = 0; i < this._meshParameterGroupList.Count; i++)
			{
				if (this._meshParameterGroupList[i]._CreateMesh(textureName, meshList, useCustomMesh, ref meshInfo, ref mesh))
				{
					return true;
				}
			}
			return false;
		}

		public AnCustomMeshInfoParameter _GetCustomMeshInfoParam(string textureName)
		{
			if (!this._customMeshInfoParameterTable.ContainsKey(textureName))
			{
				return null;
			}
			return this._customMeshInfoParameterTable[textureName] as AnCustomMeshInfoParameter;
		}

		public bool _GetMaterial(string textureName, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, bool useCustomMesh, ref Material material)
		{
			for (int i = 0; i < this._meshParameterGroupList.Count; i++)
			{
				if (this._meshParameterGroupList[i]._GetMaterial(textureName, shaderType, stencilRef, baseStencilRef, stencilCompareFunc, useCustomMesh, ref material))
				{
					return true;
				}
			}
			return false;
		}

		public void _Destroy()
		{
			if (this._meshParameterGroupList != null)
			{
				for (int i = 0; i < this._meshParameterGroupList.Count; i++)
				{
					AnMeshInfoParameterGroup anMeshInfoParameterGroup = this._meshParameterGroupList[i];
					if (anMeshInfoParameterGroup != null)
					{
						anMeshInfoParameterGroup._Destroy();
					}
				}
			}
		}

		public bool _SearchMesh(string textureName, ref AnMeshInfoParameterGroup meshInfoParamGroup, ref AnMeshInfoParameter meshInfoParam)
		{
			if (Application.isPlaying)
			{
				this._Initialize();
			}
			for (int i = 0; i < this._meshParameterGroupList.Count; i++)
			{
				AnMeshInfoParameterGroup anMeshInfoParameterGroup = this._meshParameterGroupList[i];
				if (anMeshInfoParameterGroup._SearchMesh(textureName, ref meshInfoParam))
				{
					meshInfoParamGroup = anMeshInfoParameterGroup;
					return true;
				}
			}
			return false;
		}

        public List<AnCustomMeshInfoParameter> _customMeshInfoParameterList;

        public List<AnMeshInfoParameterGroup> _meshParameterGroupList;

        private Hashtable _customMeshInfoParameterTable;

        [NonSerialized]
        private bool _initialized;
    }
}
