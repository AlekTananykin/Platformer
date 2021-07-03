using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Quests
{
    [CreateAssetMenu(menuName = "Crete Quest Config", 
        fileName = "QuestConfig", order = 0)]
    public class QuestConfig :ScriptableObject 
    {
        public int id;
        public QuestType questType;
    }

    public enum QuestType
    {
        Switch
    }
}
