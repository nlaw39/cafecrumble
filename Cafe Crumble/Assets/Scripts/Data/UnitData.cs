using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitData", menuName = "Scriptable Objects/Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public string unitDesc;
    public Sprite unitSprite;

    public int baseHealthPoints;
    public int baseAttackDamage;

    public int healthGrowth;
    public int attackGrowth;

    public int unitCost;

    public GameObject unitPrefab;
}
