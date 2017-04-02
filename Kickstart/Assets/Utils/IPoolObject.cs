using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEngine.Utilitary
{
    public interface IPoolObject
    {
        ObjectPool Owner { get; set; }
        void OnRelocation();
        void OnAllocated();
        void OnDestroyed();
    }
}
