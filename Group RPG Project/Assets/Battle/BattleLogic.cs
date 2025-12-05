using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class BattleLogic : MonoBehaviour
{
    public BattleState player;
    public BattleState enemy;

    public HeartsUI playerHearts;
    public HeartsUI enemyHearts;

    public GameObject losingScreen;


    [Header("PlayerCastUI")]
    public GameObject playerCastEarth;
    public GameObject playerCastFire;
    public GameObject playerCastWater;
    public GameObject playerCastWind;

    [Header("EnemyCastUI")]
    public GameObject enemyCastEarth;
    public GameObject enemyCastFire;
    public GameObject enemyCastWater;
    public GameObject enemyCastWind;

    [Header("VFX Spawn Points")]
    public Transform playerVFXPoint;
    public Transform enemyVFXPoint;


    private void Start()
    {
        losingScreen.SetActive(false);
        DisableAllCastUI();
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
        SpawnSpellVFX(move,true);
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


        SpawnSpellVFX(strongest, false);

        ApplyMove(enemy, player, strongest);
        playerHearts.UpdateHearts(player.characterStats.health);


        yield return new WaitForSeconds(1.2f);
        DisableAllCastUI();

        if (player.characterStats.health <= 0)
        {
            losingScreen.SetActive(true);
            losingScreen.transform.SetAsLastSibling();

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("MainScene");
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

    private void SpawnSpellVFX(MoveTemplate move, bool isPlayer)
    {
        GameObject prefab = SpellVFXDatabase.Instance.GetVFX(move.type, isPlayer);
        if (!prefab) return;

        Transform spawnPoint = isPlayer ? playerVFXPoint : enemyVFXPoint;

        Quaternion rotation = isPlayer
            ? Quaternion.Euler(0, 0, 0)       // shoot right
            : Quaternion.Euler(0, 180, 0);    // shoot left

        GameObject vfx = Instantiate(prefab, spawnPoint.position, rotation);

        Destroy(vfx, 3f);

    }




}
