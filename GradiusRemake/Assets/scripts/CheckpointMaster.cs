using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMaster : MonoBehaviour
{
    public AudioMaster MAudio;
    public Transform fullStage;
    public Vector3 originalStagePos;
    public List<Checkpoint> checkpoints;
    public int currentCheckpoint;
    protected float screenCenter;
    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = 0;
        originalStagePos = fullStage.position;
        screenCenter = Camera.main.transform.position.x;
        MAudio.PlayNewMusicCommand(checkpoints[currentCheckpoint].mySong);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCheckpoint < checkpoints.Count - 1)
        {
            if (checkpoints[currentCheckpoint + 1].transform.position.x <= screenCenter)
            {
                currentCheckpoint++;
                if (checkpoints[currentCheckpoint].mySong != checkpoints[currentCheckpoint - 1].mySong)
                {
                    MAudio.PlayNewMusicCommand(checkpoints[currentCheckpoint].mySong);
                }
            }
        }
    }

    public void MoveStage()
    {
        fullStage.position = originalStagePos + new Vector3(checkpoints[currentCheckpoint].scrollAmount, 0, 0);
        MAudio.PlayNewMusicCommand(checkpoints[currentCheckpoint].mySong);
    }
}
