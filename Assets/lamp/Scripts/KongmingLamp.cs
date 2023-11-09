using UnityEngine;

public class KongmingLamp : MonoBehaviour
{
    private Transform trans;
    private Vector3 speed;

    void Start()
    {
        trans = transform;
        // ���һ�������ٶ�
        speed = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1, 3), Random.Range(-0.5f, 0.5f));
    }


    void Update()
    {
        // �����ٶȷ�������
        trans.position += speed * Time.deltaTime;

        // �߶ȴﵽ500����������
        if (trans.position.y > 500)
        {
            Destroy(gameObject);
        }
    }
}
