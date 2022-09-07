using UnityEngine;

[System.Serializable]
public class BulletTask
{
    [SerializeField] BulletDataBase _dataBase;
    [SerializeField] float _taskTime;

    float _timer = 0;

    public BulletDataBase BulletData => _dataBase;
    
    public bool IsProcess()
    {
        _timer += Time.deltaTime;
        return _timer > _taskTime;
    }

    public void Initalize()
    {
        _dataBase.BulletData.IBulletData.Initalize();
        _timer = 0;
    }
}
