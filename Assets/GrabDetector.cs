using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabDetector : MonoBehaviour
{
    public static bool IsHeld = false;

    private void OnSelectEnter(XRBaseInteractor interactor)
    {
        IsHeld = true;
        Debug.Log("Objet s");
    }

    private void OnSelectExit(XRBaseInteractor interactor)
    {
        IsHeld = false;
        Debug.Log("Objet r");
    }
}
