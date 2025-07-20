using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(fileName = "SacrificialCapybaraPassive", menuName = "Scriptable Objects/SacrificialCapybaraPassive")]
public class SacrificialCapybaraPassive : PassiveAbility
{
    public override void OnDeath(BaseUnitScript self)
    {
        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/SacrificialBuff");

        BaseUnitController myTeamController = self.transform.parent.GetComponent<BaseUnitController>();
        List<GameObject> myTeamUnits = myTeamController.unitList;
        if (myTeamUnits.Count > 1)
        {
            BaseUnitScript inheritorScript = myTeamUnits[1].GetComponent<BaseUnitScript>();

            if (passive != null)
            {
                inheritorScript.AddPassive(Instantiate(passive));
                UnityEngine.Debug.Log("A blessing was set up for the next unit in line.");
            }
            else
            {
                UnityEngine.Debug.LogWarning("Passive not found!");
            }
        }
    }
}
