using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWeapon : MonoBehaviour
{
    [SerializeField] int weaponDamage = 1;

    private void Start()
    {
        // Default to 1 for damage
        //weaponDamage = 1;
    }


    public void SetWeaponDamage(int weaponDamage)
    {
        this.weaponDamage = weaponDamage;
    }
    public int GetWeaponDamage()
    {
        return this.weaponDamage;
    }
}
