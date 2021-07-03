using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Quests
{
    [CreateAssetMenu(menuName ="QuestItemsConfig", 
        fileName ="QuestItemsConfig", order = 0)]
    public class QuestItemConfig: ScriptableObject
    {
        public int questId;
        public List<int> questItemIdCollection;
    }
}
