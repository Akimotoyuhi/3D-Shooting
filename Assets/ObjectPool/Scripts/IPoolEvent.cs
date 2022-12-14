namespace ObjectPool
{
    /// <summary>
    /// PoolObjectが任意のタイミングで終了を判定したい場合に継承
    /// </summary>
    public interface IPoolEvent
    {
        /// <summary>
        /// Eventの終了タイミングでTrueを返すようにする
        /// </summary>
        bool IsDone { get; set; }
    }
}