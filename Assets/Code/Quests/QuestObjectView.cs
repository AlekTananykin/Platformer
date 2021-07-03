
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Quests
{
    public class QuestObjectView : LevelObjectViewTrigger
    {
        public int Id => _id;
        [SerializeField] private Color _completedColor;
        [SerializeField] private int _id;

        private Color _defaultColor;

        private void Awake()
        {
            _defaultColor = SpriteRenderer.color;
        }

        public void ProcessComplete()
        {
            SpriteRenderer.color = _completedColor;
        }

        public void ProcessActivate()
        {
            SpriteRenderer.color = _defaultColor;
        }

    }
}
