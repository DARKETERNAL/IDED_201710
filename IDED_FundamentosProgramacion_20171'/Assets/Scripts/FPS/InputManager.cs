using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Character controlledCharacter;

    [SerializeField]
    private float cooldownBase = 1F;

    private float cooldown;
    private bool firedWeapon;

    // Use this for initialization
    private void Start()
    {
        if (controlledCharacter == null)
        {
            //This ensures update isn't executed if character is not available
            enabled = false;
            cooldown = cooldownBase;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxisRaw("Fire1") != 0F && !firedWeapon)
        {
            controlledCharacter.FireWeapon();

            if (!firedWeapon)
            {
                firedWeapon = true;
            }
        }

        if (firedWeapon)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0F)
            {
                cooldown = cooldownBase;
                firedWeapon = false;
            }
        }
    }
}