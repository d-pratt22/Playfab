using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rig;
    private float startTime;
    private float timeTaken;
    private int collectablesPicked;
    public int maxCollectables = 9;
    private bool isPlaying;
    public GameObject plane;
    public GameObject leaderboard;

    public GameObject playButton;
    public TextMeshProUGUI curTimeText;
    private double curTimeNum;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isPlaying)
            return;

        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        rig.velocity = new Vector3(x, rig.velocity.y, z);

        curTimeText.text = (Time.time - startTime).ToString("F2");

     
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            collectablesPicked++;
            Destroy(other.gameObject);
            if (collectablesPicked == maxCollectables)
                End();
        }
    }

    public async void Begin()
    {
        startTime = Time.time;
        isPlaying = true;
        playButton.SetActive(false);
        leaderboard.SetActive(false);

        /* Thread.Sleep(2000);
         Destroy(plane);*/

        await Task.Delay(2000);
        Destroy(plane);
    }

   /* void PlaneControl()
    {
      if (isPlaying == true)
        {
            Thread.Sleep(2000);
            Destroy(plane);
        }
    }*/

    void End()
    {
        timeTaken = Time.time - startTime;
        isPlaying = false;
        playButton.SetActive(true);
        leaderboard.SetActive(true);
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
    }
}
