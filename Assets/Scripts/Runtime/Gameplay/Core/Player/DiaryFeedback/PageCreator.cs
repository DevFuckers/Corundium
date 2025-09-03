using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback
{
    [Serializable]
    public class PageCreator
    {
        private Transform _root;
        private DiaryConfigProvider _diaryConfigProvider;

        public PageCreator(DiaryConfigProvider diaryConfigProvider, Transform root)
        {
            _diaryConfigProvider = diaryConfigProvider;
            _root = root;
        }

        public GameObject Create(string pageId)
        {
            GameObject pagePrefab = _diaryConfigProvider.GetPagePrefabBy(pageId);
            
            if (pagePrefab == null)
            {
                Debug.Log("No page prefab found for " + pageId);
                return null;
            }

            return Object.Instantiate(pagePrefab, _root);
        }
    }
}