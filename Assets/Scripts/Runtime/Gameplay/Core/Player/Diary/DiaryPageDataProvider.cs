using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary.DiaryData
{
    public class DiaryPageDataProvider
    {
        private Dictionary<DiaryTypeContent, GameObject> _pages;

        public DiaryPageDataProvider(PageData pageData)
        {
            _pages = pageData.Pages.ToDictionary(x => x.DiaryTypeContent, x => x.Page);

        }

        public GameObject GetTemplateByTypeContent(DiaryTypeContent typeContent)
        {
            GameObject page;
            page = _pages[typeContent];
            if (page == null)
            {
                Debug.Log("Page not Found for this DiaryTypeContent");
                return null;
            }
            return page;
        }
    }
}
