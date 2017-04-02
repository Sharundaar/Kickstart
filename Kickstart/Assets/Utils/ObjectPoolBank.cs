using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DEngine.Utilitary
{
    public class ObjectPoolBank
    {
        private Dictionary<string, ObjectPool> m_pools = new Dictionary<string, ObjectPool>();

        public void CreatePool(string _id, int _size, GameObject _template)
        {
            m_pools[_id] = new ObjectPool(_size, _template);
        }

        public ObjectPool GetPool(string _id)
        {
            return m_pools[_id];
        }
    }
}
