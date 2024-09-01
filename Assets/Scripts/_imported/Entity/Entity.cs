using UnityEngine;
public class Entity : Destructible
{
    [Header("")]
    [SerializeField] private TeamId m_TeamId;
    [SerializeField] private Weapon m_Weapon;
    private EntityAsset m_currentAsset;

    public TeamId TeamId => m_TeamId;
    protected override void Start()
    {
        base.Start();
        if (m_currentAsset) UseAsset(m_currentAsset);
    }
    public void UseAsset(EntityAsset asset)
    {
        base.UseAsset(asset);
        if (!asset)
            return;
        m_currentAsset = asset;
    }
    public void Shoot()
    {
        m_Weapon.Shoot();
    }
}
public enum TeamId
{
    Neutral = 0,
    First = 1,
    Second = 2
}