using UnityEngine;

/// <summary>
/// í èÌíeÇÃÉfÅ[É^
/// </summary>
public class ForwardBullet : IBulletData
{
    [SerializeField] float _blur;
    public BulletType Type => BulletType.Forward;

    public BulletParam SendData()
    {
        BulletParam param = new BulletParam();

        param.WayCount = 1;
        param.Blur = _blur;

        return param;
    }

    public Vector3 SetDir(FieldStateHelper.ViewState state, Transform user)
    {
        Vector3 dir = Vector3.zero;

        switch (state)
        {
            case FieldStateHelper.ViewState.TopView:
                dir = user.forward;

                break;
            case FieldStateHelper.ViewState.SideView:
                break;
            case FieldStateHelper.ViewState.BackView:
                break;
        }

        return dir;
    }

    public void Initalize()
    {
        
    }

}
