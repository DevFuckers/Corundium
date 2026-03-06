using UnityEngine;

namespace CodeBase.Inventory.Architecture
{
    public interface IObjectFactory
    {
        T Instantiate<T>(T obj, Vector3 position, Quaternion rotation)
            where T : MonoBehaviour;

        T Instantiate<T>(T obj, Transform parent)
            where T : MonoBehaviour;

        void Destroy<T>(T obj) where T : MonoBehaviour;
    }
}