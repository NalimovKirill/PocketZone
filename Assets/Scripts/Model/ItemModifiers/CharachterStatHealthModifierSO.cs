using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharachterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerGeneral player = FindAnyObjectByType(typeof(PlayerGeneral)) as PlayerGeneral;
        if (player != null)
        {
            player.AddHealth((int)val);
        }
    }
}
