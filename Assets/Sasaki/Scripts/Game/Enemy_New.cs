using UnityEngine;

public class Enemy_New : EnemyBase, IDamageble
{
    private void Update()
    {
        Vector3 move = MoveOperator.Move(transform);
        Rigidbody.velocity = move;
    }

    public void GetDamage(int damage)
    {
        int hp = UserStatusData.CurrentHP - damage;

        UserStatusData.UpdateHP(hp);
    }
}
