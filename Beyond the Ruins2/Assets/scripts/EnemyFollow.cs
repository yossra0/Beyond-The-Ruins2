using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;  // العدو الذي يستخدم الـ NavMeshAgent
    public Transform player;    // اللاعب الذي سيتبعه العدو
    [SerializeField] private float time = 5f; // الوقت بين كل إطلاق رصاصة من النوع الأول
    [SerializeField] private float timeForSecondBullet = 3f; // الوقت بين كل إطلاق رصاصة من النوع الثاني
    private float bulletTime;   // الوقت الحالي بين كل إطلاق رصاصة من النوع الأول
    private float secondBulletTime; // الوقت الحالي بين كل إطلاق رصاصة من النوع الثاني
    public GameObject enemyBullet; // رصاصة العدو من النوع الأول
    public GameObject secondEnemyBullet; // رصاصة العدو من النوع الثاني
    public Transform spawnPoint; // نقطة إطلاق الرصاصة الأولى
    public Transform spawnPoint2; // نقطة إطلاق الرصاصة الثانية
    public float enemySpeed = 10f; // سرعة رصاصة العدو
    public AudioClip shootSound; // الصوت الذي سيتم تشغيله عند إطلاق الرصاصة
    private AudioSource audioSource; // مكون الصوت لتشغيل الصوت

    public float shootRange = 10f; // المسافة التي يجب أن يقترب منها اللاعب ليبدأ العدو بإطلاق النار

    void Start()
    {
        bulletTime = time; // تعيين الوقت بين الإطلاق للرصاصة الأولى
        secondBulletTime = timeForSecondBullet; // تعيين الوقت بين الإطلاق للرصاصة الثانية
        audioSource = GetComponent<AudioSource>(); // الحصول على مكون الصوت
    }

    void Update()
    {
        // العدو يتبع اللاعب
        enemy.SetDestination(player.position);

        // إذا كانت المسافة بين العدو واللاعب أقل من المسافة المحددة
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.position);

        if (distanceToPlayer <= shootRange)
        {
            // إطلاق النار على اللاعب إذا كانت المسافة قريبة
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        // تقليل الوقت بين الإطلاق للرصاصة الأولى
        bulletTime -= Time.deltaTime;

        // تقليل الوقت بين الإطلاق للرصاصة الثانية
        secondBulletTime -= Time.deltaTime;

        // إذا مر الوقت الكافي، يتم إطلاق الرصاصة الأولى
        if (bulletTime <= 0)
        {
            bulletTime = time;  // إعادة تعيين وقت الإطلاق للرصاصة الأولى

            // إنشاء الرصاصة الأولى
            GameObject bulletobj = Instantiate(enemyBullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
            Rigidbody bulletRig = bulletobj.GetComponent<Rigidbody>();

            // إضافة قوة للرصاصة لتتحرك
            bulletRig.AddForce(bulletRig.transform.forward * enemySpeed, ForceMode.Impulse);

            // تدمير الرصاصة بعد 5 ثوانٍ
            Destroy(bulletobj, 5f);

            // تشغيل صوت إطلاق الرصاصة
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound); // تشغيل الصوت
            }
        }

        // إذا مر الوقت الكافي، يتم إطلاق الرصاصة الثانية
        if (secondBulletTime <= 0)
        {
            secondBulletTime = timeForSecondBullet;  // إعادة تعيين وقت الإطلاق للرصاصة الثانية

            // إنشاء الرصاصة الثانية من النقطة الثانية
            GameObject secondBulletObj = Instantiate(secondEnemyBullet, spawnPoint2.position, spawnPoint2.rotation) as GameObject;
            Rigidbody secondBulletRig = secondBulletObj.GetComponent<Rigidbody>();

            // إضافة قوة للرصاصة لتتحرك
            secondBulletRig.AddForce(secondBulletRig.transform.forward * enemySpeed, ForceMode.Impulse);

            // تدمير الرصاصة بعد 5 ثوانٍ
            Destroy(secondBulletObj, 5f);

            // تشغيل صوت إطلاق الرصاصة
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound); // تشغيل الصوت
            }
        }
    }
}
