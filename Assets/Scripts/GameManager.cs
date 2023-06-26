using NE.Loaders;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using NE.Config;

namespace NE
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] MonsterStateConfiguration monsterStateConfiguration;
        private ILoader[] loaders = new ILoader[]
        {
            new JOTLLoader()
        };
        private Dictionary<string, ILoader> gamesLoaderMap;
        // Start is called before the first frame update
        void Awake()
        {
            gamesLoaderMap = loaders.ToDictionary(key => key.GameName, loader => loader);
            LoadGames();
            Reset();
        }

        private void Reset()
        {
            monsterStateConfiguration.Monsters = new List<Core.Cards.MonsterCard>();
            monsterStateConfiguration.Level = 0;
        }

        void LoadGames()
        {
            var games = Directory.GetDirectories(Path.Combine(Application.streamingAssetsPath, "Games"), "*", SearchOption.TopDirectoryOnly);
            foreach(string gameDir in games)
            {
                var game = Path.GetFileName(gameDir);
                if(gamesLoaderMap.TryGetValue(game, out ILoader loader))
                {
                    loader.LoadGame(gameDir);
                }
            }
        }
    }
}
