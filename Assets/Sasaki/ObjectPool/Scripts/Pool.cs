using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    /// <summary>
    /// ObjectPool�̊Ǘ��N���X
    /// </summary>
    /// <typeparam name="MonoPool">Pool�ɂ���Ώ�Object</typeparam>
    public class Pool<MonoPool> where MonoPool : MonoBehaviour, IPool
    {
        /// <summary>
        /// �ePool�̃f�[�^
        /// </summary>
        class PoolData
        {
            public MonoPool Pool { get; set; }
            public IPoolEvent Event { get; set; }
            public bool IsUse { get; set; }

            /// <summary>
            /// Event�̏I���ʒm
            /// </summary>
            /// <returns></returns>
            public bool IsEvent()
            {
                if (Event == null)
                {
                    return false;
                }

                return Event.IsDone;
            }
        }

        int _createCount;
        bool _isCreate;
        bool _isSetMono;
        bool _isAutoActive;

        MonoPool _monoPool;
        List<PoolData> _poolList = new List<PoolData>();

        Transform _parent;

        /// <summary>
        /// �����ݒ�
        /// </summary>
        /// <param name="pool">Pool�ɂ���Ώ�</param>
        /// <param name="createCount">������</param>
        public Pool<MonoPool> SetMono(MonoPool pool, int createCount = 5)
        {
            _createCount = createCount;
            _monoPool = pool;
            
            _isSetMono = true;

            return this;
        }

        /// <summary>
        /// �e�̕ێ�
        /// </summary>
        /// <param name="parent">�e�ɂ���Transform</param>
        /// <returns></returns>
        public Pool<MonoPool> IsSetParent(Transform parent)
        {
            _parent = parent;
            
            return this;
        }

        /// <summary>
        /// �g�pPool�ɑ΂���Active�����̎�����
        /// </summary>
        /// <returns></returns>
        public Pool<MonoPool> IsAutoActive()
        {
            _isAutoActive = true;

            return this;
        }

        /// <summary>
        /// Pool�̐���Request
        /// </summary>
        /// <returns></returns>
        public void CreateRequest()
        {
            CreatePool(_createCount);
            _isCreate = true;
        }

        /// <summary>
        /// Pool�̐���
        /// </summary>
        /// <param name="createCount">������</param>
        void CreatePool(int createCount)
        {
            for (int index = 0; index < createCount; index++)
            {
                MonoPool pool = Object.Instantiate(_monoPool);
                pool.Setup(_parent);

                PoolData data = CreateData(pool);
                _poolList.Add(data);

                data.Event = pool.GetComponent<IPoolEvent>();

                if (_parent != null)
                {
                    data.Pool.transform.SetParent(_parent);
                }

                if (_isAutoActive)
                {
                    data.Pool.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Pool�f�[�^�̍쐬
        /// </summary>
        /// <param name="pool">�Ώ�Pool</param>
        /// <returns>PoolData</returns>
        PoolData CreateData(MonoPool pool)
        {
            PoolData data = new PoolData();
            data.Pool = pool;
            data.IsUse = false;
            
            return data;
        }

        /// <summary>
        /// Pool�̎g�p
        /// </summary>
        public void UseRequest()
        {
            if (!ChackSuccess())
            {
                Debug.LogWarning("Pool�̎g�p����������܂���");
                return;
            }

            try
            {
                PoolData data = _poolList.First(p => !p.IsUse);

                if (_isAutoActive)
                {
                    data.Pool.gameObject.SetActive(true);
                }

                data.IsUse = true;
                data.Event.IsDone = false;
                data.Pool.OnEnableEvent();
                data.Pool.StartCoroutine(Execution(data));
            }
            catch
            {
                CreatePool(_createCount);

                Debug.LogWarning($"Pool������ɒB�����̂ŏ���𑝂₵�܂����B" +
                    $"\n �Ώ�Pool.{_monoPool.name} : ������.{_createCount} : ���.{_poolList.Count}");

                UseRequest();
            }
        }

        /// <summary>
        /// Pool�̎g�p���N�G�X�g
        /// </summary>
        /// <param name="action">Invoke(); ���邱�ƂŎg�p</param>
        /// <returns>�Ώ�Pool</returns>
        public MonoPool UseRequest(out System.Action action)
        {
            action = null;

            if (!ChackSuccess())
            {
                Debug.LogWarning("Pool�̎g�p����������܂���");
                return null;
            }

            try
            {
                PoolData data = _poolList.First(p => !p.IsUse);

                if (_isAutoActive)
                {
                    action += () => data.Pool.gameObject.SetActive(true);
                }

                action += () => data.IsUse = true;
                action += () => data.Event.IsDone = false;
                action += () => data.Pool.OnEnableEvent();
                action += () => data.Pool.StartCoroutine(Execution(data));

                return data.Pool;
            }
            catch
            {
                CreatePool(_createCount);

                Debug.LogWarning($"Pool������ɒB�����̂ŏ���𑝂₵�܂����B" +
                    $"\n �Ώ�Pool.{_monoPool.name} : ������.{_createCount} : ���.{_poolList.Count}");

                return UseRequest(out action);
            }
        }

        bool ChackSuccess()
        {
            if (!_isSetMono)
            {
                Debug.LogWarning("�Ώ�Pool������܂���B");
                return false;
            }

            if (!_isCreate)
            {
                Debug.LogWarning($"Pool�̐���Request������Ă��Ȃ��̂Ŏg�p�ł��܂���B�Ώ�Pool {_monoPool.name}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// ���s���̊eUpdate
        /// </summary>
        /// <param name="data">�Ώۂ�Pool�f�[�^</param>
        /// <returns>Null</returns>
        IEnumerator Execution(PoolData data)
        {
            while (!data.Pool.Execute() && !data.IsEvent())
            {
                Debug.Log($"Execute {data.Pool.Execute()} Event {data.IsEvent()}");
                yield return null;
            }

            Delete(data);
        }

        void Delete(PoolData data)
        {
            if (_parent != null)
            {
                data.Pool.transform.SetParent(_parent);
            }

            data.IsUse = false;
            data.Pool.Delete();

            if (_isAutoActive)
            {
                data.Pool.gameObject.SetActive(false);
            }
        }
    }
}