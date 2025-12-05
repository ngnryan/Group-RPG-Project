using UnityEngine;
using System.Collections;

public class BattleLogic : MonoBehaviour
{
    public BattleState player;
    public BattleState enemy;

    public HeartsUI playerHearts;
    public HeartsUI enemyHearts;

    public GameObject losingScreen;


    [Header("Player Cast UI")]
    public GameObject playerCastEarth;
    public GameObject playerCastFire;
    public GameObject playerCastWater;
    public GameObject playerCastWind;

    [Header("Enemy Cast UI")]
    public GameObject enemyCastEarth;
    public GameObject enemyCastFire;
    public GameObject enemyCastWater;
    public GameObject enemyCastWind;

    private void Start()
    {
        losingScreen.SetActive(false);
        //DisableAllCastUI();
    }

    private void DisableAllCastUI()
    {
        playerCastEarth.SetActive(false);
        playerCastFire.SetActive(false);
        playerCastWater.SetActive(false);
        playerCastWind.SetActive(false);

        enemyCastEarth.SetActive(false);
        enemyCastFire.SetActive(false);
        enemyCastWater.SetActive(false);
        enemyCastWind.SetActive(false);
    }

    public void PlayerSelectMove(MoveTemplate move)
    {
        StartCoroutine(PlayerMoveFlow(move));
    }

    private IEnumerator PlayerMoveFlow(MoveTemplate move)
    {
        DisableAllCastUI();

        // player move
        ShowPlayerCastUI(move);
        ApplyMove(player, enemy, move);
        enemyHearts.UpdateHearts(enemy.characterStats.health);

        yield return new WaitForSeconds(1.2f);
        DisableAllCastUI();

        if (enemy.characterStats.health <= 0)
        {
            //enemy dead
            yield break;
        }

        //enemy move
        MoveTemplate strongest = GetStrongestMove(enemy);
        ShowEnemyCastUI(strongest);
        ApplyMove(enemy, player, strongest);

        playerHearts.UpdateHearts(player.characterStats.health);

        yield return new WaitForSeconds(1.2f);
        DisableAllCastUI();

        if (player.characterStats.health <= 0)
        {
            losingScreen.SetActive(true);
            losingScreen.transform.SetAsLastSibling();
            yield break;
        }
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

    private void ShowPlayerCastUI(MoveTemplate move)
{
    switch (move.type)
    {
        case MoveTemplate.MoveTypes.Earth:
            playerCastEarth.SetActive(true);
            break;

        case MoveTemplate.MoveTypes.Fire:
            playerCastFire.SetActive(true);
            break;

        case MoveTemplate.MoveTypes.Water:
            playerCastWater.SetActive(true);
            break;

        case MoveTemplate.MoveTypes.Air:
            playerCastWind.SetActive(true);
            break;
    }
}


    private void ShowEnemyCastUI(MoveTemplate move)
{
    switch (move.type)
    {
        case MoveTemplate.MoveTypes.Earth:
            enemyCastEarth.SetActive(true);
            break;

        case MoveTemplate.MoveTypes.Fire:
            enemyCastFire.SetActive(true);
            break;

        case MoveTemplate.MoveTypes.Water:
            enemyCastWater.SetActive(true);
            break;

        case MoveTemplate.MoveTypes.Air:
            enemyCastWind.SetActive(true);
            break;
    }
}

}
