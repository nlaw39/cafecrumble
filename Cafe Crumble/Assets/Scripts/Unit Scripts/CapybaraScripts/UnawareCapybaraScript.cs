using UnityEngine;

public class UnawareCapybaraScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 6;
        baseAttackDamage = 1;

        healthGrowth = 1;
        attackGrowth = 1;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/UnawareCapybaraPassive");
        AddPassive(Instantiate(passive));
        base.Start();
    }
}
