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
        private GameObject _charecter;

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

        private GameObject _ogrePrefab;
        internal GameObject CreateOgre()
        {
            if (null == _ogrePrefab)
            {
                _ogrePrefab = Resources.Load<GameObject>("Ogre");
            }
            return GameObject.Instantiate(_ogrePrefab);
        }

        GameObject _cannonPrefab;
        internal GameObject CreateCannon()
        {
            if (null == _cannonPrefab)
            {
                _cannonPrefab = Resources.Load<GameObject>("Cannon");
            }
            return GameObject.Instantiate(_cannonPrefab);
        }

        GameObject _platformPrefab;
        internal GameObject CreatePlatform()
        {
            if (null == _platformPrefab)
            {
                _platformPrefab = Resources.Load<GameObject>("Platform");
            }
            return GameObject.Instantiate(_platformPrefab);
        }
    }
}
