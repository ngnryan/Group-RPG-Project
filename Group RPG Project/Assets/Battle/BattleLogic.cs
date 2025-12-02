using UnityEngine;
using System.Collections;

public class BattleLogic : MonoBehaviour
{
    public BattleState player;
    public BattleState enemy;

    public HeartsUI playerHearts;
    public HeartsUI enemyHearts;

    public void PlayerSelectMove(MoveTemplate move)
    {
        StartCoroutine(PlayerMoveFlow(move));
    }

    private IEnumerator PlayerMoveFlow(MoveTemplate move)
    {
        if (move == null)
        {
            Debug.LogWarning("Player selected a NULL move!");
            yield break;
        }


        ShowMessage($"Player used {move.moveDescription}!");
        ApplyMove(player, enemy, move);
        enemyHearts.UpdateHearts(enemy.characterStats.health);

        if (enemy.characterStats.health <= 0)
        {
            ShowMessage("Enemy fainted! You win!");
            yield break;
        }

        yield return new WaitForSeconds(1f);

        MoveTemplate strongest = GetStrongestMove(enemy);

        ShowMessage($"Enemy used {strongest.moveDescription}!");
        ApplyMove(enemy, player, strongest);
        playerHearts.UpdateHearts(player.characterStats.health);

        if (player.characterStats.health <= 0)
        {
            ShowMessage("You fainted! Enemy wins!");
            yield break;
        }

        yield return new WaitForSeconds(1f);
    }

    private MoveTemplate GetStrongestMove(BattleState target)
    {
        MoveTemplate best = target.moveSet.moves[0];

        foreach (var move in target.moveSet.moves)
        {
            if (move.attack > best.attack)
                best = move;
        }

        return best;
    }

    private void ApplyMove(BattleState attacker, BattleState defender, MoveTemplate move)
    {
        int dmg = move.attack * attacker.characterStats.power;
        defender.characterStats.health -= dmg;

        if (defender.characterStats.health < 0)
            defender.characterStats.health = 0;
    }

    private void ShowMessage(string msg)
    {
        Debug.Log(msg);
    }
}
