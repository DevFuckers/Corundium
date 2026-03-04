using System.Collections.Generic;
using System.Linq;
using DevFuckers._Project.CodeBase.Runtime.Features.Stamina.Data;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina
{
    public class StaminaSpendingConfigProvider
    {
        private Dictionary<ESpendindStaminaType, float> _usageConfig;

        public StaminaSpendingConfigProvider(string path)
        {
            StaminaSpendingConfig config = Resources.Load<StaminaSpendingConfig>(path);
            
            _usageConfig = config.Spendings
                .ToDictionary(x => x.Type, x => x.Value);
        }
    
        public float GetSpendingValueFor(ESpendindStaminaType type) =>
            _usageConfig[type]; 
    }
}