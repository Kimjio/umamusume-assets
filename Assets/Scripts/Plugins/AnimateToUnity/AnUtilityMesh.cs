using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnUtilityMesh
	{
		public static Mesh CreateMesh(string name, Vector2 size, Vector2 offset, Vector2 uvPosition, Vector2 uvSize, bool uvRotated)
		{
			Mesh mesh = new Mesh();
			mesh.name = name;
			Vector3[] array = new Vector3[]
			{
				new Vector3(-0.5f * size.x + offset.x, -0.5f * size.y + offset.y, 0f),
				new Vector3(0.5f * size.x + offset.x, 0.5f * size.y + offset.y, 0f),
				new Vector3(0.5f * size.x + offset.x, -0.5f * size.y + offset.y, 0f),
				new Vector3(-0.5f * size.x + offset.x, 0.5f * size.y + offset.y, 0f)
			};
			int[] array2 = new int[] { 0, 1, 2, 0, 3, 1 };
			Vector3[] array3 = new Vector3[4];
			array3[0] = new Vector3(0f, 0f, -1f);
			array3[1] = array3[0];
			array3[2] = array3[0];
			array3[3] = array3[0];
			Vector2[] array4 = new Vector2[4];
			if (!uvRotated)
			{
				array4[0] = new Vector2(0f, 0f) + uvPosition;
				array4[1] = new Vector2(uvSize.x, uvSize.y) + uvPosition;
				array4[2] = new Vector2(uvSize.x, 0f) + uvPosition;
				array4[3] = new Vector2(0f, uvSize.y) + uvPosition;
			}
			else
			{
				array4[0] = new Vector2(0f, uvSize.y) + uvPosition;
				array4[1] = new Vector2(uvSize.x, 0f) + uvPosition;
				array4[2] = new Vector2(0f, 0f) + uvPosition;
				array4[3] = new Vector2(uvSize.x, uvSize.y) + uvPosition;
			}
			Vector2[] array5 = new Vector2[4];
			array5[0] = new Vector2(0f, 0f);
			array5[1] = array5[0];
			array5[2] = array5[0];
			array5[3] = array5[0];
			Color[] array6 = new Color[4];
			array6[0] = Color.white;
			array6[1] = array6[0];
			array6[2] = array6[0];
			array6[3] = array6[0];
			mesh.vertices = array;
			mesh.triangles = array2;
			mesh.normals = array3;
			mesh.uv = array4;
			mesh.uv2 = array5;
			mesh.uv3 = array5;
			mesh.colors = array6;
			mesh.MarkDynamic();
			return mesh;
		}

		public static Mesh CreateNinesliceMesh(string name, Vector2 size, Vector2 offset, Vector2 uvPosition, Vector2 uvSize, bool uvRotated, Vector4 sliceRange)
		{
			Mesh mesh = new Mesh();
			mesh.name = name;
			Vector3[] array = new Vector3[16];
			Vector3[] array2 = new Vector3[16];
			Vector2[] array3 = new Vector2[16];
			Vector2[] array4 = new Vector2[16];
			Color[] array5 = new Color[16];
			for (int i = 0; i < 16; i++)
			{
				int num = Mathf.Min(new int[] { i / 4 });
				int num2 = i % 4;
				array[i] = Vector3.zero;
				if (!uvRotated)
				{
					if (num2 == 0)
					{
						array[i].x = -0.5f;
						array3[i].x = 0f;
					}
					else if (num2 == 1)
					{
						array[i].x = sliceRange[0] - 0.5f;
						array3[i].x = uvSize[0] * sliceRange[0];
					}
					else if (num2 == 2)
					{
						array[i].x = 1f - sliceRange[2] - 0.5f;
						array3[i].x = uvSize[0] * (1f - sliceRange[2]);
					}
					else if (num2 == 3)
					{
						array[i].x = 0.5f;
						array3[i].x = uvSize[0];
					}
					if (num == 0)
					{
						array[i].y = -0.5f;
						array3[i].y = 0f;
					}
					else if (num == 1)
					{
						array[i].y = sliceRange[1] - 0.5f;
						array3[i].y = uvSize[1] * sliceRange[1];
					}
					else if (num == 2)
					{
						array[i].y = 1f - sliceRange[3] - 0.5f;
						array3[i].y = uvSize[1] * (1f - sliceRange[3]);
					}
					else if (num == 3)
					{
						array[i].y = 0.5f;
						array3[i].y = uvSize[1];
					}
				}
				else
				{
					if (num2 == 0)
					{
						array[i].x = -0.5f;
						array3[i].y = uvSize[1];
					}
					else if (num2 == 1)
					{
						array[i].x = sliceRange[3] - 0.5f;
						array3[i].y = uvSize[1] * (1f - sliceRange[0]);
					}
					else if (num2 == 2)
					{
						array[i].x = 1f - sliceRange[1] - 0.5f;
						array3[i].y = uvSize[1] * sliceRange[2];
					}
					else if (num2 == 3)
					{
						array[i].x = 0.5f;
						array3[i].y = 0f;
					}
					if (num == 0)
					{
						array[i].y = -0.5f;
						array3[i].x = 0f;
					}
					else if (num == 1)
					{
						array[i].y = sliceRange[0] - 0.5f;
						array3[i].x = uvSize[0] * sliceRange[1];
					}
					else if (num == 2)
					{
						array[i].y = 1f - sliceRange[2] - 0.5f;
						array3[i].x = uvSize[0] * (1f - sliceRange[3]);
					}
					else if (num == 3)
					{
						array[i].y = 0.5f;
						array3[i].x = uvSize[0];
					}
				}
				array[i].x = array[i].x * size.x + offset.x;
				array[i].y = array[i].y * size.y + offset.y;
				array3[i] += uvPosition;
				array2[i] = new Vector3(0f, 0f, -1f);
				array4[i] = Vector2.zero;
				array5[i] = Color.white;
			}
			int[] array6 = new int[54];
			for (int j = 0; j < 9; j++)
			{
				int num = Mathf.Min(new int[] { j / 3 });
				int num2 = j % 3;
				array6[j * 6] = num2 + num * 4;
				array6[j * 6 + 1] = 5 + num2 + num * 4;
				array6[j * 6 + 2] = 1 + num2 + num * 4;
				array6[j * 6 + 3] = num2 + num * 4;
				array6[j * 6 + 4] = 4 + num2 + num * 4;
				array6[j * 6 + 5] = 5 + num2 + num * 4;
			}
			mesh.vertices = array;
			mesh.triangles = array6;
			mesh.normals = array2;
			mesh.uv = array3;
			mesh.uv2 = array4;
			mesh.colors = array5;
			mesh.MarkDynamic();
			return mesh;
		}

		public static void UpdateNinesliceVertexPositionList(Vector2 originalSize, Vector2 scale, Vector4 sliceValue, Vector3 offset, ref Vector3[] _vertexPositionList)
		{
			if (scale.x != 0f)
			{
				sliceValue.x /= scale.x;
				sliceValue.z /= scale.x;
			}
			else
			{
				sliceValue.x = 100f;
				sliceValue.z = 100f;
			}
			if (scale.y != 0f)
			{
				sliceValue.y /= scale.y;
				sliceValue.w /= scale.y;
			}
			else
			{
				sliceValue.y = 100f;
				sliceValue.w = 100f;
			}
			for (int i = 0; i < 16; i++)
			{
				int num = i / 4;
				int num2 = i % 4;
				_vertexPositionList[i] = Vector3.zero;
				if (num2 == 0)
				{
					_vertexPositionList[i].x = -0.5f;
				}
				else if (num2 == 1)
				{
					_vertexPositionList[i].x = AnUtilityValue.GetLimitValue(sliceValue.x - 0.5f, -0.5f, 0f);
				}
				else if (num2 == 2)
				{
					_vertexPositionList[i].x = AnUtilityValue.GetLimitValue(1f - sliceValue.z - 0.5f, 0f, 0.5f);
				}
				else if (num2 == 3)
				{
					_vertexPositionList[i].x = 0.5f;
				}
				if (num == 0)
				{
					_vertexPositionList[i].y = -0.5f;
				}
				else if (num == 1)
				{
					_vertexPositionList[i].y = AnUtilityValue.GetLimitValue(sliceValue.y - 0.5f, -0.5f, 0f);
				}
				else if (num == 2)
				{
					_vertexPositionList[i].y = AnUtilityValue.GetLimitValue(1f - sliceValue.w - 0.5f, 0f, 0.5f);
				}
				else if (num == 3)
				{
					_vertexPositionList[i].y = 0.5f;
				}
				_vertexPositionList[i].x = _vertexPositionList[i].x * originalSize.x * scale.x + offset.x * (scale.x - 1f);
				_vertexPositionList[i].y = _vertexPositionList[i].y * originalSize.y * scale.y + offset.y * (scale.y - 1f);
			}
		}

		public static Mesh CloneMesh(Mesh baseMesh)
		{
			Mesh mesh = new Mesh();
			mesh.name = baseMesh.name;
			mesh.vertices = baseMesh.vertices.Clone() as Vector3[];
			mesh.triangles = baseMesh.triangles.Clone() as int[];
			mesh.normals = baseMesh.normals.Clone() as Vector3[];
			mesh.uv = baseMesh.uv.Clone() as Vector2[];
			mesh.uv2 = baseMesh.uv2.Clone() as Vector2[];
			mesh.uv3 = baseMesh.uv3.Clone() as Vector2[];
			mesh.colors = baseMesh.colors.Clone() as Color[];
			mesh.MarkDynamic();
			return mesh;
		}

		public static void FixMeshColorAndUV2AndUV3(Mesh targetMesh)
		{
			if (targetMesh.colors.Length != targetMesh.vertices.Length)
			{
				Color[] array = new Color[targetMesh.vertices.Length];
				for (int i = 0; i < targetMesh.vertices.Length; i++)
				{
					array[i] = new Color(1f, 1f, 1f, 1f);
				}
				targetMesh.colors = array;
			}
			if (targetMesh.uv2.Length != targetMesh.vertices.Length)
			{
				Vector2[] array2 = new Vector2[targetMesh.vertices.Length];
				for (int j = 0; j < targetMesh.vertices.Length; j++)
				{
					array2[j] = new Vector2(0f, 0f);
				}
				targetMesh.uv2 = array2;
				targetMesh.uv3 = array2;
			}
		}

		public static Mesh GetPrimitiveMesh(AnPrimitiveMeshTypes primitiveMeshType)
		{
			if (primitiveMeshType == AnPrimitiveMeshTypes.None)
			{
				return null;
			}
			string text = "";
			if (primitiveMeshType == AnPrimitiveMeshTypes.Cube)
			{
				text = AnValue.PrimitiveMeshCubePath;
			}
			else if (primitiveMeshType == AnPrimitiveMeshTypes.Cylinder)
			{
				text = AnValue.PrimitiveMeshCylinderPath;
			}
			else if (primitiveMeshType == AnPrimitiveMeshTypes.Ring)
			{
				text = AnValue.PrimitiveMeshRingPath;
			}
			else if (primitiveMeshType == AnPrimitiveMeshTypes.Sphere)
			{
				text = AnValue.PrimitiveMeshSpherePath;
			}
			else if (primitiveMeshType == AnPrimitiveMeshTypes.Plane)
			{
				text = AnValue.PrimitiveMeshPlanePath;
			}
			GameObject gameObject = Resources.Load<GameObject>(text);
			if (gameObject == null)
			{
				return null;
			}
			MeshFilter componentInChildren = gameObject.GetComponentInChildren<MeshFilter>(true);
			if (componentInChildren == null)
			{
				return null;
			}
			Mesh sharedMesh = componentInChildren.sharedMesh;
			if (sharedMesh == null)
			{
				return null;
			}
			return sharedMesh;
		}

		public static void FixMeshSize(Mesh baseMesh, Vector2 imageSize, float marginTop, float marginButtom, float marginRight, float marginLeft, bool keepAspect)
		{
			baseMesh.RecalculateBounds();
			Vector3 center = baseMesh.bounds.center;
			Vector3 size = baseMesh.bounds.size;
			float num = 1f - marginRight - marginLeft;
			float num2 = 1f - marginTop - marginButtom;
			float num3 = (marginRight - marginLeft) * imageSize.x * 0.5f;
			float num4 = (marginTop - marginButtom) * imageSize.y * 0.5f;
			Vector2 vector = imageSize;
			vector.x = imageSize.x * num;
			vector.y = imageSize.y * num2;
			if (keepAspect)
			{
				float num5 = vector.x / vector.y;
				float num6 = size.x / size.y;
				if (num5 > num6)
				{
					vector.x = vector.y * num6;
				}
				else if (num5 < num6)
				{
					vector.y = vector.x / num6;
				}
			}
			Vector3[] vertices = baseMesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vector2 = vertices[i];
				vector2 -= center;
				vector2.x /= size.x;
				vector2.y /= size.y;
				vector2.x *= vector.x;
				vector2.y *= vector.y;
				vector2.x -= num3;
				vector2.y -= num4;
				vertices[i] = vector2;
			}
			baseMesh.vertices = vertices;
			baseMesh.RecalculateBounds();
		}

		public static void OffsetMesh(Mesh baseMesh, Vector3 positionOffset, Vector3 rotateOffset, Vector3 scaleOffset)
		{
			if (positionOffset == Vector3.zero && scaleOffset == Vector3.zero && rotateOffset == Vector3.zero)
			{
				return;
			}
			baseMesh.RecalculateBounds();
			Vector3 center = baseMesh.bounds.center;
			Vector3[] vertices = baseMesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vector = vertices[i];
				vector -= center;
				vector = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(rotateOffset), Vector3.one).MultiplyPoint(vector);
				vector.x *= 1f + scaleOffset.x;
				vector.y *= 1f + scaleOffset.y;
				vector.z *= 1f + scaleOffset.z;
				vector += center + positionOffset;
				vertices[i] = vector;
			}
			baseMesh.vertices = vertices;
			baseMesh.RecalculateBounds();
		}

		public static void InvertMeshNormal(Mesh baseMesh)
		{
			Vector3[] normals = baseMesh.normals;
			for (int i = 0; i < normals.Length; i++)
			{
				Vector3 vector = normals[i];
				vector *= -1f;
				normals[i] = vector;
			}
			baseMesh.normals = normals;
		}

		public static void FixMeshUV(Mesh baseMesh, Vector2 uvOffset, Vector2 uvSize, bool rotated, Vector2 packImageSize, Vector2 imageSize, float marginTop, float marginButtom, float marginRight, float marginLeft)
		{
			Vector2[] uv = baseMesh.uv;
			Vector2 vector = Vector2.zero;
			float num = 100000f;
			float num2 = -100000f;
			float num3 = 100000f;
			float num4 = -100000f;
			foreach (Vector2 vector2 in uv)
			{
				vector += vector2;
				if (vector2.x < num)
				{
					num = vector2.x;
				}
				if (vector2.x > num2)
				{
					num2 = vector2.x;
				}
				if (vector2.y < num3)
				{
					num3 = vector2.y;
				}
				if (vector2.y > num4)
				{
					num4 = vector2.y;
				}
			}
			vector /= (float)uv.Length;
			float num5 = 1f - marginRight - marginLeft;
			float num6 = 1f - marginTop - marginButtom;
			float num7 = (marginRight - marginLeft) * 0.5f * imageSize.x / packImageSize.x;
			float num8 = (marginTop - marginButtom) * 0.5f * imageSize.y / packImageSize.y;
			for (int j = 0; j < uv.Length; j++)
			{
				Vector2 vector3 = uv[j];
				vector3 -= vector;
				vector3.x /= num2 - num;
				vector3.y /= num4 - num3;
				if (rotated)
				{
					vector3 = AnUtilityVector.Rotate2DPosition(vector3, Vector2.zero, -90f);
					vector3.x *= uvSize.x;
					vector3.y *= uvSize.y;
					vector3.x += uvSize.x * 0.5f + uvOffset.x;
					vector3.y += uvSize.y * 0.5f + uvOffset.y;
				}
				else
				{
					vector3.x *= uvSize.x;
					vector3.y *= uvSize.y;
					vector3.x *= num5;
					vector3.y *= num6;
					vector3.x -= num7;
					vector3.y -= num8;
					vector3.x += uvSize.x * 0.5f + uvOffset.x;
					vector3.y += uvSize.y * 0.5f + uvOffset.y;
				}
				uv[j] = vector3;
			}
			baseMesh.uv = uv;
		}

		public static void OffsetMeshUV(Mesh baseMesh, Vector2 uvOffset, Vector2 uvSize)
		{
			Vector2[] uv = baseMesh.uv;
			for (int i = 0; i < uv.Length; i++)
			{
				Vector2 vector = uv[i];
				vector.x += uvOffset.x;
				vector.y += uvOffset.y;
				vector.x *= uvSize.x;
				vector.y *= uvSize.y;
				uv[i] = vector;
			}
			baseMesh.uv = uv;
		}

		public static Vector2 FixUV(Vector2 value)
		{
			value.x %= 10f;
			value.y %= 10f;
			return value;
		}

		public static Vector3 CalculateShearPosition(Vector3 original, Vector3 offset, float cosX, float sinX, float cosY, float sinY, float scaleX, float scaleY)
		{
			Vector3 vector = original - offset;
			Vector3 vector2 = vector;
			vector.x = vector2.x * scaleX * cosY - vector2.y * scaleY * sinX;
			vector.y = vector2.x * scaleX * sinY + vector2.y * scaleY * cosX;
			return vector + offset;
		}

		public static void FillPlane(Mesh mesh, float percent, AnMeshInfoParameter meshInfoPram, AnFillType fillType)
		{
			if (fillType == AnFillType.None)
			{
				return;
			}
			if (mesh.vertices.Length != 4 || mesh.uv.Length != 4)
			{
				return;
			}
			if (percent > 1f)
			{
				return;
			}
			if (percent < 0f)
			{
				percent = 0f;
			}
			Vector3[] vertices = mesh.vertices;
			Vector2[] uv = mesh.uv;
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			Vector2 vector3 = Vector2.zero;
			Vector2 vector4 = Vector2.zero;
			Vector3 vector5 = vertices[0];
			Vector2 uvoffset = meshInfoPram.UVOffset;
			Vector3 vector6 = vertices[2] - vertices[0];
			Vector3 vector7 = vertices[3] - vertices[0];
			Vector3 normalized = vector6.normalized;
			Vector3 normalized2 = vector7.normalized;
			Vector3 vector8 = new Vector3(vector6.magnitude, vector7.magnitude, 0f);
			Vector2 vector9 = new Vector3((vector6 * meshInfoPram.Size.x).magnitude, (vector7 * meshInfoPram.Size.y).magnitude, 0f);
			Vector3 vector10 = (vector8.x / vector9.x - 1f) * vector9.x * normalized;
			Vector3 vector11 = (vector8.y / vector9.y - 1f) * vector9.y * normalized2;
			Vector2 vector12 = new Vector2(1f, 0f);
			Vector2 vector13 = new Vector2(0f, 1f);
			Vector2 uvsize = meshInfoPram.UVSize;
			if (meshInfoPram._rotated)
			{
				uvsize.x = meshInfoPram.UVSize.y;
				uvsize.y = meshInfoPram.UVSize.x;
				vector12 = new Vector2(0f, 1f);
				vector13 = new Vector2(1f, 0f);
			}
			if (fillType == AnFillType.TopToButtom)
			{
				vertices[0] = (vector7 * meshInfoPram.Size.y + vector11) * (1f - percent);
				vertices[1] = vector6 * meshInfoPram.Size.x + vector10 + vector7 * meshInfoPram.Size.y + vector11;
				vertices[2] = vector6 * meshInfoPram.Size.x + vector10 + (vector7 * meshInfoPram.Size.y + vector11) * (1f - percent);
				vertices[3] = vector7 * meshInfoPram.Size.y + vector11;
				vector = vector13 * uvsize.y * (1f - percent);
				vector2 = vector12 * uvsize.x + vector13 * uvsize.y;
				vector3 = vector12 * uvsize.x + vector13 * uvsize.y * (1f - percent);
				vector4 = vector13 * uvsize.y;
			}
			else if (fillType == AnFillType.ButtomToTop)
			{
				vertices[0] = new Vector3(0f, 0f, 0f);
				vertices[1] = vector6 * meshInfoPram.Size.x + vector10 + (vector7 * meshInfoPram.Size.y + vector11) * percent;
				vertices[2] = vector6 * meshInfoPram.Size.x + vector10;
				vertices[3] = (vector7 * meshInfoPram.Size.y + vector11) * percent;
				vector = new Vector2(0f, 0f);
				vector2 = vector12 * uvsize.x + vector13 * uvsize.y * percent;
				vector3 = vector12 * uvsize.x;
				vector4 = vector13 * uvsize.y * percent;
			}
			else if (fillType == AnFillType.CenterToVerticalSide)
			{
				vertices[0] = (vector7 * meshInfoPram.Size.y + vector11) * (1f - percent) * 0.5f;
				vertices[1] = vector6 * meshInfoPram.Size.x + vector10 + (vector7 * meshInfoPram.Size.y + vector11) * (1f + percent) * 0.5f;
				vertices[2] = vector6 * meshInfoPram.Size.x + vector10 + (vector7 * meshInfoPram.Size.y + vector11) * (1f - percent) * 0.5f;
				vertices[3] = (vector7 * meshInfoPram.Size.y + vector11) * (1f + percent) * 0.5f;
				vector = vector13 * uvsize.y * (1f - percent) * 0.5f;
				vector2 = vector12 * uvsize.x + vector13 * uvsize.y * (1f + percent) * 0.5f;
				vector3 = vector12 * uvsize.x + vector13 * uvsize.y * (1f - percent) * 0.5f;
				vector4 = vector13 * uvsize.y * (1f + percent) * 0.5f;
			}
			else if (fillType == AnFillType.LeftToRight)
			{
				vertices[0] = new Vector3(0f, 0f, 0f);
				vertices[1] = (vector6 * meshInfoPram.Size.x + vector10) * percent + vector7 * meshInfoPram.Size.y + vector11;
				vertices[2] = (vector6 * meshInfoPram.Size.x + vector10) * percent;
				vertices[3] = vector7 * meshInfoPram.Size.y + vector11;
				vector = new Vector2(0f, 0f);
				vector2 = vector12 * uvsize.x * percent + vector13 * uvsize.y;
				vector3 = vector12 * uvsize.x * percent;
				vector4 = vector13 * uvsize.y;
				if (meshInfoPram._rotated)
				{
					vector = vector12 * uvsize.x * (1f - percent);
					vector2 = vector12 * uvsize.x + vector13 * uvsize.y;
					vector3 = vector12 * uvsize.x;
					vector4 = vector12 * uvsize.x * (1f - percent) + vector13 * uvsize.y;
				}
			}
			else if (fillType == AnFillType.RightToLeft)
			{
				vertices[0] = (vector6 * meshInfoPram.Size.x + vector10) * (1f - percent);
				vertices[1] = vector6 * meshInfoPram.Size.x + vector10 + vector7 * meshInfoPram.Size.y + vector11;
				vertices[2] = vector6 * meshInfoPram.Size.x + vector10;
				vertices[3] = (vector6 * meshInfoPram.Size.x + vector10) * (1f - percent) + vector7 * meshInfoPram.Size.y + vector11;
				vector = vector12 * uvsize.x * (1f - percent);
				vector2 = vector12 * uvsize.x + vector13 * uvsize.y;
				vector3 = vector12 * uvsize.x;
				vector4 = vector12 * uvsize.x * (1f - percent) + vector13 * uvsize.y;
				if (meshInfoPram._rotated)
				{
					vector = new Vector2(0f, 0f);
					vector2 = vector12 * uvsize.x * percent + vector13 * uvsize.y;
					vector3 = vector12 * uvsize.x * percent;
					vector4 = vector13 * uvsize.y;
				}
			}
			else if (fillType == AnFillType.CenterToSide)
			{
				vertices[0] = (vector6 * meshInfoPram.Size.x + vector10) * (1f - percent) * 0.5f;
				vertices[1] = (vector6 * meshInfoPram.Size.x + vector10) * (1f + percent) * 0.5f + vector7 * meshInfoPram.Size.y + vector11;
				vertices[2] = (vector6 * meshInfoPram.Size.x + vector10) * (1f + percent) * 0.5f;
				vertices[3] = (vector6 * meshInfoPram.Size.x + vector10) * (1f - percent) * 0.5f + vector7 * meshInfoPram.Size.y + vector11;
				vector = vector12 * uvsize.x * (1f - percent) * 0.5f;
				vector2 = vector12 * uvsize.x * (1f + percent) * 0.5f + vector13 * uvsize.y;
				vector3 = vector12 * uvsize.x * (1f + percent) * 0.5f;
				vector4 = vector12 * uvsize.x * (1f - percent) * 0.5f + vector13 * uvsize.y;
			}
			if (!meshInfoPram._rotated)
			{
				uv[0] = vector;
				uv[1] = vector2;
				uv[2] = vector3;
				uv[3] = vector4;
			}
			else
			{
				uv[0] = vector3;
				uv[1] = vector4;
				uv[2] = vector;
				uv[3] = vector2;
			}
			vertices[0] += vector5;
			vertices[1] += vector5;
			vertices[2] += vector5;
			vertices[3] += vector5;
			uv[0] += uvoffset;
			uv[1] += uvoffset;
			uv[2] += uvoffset;
			uv[3] += uvoffset;
			mesh.vertices = vertices;
			mesh.uv = uv;
		}
	}
}
