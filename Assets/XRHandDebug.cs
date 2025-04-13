using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHandDebug : MonoBehaviour
{
    public InputDeviceCharacteristics handType = InputDeviceCharacteristics.Right; // Choisis la main ici

    private InputDevice device;
    private bool deviceFound = false;

    void Start()
    {
        TryInitializeDevice();
    }

    void TryInitializeDevice()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(handType | InputDeviceCharacteristics.Controller, inputDevices);

        if (inputDevices.Count > 0)
        {
            device = inputDevices[0];
            deviceFound = true;
            Debug.Log($"Contrôleur détecté : {device.name}");
        }
        else
        {
            Debug.LogWarning("Aucun contrôleur trouvé pour cette main.");
        }
    }

    void Update()
    {
        if (!deviceFound)
        {
            TryInitializeDevice(); // Réessayer jusqu’à ce que le contrôleur soit actif
            return;
        }

        // Boutons numériques
        device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);
        device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryPressed);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryPressed);
        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool joystickPressed);

        // Valeurs analogiques
        device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        device.TryGetFeatureValue(CommonUsages.grip, out float gripValue);

        // Log
        Debug.Log(
            $"[XR DEBUG - {handType}]\n" +
            $"- Trigger Button: {triggerPressed} ({triggerValue:0.00})\n" +
            $"- Grip Button: {gripPressed} ({gripValue:0.00})\n" +
            $"- A/X: {primaryPressed}, B/Y: {secondaryPressed}, Joystick Click: {joystickPressed}"
        );
    }
}
