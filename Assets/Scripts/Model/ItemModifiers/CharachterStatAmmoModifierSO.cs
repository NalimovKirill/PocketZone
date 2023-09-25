using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharachterStatAmmoModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerShoot playerShoot = FindAnyObjectByType(typeof(PlayerShoot)) as PlayerShoot;
        if (playerShoot != null)
        {
            playerShoot.AddAmmo((int)val);
        }
    }
}
