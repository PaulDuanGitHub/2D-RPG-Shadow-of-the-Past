using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Bar;
    private float amount = 1.0f;
    // Update is called once per frame
    private void Awake()
    {
        Bar = GetComponent<Image>();
    }

    private void Update()
    {
        Bar.fillAmount = this.amount;
    }

    void BarFiller(float amount)
    {
        this.amount = amount;
        Debug.Log(this.amount);
    }
}
