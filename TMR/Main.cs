using System;
using System.Collections.Generic;
using System.IO;
using BWModLoader;
using Harmony;
using UnityEngine;

namespace TMR
{
    [Mod]
    public class Main : MonoBehaviour
    {
        public static Main Instance;
        static string FilePath = "/Managed/Mods/Assets/TMR/";
        public Dictionary<string, Mesh> allMeshes = new Dictionary<string, Mesh>();
        public Dictionary<string, Texture2D> allTextures = new Dictionary<string, Texture2D>();
        static int logLevel = 1;

        void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(this);
            }
        }

        void Start()
        {
            //Setup harmony patching
            HarmonyInstance harmony = HarmonyInstance.Create("com.github.archie");
            harmony.PatchAll();
            initialSetup();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                initialSetup();
            }
        }

        void initialSetup()
        {
            ObjImporter meshImporter = new ObjImporter();
            allMeshes.Clear();
            allTextures.Clear();
            try
            {
                string[] files = Directory.GetFiles(Application.dataPath + FilePath, "*.obj", SearchOption.TopDirectoryOnly);
                foreach (string file in files)
                {
                    Mesh temp = meshImporter.ImportFile(file);
                    //log("Loading: " + file);
                    temp.name = file.Split('/')[file.Split('/').Length - 1].Split('.')[0];
                    //log("Name: " + temp.name);
                    allMeshes.Add(temp.name, temp);
                }

                string[] textures = Directory.GetFiles(Application.dataPath + FilePath + "Textures/", "*.png", SearchOption.TopDirectoryOnly);
                foreach (string tex in textures)
                {
                    //log("Loading: " + tex);
                    string temp = tex.Split('/')[tex.Split('/').Length - 1].Split('.')[0];
                    Texture2D newTex = loadTexture(temp, 2048, 2048);
                    if (newTex.name != "FAILED")
                    {
                        allTextures.Add(newTex.name, newTex);
                    }
                }
            }catch(Exception e)
            {
                debugLog(e.Message);
            }
        }

        public static void log(string message)
        {
            if (logLevel >= 1)
            {
                Log.logger.Log(message);
            }
        }
        
        public static void debugLog(string message)
        {
            Log.logger.Log(message);
        }

        public static Texture2D loadTexture(string texName, int imgWidth, int imgHeight)
        {
            try
            {
                byte[] fileData = File.ReadAllBytes(Application.dataPath + FilePath + "Textures/" + texName + ".png");

                Texture2D tex = new Texture2D(imgWidth, imgHeight, TextureFormat.RGB24, false);
                tex.name = texName;
                tex.LoadImage(fileData);
                return tex;

            }
            catch (Exception e)
            {
                debugLog(string.Format("Error loading texture {0}", texName));
                debugLog(e.Message);
                Texture2D tex = Texture2D.whiteTexture;
                tex.name = "FAILED";
                return tex;
            }
        }
    }
}
