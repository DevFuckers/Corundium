using System.Collections.Generic;
using System.Linq;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.ConfigProvider;
using UnityEngine;
using Zenject;

namespace CodeBase.Inventory
{
    public class ItemsLoader
    {
        private string _path;
        private IConfigProvider _configProvider;

        public ItemsLoader(string path)
        {
            _path = path;
        }

        [Inject]
        private void Construct(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public async List<ItemDataSO> Load()
        {
            IList<ItemDataSO> items = await _configProvider.GetAllConfigsFromFolderAsync<ItemDataSO>(_path);
           
            //Debug.Log($"Loaded {items.Count} items data.");

            return items as List<ItemDataSO>;
        }
    }
}