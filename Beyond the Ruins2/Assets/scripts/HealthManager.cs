using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Slider healthSlider; // السلايدر لعرض الصحة
    public float healthAmount = 100f; // الصحة الأولية

    // Start is called before the first frame update
    void Start()
    {
        // تحديث السلايدر عند بدء اللعبة
        healthSlider.maxValue = 100f;
        healthSlider.value = healthAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            // تحميل المشهد الحالي بعد موت اللاعب
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // تطبيق الضرر عند الضغط على Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(50);
        }

        // شفاء عند الضغط على Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(5);
        }
    }

    // دالة لتطبيق الضرر
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // تأكد أن الصحة لا تصبح أقل من 0
        healthSlider.value = healthAmount; // تحديث السلايدر ليعكس الصحة
    }

    // دالة للشفاء
    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // تأكد أن الصحة لا تتجاوز 100
        healthSlider.value = healthAmount; // تحديث السلايدر ليعكس الصحة
    }

    // دالة يتم استدعاؤها عند التصادم مع رصاصة
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // تحقق من إذا كان الجسم المتصادم هو "رصاصة"
        {
            TakeDamage(20); // تطبيق ضرر عند التصادم
        }
    }

    // إذا كان الـ Bullet يحتوي على Collider مع Trigger، يمكن استخدام OnTriggerEnter بدلاً من OnCollisionEnter
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Bullet"))
    //     {
    //         TakeDamage(20);
    //     }
    // }
}
