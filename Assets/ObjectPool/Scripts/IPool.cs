namespace ObjectPool
{
    /// <summary>
    /// PoolさせるObjectに継承
    /// </summary>
    public interface IPool
    {
        /// <summary>
        /// Start関数
        /// </summary>
        /// <param name="parent">Poolの親</param>
        void Setup(UnityEngine.Transform parent);

        /// <summary>
        /// OnEnable関数
        /// </summary>
        void OnEnableEvent();

        /// <summary>
        /// 使用中のUpdate関数
        /// </summary>
        /// <returns>使用終了時にTureを返す</returns>
        bool Execute();

        /// <summary>
        /// 使用後のDestroy関数
        /// </summary>
        void Delete();
    }
}
