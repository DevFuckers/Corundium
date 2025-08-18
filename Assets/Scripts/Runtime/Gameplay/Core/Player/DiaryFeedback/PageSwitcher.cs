using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback
{
    public class PageSwitcher
    {
        private PageCreator _pageCreator;
        private Dictionary<string, GameObject> _pages;
        private string _activePageId;

        public PageSwitcher(PageCreator pageCreator, IEnumerable<string> pagesIds)
        {
            _pageCreator = pageCreator;
            _pages = pagesIds.ToDictionary(id => id, page => (GameObject)null);
        }

        public void SwitchToPageBy(string pageId)
        {
            if (pageId == _activePageId) return;

            bool pageIsNotRegistered = _pages.ContainsKey(pageId) == false;
            bool pageIsNotCreated = _pages[pageId] is null;

            if (pageIsNotRegistered || pageIsNotCreated)
                _pages[pageId] = _pageCreator.Create(pageId);

            SetActivePageTo(pageId);
        }

        public void SwitchToNextPage()
        {
            int nextPageIndex = PagesIds.IndexOf(_activePageId) + 1;

            if (nextPageIndex < 0 || nextPageIndex >= _pages.Count)
                nextPageIndex = 0;

            SwitchToPageBy(PagesIds[nextPageIndex]);
        }

        public void SwitchToPreviousPage()
        {
            int previousPageIndex = PagesIds.IndexOf(_activePageId) - 1;

            if (previousPageIndex < 0)
                previousPageIndex = _pages.Count - 1;

            SwitchToPageBy(PagesIds[previousPageIndex]);
        }

        private List<string> PagesIds => _pages.Keys.ToList();

        private void SetActivePageTo(string pageId)
        {
            if (_activePageId != null)
                _pages[_activePageId].SetActive(false);

            _activePageId = pageId;

            _pages[_activePageId].SetActive(true);
        }
    }
}