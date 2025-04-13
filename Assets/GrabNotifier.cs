using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabNotifier : MonoBehaviour
{
    public System.Action<bool> onGrabChanged;

    private void OnSelectEnter(XRBaseInteractor interactor)
    {
        onGrabChanged?.Invoke(true);
    }

    private void OnSelectExit(XRBaseInteractor interactor)
    {
        onGrabChanged?.Invoke(false);
    }
}

public class PowerObject : MonoBehaviour
{
    private void Awake()
    {
        GrabNotifier grab = GetComponent<GrabNotifier>();
        PowerController controller = FindObjectOfType<PowerController>();
        controller.RegisterObject(grab);
    }
}
