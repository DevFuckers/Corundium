using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary.DiaryData
{
    [CreateAssetMenu(fileName = Name, menuName = "Data/" + Name)]
    public class PageData : ScriptableObject
    {
         public const string Name = nameof(PageData);
         public List<PageInfo> Pages =  new List<PageInfo>();
    }
}
