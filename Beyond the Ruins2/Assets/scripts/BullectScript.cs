using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 0.1f; // الضرر الذي ستسببه الرصاصة

    // دالة لتحديد ما إذا كانت الرصاصة تصطدم بشيء
    private void OnTriggerEnter(Collider other)
    {
        // تحقق إذا كانت الرصاصة تصطدم باللاعب
        if (other.CompareTag("Player"))
        {
            // الحصول على السكربت الخاص بصحة اللاعب
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                // تطبيق الضرر
                playerHealth.TakeDamage(damage);
            }

            // تدمير الرصاصة بعد الاصطدام
            Destroy(gameObject);
        }
    }
}
