using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileAsset : MovableAsset
{
    [Header("Projectile Settings")]
    [SerializeField] private bool m_IsHoming = false;
    [SerializeField, Range(0.0f, 180.0f)] private float m_HomingAngle = 45;
    [SerializeField] private float m_ImpactCheckLineLenght;
    [SerializeField] private bool m_HasImpactForce = true;
    [SerializeField, Range(0.0f, 2.0f)] private float m_ImpactForceModifier = 1;
    [SerializeField] private Sound m_ImpactSFX;
    [SerializeField] private bool m_CanDamageParrent = false;
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_Lifetime;
    [SerializeField] private bool m_CanBounce = false;
    [SerializeField] private int m_MaxBounceNum;
    [SerializeField] private int m_DamadeLossPerBounce;
    #region Public
    public bool CanDamageParrent => m_CanDamageParrent;
    public bool IsHoming => m_IsHoming;
    public float HomingAngle => m_HomingAngle;
    public float ImpactCheckLineLenght => m_ImpactCheckLineLenght;
    public bool HasImpactForce => m_HasImpactForce;
    public float ImpactForceModifier => m_ImpactForceModifier;
    public Sound ImpactSFX => m_ImpactSFX;
    public int Damage => m_Damage;
    public float Lifetime => m_Lifetime;
    public bool CanBounce => m_CanBounce;
    public int MaxBounceNum => m_MaxBounceNum;
    public int DamadeLossPerBounce => m_DamadeLossPerBounce;
    #endregion

    #region Editor
    #if UNITY_EDITOR
    [CustomEditor(typeof(ProjectileAsset))]
    public class ProjectileAssetInspector : MovableAssetInspector
    {
        private ProjectileAsset projectileAsset;
        protected override void OnEnable()
        {
            base.OnEnable();
            projectileAsset = (ProjectileAsset)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_IsHoming)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_HomingAngle)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_ImpactCheckLineLenght)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_HasImpactForce)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_ImpactForceModifier)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_ImpactSFX)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_CanDamageParrent)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_Damage)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_Lifetime)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_CanBounce)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_MaxBounceNum)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(projectileAsset.m_DamadeLossPerBounce)));
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
    #endregion
}