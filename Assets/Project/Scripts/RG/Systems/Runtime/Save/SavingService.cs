using UnityEngine;
using static UnityEngine.Object;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DesignPatterns;
using System;
using LitJson;

namespace RG.Systems
{
    public abstract class SavingService
    {
        private const string ACTIVE_SCENES_KEY = "activeScene";
        private const string SCENES_KEY = "scenes";
        private const string OBJECTS_KEY = "objects";
        private const string SAVEID_KEY = "$saveID";

        public static void SaveGame(string fileName)
        {
            var result = new JsonData();

            var allSavebleObjects = FindObjectsByType<MonoBehaviour>(sortMode: FindObjectsSortMode.None).OfType<ISaveable>();

            if (allSavebleObjects.Count() > 0)
            {
                var savedObjects = new JsonData();
                foreach (var saveableObject in allSavebleObjects)
                {
                    var data = saveableObject.SavedData;
                    if (data.IsObject)
                    {
                        data[SAVEID_KEY] = saveableObject.SaveID;
                        savedObjects.Add(data);
                    }
                    else
                    {
                        var behaviour = saveableObject as MonoBehaviour;
                        Debug.LogWarningFormat(behaviour, "{0}s save data is not dictionary. The" + "object was not saved.", behaviour.name);
                    }
                }
                result.Add(savedObjects);
            }
            else
            {
                Debug.LogWarningFormat("The scene did not include any saveable objects");
            }
            var openScenes = new JsonData();
            var sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                openScenes.Add(scene.name);
            }
            result[SCENES_KEY].Add(openScenes);
            result[ACTIVE_SCENES_KEY].Add(SceneManager.GetActiveScene().name);

            var outputPath = Path.Combine(Application.persistentDataPath, fileName);
            var writer = new JsonWriter();
            writer.PrettyPrint = true;

            result.ToJson(writer);
            File.WriteAllText(outputPath, writer.ToString());
            Debug.LogFormat("Wrote saved game to {0}", outputPath);
            result = null;
            GC.Collect();

        }
    }
}
