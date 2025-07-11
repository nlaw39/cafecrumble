using UnityEngine;

public class BaseUnitScript : MonoBehaviour
{
    public int baseHealthPoints = 5;
    public int baseAttackDamage = 2;

    public int currentHealthPoints;
    public int currentAttackDamage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealthPoints = baseHealthPoints;
        currentAttackDamage = baseAttackDamage;
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
