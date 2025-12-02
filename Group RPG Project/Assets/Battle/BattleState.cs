using UnityEngine;

public class BattleState : MonoBehaviour
{
   public CharacterStats characterStats;
   public MoveSet moveSet;

   private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
        moveSet = GetComponent<MoveSet>();
    }
}
