using System.Collections.Generic;
using UnityEngine;

namespace DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback.Data
{
    [CreateAssetMenu(fileName = Name, menuName = "Configs/" + Name)]
    public class DiaryConfig : ScriptableObject
    {
        public const string Name = nameof(DiaryConfig);

        public List<PageWithId> Pages;
    }
}