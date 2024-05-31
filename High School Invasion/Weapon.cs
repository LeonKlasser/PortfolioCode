using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance { get; private set; }

    public Gun gun;

    [SerializeField] private Transform firePoint;

    [SerializeField] private Image normalslider;
    [SerializeField] private Image RPGSLider;
    [SerializeField] private Image shotgunSlider;

    [SerializeField] private Image gunImage;

    [SerializeField] public Sprite[] bullets;
    [SerializeField] public Sprite[] reloadSprites;
    [SerializeField] private float timeBetweenSprites;

    [SerializeField] private TextMeshProUGUI maxAmmo;
    [SerializeField] private GameObject devider;

    [SerializeField] private PlayerController playerController;

    private Vector2 imageSize;

    private bool sliderIsFinished;
    private bool UIAnimationIsFinished;

    private void Awake()
    {
        gun = GameObject.Find("Player").GetComponent<PlayerController>().gun;
    }

    public void Start()
    {
        gun.firePoint = firePoint;
        GetComponent<SpriteRenderer>().sprite = gun.sprite;

        RPGSLider.gameObject.SetActive(false);
        normalslider.gameObject.SetActive(false);
        shotgunSlider.gameObject.SetActive(false);

        gun.magazine = gun.magazineSize;

        bullets = gun.bullets;
        reloadSprites = gun.reloadSprites;

        gun.weaponScript = this;
        gun.isReloading = false;

        normalslider.fillAmount = 0;
        RPGSLider.fillAmount = 0;
        shotgunSlider.fillAmount = 0;
        gun.bulletsShot = 0;

        gunImage.sprite = gun.sprite;
        gunImage.sprite = bullets[0];

        if (gun.gunType == GunType.RPG)
        {
            //Get the size of the image and change to the size of the Image
            RectTransform rectTransform = gunImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(gun.bullets[0].bounds.size.x * 40, gun.bullets[0].bounds.size.y * 40);
            gunImage.rectTransform.sizeDelta = rectTransform.sizeDelta;
        }

        if (gun.gunType == GunType.ShotGun)
        {
            RectTransform rectTransform = gunImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(gun.bullets[0].bounds.size.x * 200, gun.bullets[0].bounds.size.y * 200);
            imageSize = rectTransform.sizeDelta;
            gunImage.rectTransform.sizeDelta = imageSize;
        }

        gun.playerController = playerController;

    }

    private void Update()
    {
        if (gun.magazine <= 0 && !gun.isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public void UpdateUI()
    {
        if (gun.gunType != GunType.RPG)
        {
            gunImage.sprite = bullets[gun.bulletsShot];
            if (gun.gunType == GunType.ShotGun)
            {
                gunImage.sprite = bullets[gun.bulletsShot];
                gunImage.rectTransform.sizeDelta = imageSize;
            }
        }
        else
        {
            gunImage.sprite = bullets[0];
        }

        if (gun.gunType == GunType.RPG)
        {
            //Get the size of the image and change to the size of the Image
            RectTransform rectTransform = gunImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(gun.bullets[0].bounds.size.x * 200, gun.bullets[0].bounds.size.y * 200);
            gunImage.rectTransform.sizeDelta = rectTransform.sizeDelta;
        }
    }

    public IEnumerator Reload()
    {
        //AudioManager.instance.Play("Reload");
        gun.isReloading = true;

        float sliderValue = 0;
        if (gun.gunType == GunType.NormalGun)
        {
            normalslider.gameObject.SetActive(true);
            RPGSLider.gameObject.SetActive(false);
            shotgunSlider.gameObject.SetActive(false);
            normalslider.fillAmount = sliderValue;
        }
        else if (gun.gunType == GunType.RPG)
        {
            normalslider.gameObject.SetActive(false);
            RPGSLider.gameObject.SetActive(true);
            shotgunSlider.gameObject.SetActive(false);
            RPGSLider.fillAmount = sliderValue;
        }
        else if (gun.gunType == GunType.ShotGun)
        {
            shotgunSlider.gameObject.SetActive(true);
            normalslider.gameObject.SetActive(false);
            RPGSLider.gameObject.SetActive(false);
            shotgunSlider.fillAmount = sliderValue;
        }

        //slider.maxValue = gun.reloadTime;
        int bulletsShot = 0;

        gun.bulletsShot = 0;

        if (gun.magazine != gun.magazineSize)
        {
            bulletsShot = gun.magazineSize - gun.magazine;
        }


        if (gun.gunType == GunType.NormalGun)
        {
            ReloadAnimationAndSlider(sliderValue, normalslider);
        }
        else if (gun.gunType == GunType.RPG)
        {
            ReloadAnimationAndSlider(sliderValue, RPGSLider);
        }
        else if (gun.gunType == GunType.ShotGun)
        {
            ReloadAnimationAndSlider(sliderValue, shotgunSlider);
        }

        yield return new WaitForSeconds(gun.reloadTime);

        StartCoroutine(ReloadReset(bulletsShot));
    }

    private IEnumerator ReloadReset(int bulletsShot)
    {
        gun.magazine = gun.magazineSize;
        if (gun.gunType == GunType.ShotGun)
        {
            gunImage.sprite = bullets[0];
            gunImage.rectTransform.sizeDelta = imageSize;
        }
        else
        {
            gunImage.sprite = bullets[0];
        }

        if (gun.gunType == GunType.NormalGun)
        {
            normalslider.fillAmount = 0;
            normalslider.gameObject.SetActive(false);
        }
        else if (gun.gunType == GunType.RPG)
        {
            RPGSLider.fillAmount = 0;
            RPGSLider.gameObject.SetActive(false);
        }
        else if (gun.gunType == GunType.ShotGun)
        {
            shotgunSlider.fillAmount = 0;
            shotgunSlider.gameObject.SetActive(false);
        }

        RPGSLider.gameObject.SetActive(false);
        gun.isReloading = false;
        yield return null;
    }

    private void ReloadAnimationAndSlider(float sliderValue, Image slider)
    {
        StartCoroutine(ReloadAnimation());
        StartCoroutine(SliderIncreaser(sliderValue, slider));
    }

    private IEnumerator ReloadAnimation()
    {
        if (gun.gunType == GunType.ShotGun)
        {
            RectTransform rectTransform = gunImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(reloadSprites[0].bounds.size.x * 200, reloadSprites[0].bounds.size.y * 200);
            gunImage.rectTransform.sizeDelta = rectTransform.sizeDelta;
        }

        for (int i = 0; i < reloadSprites.Length; i++)
        {
            gunImage.sprite = reloadSprites[i];
            //Get the size of the image and change to the size of the sprite
            if (gun.gunType == GunType.RPG)
            {
                RectTransform rectTransform = gunImage.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(reloadSprites[i].bounds.size.x * 40, reloadSprites[i].bounds.size.y * 40);
                gunImage.rectTransform.sizeDelta = rectTransform.sizeDelta;
            }

            if (gun.gunType == GunType.ShotGun)
            {
                RectTransform rectTransform = gunImage.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(reloadSprites[i].bounds.size.x * 200, reloadSprites[i].bounds.size.y * 200);
                gunImage.rectTransform.sizeDelta = rectTransform.sizeDelta;
            }

            yield return new WaitForSeconds(timeBetweenSprites);
        }
        UIAnimationIsFinished = true;
    }

    private IEnumerator SliderIncreaser(float sliderValue, Image slider)
    {
        while (sliderValue < gun.reloadTime)
        {
            slider.fillAmount = sliderValue;
            sliderValue += Time.deltaTime / gun.reloadTime;
            yield return null;
        }

        sliderIsFinished = true;
    }
}
