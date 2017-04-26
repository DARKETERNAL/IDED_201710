using UnityEngine;

public class Character : MonoBehaviour
{
    public static float baseHealth = 100F;

    [SerializeField]
    private GameObject bulletBaseGameObject;

    [SerializeField]
    private Transform bulletSpawnOrigin;

    [SerializeField]
    private float bulletSpeed = 10F;

    private float health;
    private float armor;
    private float damageBoost;

    public void OnBulletHit(Bullet bullet)
    {
        if (bullet != null)
        {
            if (armor > 0F)
            {
                //TODO: Use Mathf.Clamp
                armor -= bullet.Damage;

                if (armor < 0F)
                {
                    armor = 0F;
                }
            }
            else
            {
                health -= bullet.Damage;
            }
        }
    }

    public void FireWeapon()
    {
        if (bulletBaseGameObject != null && bulletSpawnOrigin != null)
        {
            GameObject bulletInstance = GameObject.Instantiate(bulletBaseGameObject, bulletSpawnOrigin.position, bulletSpawnOrigin.rotation) as GameObject;
            Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();
            bulletRigidbody.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Force);
        }
    }

    // Use this for initialization
    private void Start()
    {
        health = baseHealth;
    }
}