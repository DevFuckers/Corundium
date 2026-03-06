using UnityEngine;

namespace CodeBase.Inventory.Architecture
{
    public class ObjectFactory : IObjectFactory
    {
        public T Instantiate<T>(T obj, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            return Object.Instantiate(obj, position, rotation);
        }

        public T Instantiate<T>(T obj, Transform parent) where T : MonoBehaviour
        {
            return Object.Instantiate(obj, parent);
        }

        public void Destroy<T>(T obj) where T : MonoBehaviour
        {
            Object.Destroy(obj.gameObject);
        }
    }
}