using UnityEngine;
using UnityEngine.SceneManagement; // لإدارة المشاهد
using UnityEngine.UI; // لإضافة دعم للسلايدر

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // الصحة الأولية للاعب
    public float healthThreshold = 20f; // العتبة التي عندها يتم الانتقال إلى سيناريو "اللوز"
    public string nextSceneName = "Loss"; // اسم المشهد الذي سيتم الانتقال إليه عند انخفاض الصحة
    public Slider healthSlider; // السلايدر لعرض الصحة
    public float lerpSpeed = 2f; // سرعة الانتقال التدريجي للسلايدر

    private float targetHealth; // الهدف المستقبلي للصحة لتحديث السلايدر
    Rigidbody PlayerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // تحديث السلايدر عند بدء اللعبة
       healthSlider.value = health;
       PlayerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { if (healthSlider != null)
        {
            healthSlider.maxValue = 100f;
            healthSlider.value = health;
            targetHealth = health; // تعيين القيمة الابتدائية
        }
        // تحديث السلايدر تدريجياً نحو الصحة المستهدفة
        if (healthSlider != null)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, Time.deltaTime * lerpSpeed);
        }
    }

    // دالة لتطبيق الضرر
    public void TakeDamage(float damage)
    {
        health -= damage;
        targetHealth = Mathf.Clamp(health, 0, 100); // تأكد أن الصحة بين 0 و 100
        Debug.Log("Player Health: " + health);

        // إذا كانت الصحة أقل من أو تساوي العتبة المحددة، يتم الانتقال إلى سيناريو "اللوز"
        if (health <= healthThreshold)
        {
            GoToNextScene();
        }

        // إذا كانت الصحة صفر أو أقل، يمكن تنفيذ منطق الموت هنا
        if (health <= 0)
        {
            Die();
        }
    }

    // دالة للموت (يمكنك تخصيصها حسب الحاجة)
    private void Die()
    {
        Debug.Log("Player has died!");
        // يمكنك هنا تنفيذ منطق الموت مثل تفعيل شاشة النهاية أو إعادة تحميل المشهد
    }

    // دالة للانتقال إلى المشهد التالي
    private void GoToNextScene()
    {
        // الانتقال إلى المشهد التالي الذي تم تحديده في `nextSceneName`
        SceneManager.LoadScene(nextSceneName);
    }
}
