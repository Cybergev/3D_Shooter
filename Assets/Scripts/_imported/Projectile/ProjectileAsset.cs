using UnityEngine;

[CreateAssetMenu]
public class ProjectileAsset : MovableAsset
{
    [SerializeField] private bool m_CanDamageParrent = false;
    [SerializeField] private bool m_IsHoming = false;
    [SerializeField] private float m_HomingAngle = 45;
    [SerializeField] private float m_ImpactCheckLineLenght;
    [SerializeField] private bool m_HasImpactForce = true;
    [SerializeField, Range(0.0f, 2.0f)] private float m_ImpactForceModifier = 1;
    [SerializeField] private Sound m_ImpactSFX;
    public bool CanDamageParrent => m_CanDamageParrent;
    public bool IsHoming => m_IsHoming;
    public float HomingAngle => m_HomingAngle;
    public float ImpactCheckLineLenght => m_ImpactCheckLineLenght;
    public bool HasImpactForce => m_HasImpactForce;
    public float ImpactForceModifier => m_ImpactForceModifier;
    public Sound ImpactSFX => m_ImpactSFX;

    [Header("")]
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_Lifetime;
    public int Damage => m_Damage;
    public float Lifetime => m_Lifetime;


    [SerializeField] private bool m_CanBounce = false;
    [SerializeField] private int m_MaxBounceNum;
    [SerializeField] private int m_DamadeLossPerBounce;
    public bool CanBounce => m_CanBounce;
    public int MaxBounceNum => m_MaxBounceNum;
    public int DamadeLossPerBounce => m_DamadeLossPerBounce;
}