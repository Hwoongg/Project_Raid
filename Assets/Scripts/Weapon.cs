using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] int damagePerShot = 1;
    [SerializeField] float timeBetweenBullets = 0.1f;
    [SerializeField] float range = 100f;
    [SerializeField] int MaxBullet = 30;
    int CurrentBullet;

    float timer;
    Ray ShootRay;
    RaycastHit ShootHit;
    Vector3 ScreenCenter;
    int shootableMask;

    float effectDisplayTime = 0.2f;

    [SerializeField] Text aimText;

    bool isReloading;
    float Reloadtime = 1.5f;
    Animator animator;

    [SerializeField] GameObject[] objFireEfx;

    AudioSource GunSound;


    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Enemy");
        isReloading = false;
        GunSound = GetComponent<AudioSource>();
        CurrentBullet = MaxBullet;
    }

    void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        //animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
        }

        if (!isReloading)
        {
            if (Input.GetMouseButton(0) && (Time.timeScale != 0))
            {
                animator.SetBool("onFire", true);
                if (timer >= timeBetweenBullets)
                {
                    Fire();
                }
            }
            else
            {
                animator.SetBool("onFire", false);
            }

            if (timer >= timeBetweenBullets * effectDisplayTime)
            {
                DisableEffects();
            }
        }
        else
        {
            Reload();
        }

    }


    void Fire()
    {
        // 타이머 초기화
        timer = 0f;

        CurrentBullet -= 1;

        ShootRay = Camera.main.ScreenPointToRay(ScreenCenter);

        // 각종 이펙트 재생
        GunSound.Play();
        //GunParticle.Stop(); // 파티클은 멈추고 시작해주는 작업 필요.
        //GunParticle.Play();
        for (int i = 0; i < objFireEfx.Length; i++)
        {
            objFireEfx[i].SetActive(true);
        }
        LogicEventListener.Invoke(eEventType.FOR_UI, eEventMessage.ON_AMMUNITION_COUNT_CHANGED, (object)CurrentBullet, (object)MaxBullet);

        // RayCast
        if (Physics.Raycast(ShootRay, out ShootHit, range, shootableMask))
        {
            var objHealth = ShootHit.collider.gameObject.GetComponent<Health>();
            aimText.text = "Hit";
            objHealth.TakeDamage(1);
        }
        //else
        //{
        //    aimText.text = "+";
        //}
    }

    void Reload()
    {
        CurrentBullet = MaxBullet;
        StartCoroutine("ReloadingAnimationPlay");
    }

    IEnumerator ReloadingAnimationPlay()
    {
        animator.SetBool("isReloading", true);
        float animatorTime = 0f;

        while (animatorTime < Reloadtime)
        {
            animatorTime += Time.deltaTime;
            animator.SetFloat("ReloadTime", animatorTime);
            yield return null;
        }

        animator.SetBool("isReloading", false);
        animator.SetFloat("ReloadTime", 0f);
        isReloading = false;
        yield break;
    }

    // Enable 방식으로 작동하는 이펙트의 경우 이곳에서 해제합니다.
    void DisableEffects()
    {
        for (int i = 0; i < objFireEfx.Length; i++)
        {
            objFireEfx[i].SetActive(false);
        }
    }


}
