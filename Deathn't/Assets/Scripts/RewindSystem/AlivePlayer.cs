using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlivePlayer : MonoBehaviour
{
    public static AlivePlayer instance = null;
    [SerializeField] GameObject deadPlayerPrefab = null;
    public bool died = false;
    private GameObject ghost = null;

    private void OnEnable()
    {
        instance = this;
    }

    public void AliveAgain()
    {
        RewindController.stopAll = true;
        Destroy(ghost);
        died = false;
    }

    void GameOver()
    {
        RewindController.globalRewind = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void KillPlayer()
    {
        if (died == false)
        {
            Debug.Log("Killing player");
            BGM.instance.PlayDeathClip();
            ghost = Instantiate(deadPlayerPrefab, transform.position, transform.rotation, null);
            died = true;
            RewindController.globalRewind = true;
        }
        else
        {
            Debug.LogError("PlayerKilled while already dead");
            GameOver();
        }
    }

    // check collisions with enemies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            KillPlayer();
        }
    }

}
