using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SlotLogic : MonoBehaviour
{
    public ReelManager reelManager;

    public bool isSpinning = false;

    public int reelNum = 5;

    public int coins = 500;
    public int bet = 200;
    public int winnings = 0;

    //reel Values
    private int[] reel1 = {0,1,2,3,4,5,6,7,8,9};
    private int[] reel2 = {4,1,7,3,0,6,8,2,5,9};
    private int[] reel3 = {0,3,6,3,4,5,1,7,8,9};
    private int[] reel4 = {0,1,3,3,5,5,7,7,9,9};
    private int[] reel5 = {1,2,3,4,5,6,7,8,9,9};

    public int[][] reels = new int[5][];

    //rows that will hold the reel values on a spin
    public int[] row1 = new int[5];
    public int[] row2 = new int[5];
    public int[] row3 = new int[5];

    public int[][] rows = new int[3][];

    //row that will hold the reel position on a spin
    public int[] rowCoord = new int[5];


    //lines
    public int[] line1 = {0,0,0,0,0};
    public int[] line2 = {1,1,1,1,1};
    public int[] line3 = {2,2,2,2,2};
    public int[] line4 = {2,1,0,1,2};
    public int[] line5 = {0,1,2,1,0};
    public int[] line6 = {0,2,0,2,0};
    public int[] line7 = {2,0,2,0,2};
    public int[] line8 = {1,2,1,2,1};
    public int[] line9 = {2,1,2,1,2};
    public int[] line10 = {0,1,0,1,0};
    public int[] line11 = {1,0,1,0,1};
    public int[] line12 = {0,0,2,0,0};
    public int[] line13 = {2,2,0,2,2};
    public int[] line14 = {0,0,0,0,2};
    public int[] line15 = {2,2,2,2,0};
    public int[] line16 = {0,2,2,2,2};
    public int[] line17 = {2,0,0,0,0};
    public int[] line18 = {1,0,2,0,2};
    public int[] line19 = {1,2,0,2,0};
    public int[] line20 = {1,0,1,2,1};

    public int[][] lines = new int[20][];
    
    //array of payout numbers
    public int[] payout1 = {0,0,0,0,0,0};
    public int[] payout2 = {1,0,0,1,2,4};
    public int[] payout3 = {2,0,0,2,3,5};
    public int[] payout4 = {3,0,0,3,4,7};
    public int[] payout5 = {4,0,0,5,6,9};
    public int[] payout6 = {5,0,0,7,8,12};
    public int[] payout7 = {6,0,0,8,9,14};
    public int[] payout8 = {7,0,0,9,10,16};
    public int[] payout9 = {8,0,0,10,14,18};
    public int[] payout10 = {9,0,0,12,16,20};

    public int[][] payouts = new int[10][];

    
    void Start(){
        SetRows();
        SetLines();
        SetPayouts();
    }


    //called when the spin button is pressed
    public void Spin(){
        if(!isSpinning){
            coins -= bet;
            reelManager.UpdateBet();
            for(int i = 0; i < reelNum; i++)
            {
                SetTarget(i);
            }
            reelManager.SpinSlot(rowCoord, true);
            isSpinning = true;            
            PrintRows();
        }else{
            reelManager.SpinSlot(rowCoord, false);
            isSpinning = false;
            Calculate();
            // Debug.Log(Analyze(row1));
            // Debug.Log(Analyze(row2));
            // Debug.Log(Analyze(row3));
        }
    }

    public void Calculate(){
        int[] column = new int[5];
        int total = 0;
        int score = 0;
        string linesUsed = "Lines Used: ";
        for(int i = 0; i < 20; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                column[j] = rows[lines[i][j]][j];
            }
            Debug.Log(column[0] + " " + column[1] + " " + column[2] + " " + column[3] + " " + column[4] + " " + Analyze(column));
            score = Analyze(column);
            if(score > 0){
                linesUsed = linesUsed + i + " ";
                total += score;
            }
        }
        total = total * bet;
        string output = "Payout: " + total + "\n" + linesUsed;
        reelManager.Results(output);
        winnings += total;
        coins += total;
    }

    //Get the value of a column from a line
    public int Analyze(int[] column){
        int count = 1;
        for(int i = 1; i < reelNum; i++){
            if(column[i-1] == column[i]){
                count++;
            }else{
                break;
            }
        }
        return payouts[column[0]][count];
    }

    //randomly chooses targets per reel and assigns them to the corresponding row position
    public void SetTarget(int column){
        int target = Random.Range(0,10);
        rowCoord[column] = target;
        row2[column] = reels[column][target];

        if(target == 0){
            row1[column] = reels[column][9];
        }else{
            row1[column] = reels[column][target-1];
        }

        if(target == 9){
            row3[column] = reels[column][0];
        }else{
            row3[column] = reels[column][target+1];
        }
    }

    public void IncreaseBet(){
        if(bet + 20 <= coins){
            bet += 20;
        }
        reelManager.UpdateBet();
    }

    public void DecreaseBet(){
        if(bet - 20 >= 0){
            bet -= 20;
        }
        reelManager.UpdateBet();
    }

    public void CheckBet(){
        if(bet > coins){
            bet = coins;
        }
    }

    public int[] GetReel(int num){
        SetReels();
        return reels[num];
    }

    void SetRows(){
        rows[0] = row1;
        rows[1] = row2;
        rows[2] = row3;
    }

    void SetReels(){
        reels[0] = reel1;
        reels[1] = reel2;
        reels[2] = reel3;
        reels[3] = reel4;
        reels[4] = reel5;
    }

    void SetLines(){
        lines[0] = line1;
        lines[1] = line2;
        lines[2] = line3;
        lines[3] = line4;
        lines[4] = line5;
        lines[5] = line6;
        lines[6] = line7;
        lines[7] = line8;
        lines[8] = line9;
        lines[9] = line10;
        lines[10] = line11;
        lines[11] = line12;
        lines[12] = line13;
        lines[13] = line14;
        lines[14] = line15;
        lines[15] = line16;
        lines[16] = line17;
        lines[17] = line18;
        lines[18] = line19;
        lines[19] = line20;
    }

    void SetPayouts(){
        payouts[0] = payout1;
        payouts[1] = payout2;
        payouts[2] = payout3;
        payouts[3] = payout4;
        payouts[4] = payout5;
        payouts[5] = payout6;
        payouts[6] = payout7;
        payouts[7] = payout8;
        payouts[8] = payout9;
        payouts[9] = payout10;
    }

    //used for testing purposes, prints all rows visible on the machine
    public void PrintRows(){
        Debug.Log(rowCoord[0] + " " +rowCoord[1] + " " +rowCoord[2] + " " +rowCoord[3] + " " +rowCoord[4]);
        for(int i = 0; i < 3; i++)
        {
            Debug.Log(rows[i][0] + " " +rows[i][1] + " " +rows[i][2] + " " +rows[i][3] + " " +rows[i][4]);
        }
    }
}
