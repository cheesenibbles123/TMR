using System;
using Harmony;
using BWModLoader;
using UnityEngine;

namespace TMR
{
    [Mod]
    public class CharacterStuff : MonoBehaviour
    {

        [HarmonyPatch(typeof(PlayerBuilder), "òêîîòçóêçìï", new Type[] { typeof(OutfitItem), typeof(OutfitItem), typeof(OutfitItem), typeof(OutfitItem), typeof(OutfitItem), typeof(Transform), typeof(int) })]
        static class PlayerBuilderPatchPostfix
        {
            static void Postfix(PlayerBuilder __instance, OutfitItem êïòèíèñæêðé, OutfitItem êëëðîñìîçíê, OutfitItem îèòçòñïéèêê, OutfitItem ðçëìèêîïæäè, OutfitItem òìðêäëêääåï, Transform ëççêîèåçåäï, int óïèññðæìëòè)
            {
                //MAINOUTFIT
                Texture2D tempTex;
                string texName;

                try
                {

                    if (__instance.êêïæëëëòììó.material.HasProperty("_ClothesAlbedoFuzzMask"))
                    {
                        texName = __instance.êêïæëëëòììó.material.GetTexture("_ClothesAlbedoFuzzMask").name;
                        Main.log($"ALB found, name: {texName}");

                        if (Main.Instance.allTextures.TryGetValue(texName, out tempTex))
                        {
                            __instance.êêïæëëëòììó.material.SetTexture("_ClothesAlbedoFuzzMask", tempTex);
                            Main.log("Set texture for Alb");
                        }
                    }

                    if (__instance.êêïæëëëòììó.material.HasProperty("_ClothesMetSmoothOccBlood"))
                    {
                        texName = __instance.êêïæëëëòììó.material.GetTexture("_ClothesMetSmoothOccBlood").name;
                        Main.log($"MET found, name: {texName}");

                        if (Main.Instance.allTextures.TryGetValue(texName, out tempTex))
                        {
                            __instance.êêïæëëëòììó.material.SetTexture("_ClothesMetSmoothOccBlood", tempTex);
                            Main.log("Set texture for Clothes Met");
                        }
                    }

                    if (__instance.êêïæëëëòììó.material.HasProperty("_ClothesBump"))
                    {
                        texName = __instance.êêïæëëëòììó.material.GetTexture("_ClothesBump").name;
                        Main.log($"BUMP found, name: {texName}");

                        if (Main.Instance.allTextures.TryGetValue(texName, out tempTex))
                        {
                            __instance.êêïæëëëòììó.material.SetTexture("_ClothesBump", tempTex);
                            Main.log("Set texture for Clothes Bump");
                        }
                    }

                }
                catch (Exception e)
                {
                    Main.debugLog("Error loading outfit texture");
                    Main.debugLog(e.Message);
                }

                //HATS
                try
                {
                    if (__instance.êêïæëëëòììó.material.HasProperty("_HatAOMetallic"))
                    {
                        texName = __instance.êêïæëëëòììó.material.GetTexture("_HatAlbedoSmooth").name;
                        Main.log($"ALB found, name: {texName}");

                        if (Main.Instance.allTextures.TryGetValue(texName, out tempTex))
                        {
                            __instance.êêïæëëëòììó.material.SetTexture("_HatAlbedoSmooth", tempTex);
                            Main.log("Loaded texture for Hat Alb");
                        }
                    }

                    if (__instance.êêïæëëëòììó.material.HasProperty("_HatAOMetallic"))
                    {
                        texName = __instance.êêïæëëëòììó.material.GetTexture("_HatAOMetallic").name;
                        Main.log($"MET found, name: {texName}");

                        if (Main.Instance.allTextures.TryGetValue(texName, out tempTex))
                        {
                            __instance.êêïæëëëòììó.material.SetTexture("_HatAOMetallic", tempTex);
                            Main.log("Loaded texture for Hat Met");
                        }
                    }

                    if (__instance.êêïæëëëòììó.material.HasProperty("_HatBump"))
                    {
                        texName = __instance.êêïæëëëòììó.material.GetTexture("_HatBump").name;
                        Main.log($"BMP found, name: {texName}");

                        if (Main.Instance.allTextures.TryGetValue(texName, out tempTex))
                        {
                            __instance.êêïæëëëòììó.material.SetTexture("_HatBump", tempTex);
                            Main.log("Loaded texture for Hat Bump");
                        }
                    }
                }
                catch (Exception e)
                {
                    Main.debugLog("Error loading hat texture");
                    Main.debugLog(e.Message);
                }
            }

            static void Prefix(PlayerBuilder __instance, OutfitItem êïòèíèñæêðé, OutfitItem êëëðîñìîçíê, OutfitItem îèòçòñïéèêê, OutfitItem ðçëìèêîïæäè, OutfitItem òìðêäëêääåï, Transform ëççêîèåçåäï, int óïèññðæìëòè)
            {
                try
                {
                    if (êïòèíèñæêðé.óïêäðîäòçêæ)
                    {
                        string outfitName = êïòèíèñæêðé.óïêäðîäòçêæ.sharedMesh.name;
                        Main.log("Got mesh -" + outfitName + "-");
                        if (Main.Instance.allMeshes.TryGetValue(outfitName, out Mesh newOutfit))
                        {
                            Main.log("Found mesh -" + newOutfit.name + "-");
                            êïòèíèñæêðé.óïêäðîäòçêæ.sharedMesh = newOutfit;
                            Main.log("Set shared mesh");
                            Main.log("---Mesh Info---");
                            Main.log($"Verts: {newOutfit.vertices.Length}");
                            Main.log($"Tria: {newOutfit.triangles.Length}");
                        }
                    }
                }catch(Exception e)
                {
                    Main.debugLog(e.Message);
                }
            }
        }
    }
}
