using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class KeyInputHint : MonoBehaviour
{
    [SerializeField] private float _disappearTime;
    [SerializeField] private KeyCode _triggerKey;

    private bool _triggered = false;
    private float _timer = 0f;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(_triggerKey))
        {
            _triggered = true;
        }

        if(_triggered)
        {
            _timer += Time.deltaTime;
            var col = _text.color;
            col.a = 1 - (_timer / _disappearTime);
            _text.color = col;
            if (_timer > _disappearTime) Destroy(gameObject);
        }
    }
}
