using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// ������, ������� ���������.
/// </summary>
public class Destructible : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// �������� �������������� �������.
    /// ���� true - ������ �� ����� ���� ���������.
    /// ���� false - ������ ����� ���� ���������.
    /// </summary>
    [SerializeField] private bool m_Indestructible;
    public bool Indestuctible => m_Indestructible;
    /// <summary>
    /// �������� �������������� �������.
    /// ���� true - ������ �� ����� ���� ��������
    /// ���� true - ������ ����� ���� ��������
    /// </summary>
    [SerializeField] private bool m_Indamageble;
    public bool Indamageble => m_Indamageble;
    /// <summary>
    /// ������� ���������� ����������.
    /// </summary>
    [SerializeField] protected int m_HitPoints;
    /// <summary>
    /// ������� ���������.
    /// </summary>
    [SerializeField] private int m_CurrentHitPoints;
    public int HitPoints => m_CurrentHitPoints;
    [SerializeField] private UnityEvent m_ChangeHitPoints;
    public UnityEvent ChangeHitPoints => m_ChangeHitPoints;
    #endregion

    #region Unity Events
    protected virtual void Start()
    {
        SetCurrentHitPoints(m_HitPoints);
        ChangeHitPoints.Invoke();
    }
    /// <summary>
    /// ���������������� ������� ����������� �������, ����� ��������� ���� ��� ����� ����.
    /// </summary>
    protected virtual void OnDeath()
    {
        if (m_Indestructible)
            return;

        m_EventOnDeath?.Invoke();
        Destroy(gameObject);
    }
    protected virtual void OnEnable()
    {
        AllDestructibles ??= new HashSet<Destructible>();
        AllDestructibles.Add(this);
    }
    protected virtual void OnDestroy()
    {
        AllDestructibles.Remove(this);
        NumDestroyed++;
    }
    [SerializeField] private UnityEvent m_EventOnDeath;
    public UnityEvent EventOnDeath => m_EventOnDeath;
    #endregion

    #region Public API
    public void UseAsset(DestructibleAsset asset)
    {
        SetIndestructible(asset.IsIndestructible);
        SetIndamageble(asset.IsIndamageble);
        if (asset.HealthPointsIsRandom)
        {
            int hitPoints = Random.Range(asset.HealthPointsRandomRange.x, asset.HealthPointsRandomRange.y);
            SetMaxHitPoints(hitPoints);
            SetCurrentHitPoints(hitPoints);
        }
        else
        {
            SetMaxHitPoints(asset.HealthPoints);
            SetCurrentHitPoints(asset.HealthPoints);
        }
    }
    public void SetIndestructible(bool value)
    {
        m_Indestructible = value;
    }
    public void SetIndamageble(bool value)
    {
        m_Indamageble = value;
    }
    /// <summary>
    /// ���������� ���������� ����������.
    /// </summary>
    public void SetMaxHitPoints(int v_hitPoints)
    {
        if (v_hitPoints <= 0) 
            return;

        m_HitPoints = v_hitPoints;
    }
    public void SetCurrentHitPoints(int v_hitPoints)
    {
        if (v_hitPoints <= 0) 
            return;

        m_CurrentHitPoints = v_hitPoints;
    }
    /// <summary>
    /// ���������� ����� � �������.
    /// </summary>
    /// <param name="damage"> ���� ��������� �������.</param>
    public virtual void ApplyDamage(int damage)
    {
        if (m_Indamageble) 
            return;

        m_CurrentHitPoints -= damage;

        if (m_CurrentHitPoints <= 0)
        {
            m_CurrentHitPoints = 0;
            OnDeath();
        }

        ChangeHitPoints.Invoke();

    }
    #endregion

    #region Statics
    public static HashSet<Destructible> AllDestructibles { get; private set; }
    public static int NumDestroyed { get; private set; }
    public static void ClearNumDestroyed()
    {
        NumDestroyed = 0;
    }
    #endregion
}