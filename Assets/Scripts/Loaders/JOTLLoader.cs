using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using NE.DataModels;
using UnityEditor;

namespace NE.Loaders
{
    /// <summary>
    /// Currently loads JOTL game from editor and imports a ScriptableObject
    /// This should be rewritten to dynamically load when app starts
    /// </summary>
    public class JOTLLoader : ILoader
    {
        public string GameName => "JOTL";

        public void LoadGame(string path)
        {
#if UNITY_EDITOR
            var monsterStatCardsData = File.ReadAllText(Path.Combine(path, "monster-stat-cards.json"));
            var cardData = JsonConvert.DeserializeObject<List<CardData>>(monsterStatCardsData);
            foreach(var data in cardData)
            {
                // This currently loads the pre-loaded image data instead of dynamic loading later.
                // GameObjectFind is only used now during initialization so shouldn't be too much of a performance hit.
                Debug.Log("Trying to load: " + "Assets/Prefabs/" + data.ImageCardLowLevel + ".prefab");
                data.LowLevelImage = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + data.ImageCardLowLevel + ".prefab", typeof(GameObject));
                data.HighLevelImage = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + data.ImageCardHighLevel + ".prefab", typeof(GameObject));
                var isNull = data.HighLevelImage == null ? "yes" : "no";
                Debug.Log("Is null " + isNull);
            }
            var jotl = new JOTL();
            jotl.cardData = cardData;
            AssetDatabase.CreateAsset(jotl, "Assets/Config/JOTLData.asset");
            AssetDatabase.SaveAssets();
            Debug.Log($"Game {GameName} Loaded");
#endif
        }
    }
}
