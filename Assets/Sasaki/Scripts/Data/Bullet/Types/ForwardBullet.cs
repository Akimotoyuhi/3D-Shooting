using UnityEngine;

/// <summary>
/// 通常弾のデータ
/// </summary>
public class ForwardBullet : IBulletData
{
    [SerializeField] MoveType _towardType;
    [SerializeField, Range(0, 1)] float _horizontalBlur;
    [SerializeField, Range(0, 1)] float _virticleBlur;

    public BulletParam SendData()
    {
        BulletParam param = new BulletParam();

        param.WayCount = 1;
        
        return param;
    }

    public Vector3 SetNormalizeDir(FieldStateHelper.State state, Transform user)
    {
        Vector3 dir = Vector3.zero;

        switch (_towardType)
        {
            case MoveType.Forward:
                dir = user.forward;

                break;
            case MoveType.ToPlayer:

                if (GameManager.Instance != null && GameManager.Instance.CurrentPlayer !=null)
                {
                    dir = user.forward - GameManager.Instance.CurrentPlayer.transform.position;
                }
                else
                {
                    dir = user.forward;
                    Debug.LogWarning("GameManagerまたは、Playerが存在しません。");
                }

                break;
        }

        SetBlur(state, ref dir);

        return dir;
    }

    void SetBlur(FieldStateHelper.State state, ref Vector3 dir)
    {
        Vector3 blurDir = Vector3.zero;

        float horizontal = Random.Range(_horizontalBlur * -1, _horizontalBlur);
        float verticle = Random.Range(_virticleBlur * -1, _virticleBlur);

        switch (state)
        {
            case FieldStateHelper.State.TopView:
                blurDir = new Vector3(horizontal, 0, verticle);

                break;
            case FieldStateHelper.State.SideView:
                blurDir = new Vector3(0, verticle, horizontal);

                break;
            case FieldStateHelper.State.BackView:
                blurDir = new Vector3(horizontal, verticle, 0);

                break;
        }

        dir += blurDir;
    }

    public void Initalize()
    {
        
    }
}
