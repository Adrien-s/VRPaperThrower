using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PowerController : MonoBehaviour
{
    [Header("UI")]
    public GameObject powerGaugeUI;       // Le canvas world space
    public Image powerFillImage;          // L'image circulaire (Filled Radial)

    [Header("Entrée utilisateur")]
    public XRController inputSource;      // Main libre

    [Header("Puissance")]
    public float maxPower = 1f;
    public float chargeSpeed = 1f;

    [Header("Trajectoire")]
    public TrajectoryPredictor predictor; // Script qui affiche la trajectoire

    // --- Variables internes ---
    private float currentPower = 0f;
    private bool isCharging = false;
    private bool powerLocked = false;
    private bool isHoldingObject = false;

    private GrabNotifier currentGrabbed;

    void Update()
    {

        Debug.Log("La scène tourne !");


        if (!isHoldingObject)
        {
            powerGaugeUI.SetActive(false);
            currentPower = 0;
            powerFillImage.fillAmount = 0;
            powerLocked = false;
            return;
        }

        powerGaugeUI.SetActive(true);

        inputSource.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        Debug.Log("Trigger pressed ? " + triggerPressed);


        inputSource.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed);
        Debug.Log("Grip pressed ? " + gripPressed);



        if (triggerPressed && !powerLocked)
        {
            isCharging = true;
            currentPower += Time.deltaTime * chargeSpeed;
            currentPower = Mathf.Clamp(currentPower, 0f, maxPower);
            powerFillImage.fillAmount = currentPower / maxPower;
        }
        else if (isCharging)
        {
            isCharging = false;
            powerLocked = true;
            ShowTrajectory(currentPower);
        }
    }

    void ShowTrajectory(float power)
    {
        if (predictor != null)
        {
            predictor.DrawTrajectory(power);
        }
        else
        {
            Debug.LogWarning("TrajectoryPredictor n'est pas assigné !");
        }
    }

    // --- Méthode appelée par les objets grabables ---
    public void RegisterObject(GrabNotifier notifier)
    {
        if (currentGrabbed != null)
        {
            currentGrabbed.onGrabChanged -= OnGrabChanged;
        }

        currentGrabbed = notifier;
        currentGrabbed.onGrabChanged += OnGrabChanged;
    }

    private void OnGrabChanged(bool isHeld)
    {
        isHoldingObject = isHeld;

        if (!isHeld)
        {
            powerGaugeUI.SetActive(false);
            currentPower = 0;
            powerFillImage.fillAmount = 0;
            powerLocked = false;
        }
    }
}
