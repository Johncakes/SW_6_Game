using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_spawn : MonoBehaviour
{

    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private float spawnTime;
    private float[] arrPosY = { -3.5f, -1f, 2f };
    public bool test;

    void Start()
    {
        StartItemRotine();
    }
    void StartItemRotine()
    {
        StartCoroutine("itemRoutine");
    }
    IEnumerator itemRoutine()
    {
        yield return new WaitForSeconds(5f);
        int k = items.Length - 1;
        while (true)
        {
            if (test)
            {

                spawnitem(k, arrPosY[0]);
                k++;
                if (k == items.Length)
                    k = 0;
                yield return new WaitForSeconds(5);
                continue;
            }
            int i = Random.Range(0, 3);
            int j = Random.Range(0, 4);
            int index = Random.Range(0, items.Length);

            if (j == 0)
            {
                spawnitem(index, arrPosY[i]);
            }
            yield return new WaitForSeconds(5);

        }


    }

    void spawnitem(int index, float posY)
    {
        // Debug.Log("Item Index : " + index);
        Vector3 spawnPos = new Vector3(transform.position.x, posY, transform.position.z + 1);
        Instantiate(items[index], spawnPos, Quaternion.identity);
    }
    void Update()
    {

    }
}
