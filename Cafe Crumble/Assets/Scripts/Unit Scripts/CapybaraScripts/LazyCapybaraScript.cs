using UnityEngine;

public class LazyCapybaraScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 8;
        baseAttackDamage = 1;

        healthGrowth = 2;
        attackGrowth = 0;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/LazyCapybaraPassive");
        AddPassive(Instantiate(passive));

        base.Start();
    }
}
