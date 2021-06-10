using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Views
{
    internal sealed class GameObjectFabric
    {
        GameObject _charecter;

        internal GameObject CreateCharecter()
        {
            if (null == _charecter)
            {
                GameObject charecterPrefab = 
                    Resources.Load<GameObject>("Archer");

                _charecter = GameObject.Instantiate<GameObject>(charecterPrefab);
            }
            return _charecter;
        }

    }
}
