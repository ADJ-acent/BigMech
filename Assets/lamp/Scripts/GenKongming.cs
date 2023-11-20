using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenKongming : MonoBehaviour
{
    /// <summary>
    /// 孔明灯预设
    /// </summary>
    public GameObject lanternObj;
    /// <summary>
    /// 高空摄像机
    /// </summary>
    public GameObject cam2;

    private Transform selfTrans;
    /// <summary>
    /// 主摄像机的Transform
    /// </summary>
    private Transform camTrans;
    /// <summary>
    /// 主摄像机
    /// </summary>
    private Camera cam;
    

    void Start()
    {
        selfTrans = transform;
        cam = Camera.main;
        camTrans = cam.transform;

        //初始创建50个孔明灯
        for (int i = 0; i < 50; ++i)
        {
            var go = Instantiate(lanternObj);
            go.transform.position = new Vector3(Random.Range(-100, 100), Random.Range(50, 100), Random.Range(-100, 100));
            go.transform.SetParent(selfTrans, false);
        }

        // 协程无限循环创建孔明灯
        StartCoroutine(StartGen());
    }


    private IEnumerator StartGen()
    {
        while (true)
        {
            var go = Instantiate(lanternObj);
            go.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            go.transform.SetParent(selfTrans, false);
            yield return new WaitForSeconds(1);
        }
    }

    private void Update()
    {
        // 点击鼠标右键，在鼠标位置处生成一个孔明灯
        if (Input.GetMouseButtonDown(1))
        {
            var go = Instantiate(lanternObj);
            var pos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f));
            go.transform.SetParent(selfTrans, false);
            go.transform.position = pos;
        }


        // 鼠标左键拖动，旋转摄像机
        if(Input.GetMouseButton(0))
        {
            float inputX = Input.GetAxis("Mouse X")*1.5f;
            camTrans.Rotate(Vector3.up, inputX, Space.World);
        }

        // 高空摄像机的激活控制
        if(Input.GetKeyDown(KeyCode.Space))
        {
            cam2.gameObject.SetActive(!cam2.activeSelf);
        }
    }
}
