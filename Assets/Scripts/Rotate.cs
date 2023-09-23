using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    private void Update()
    {
        if (HpManager.instance.hp <= 0) return;

        var rot = transform.rotation.eulerAngles;
        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var axis = target - transform.position;

        rot.z = (Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg + 360 - 90) % 360f;

        if (rot.z > 180 && rot.z < 280) rot.z = 280;
        else if (rot.z > 80 && rot.z < 180) rot.z = 80;
        transform.rotation = Quaternion.Euler(rot);
    }
}
