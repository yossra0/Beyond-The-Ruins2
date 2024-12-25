using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash; // التأثير البصري عند الإطلاق
    [SerializeField] LayerMask interactionLayers; // الطبقات التي يمكن التفاعل معها
    [SerializeField] AudioClip shootSound; // الصوت الذي سيتم تشغيله عند الإطلاق
    private AudioSource audioSource; // مرجع إلى مكون AudioSource لتشغيل الصوت

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        // الحصول على مكونات CinemachineImpulseSource و AudioSource
        impulseSource = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>(); // تأكد من أن الكائن يحتوي على مكون AudioSource
    }

    public void Shoot(WeaponSO weaponSO)
    {
        // تشغيل التأثير البصري (muzzle flash)
        muzzleFlash.Play();

        // توليد تأثير الاهتزاز (impulse)
        impulseSource.GenerateImpulse();

        // تشغيل الصوت عند الإطلاق
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // تنفيذ التصويب وإطلاق الرصاص
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            // إنشاء تأثير الاصطدام عند النقطة التي تم التصويب عليها
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);

            // تطبيق الضرر على العدو إذا تم التصويب عليه
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
