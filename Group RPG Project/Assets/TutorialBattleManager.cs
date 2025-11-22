using UnityEngine; //REFRAME LOGIC TO FIT CAT WINNING

public class BattleManager : MonoBehaviour
{
    public PlayerBattleController playerBattleController;
    public EnemyBattleController enemyBattleController;

    private bool playerTurn = true;
    private void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        playerTurn = true;
        Debug.Log("Player's turn!");
    }

    public void PlayerAttack(int damage)
    {
        enemy.TakeDamage(damage);

        if (enemy.stats.currentHP <=0)
        {
            EnemyDefeated();
        }
        else
        {
            StartEnemyTurn();
        }
    }

    public void StartEnemyTurn()
    {
        playerTurn = false;
        Debug.Log("Enemy's turrn!");

        Invoke(nameof(EnemyAttack), 1f);
    }

    private void EnemyAttackAction()
    {
        int damage = enemy.stats.attackDamage;
        player.TakeDamage(damage);

        if (player.stats.currenthp <= 0)
        {
            PlayerDefeated();
        }
        else
        {
            StartPlayerTurn();
        }
    }

    private void EnemyDefeated()
    {
        Debug.Log("Enemy was defeated!"); //drop loot (UI), exit battle
    }
    private void PlayerDefeated()
    {
        Debug.Log("Player lost!");
    }

    public void PlayerUseMove(MoveBase move)
{
    Debug.Log("Resolving move: " + move.moveName);

    // Accuracy check
    int roll = Random.Range(1, 101);
    if (roll > move.accuracy)
    {
        Debug.Log("The attack missed!");
        StartEnemyTurn();
        return;
    }

    // Calculate damage
    int damage = move.damage;

    enemy.TakeDamage(damage);

    if (enemy.GetStats().currentHP > 0)
        StartEnemyTurn();
    else
        OnEnemyFainted();
}



    // Update is called once per frame
    void Update()
    {

    }
}
