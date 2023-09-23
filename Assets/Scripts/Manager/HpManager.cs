using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public static HpManager instance;

    [SerializeField] private Image _heartPrefab;
    [SerializeField] private float _maxHp = 5f;
    [SerializeField] private TextMeshProUGUI _gameOverUI;
    [HideInInspector] public float hp;
    public float maxHp { get { return _maxHp; } }

    private Image[] _hearts;

    private void Awake()
    {
        instance = this;
        hp = _maxHp;

        _hearts = new Image[(int)_maxHp];
        for(int i = 0; i < _hearts.Length; i++)
        {
            var o = Instantiate(_heartPrefab, transform);
            o.transform.localPosition = Vector3.right * 40 * (transform.childCount - 1);
            _hearts[i] = o;
        }
    }

    private void Update()
    {
        for(int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i].color = i < hp ? Color.white : Color.gray;
        }

        if(hp <= 0 && !_gameOverUI.gameObject.activeSelf)
        {
            _gameOverUI.gameObject.SetActive(true);
            Time.timeScale = .3f;
            if (GameManager.instance.volumeProfile.TryGet(out ColorAdjustments ca))
            {
                ca.saturation.value = -100f;
            }
        }
    }
}
