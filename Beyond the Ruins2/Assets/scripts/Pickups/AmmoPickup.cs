using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public AudioClip pickUpSound; // الصوت الذي سيتم تشغيله عند التقاط العنصر
    private AudioSource audioSource; // مكون الصوت لتشغيل الصوت

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // الحصول على مكون الصوت
    }

    // دالة للقبض على العناصر باستخدام OnTriggerEnter
    void OnTriggerEnter(Collider other)
    {
        // تحقق مما إذا كان الكائن الذي التقطه هو كائن قابل للتجميع
        if (other.CompareTag("PickUp"))
        {
            // تشغيل الصوت عند التقاط العنصر
            if (audioSource != null && pickUpSound != null)
            {
                audioSource.PlayOneShot(pickUpSound); // تشغيل الصوت عند التقاط العنصر
            }

            // تدمير الكائن الذي تم التقاطه
            Destroy(other.gameObject);
        }
    }
}
