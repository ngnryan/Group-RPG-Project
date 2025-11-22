using UnityEngine;

public class PlayerBattleController : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    [SerializeField] private Moveset moveset;

    private BattleManager battleManager;

    private void Start()
    {
        battleManager = FindAnyObjectByType<BattleManager>();
    }

    public CharacterStats GetStats()
    {
        return stats;
    }

    public Moveset GetMoveset()
    {
        return moveset;
    }

    // Called when the player selects a move from the UI
    public void UseMove(int moveIndex)
    {
        MoveBase move = moveset.moves[moveIndex];

        Debug.Log("Player used " + move.moveName);

        // PIPES TO BATTLE MANAGER TO DEAL WITH ACCURACY, SPEED, ETC.
        battleManager.PlayerUseMove(move);
    }

    // Getting attacked
    public void TakeDamage(int amount)
    {
        stats.currentHP -= amount;

        if (stats.currentHP < 0)
            stats.currentHP = 0;

        Debug.Log("Player took " + amount + " damage!");

        // Optional: animation trigger
        // animator.SetTrigger("Hit");

        if (stats.currentHP <= 0)
        {
            Debug.Log("Player fainted...");
            battleManager.OnPlayerFainted();
        }
    }
}
