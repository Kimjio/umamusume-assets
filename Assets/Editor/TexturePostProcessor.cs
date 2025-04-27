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
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.npotScale = TextureImporterNPOTScale.None;

        TextureImporterSettings settings = new TextureImporterSettings();
        importer.ReadTextureSettings(settings);

        // settings.spriteMeshType = SpriteMeshType.FullRect;
        importer.SetTextureSettings(settings);

        var platform = importer.GetDefaultPlatformTextureSettings();
        platform.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;
        // platform.textureCompression = TextureImporterCompression.Uncompressed;
        importer.SetPlatformTextureSettings(platform);

        importer.SaveAndReimport();
    }
}
