using UnityEngine;
using UnityEditor;

public class TexturePostProcessor : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        var importer = assetImporter as TextureImporter;
        importer.textureType = TextureImporterType.Sprite;
        importer.wrapMode = TextureWrapMode.Clamp;
        importer.mipmapEnabled = false;
        importer.npotScale = TextureImporterNPOTScale.None;
        var platform = importer.GetDefaultPlatformTextureSettings();
        platform.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;
        platform.textureCompression = TextureImporterCompression.CompressedHQ;
        importer.SetPlatformTextureSettings(platform);
    }
}
