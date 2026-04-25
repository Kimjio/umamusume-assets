using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnMeshParameterGroup
	{
		public List<AnMeshParameter> MeshParameterList
		{
			get
			{
				return this._meshParameterList;
			}
			set
			{
				this._meshParameterList = value;
			}
		}

		public void _Initialize()
		{
			for (int i = 0; i < this._meshParameterList.Count; i++)
			{
				this._meshParameterList[i] = AnMonoSingleton<AnRootManager>.Instance._GetMeshParameter(this._meshParameterList[i]);
			}
		}

		public void _InitializeEditor()
		{
			for (int i = 0; i < this._meshParameterList.Count; i++)
			{
				this._meshParameterList[i]._Initialize();
			}
		}

		public bool _CreateMesh(string textureName, bool useCustomMesh, ref AnMeshInfoParameter meshInfo, ref Mesh mesh)
		{
			if (this._notSharedMeshList == null)
			{
				this._notSharedMeshList = new List<Mesh>();
			}
			for (int i = 0; i < this._meshParameterList.Count; i++)
			{
				if (this._meshParameterList[i]._CreateMesh(textureName, this._notSharedMeshList, useCustomMesh, ref meshInfo, ref mesh))
				{
					return true;
				}
			}
			return false;
		}

		public bool _GetMaterial(string textureName, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, bool useCustomMesh, ref Material material)
		{
			for (int i = 0; i < this._meshParameterList.Count; i++)
			{
				if (this._meshParameterList[i]._GetMaterial(textureName, shaderType, stencilRef, baseStencilRef, stencilCompareFunc, useCustomMesh, ref material))
				{
					return true;
				}
			}
			return false;
		}

		public bool _CloneMaterial(Material baseMaterial, string id, ref Material material)
		{
			if (baseMaterial == null)
			{
				return false;
			}
			if (this._notSharedMaterialTable == null)
			{
				this._notSharedMaterialTable = new Hashtable();
			}
			string text = baseMaterial.name + AnValue.CloneString + id;
			if (this._notSharedMaterialTable.ContainsKey(text))
			{
				material = this._notSharedMaterialTable[text] as Material;
				return true;
			}
			material = new Material(baseMaterial);
			material.name = text;
			this._notSharedMaterialTable.Add(text, material);
			return true;
		}

		public void _Destroy()
		{
			if (this._notSharedMeshList != null)
			{
				foreach (Mesh mesh in this._notSharedMeshList)
				{
					if (!(mesh == null))
					{
						if (Application.isPlaying)
						{
							global::UnityEngine.Object.Destroy(mesh);
						}
						else
						{
							global::UnityEngine.Object.DestroyImmediate(mesh);
						}
					}
				}
				this._notSharedMeshList = null;
			}
			if (this._notSharedMaterialTable != null)
			{
				foreach (object obj in this._notSharedMaterialTable.Values)
				{
					Material material = (Material)obj;
					if (!(material == null))
					{
						if (Application.isPlaying)
						{
							global::UnityEngine.Object.Destroy(material);
						}
						else
						{
							global::UnityEngine.Object.DestroyImmediate(material);
						}
					}
				}
				this._notSharedMaterialTable = null;
			}
			if (this._meshParameterList != null)
			{
				for (int i = 0; i < this._meshParameterList.Count; i++)
				{
					AnMeshParameter anMeshParameter = this._meshParameterList[i];
					if (!(anMeshParameter == null))
					{
						anMeshParameter._Destroy();
					}
				}
			}
		}

		public bool _SearchMesh(string textureName, ref AnMeshInfoParameterGroup meshInfoParamGroup, ref AnMeshInfoParameter meshInfoParam)
		{
			using (List<AnMeshParameter>.Enumerator enumerator = this._meshParameterList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current._SearchMesh(textureName, ref meshInfoParamGroup, ref meshInfoParam))
					{
						return true;
					}
				}
			}
			return false;
		}

		public List<AnMeshParameter> _meshParameterList;

		private List<Mesh> _notSharedMeshList;

		private Hashtable _notSharedMaterialTable;
	}
}
