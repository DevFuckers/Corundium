using System.Collections.Generic;
using System.Linq;
using DevFuckers._Project.CodeBase.Runtime.Features.Stamina.Data;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina
{
    public class StaminaSpendingConfigProvider
    {
        private Dictionary<string, float> _usageConfig;

        public StaminaSpendingConfigProvider(string path)
        {
            StaminaSpendingConfig config = Resources.Load<StaminaSpendingConfig>(path);
            
            _usageConfig = config.Spendings
                .ToDictionary(x => x.Id, x => x.Value);
        }
    
        public float GetSpendingValueFor(string id) =>
            _usageConfig[id]; 
    }
}