using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Объект, имеющий движение.
/// </summary>
public class MovableAsset : ScriptableObject
{
    [Header("Move Settings")]
    [SerializeField] private ForceMode moveType;

    [SerializeField] private bool massIsRandom = false;
    [SerializeField] private float mass = 1;
    [SerializeField] private Vector2 massRandomRange = new Vector2(1, 10);

    [SerializeField] private bool useGravity = false;

    [SerializeField] private bool linearDragIsRandom = false;
    [SerializeField] private float linearDrag = 1;
    [SerializeField] private Vector2 linearDragRandomRange = new Vector2(1, 10);

    [SerializeField] private bool angularDragIsRandom = false;
    [SerializeField] private float angularDrag = 1;
    [SerializeField] private Vector2 angularDragRandomRange = new Vector2(1, 10);
    
    [SerializeField] private bool linearForceIsRandom = false;
    [SerializeField] private float linearForce = 1;
    [SerializeField] private Vector2 linearForceRandomRange = new Vector2(1, 10);

    [SerializeField] private bool angularForceIsRandom = false;
    [SerializeField] private float angularForce = 1;
    [SerializeField] private Vector2 angularForceRandomRange = new Vector2(1, 10);

    [SerializeField] private bool maxLinearVelocityIsRandom = false;
    [SerializeField] private float maxLinearVelocity = 1;
    [SerializeField] private Vector2 maxLinearVelocityRandomRange = new Vector2(1, 10);

    [SerializeField] private bool maxAngularVelocityIsRandom = false;
    [SerializeField] private float maxAngularVelocity = 1;
    [SerializeField] private Vector2 maxAngularVelocityRandomRange = new Vector2(1, 10);
    #region Public
    public ForceMode MoveType => moveType;
    public bool MassIsRandom => massIsRandom;
    public float Mass => mass;
    public Vector2 MassRandomRange => massRandomRange;
    public bool UseGravity => useGravity;
    public bool LinearDragIsRandom => linearDragIsRandom;
    public float LinearDrag => linearDrag;
    public Vector2 LinearDragRange => linearDragRandomRange;
    public bool AngularDragIsRandom => angularDragIsRandom;
    public float AngularDrag => angularDrag;
    public Vector2 AngularDragRange => angularDragRandomRange;
    public bool LinearForceIsRandom => linearForceIsRandom;
    public float LinearForce => linearForce;
    public Vector2 LinearForceRandomRange => linearForceRandomRange;
    public bool AngularForceIsRandom => angularForceIsRandom;
    public float AngularForce => angularForce;
    public Vector2 AngularForceRandomRange => angularForceRandomRange;
    public bool MaxLinearVelocityIsRandom => maxLinearVelocityIsRandom;
    public float MaxLinearVelocity => maxLinearVelocity;
    public Vector2 MaxLinearVelocityRandomRange => maxLinearVelocityRandomRange;
    public bool MaxAngularVelocityIsRandom => maxAngularVelocityIsRandom;
    public float MaxAngularVelocity => maxAngularVelocity;
    public Vector2 MaxAngularVelocityRandomRange => maxAngularVelocityRandomRange;
    #endregion

    #region Editor
    #if UNITY_EDITOR
    [CustomEditor(typeof(MovableAsset))]
    public class MovableAssetInspector : Editor
    {
        private MovableAsset movableAsset;
        private void OnEnable()
        {
            movableAsset = (MovableAsset)target;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.moveType)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.massIsRandom)));
            EditorGUILayout.PropertyField(
                !movableAsset.massIsRandom ? serializedObject.FindProperty(nameof(movableAsset.mass)) : serializedObject.FindProperty(nameof(movableAsset.massRandomRange))
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.useGravity)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.linearDragIsRandom)));
            EditorGUILayout.PropertyField(
                !movableAsset.linearDragIsRandom ? serializedObject.FindProperty(nameof(movableAsset.linearDrag)) : serializedObject.FindProperty(nameof(movableAsset.linearDragRandomRange))
            );
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.angularDragIsRandom)));
            EditorGUILayout.PropertyField(
                !movableAsset.angularDragIsRandom ? serializedObject.FindProperty(nameof(movableAsset.angularDrag)) : serializedObject.FindProperty(nameof(movableAsset.angularDragRandomRange))
            );
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.linearForceIsRandom)));
            EditorGUILayout.PropertyField(
                !movableAsset.linearForceIsRandom ? serializedObject.FindProperty(nameof(movableAsset.linearForce)) : serializedObject.FindProperty(nameof(movableAsset.linearForceRandomRange))
            );
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.angularForceIsRandom)));
            EditorGUILayout.PropertyField(
                !movableAsset.angularForceIsRandom ? serializedObject.FindProperty(nameof(movableAsset.angularForce)) : serializedObject.FindProperty(nameof(movableAsset.angularForceRandomRange))
            );
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.maxLinearVelocityIsRandom)));
            EditorGUILayout.PropertyField(
                !movableAsset.maxLinearVelocityIsRandom ? serializedObject.FindProperty(nameof(movableAsset.maxLinearVelocity)) : serializedObject.FindProperty(nameof(movableAsset.maxLinearVelocityRandomRange))
            );
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(movableAsset.maxAngularVelocityIsRandom)));
            EditorGUILayout.PropertyField(
                !movableAsset.maxAngularVelocityIsRandom ? serializedObject.FindProperty(nameof(movableAsset.maxAngularVelocity)) : serializedObject.FindProperty(nameof(movableAsset.maxAngularVelocityRandomRange))
            );
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
    #endregion
}
