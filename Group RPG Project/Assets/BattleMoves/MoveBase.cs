using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Moves/Create New Move")]
public class MoveBase : ScriptableObject
{
    public string moveName;
    public int damage;
    public int accuracy;
    public int maxPP;

    public enum MoveType { Physical, Arcane, Elemental, EnhancedPhysical, Summon, Illusory, Support }
    public MoveType type;

    [TextArea]
    public string description;
}
