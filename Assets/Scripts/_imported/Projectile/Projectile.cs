using UnityEngine;
using UnityEngine.Events;

public class Projectile : Movable
{
    [SerializeField] private ProjectileAsset projectileAsset;
    private class ProjectileTarget
    {
        private Transform targetTransform;
        private Rigidbody2D targetRigid;

        public Transform TargetTransform => targetTransform;
        public Rigidbody2D TargetRigid => targetRigid;

        public ProjectileTarget(Transform transform)
        {
            if (!transform)
                return;
            targetTransform = transform;
        }
        public ProjectileTarget(Transform transform, Rigidbody2D rigid) : this(transform)
        {
            if (!transform)
                return;
            if (rigid)
                targetRigid = rigid;
        }
    }
    private ProjectileTarget projectileTarget;
    private Destructible parent;
    private int bounceNum;
    private int lostDamge;
    #region
    private bool isHoming => projectileAsset.IsHoming;
    private float homingAngle => projectileAsset.HomingAngle;
    private float impactCheckLineLenght => projectileAsset.ImpactCheckLineLenght;
    private bool hasImpactForce => projectileAsset.HasImpactForce;
    private float impactForceModifier => projectileAsset.ImpactForceModifier;
    private Sound impactSFX => projectileAsset.ImpactSFX;
    private int damage => projectileAsset.Damage;
    private float lifetime => projectileAsset.Lifetime;
    private bool canBounce => projectileAsset.CanBounce;
    private int maxBounceNum => projectileAsset.MaxBounceNum;
    private int damadeLossPerBounce => projectileAsset.DamadeLossPerBounce;
    #endregion


    [SerializeField] private UnityEvent m_ImpactEffect;
    [SerializeField] private UnityEvent m_DestroyEffect;

    protected override void Start()
    {
        if (projectileAsset)
            SetAsset(projectileAsset as MovableAsset);
        CalculateLead();
        Destroy(gameObject, lifetime);
        base.Start();
    }

    protected override void FixedUpdate()
    {
        CalculateLead();
        base.FixedUpdate();
    }

    private void OnDestroy()
    {
        m_DestroyEffect.Invoke();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.root.gameObject)
            return;
        Impact(collision.transform.root.gameObject);
    }
    private void Impact(GameObject v_object)
    {
        if (!v_object)
            return;
        var v_dest = v_object.transform.root.GetComponent<Destructible>();
        var v_rigid = v_object.transform.root.GetComponent<Rigidbody>();
        if (v_rigid && hasImpactForce)
            v_rigid.AddForce(rigid.mass * rigid.velocity * impactForceModifier, ForceMode.Impulse);
        if (v_dest && (v_dest != parent || projectileAsset.CanDamageParrent))
        {
            v_dest.ApplyDamage(damage - lostDamge);
        }
        if (canBounce && bounceNum < maxBounceNum)
        {
            bounceNum++;
            lostDamge -= damadeLossPerBounce;
        }
        else
        {
            SoundController.Instance.Play(impactSFX);
            Destroy(gameObject);
        }
        m_ImpactEffect.Invoke();
    }
    private float CalculateAngle(Vector3 targetPosition)
    {
        if (targetPosition == null)
            return 0;
        Vector2 localTargetPosition = transform.InverseTransformPoint(targetPosition);
        float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
        angle = Mathf.Clamp(angle, -homingAngle, homingAngle) / homingAngle;
        return -angle;
    }
    private void CalculateLead()
    {
        if (isHoming)
        {
            if (projectileTarget.TargetTransform)
                AngularForceControl = CalculateAngle(projectileTarget.TargetTransform.position);
            else
                Destroy(gameObject);
        }
        LinearForceControl = 1;
    }
    public void SetAsset(ProjectileAsset asset)
    {
        if (!asset)
            return;
        projectileAsset = asset;
        base.SetAsset(asset);
    }
    public void SetTarget(Transform target)
    {
        projectileTarget = new ProjectileTarget(target, target.GetComponent<Rigidbody2D>());
    }
    public void SetParentShooter(Destructible v_parent)
    {
        parent = v_parent;
    }
}
