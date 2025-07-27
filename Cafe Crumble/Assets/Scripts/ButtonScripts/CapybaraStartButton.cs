using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Image), typeof(Button))]
public class CapybaraStartButton : MonoBehaviour
{
    public Faction playerFaction;

    void Awake()
    {
        UnityEngine.UI.Image image = GetComponent<UnityEngine.UI.Image>();
        //image.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void OnClickFaction()
    {
        FactionManager.Instance.SetFaction(playerFaction);
        SceneManager.LoadScene("CafeScene");
    }
}
