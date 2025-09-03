using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary.DiaryData;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary
{
    public class DiaryTemplateCreator
    {
        private DiaryPageDataProvider _dataProvider;
        private Transform _pagesParent;

        public DiaryTemplateCreator(Transform pagesParent, DiaryPageDataProvider dataProvider)
        {
            _pagesParent = pagesParent;
            _dataProvider = dataProvider;

        }

        public GameObject CreateTemplate(DiaryTypeContent typeContent)
        {
            GameObject page;

            page = _dataProvider.GetTemplateByTypeContent(typeContent);

            return GameObject.Instantiate(page, _pagesParent);

        }
    }
}
