using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public AudioClip impactSound; // الصوت الخاص بالاصطدام
    private AudioSource audioSource; // مكون الصوت

    void Start()
    {
        // إضافة مكون AudioSource إذا لم يكن موجوداً
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // تشغيل الصوت عند الاصطدام
        if (impactSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(impactSound);
        }

        // التحقق إذا كانت الكابسولة اصطدمت بالصبار
        if (collision.collider.CompareTag("Bad"))
        {
            // تدمير الكائن الصبار
            Destroy(collision.collider.gameObject);

            // تدمير الطلقة بعد الاصطدام
            Destroy(gameObject);

            Debug.Log("Cactus destroyed!"); // تأكيد الاصطدام بالصبار
        }
        else
        {
            Debug.Log("Hit: " + collision.collider.name); // تأكيد الاصطدام بكائن آخر
        }
    }
}