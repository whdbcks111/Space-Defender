using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI _goldUI;
    [SerializeField] private TextMeshProUGUI _phaseUI, _staticPhaseUI;
    [SerializeField] private GameObject _laserShopContainer;
    [SerializeField] private LaserShopItem _laserShopItemPrefab;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] public ParticleSystem boomParticlePrefab;
    [SerializeField] public VolumeProfile volumeProfile;
    public int gold = 0;
    public int phase = 1;

    private float _phaseTimer = 30f;
    private float _breakTimer = 0f;

    public float playTime = 0f;

    public bool IsBreakTime { get { return _breakTimer > 0f; } }

    private void Awake()
    {
        gold = 0;
        phase = 1;
        instance = this;
    }

    private void Start()
    {
        if (volumeProfile.TryGet(out ColorAdjustments ca))
        {
            ca.saturation.value = 0f;
        }
        UpdateShop();
        ShowPhase();
    }

    private void Update()
    {
        playTime += Time.unscaledDeltaTime;
        _goldUI.SetText(gold.ToString());

        if(_breakTimer > 0)
        {
            _breakTimer -= Time.deltaTime;
        }
        else if ((_phaseTimer -= Time.deltaTime) <= 0)
        {
            phase++;
            _phaseTimer = 30f - phase * .5f;
            _breakTimer = 5f;
            _spawner.interval *= 0.9f;
            ShowPhase();
        }
    }

    private void UpdateShop()
    {
        foreach (Transform t in _laserShopContainer.transform)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < _shooter.laserPrefab.upgrades.Length; i++)
        {
            var laser = _shooter.laserPrefab.upgrades[i];
            var shopItem = Instantiate(_laserShopItemPrefab, _laserShopContainer.transform);
            var pos = shopItem.rectTransform.anchoredPosition;
            pos.y -= i * shopItem.rectTransform.rect.height + 10;
            shopItem.rectTransform.anchoredPosition = pos;
            shopItem.icon.sprite = laser.GetComponent<SpriteRenderer>().sprite;
            shopItem.priceUI.SetText(laser.price.ToString());

            shopItem.button.onClick.AddListener(() =>
            {
                if(gold >= laser.price)
                {
                    gold -= laser.price;
                    _shooter.laserPrefab = laser;

                    UpdateShop();
                }
            });
        }
    }

    private void ShowPhase()
    {
        _staticPhaseUI.SetText("Phase " + phase);
        _phaseUI.SetText("Phase " + phase);
        _phaseUI.gameObject.SetActive(true);
        StartCoroutine(ShowPhaseRoutine());
    }

    private IEnumerator ShowPhaseRoutine()
    {
        var originalSize = _phaseUI.fontSize;
        Color col;
        for (float timer = 0f; timer < 1f; timer += Time.deltaTime)
        {
            yield return null;
            col = _phaseUI.color;
            col.a = 1f - timer;
            _phaseUI.color = col;
            _phaseUI.fontSize = originalSize * (1f + timer * .5f);
        }
        _phaseUI.gameObject.SetActive(false);
        _phaseUI.fontSize = originalSize;
        col = _phaseUI.color;
        col.a = 1f;
        _phaseUI.color = col;
    }

    public void Heal(int price)
    {
        if (gold < price) return;
        if (HpManager.instance.hp >= HpManager.instance.maxHp) return;

        gold -= price;
        HpManager.instance.hp += 1;
    }

    public void Restart()
    {
        if(volumeProfile.TryGet(out ColorAdjustments ca))
        {
            ca.saturation.value = 0f;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
