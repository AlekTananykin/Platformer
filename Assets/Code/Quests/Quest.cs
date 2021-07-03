using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Quests
{
    public sealed class Quest : IQuest
    {
        private readonly QuestObjectView _view;
        private readonly IQuestModel _model;
        private bool _active;

        public Quest(QuestObjectView view, IQuestModel model)
        {
            _view = null != view ? view : throw new ArgumentNullException(nameof(view));
            _model = null != model ? model : throw new ArgumentNullException(nameof(model));
        }

        private void OnContact(object sender, 
            HeroView arg2)
        {
            //var completed = _model.TryComplete(arg2.gameObject);
            //if (completed) Complete();

            Debug.Log("Mission is compleeted! ");
            Complete();
        }

        private void Complete()
        {
            if (!_active) 
                return;

            _active = false;
            IsCompleted = true;
            _view.OnLevelObjectContact -= OnContact;
            _view.ProcessComplete();
            OnCompleted();
        }

        private void OnCompleted()
        {
            Completed?.Invoke(this, this);
        }

        public event EventHandler<IQuest> Completed;

        public bool IsCompleted { get; private set; }

        public void Reset()
        {
            if (_active) return;
            _active = true;
            IsCompleted = false;

            _view.OnLevelObjectContact += OnContact;
            _view.ProcessActivate();

        }

        public void Dispose()
        {
            _view.OnLevelObjectContact -= OnContact;
        }
    }
}
