using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReelManager : MonoBehaviour
{
    public SlotLogic slotLogic;
    public Button spinButton;
    public bool isStarting = false;
    public bool isWaiting = false;
    public string output = ""; //for the reward text


    //Ui Reel Panels
    public GameObject pReel1;
    public GameObject pReel2;
    public GameObject pReel3;
    public GameObject pReel4;
    public GameObject pReel5;
    public GameObject pReel1b;
    public GameObject pReel2b;
    public GameObject pReel3b;
    public GameObject pReel4b;
    public GameObject pReel5b;

    //Ui Reel Scripts
    public ReelEditor reelEdit1;
    public ReelEditor reelEdit2;
    public ReelEditor reelEdit3;
    public ReelEditor reelEdit4;
    public ReelEditor reelEdit5;
    public ReelEditor reelEdit1b;
    public ReelEditor reelEdit2b;
    public ReelEditor reelEdit3b;
    public ReelEditor reelEdit4b;
    public ReelEditor reelEdit5b;

    private int[] reel1;
    private int[] reel2;
    private int[] reel3;
    private int[] reel4;
    private int[] reel5;

    //rewards panel
    public GameObject rewards;   
    public TMP_Text rewardsText;

    //balance information
    public TMP_Text balanceAmt;
    public TMP_Text betAmt;
    public TMP_Text winningsAmt;
    
    // Start is called before the first frame update
    void Start()
    {
        reel1 = slotLogic.GetReel(0);
        reel2 = slotLogic.GetReel(1);
        reel3 = slotLogic.GetReel(2);
        reel4 = slotLogic.GetReel(3);
        reel5 = slotLogic.GetReel(4);

        reelEdit1.GenerateReel(reel1);
        reelEdit2.GenerateReel(reel2);
        reelEdit3.GenerateReel(reel3);
        reelEdit4.GenerateReel(reel4);
        reelEdit5.GenerateReel(reel5);
        reelEdit1b.GenerateReel(reel1);
        reelEdit2b.GenerateReel(reel2);
        reelEdit3b.GenerateReel(reel3);
        reelEdit4b.GenerateReel(reel4);
        reelEdit5b.GenerateReel(reel5);
        reelEdit1b.isPrimary = false;
        reelEdit2b.isPrimary = false;
        reelEdit3b.isPrimary = false;
        reelEdit4b.isPrimary = false;
        reelEdit5b.isPrimary = false;
    }

    public void SpinSlot(int[] coords, bool starting){
        HideRewards();
        isStarting = starting;
        reelEdit1.SpinReel(coords[0],0f);
        reelEdit1b.SpinReel(coords[0],0f);
        reelEdit2.SpinReel(coords[1],0.5f);
        reelEdit2b.SpinReel(coords[1],0.5f);
        reelEdit3.SpinReel(coords[2],1f);
        reelEdit3b.SpinReel(coords[2],1f);
        reelEdit4.SpinReel(coords[3],1.5f);
        reelEdit4b.SpinReel(coords[3],1.5f);
        reelEdit5.SpinReel(coords[4],2f);
        reelEdit5b.SpinReel(coords[4],2f);

    }
    
    public void Results(string outputText){
        output = outputText;
    }

    public void ShowRewards(){
        rewards.SetActive(true);
        rewardsText.text = output;
    }
    
    public void HideRewards(){
        rewards.SetActive(false);
    }

    public void UpdateBet(){
        betAmt.text = slotLogic.bet.ToString();
        balanceAmt.text = slotLogic.coins.ToString();
    }

    public void UpdateWin(){
        winningsAmt.text = slotLogic.winnings.ToString();
        balanceAmt.text = slotLogic.coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(reelEdit5.isWaiting){
            spinButton.interactable = false;
        }else if(isWaiting){
            ShowRewards();
            UpdateWin();
            slotLogic.CheckBet();
            spinButton.interactable = true;
        }
        if(reelEdit1.state == 0){
            reelEdit1b.state = 0;
        }
        if(reelEdit2.state == 0){
            reelEdit2b.state = 0;
        }
        if(reelEdit3.state == 0){
            reelEdit3b.state = 0;
        }
        if(reelEdit4.state == 0){
            reelEdit4b.state = 0;
        }
        if(reelEdit5.state == 0){
            reelEdit5b.state = 0;
        }

        isWaiting = reelEdit5.isWaiting;

    }
    
}
