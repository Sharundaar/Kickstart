using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace DEngine.Utilitary
{
    public class ObjectPool
    {
        LinkedList<GameObject> m_activeObjects = new LinkedList<GameObject>();
        LinkedList<GameObject> m_inactiveObjects = new LinkedList<GameObject>();

        bool m_IsPoolObject = false;

        GameObject m_PoolParent;

        public ObjectPool(int _size, GameObject _template)
        {
            GameObject parent = new GameObject(_template.name + "_ObjectPool");
            parent.transform.position = Vector3.zero;
            parent.transform.rotation = Quaternion.identity;
            m_PoolParent = parent;

            if (_template.GetComponent<IPoolObject>() != null)
                m_IsPoolObject = true;

            for(int i=0; i<_size; ++i)
            {
                GameObject obj = GameObject.Instantiate<GameObject>(_template);
                obj.transform.parent = m_PoolParent.transform;
                obj.transform.position = Vector3.zero;
                obj.transform.rotation = Quaternion.identity;
                obj.name = _template.name + i;

                obj.SetActive(false);

                if (m_IsPoolObject)
                    obj.GetComponent<IPoolObject>().Owner = this;

                m_inactiveObjects.AddLast(obj);
            }
        }

        public GameObject GetObject()
        {
            if(m_inactiveObjects.Count > 0)
            {
                GameObject obj = m_inactiveObjects.First();
                m_inactiveObjects.RemoveFirst();
                m_activeObjects.AddFirst(obj);

                obj.SetActive(true);

                if (m_IsPoolObject)
                    obj.GetComponent<IPoolObject>().OnAllocated();

                return obj;
            }

            if(m_activeObjects.Count > 0)
            {
                // taking the last (oldest) object
                GameObject obj = m_activeObjects.Last();
                m_activeObjects.RemoveLast();
                m_activeObjects.AddFirst(obj);
                
                
                if (m_IsPoolObject)
                    obj.GetComponent<IPoolObject>().OnRelocation();

                return obj;
            }

            return null;
        }

        public void ReturnObject(GameObject _obj)
        {
            _obj.SetActive(false);
            m_activeObjects.Remove(_obj);
            m_inactiveObjects.AddLast(_obj);

            if (m_IsPoolObject)
                _obj.GetComponent<IPoolObject>().OnDestroyed();
        }
    }
}
