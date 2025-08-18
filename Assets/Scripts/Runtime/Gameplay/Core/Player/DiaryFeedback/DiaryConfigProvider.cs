using System.Collections.Generic;
using System.Linq;
using DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback.Data;
using UnityEngine;

namespace DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback
{
    public class DiaryConfigProvider
    {
        private Dictionary<string, GameObject> _pages;

        public DiaryConfigProvider(string configPath)
        {
            DiaryConfig config = Resources.Load<DiaryConfig>(configPath);
            
            _pages = config.Pages
                .ToDictionary(x => x.Id, x => x.Page);
        } 
            
        public GameObject GetPagePrefabBy(string id) => 
            _pages[id];
    }
}