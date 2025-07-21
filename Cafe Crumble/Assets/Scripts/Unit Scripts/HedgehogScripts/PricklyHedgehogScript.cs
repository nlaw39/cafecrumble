using UnityEngine;

public class PricklyHedgehogScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 6;
        baseAttackDamage = 0;

        healthGrowth = 1;
        attackGrowth = 0;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/PricklyHedgehogPassive");
        AddPassive(Instantiate(passive));

        base.Start();
    }
}
