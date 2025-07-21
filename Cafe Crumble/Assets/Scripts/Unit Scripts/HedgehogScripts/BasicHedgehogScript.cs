using UnityEngine;

public class BasicHedgehogScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 5;
        baseAttackDamage = 1;

        healthGrowth = 1;
        attackGrowth = 0;

        unitCost = 1;

        base.Start();
    }
}
