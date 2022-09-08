namespace ObjectPool
{
    /// <summary>
    /// Pool������Object�Ɍp��
    /// </summary>
    public interface IPool
    {
        /// <summary>
        /// Start�֐�
        /// </summary>
        /// <param name="parent">Pool�̐e</param>
        void Setup(UnityEngine.Transform parent);

        /// <summary>
        /// OnEnable�֐�
        /// </summary>
        void OnEnableEvent();

        /// <summary>
        /// �g�p����Update�֐�
        /// </summary>
        /// <returns>�g�p�I������Ture��Ԃ�</returns>
        bool Execute();

        /// <summary>
        /// �g�p���Destroy�֐�
        /// </summary>
        void Delete();
    }
}