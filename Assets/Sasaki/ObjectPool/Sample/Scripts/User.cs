using UnityEngine;
using ObjectPool;

namespace ObjectPool.Sample
{
    public class User : MonoBehaviour
    {
        [SerializeField] Bullet _poolObject;

        Pool<Bullet> _bullet = new Pool<Bullet>();

        void Start()
        {
            _bullet
                .SetMono(_poolObject)
                .IsSetParent(transform)
                .CreateRequest();
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Request();
            }
        }

        void Request()
        {
            System.Action action;
            Bullet bullet = _bullet.UseRequest(out action);

            bullet.SetParam(Vector3.forward, 100);
            action.Invoke();
        }
    }
}
