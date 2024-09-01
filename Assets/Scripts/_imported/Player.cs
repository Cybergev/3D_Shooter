using UnityEngine;
[RequireComponent (typeof(Entity))]
public class Player : MonoSingleton<Player>
{
    [SerializeField] private Entity curentEntity;

    private void FixedUpdate()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            curentEntity.Shoot();
        }
    }
}