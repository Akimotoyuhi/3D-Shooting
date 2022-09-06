using UnityEngine;

/// <summary>
/// Soundのデータクラス
/// </summary>
[System.Serializable]
public class SoundData
{
    [SerializeField] AudioClip _clip;
    [SerializeField, Range(0, 1)] float _volume;
    [SerializeField, Range(0, 1)] int _spatialBlend;

    public string Path => _clip.name;
    public float Volume => _volume;
    public int SpatialBlend => _spatialBlend;
}
