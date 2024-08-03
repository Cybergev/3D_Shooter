using UnityEngine;
[RequireComponent(typeof(Movable))]
public class Entity : Destructible
{
    [Header("")]
    [SerializeField] private TeamId m_TeamId;
    [SerializeField] private Movable m_Movable;
    [SerializeField] private Weapon m_Weapon;
    private EntityAsset m_currentAsset;

    public TeamId TeamId => m_TeamId;
    public float LinearForceControl 
    {
        get
        {
            return m_Movable.LinearForceControl;
        }
        set
        {
            m_Movable.LinearForceControl = value;
        }
    }
    public float AngularForceControl
    {
        get
        {
            return m_Movable.AngularForceControl;
        }
        set
        {
            m_Movable.AngularForceControl = value;
        }
    }
    protected override void Start()
    {
        base.Start();
        m_Movable ??= GetComponent<Movable>();
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