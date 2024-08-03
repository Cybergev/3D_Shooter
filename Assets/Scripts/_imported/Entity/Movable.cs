using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movable : MonoBehaviour
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected MovableAsset movableAsset;
    protected ForceMode moveType;
    private float linearVelocityMidifier = 1;
    private float angularVelocityMidifier = 1;

    #region Public API
    public void SetAsset(MovableAsset asset)
    {
        if (!asset)
            return;
        movableAsset = asset;
        UseAsset(asset);
    }
    public void UseAsset(MovableAsset asset)
    {
        if (!asset)
            return;
        moveType = asset.MoveType;
        rigid.mass = asset.MassIsRandom ? Random.Range(asset.MassRandomRange.x, asset.MassRandomRange.y) : asset.Mass;
        rigid.useGravity = asset.UseGravity;
        rigid.drag = asset.LinearDragIsRandom ? Random.Range(asset.LinearDragRange.x, asset.LinearDragRange.y) : asset.LinearDrag;
        rigid.angularDrag = asset.AngularDragIsRandom ? Random.Range(asset.AngularDragRange.x, asset.AngularDragRange.y) : asset.AngularDrag;
        rigid.maxLinearVelocity = asset.MaxLinearVelocityIsRandom ? Random.Range(asset.MaxLinearVelocityRandomRange.x, asset.MaxLinearVelocityRandomRange.y) : asset.MaxLinearVelocity;
        rigid.maxAngularVelocity = asset.MaxAngularVelocityIsRandom ? Random.Range(asset.MaxAngularVelocityRandomRange.x, asset.MaxAngularVelocityRandomRange.y) : asset.MaxAngularVelocity;
    }
    public void ChangeMoveType(ForceMode mode)
    {
        moveType = mode;
    }
    public void ModifyLinearVelocity(float velocity)
    {
        linearVelocityMidifier = velocity >= 0 ? velocity : linearVelocityMidifier;
    }
    public void ModifyAngularVelocity(float velocity)
    {
        angularVelocityMidifier = velocity >= 0 ? velocity : angularVelocityMidifier;
    }
    public void BackupLinearVelocity()
    {
        linearVelocityMidifier = 1;
    }
    public void BackupAngularVelocity()
    {
        angularVelocityMidifier = 1;
    }
    /// <summary>
    /// ”правление линейной силы. -1.0 до +1.0
    /// </summary>
    public float LinearForceControl { get; set; }

    /// <summary>
    /// ”правление вращательной силы. -1.0 до +1.0
    /// </summary>
    public float AngularForceControl { get; set; }
    #endregion

    #region Unity Events
    protected virtual void Start()
    {
        rigid ??= GetComponent<Rigidbody>();
        if (movableAsset)
            UseAsset(movableAsset);
    }

    protected virtual void FixedUpdate()
    {
        UpdateRigitBody(moveType);
    }
    #endregion

    #region Movement
    /// <summary>
    /// ћетод применени€ силы к объекту дл€ движени€.
    /// </summary>
    private void UpdateRigitBody(ForceMode forceType)
    {
        if (!movableAsset)
            return;
        if (forceType == ForceMode.Force)
        {
            rigid.AddForce(LinearForceControl * movableAsset.LinearForce * transform.up * linearVelocityMidifier * Time.fixedDeltaTime, forceType);
            rigid.AddForce(-rigid.velocity * (movableAsset.LinearForce / rigid.maxLinearVelocity) * Time.fixedDeltaTime, forceType);
            rigid.AddTorque(AngularForceControl * movableAsset.AngularForce * transform.up * angularVelocityMidifier * Time.fixedDeltaTime, forceType);
            rigid.AddTorque(-rigid.angularVelocity * (movableAsset.AngularForce / rigid.maxAngularVelocity) * Time.fixedDeltaTime, forceType);
        }
        if (forceType == ForceMode.Impulse)
        {
            rigid.AddForce(LinearForceControl * movableAsset.LinearForce * transform.up * linearVelocityMidifier * Time.fixedDeltaTime, forceType);
            rigid.AddTorque(AngularForceControl * movableAsset.AngularForce * transform.up * angularVelocityMidifier * Time.fixedDeltaTime, forceType);
            ModifyLinearVelocity(0);
            ModifyAngularVelocity(0);
        }
    }
    #endregion
}