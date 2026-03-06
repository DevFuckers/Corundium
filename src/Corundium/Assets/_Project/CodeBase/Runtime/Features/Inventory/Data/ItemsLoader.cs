using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DevFuckers._Project.CodeBase.Runtime.Common.Helpers;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class ItemsLoader
    {
        private string _path;

        public ItemsLoader(string path)
        {
            _path = path;
        }

        public async UniTask<List<ItemDataSO>> Load()
        {
            IList<ItemDataSO> items = EditorFinder.Instance.GetAssetsFromFolder<ItemDataSO>(_path).ToList();
           
            Debug.Log($"Loaded {items.Count} items data.");

            return items as List<ItemDataSO>;
        }
    }
}