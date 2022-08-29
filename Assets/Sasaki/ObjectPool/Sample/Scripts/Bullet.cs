using UnityEngine;
using ObjectPool;

namespace ObjectPool.Sample
{
    public class Bullet : MonoBehaviour, IPool, IPoolEvent
    {
        [SerializeField] float _activeTime = 5;

        float _timer;
        float _power;
        Vector3 _dir;

        Transform _parent;
        Rigidbody _rb;

        public bool IsDone { get; set; }

        public void SetParam(Vector3 dir, float power)
        {
            _dir = dir;
            _power = power;
        }

        public void Delete()
        {
            _timer = 0;
            _dir = Vector3.zero;
            _power = 0;

            gameObject.SetActive(false);
        }

        public void OnEnableEvent()
        {
            gameObject.SetActive(true);
            transform.SetParent(null);

            _rb.AddForce(_dir * _power, ForceMode.Impulse);

            transform.position = _parent.position;
        }

        public bool Execute()
        {
            _timer += Time.deltaTime;

            return _timer > _activeTime;
        }

        public void Setup(Transform parent)
        {
            _parent = parent;
            _rb = GetComponent<Rigidbody>();
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            IsDone = true;
        }
    }
}
