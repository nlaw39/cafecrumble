using System.Diagnostics;
using UnityEngine;

public class FactionManager : MonoBehaviour
{
    public static FactionManager Instance { get; private set; }

    public Faction SelectedFaction { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetFaction(Faction faction)
    {
        SelectedFaction = faction;
        UnityEngine.Debug.Log("Faction selected: " + faction);
    }
}
