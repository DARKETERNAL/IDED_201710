using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 5F;

    public float Damage { get; protected set; }

    public Bullet(float damage)
    {
        Damage = Mathf.Abs(damage);
    }

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Character hitCharacter = otherCollider.gameObject.GetComponent<Character>();

        if (hitCharacter != null)
        {
            hitCharacter.OnBulletHit(this);
        }
    }
}