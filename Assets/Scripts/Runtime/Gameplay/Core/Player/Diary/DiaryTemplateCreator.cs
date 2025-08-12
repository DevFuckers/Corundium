using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace DevFuckers
{
    public class DiaryTemplateCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _craftPageTemplate;
        [SerializeField] private GameObject _lightHousePageTemplate;

        [SerializeField] private GameObject _pagesParent;

        public GameObject CreateTemplate(DiaryTypeContent typeContent)
        {
            GameObject page;
            switch (typeContent)
            {
                case DiaryTypeContent.Craft:
                    page = Instantiate(_craftPageTemplate,_pagesParent.transform);
                    return page;
                    

                case DiaryTypeContent.Lighthouse:
                    page = Instantiate(_lightHousePageTemplate, _pagesParent.transform);
                    return page;
                
            }
            page = new GameObject();
            Debug.Log("Страницы не существует");
            return page;
        }
    }
}
