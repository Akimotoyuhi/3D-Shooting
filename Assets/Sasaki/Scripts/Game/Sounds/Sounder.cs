using UnityEngine;
using ObjectPool;

public class Sounder : MonoBehaviour, IPool
{
    AudioSource _source;

    public void Setup(Transform parent)
    {
        _source = gameObject.AddComponent<AudioSource>();
    }

    public void OnEnableEvent()
    {
        
    }

    public bool Execute()
    {
        return true;
    }

    public void Delete()
    {
        
    }
}
