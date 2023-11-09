using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenKongming : MonoBehaviour
{
    /// <summary>
    /// ������Ԥ��
    /// </summary>
    public GameObject lanternObj;
    /// <summary>
    /// �߿������
    /// </summary>
    public GameObject cam2;

    private Transform selfTrans;
    /// <summary>
    /// ���������Transform
    /// </summary>
    private Transform camTrans;
    /// <summary>
    /// �������
    /// </summary>
    private Camera cam;
    

    void Start()
    {
        selfTrans = transform;
        cam = Camera.main;
        camTrans = cam.transform;

        //��ʼ����50��������
        for (int i = 0; i < 50; ++i)
        {
            var go = Instantiate(lanternObj);
            go.transform.position = new Vector3(Random.Range(-100, 100), Random.Range(50, 100), Random.Range(-100, 100));
            go.transform.SetParent(selfTrans, false);
        }

        // Э������ѭ������������
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
        // �������Ҽ��������λ�ô�����һ��������
        if (Input.GetMouseButtonDown(1))
        {
            var go = Instantiate(lanternObj);
            var pos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f));
            go.transform.SetParent(selfTrans, false);
            go.transform.position = pos;
        }


        // �������϶�����ת�����
        if(Input.GetMouseButton(0))
        {
            float inputX = Input.GetAxis("Mouse X")*1.5f;
            camTrans.Rotate(Vector3.up, inputX, Space.World);
        }

        // �߿�������ļ������
        if(Input.GetKeyDown(KeyCode.Space))
        {
            cam2.gameObject.SetActive(!cam2.activeSelf);
        }
    }
}
