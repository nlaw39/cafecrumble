using UnityEngine;

public class BrainwashHedgehogScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 4;
        baseAttackDamage = 2;

        healthGrowth = 1;
        attackGrowth = 0;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/BrainwashHedgehogPassive");
        AddPassive(Instantiate(passive));

        base.Start();
    }
}
