using UnityEngine;

namespace GameJam
{
    public interface IAttack
    {
        void Shoot(Vector3 direction,LayerMask? whatIsEnemies);
    }
}