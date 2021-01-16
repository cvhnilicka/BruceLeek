using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWeapon : MonoBehaviour
{
    [SerializeField] float weaponDamage = 1f;


    public void SetWeaponDamage(float weaponDamage)
    {
        this.weaponDamage = weaponDamage;
    }
    public float GetWeaponDamage()
    {
        return this.weaponDamage;
    }
}
