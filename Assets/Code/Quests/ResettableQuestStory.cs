using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Quests
{
    public sealed class ResettableQuestStory : IQuestStory
    {
        #region Fields

        private readonly List<IQuest> _questsCollection;
        private int _currentIndex;

        #endregion


        #region Life Cycle

        public ResettableQuestStory(List<IQuest> questsCollection)
        {
            _questsCollection = questsCollection ?? throw new ArgumentNullException(nameof(questsCollection));
            Subscribe();
            // все квесты активны сразу
            ResetQuests();
        }

        #endregion


        #region Methods

        private void Subscribe()
        {
            foreach (var quest in _questsCollection)
            {
                quest.Completed += OnQuestCompleted;
            }
        }

        private void Unsubscribe()
        {
            foreach (var quest in _questsCollection)
            {
                quest.Completed -= OnQuestCompleted;
            }
        }

        private void OnQuestCompleted(object sender, IQuest quest)
        {
            var index = _questsCollection.IndexOf(quest);
            // отслеживаем текущий индекс
            if (_currentIndex == index)
            {
                _currentIndex++;
                if (IsDone) Debug.Log("Story done!");
            }
            else
            {
                // сбрасываем цепочку, если был выполнен не целевой квест
                ResetQuests();
            }
        }

        private void ResetQuests()
        {
            _currentIndex = 0;
            foreach (var quest in _questsCollection)
            {
                quest.Reset();
            }
        }

        #endregion


        #region IQuestStory

        public bool IsDone => _questsCollection.All(value => value.IsCompleted);

        public void Dispose()
        {
            Unsubscribe();
            foreach (var quest in _questsCollection)
            {
                quest.Dispose();
            }
        }

        #endregion
    }
}
