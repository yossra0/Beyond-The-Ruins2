using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
public Slider slider;
HealthSystem healthSystem = new HealthSystem(100);

public void Update()
{
    slider.value = healthSystem.gethealthprecentage();
}


public void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag =="Health")
    {healthSystem.heal(10); Debug.Log("heal-10");}
     if (other.gameObject.tag =="Damage")
    {healthSystem.Damage(10); }
}
}