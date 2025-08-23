using System.Collections.Generic;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina.Data
{
    [CreateAssetMenu(fileName = Name, menuName = "Configs/" +Name)]
    public class StaminaSpendingConfig : ScriptableObject
    {
        public const string Name = nameof(StaminaSpendingConfig);
        public List<StaminaSpending> Spendings;
    }
}