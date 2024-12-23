using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReduceGaugebar : MonoBehaviour
{
    public GameObject Player;
    private RectTransform GaugeBar;


    public float maxShakeAmount = 0.1f;
    public float shakeIntensityMultiplier = 1.0f;
    Slider slider;

    private Vector2 initialPosition;
    public float MaxPlatformTime = 5f;
    public float platformTime = 5f;
    private bool shouldReduceGague = false;
    public float replinishmentAmount = 0.2f;
    public float replenishmentSpeed = 1.5f;

    public bool isClone;
    public bool isPow;

    Platform plat1;
    Platform plat2;

    void Start()
    {
        plat1 = GameObject.Find("2nd floor").GetComponent<Platform>();
        plat2 = GameObject.Find("3rd floor").GetComponent<Platform>();
        GaugeBar = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();

        initialPosition = GaugeBar.anchoredPosition;

        isClone = false;
    }

    void Update()
    {
        //Debug.Log("shouldReduceGague: " + shouldReduceGague);
        //Debug.Log("PlatformTime: " + platformTime);
        slider.value = platformTime / MaxPlatformTime;
        if (isClone)
        {
            platformTime = MaxPlatformTime;
        }
        else if (shouldReduceGague)
        {
            platformTime -= Time.deltaTime;
        }
        else
        {
            if (platformTime < MaxPlatformTime)
            {

                platformTime += Time.deltaTime * replenishmentSpeed;
            }
        }


        if (slider.value < 0.5f)
        {
            Shake();
        }
        else
        {
            GaugeBar.anchoredPosition = initialPosition;
        }

        if (platformTime <= 0)
        {
            DisablePlatforms();
            if(isPow){
                PlayerDamageUpEffect PD =FindObjectOfType<PlayerDamageUpEffect>();
                PD.move();
            }
            
            
        }
        else
        {
            EnablePlatforms();
        }

    }

    public void DisablePlatforms()
    {
        plat1.DisablePlatform();
        plat2.DisablePlatform();
    }

    public void EnablePlatforms()
    {
        plat1.EnablePlatform();
        plat2.EnablePlatform();
    }

    public void StartStaminaDecrease()
    {
        shouldReduceGague = true;
    }

    public void StopStaminaDecrease()
    {
        shouldReduceGague = false;
    }

    public void IncreaseGaugeBySmallAmount()
    {
        platformTime += MaxPlatformTime * replinishmentAmount;
        if (platformTime > MaxPlatformTime)
        {
            platformTime = MaxPlatformTime;
        }
    }

    void Shake()
    {

        float shakeAmount = (1 - slider.value) * shakeIntensityMultiplier * maxShakeAmount;

        // Apply random offset for shake effect
        float offsetX = Random.Range(-shakeAmount, shakeAmount);
        float offsetY = Random.Range(-shakeAmount, shakeAmount);

        GaugeBar.anchoredPosition = initialPosition + new Vector2(offsetX, offsetY);
    }
}
