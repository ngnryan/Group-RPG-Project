using UnityEngine;

public class SpellVFXDatabase : MonoBehaviour
{
    [Header("Player Spell Effects")]
    public GameObject fireVFX;
    public GameObject waterVFX;
    public GameObject airVFX;
    public GameObject earthVFX;

    [Header("Enemy Spell Effects")]
    public GameObject enemyFireVFX;
    public GameObject enemyWaterVFX;
    public GameObject enemyAirVFX;
    public GameObject enemyEarthVFX;

    public static SpellVFXDatabase Instance;

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetVFX(MoveTemplate.MoveTypes type, bool isPlayer)
    {
        switch (type)
        {
            case MoveTemplate.MoveTypes.Fire:  return isPlayer ? fireVFX : enemyFireVFX;
            case MoveTemplate.MoveTypes.Water: return isPlayer ? waterVFX : enemyWaterVFX;
            case MoveTemplate.MoveTypes.Air:   return isPlayer ? airVFX  : enemyAirVFX;
            case MoveTemplate.MoveTypes.Earth: return isPlayer ? earthVFX : enemyEarthVFX;
        }
        return null;
    }
}
