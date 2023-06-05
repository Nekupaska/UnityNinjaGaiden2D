using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnPlayer : MonoBehaviour
{
    public Transform repawnPoint;
    GameObject g;

    void OnTriggerEnter(Collider col)
    {
        if (col!=null && col.CompareTag("player"))
        {
            /*g = col.gameObject;
            g.GetComponent<MainChar>().enabled = false;
            g.gameObject.transform.position = repawnPoint.position;
            StartCoroutine("delay");*/
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

    public IEnumerable delay()
    {
        g.GetComponent<MainChar>().enabled = true;
        yield return new WaitForSeconds(1);
    }
}
