using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    /// <summary>
    /// ObjectPoolの管理クラス
    /// </summary>
    /// <typeparam name="MonoPool">Poolにする対象Object</typeparam>
    public class Pool<MonoPool> where MonoPool : MonoBehaviour, IPool
    {
        /// <summary>
        /// 各Poolのデータ
        /// </summary>
        class PoolData
        {
            public MonoPool Pool { get; set; }
            public IPoolEvent Event { get; set; }
            public bool IsUse { get; set; }

            /// <summary>
            /// Eventの終了通知
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

        HideFlags _hideFlags = HideFlags.None;

        MonoPool _monoPool;
        List<PoolData> _poolList = new List<PoolData>();

        Transform _parent;

        /// <summary>
        /// 初期設定
        /// </summary>
        /// <param name="pool">Poolにする対象</param>
        /// <param name="createCount">生成数</param>
        public Pool<MonoPool> SetMono(MonoPool pool, int createCount = 5)
        {
            _createCount = createCount;
            _monoPool = pool;
            
            _isSetMono = true;

            return this;
        }

        /// <summary>
        /// 親の保持
        /// </summary>
        /// <param name="parent">親にするTransform</param>
        /// <returns></returns>
        public Pool<MonoPool> IsSetParent(Transform parent)
        {
            _parent = parent;
            
            return this;
        }

        /// <summary>
        /// 使用Poolに対するActive権限の自動化
        /// </summary>
        /// <returns></returns>
        public Pool<MonoPool> IsAutoActive()
        {
            _isAutoActive = true;

            return this;
        }

        /// <summary>
        /// Hierarchy上でのObjectの有無を設定
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Pool<MonoPool> SetHideFlags()
        {
            _hideFlags = HideFlags.HideInHierarchy;

            return this;
        }

        /// <summary>
        /// Poolの生成Request
        /// </summary>
        /// <returns></returns>
        public void CreateRequest()
        {
            CreatePool(_createCount);
            _isCreate = true;
        }

        /// <summary>
        /// Poolの生成
        /// </summary>
        /// <param name="createCount">生成数</param>
        void CreatePool(int createCount)
        {
            for (int index = 0; index < createCount; index++)
            {
                MonoPool pool = Object.Instantiate(_monoPool);
                pool.Setup(_parent);

                PoolData data = CreateData(pool);
                _poolList.Add(data);

                data.Event = pool.GetComponent<IPoolEvent>();
                data.Pool.gameObject.hideFlags = _hideFlags;

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
        /// Poolデータの作成
        /// </summary>
        /// <param name="pool">対象Pool</param>
        /// <returns>PoolData</returns>
        PoolData CreateData(MonoPool pool)
        {
            PoolData data = new PoolData();
            data.Pool = pool;
            data.IsUse = false;
            
            return data;
        }

        /// <summary>
        /// Poolの使用
        /// </summary>
        public void UseRequest()
        {
            if (!ChackSuccess())
            {
                Debug.LogWarning("Poolの使用権限がありません");
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

                Debug.LogWarning($"Poolが上限に達したので上限を増やしました。" +
                    $"\n 対象Pool.{_monoPool.name} : 生成数.{_createCount} : 上限.{_poolList.Count}");

                UseRequest();
            }
        }

        /// <summary>
        /// Poolの使用リクエスト
        /// </summary>
        /// <param name="action">Invoke(); することで使用</param>
        /// <returns>対象Pool</returns>
        public MonoPool UseRequest(out System.Action action)
        {
            action = null;

            if (!ChackSuccess())
            {
                Debug.LogWarning("Poolの使用権限がありません");
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

                Debug.LogWarning($"Poolが上限に達したので上限を増やしました。" +
                    $"\n 対象Pool.{_monoPool.name} : 生成数.{_createCount} : 上限.{_poolList.Count}");

                return UseRequest(out action);
            }
        }

        bool ChackSuccess()
        {
            if (!_isSetMono)
            {
                Debug.LogWarning("対象Poolがありません。");
                return false;
            }

            if (!_isCreate)
            {
                Debug.LogWarning($"Poolの生成Requestがされていないので使用できません。対象Pool {_monoPool.name}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 実行中の各Update
        /// </summary>
        /// <param name="data">対象のPoolデータ</param>
        /// <returns>Null</returns>
        IEnumerator Execution(PoolData data)
        {
            while (!data.Pool.Execute() && !data.IsEvent())
            {
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
