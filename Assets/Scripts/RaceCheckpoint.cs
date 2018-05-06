﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCheckpoint : MonoBehaviour {
    Image checkpointMark;
    private bool scored = false;

    // Use this for initialization
    void Start () {
        checkpointMark = GameObject.FindGameObjectWithTag("CheckpointMark").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {
        Vector3 p = Camera.main.WorldToScreenPoint(transform.position);
        Rect pr = Camera.main.pixelRect;
        if (p.x > 0 && p.y > 0 && p.x < pr.width && p.y < pr.height && p.z > 0f)
        {
            checkpointMark.transform.position = p; //new Vector3(Mathf.Clamp(Mathf.Abs(p.x * Mathf.Sign(p.z)), 0f, pr.width), Mathf.Clamp(Mathf.Abs(p.y * Mathf.Sign(p.z)), 0f, pr.height), p.z);
            checkpointMark.enabled = true;
        }
        else
        {
            checkpointMark.enabled = false;
        }

        if (scored && !GetComponent<AudioSource>().isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    public void Score(string scoreName)
    {
        if (scored) return;

        if (scoreName.Contains("ScoreHigh"))
        {
            Global.global.CoinBurst(transform.position, 16);
        }
        else if (scoreName.Contains("ScoreNormal"))
        {
            Global.global.CoinBurst(transform.position, 8);
        }

        GetComponent<AudioSource>().Play();

        scored = true;
        GetComponentInParent<Race>().AdvanceCheckpoint(transform.GetSiblingIndex());
    }

    public void ResetState()
    {
        scored = false;
    }
}