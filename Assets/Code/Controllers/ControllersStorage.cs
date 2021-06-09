using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Controllers
{
    class ControllersStorage : 
        IExecute, IInitialization, ICleanup
    {
        IList<IExecute> _executeStorage;
        IList<IInitialization> _intializeStorage;
        IList<ICleanup> _cleanupStorage;


        public ControllersStorage()
        {
            _executeStorage = new List<IExecute>();
            _intializeStorage = new List<IInitialization>();
            _cleanupStorage = new List<ICleanup>();
        }

        public void Add(IInteractionObject interactionObject)
        {
            if (interactionObject is IExecute executeObject)
            {
                _executeStorage.Add(executeObject);   
            }
            if (interactionObject is IInitialization initializationObject)
            {
                _intializeStorage.Add(initializationObject);
            }
            if (interactionObject is ICleanup cleanupObject)
            {
                _cleanupStorage.Add(cleanupObject);
            }
        }

        public void Execute(float deltaTime)
        {
            for (int i = 0; i < _executeStorage.Count; ++i)
            {
                _executeStorage[i].Execute(deltaTime);
            }
        }

        public void Initialize()
        {
            for (int i = 0; i < _intializeStorage.Count; ++i)
            {
                _intializeStorage[i].Initialize();
            }
        }

        public void Cleanup()
        {
            for (int i = _cleanupStorage.Count - 1; i >= 0 ; -- i)
            {
                _cleanupStorage[i].Cleanup();
            }
        }
    }
}
