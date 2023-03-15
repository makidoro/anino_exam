using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelEditor : MonoBehaviour
{

    //transform for moving the reel
    public Transform tform;

    //variables for managing the state of the reel
    public int state = 0;
    //0 - Stopped
    //1 - Spinning
    //2 - Waiting
    public int target;
    public float wait;
    public bool isWaiting = false;

    //variable to track whether this reel is the primary or secondary of the loop
    public bool isPrimary = true;

    //sprites for the symbols
    public Sprite[] sprites = new Sprite[10];

    //image ui objects that will hold the sprites
    public Image[] images = new Image[10];

    
    public void GenerateReel(int[] numbers){
        // Debug.Log(numbers[0]
        //             + " " + numbers[1]
        //             + " " + numbers[2]
        //             + " " + numbers[3]
        //             + " " + numbers[4]
        //             + " " + numbers[5]
        //             + " " + numbers[6]
        //             + " " + numbers[7]
        //             + " " + numbers[8]
        //             + " " + numbers[9]
        //             );
        for(int i = 0; i < 10; i++)
        {
            images[i].sprite = sprites[numbers[i]];
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpinReel(int targetParam, float waitParam){
        target = targetParam;
        wait = waitParam;

        if(state == 0){
            state++;
            return;
        }else{
            if(isPrimary == true){
                StartCoroutine(Stop());
            }
        }
    }

    IEnumerator Stop(){
        isWaiting = true;
        yield return new WaitForSeconds(wait);
        state = 2;

    }


    // Update is called once per frame
    void Update()
    {
        if(state == 1){
            tform.transform.localPosition += new Vector3(0,-20,0);
            if(tform.transform.localPosition.y == -500){
                tform.transform.localPosition += new Vector3(0,2000,0);
            }
        }
        if(state == 2){
            tform.transform.localPosition += new Vector3(0,-20,0);
            if(tform.transform.localPosition.y == 100*target){
                isWaiting = false;
                state = 0;
            }
            if(tform.transform.localPosition.y == -500){
                tform.transform.localPosition += new Vector3(0,2000,0);
            }
        }
        //Debug.Log(tform.transform.localPosition.y);
    }
}
