using UnityEngine;
using TMPro;

public class SacrificialCapybaraScript : BaseUnitScript
{
    // This will be assigned in Awake
    public override UnitData GetUnitData()
    { 
        UnitData data = ScriptableObject.CreateInstance<UnitData>();
        data.unitName = "Cappie-bara";
        data.unitDesc = "";
        data.baseHealthPoints = 4;
        data.baseAttackDamage = 1;
        data.healthGrowth = 1;
        data.attackGrowth = 0;
        data.unitCost = 2;
        data.unitSprite = sprite;
        data.unitPrefab = prefab;
        return data;
    }

    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject prefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        UnitData data = GetUnitData();

        unitName = data.unitName;
        baseHealthPoints = data.baseHealthPoints;
        baseAttackDamage = data.baseAttackDamage;
        healthGrowth = data.healthGrowth;
        attackGrowth = data.attackGrowth;
        unitCost = data.unitCost;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/SacrificialCapybaraPassive");
        AddPassive(Instantiate(passive));

        Transform child = transform.Find("Canvas/HealthText");
        if (child != null)
        {
            TMP_Text tmp = child.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                healthText = tmp;
            }
        }

        Transform child2 = transform.Find("Canvas/AttackText");
        if (child2 != null)
        {
            TMP_Text tmp2 = child2.GetComponent<TextMeshProUGUI>();
            if (tmp2 != null)
            {
                attackText = tmp2;
            }
        }

        base.Start();
    }
}
