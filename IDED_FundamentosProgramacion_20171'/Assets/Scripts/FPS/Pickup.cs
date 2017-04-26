using UnityEngine;

public enum PickupType
{
    Health,
    Ammo,
    Armor,
    DamageBoost
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    [SerializeField]
    private PickupType type;

    [SerializeField]
    private float amount;

    // Use this for initialization
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider != null)
        {
            //Cast collider object to character
            //Do stuff according to type
            //Armor: Adds armor value
            //Ammo: Adds ammo to player weapon
            //Health: Adds health to player
            //DamageBoost: Adds temporal damage boost to player

            Debug.Log(string.Format("Picked up item of type {0}", type));
        }
    }
}