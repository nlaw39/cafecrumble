using UnityEngine;

public class TricksterCatScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 3;
        baseAttackDamage = 2;

        healthGrowth = 2;
        attackGrowth = 1;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/TricksterCatSwap");
        AddPassive(Instantiate(passive));

        base.Start();
    }
}
