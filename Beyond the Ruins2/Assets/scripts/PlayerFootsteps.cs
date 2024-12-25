using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private AudioClip footstepSound; // الصوت الذي سيتم تشغيله عند كل خطوة
    [SerializeField] private float stepInterval = 0.5f; // الفاصل الزمني بين كل خطوة والأخرى
    private AudioSource audioSource; // مكون AudioSource لتشغيل الصوت
    private float stepTimer = 0f; // مؤقت لضبط توقيت الخطوات
    private CharacterController characterController; // للتحقق من حركة اللاعب

    void Start()
    {
        // الحصول على مكونات AudioSource و CharacterController
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // التحقق إذا كان اللاعب يتحرك
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            // تحديث مؤقت الخطوة
            stepTimer += Time.deltaTime;

            // إذا مر الوقت الكافي بين الخطوات
            if (stepTimer >= stepInterval)
            {
                // تشغيل الصوت
                PlayFootstepSound();

                // إعادة تعيين مؤقت الخطوة
                stepTimer = 0f;
            }
        }
    }

    // تشغيل صوت الخطوة
    void PlayFootstepSound()
    {
        if (footstepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
}
