using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX; // تأثير الانفجار عند تدمير العدو
    [SerializeField] int startingHealth = 3; // الصحة الأولية للعدو
    [SerializeField] AudioClip deathSound; // الصوت الذي سيتم تشغيله عند تدمير العدو
    private AudioSource audioSource; // مكون الصوت لتشغيل الصوت

    int currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
        audioSource = GetComponent<AudioSource>(); // الحصول على مكون الصوت
    }

    void Start()
    {
        // يمكن إضافة أي منطق آخر عند بداية اللعبة هنا
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        // تشغيل الصوت عند تدمير العدو
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound); // تشغيل الصوت
        }

        // إنشاء تأثير الانفجار
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);

        // تدمير العدو
        Destroy(this.gameObject);
    }
}
