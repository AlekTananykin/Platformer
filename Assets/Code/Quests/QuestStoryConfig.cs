using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Quests
{
    [CreateAssetMenu(menuName ="QuestStoryConfig",
        fileName = "QuestStoryConfig",
        order = 0)]
    class QuestStoryConfig: ScriptableObject
    {
        public QuestConfig[] quests;
        public QuestStoryType questStoryType;
    }

    public enum QuestStoryType
    {
        Common,
        Resettable
    }
}
