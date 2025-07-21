using UnityEngine;

public class BasicCatScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 3;
        baseAttackDamage = 2;

        healthGrowth = 1;
        attackGrowth = 1;

        unitCost = 1;

        base.Start();
    }
}
