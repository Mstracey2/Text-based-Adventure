using System;
using System.Collections.Generic;
using System.Text;

namespace Artifact_Project
{
    class Combat
    {
        public bool playerDefeat = false;
        public void CombatStart()
        {
            string playerInput = "";// players input
            bool liveEnemy = true;// this sequence plays as long as this live enemy bool is true.

            bool playerDodge = false;// this bool is for when the player chooses to block as their turn.
            bool stun = false;// this bool is for when the player chooses to stun as their turn.
            int stunCount = 0;// this counts how many turns the creature gets stunned for.
            int enemyDice;//dice to decide what the enemy chooses as their turn.

            while (liveEnemy == true)
            {
                int playerHealth = 250;// players health
                playerDodge = false;

                //random number generator initiated
                Random rnd = new Random();

                int[] EnemyHealthGen = { 200, 200, 300, 250, 100, 200 };//list of enemy health
                int enemyHealth = 0;

                if (enemyHealth == 0)
                {
                    int enemyHPDice = rnd.Next(0, 6);
                    enemyHealth = EnemyHealthGen[enemyHPDice];//chooses the health of the enemy through the array and the randome number from the HP dice.
                }

                Console.WriteLine("A creature has appeared with " + enemyHealth + " health");

                while (enemyHealth > 0 && playerHealth > 0)// checks to see if anyone's health is below 0
                {
                    #region player choice
                    bool combatOptions = true;// turns the players choice on.
                    while (combatOptions == true && playerHealth > 0)
                    {
                        Console.WriteLine("The creature has " + enemyHealth + " remaining, whilst you have " + playerHealth);
                        Console.WriteLine("Choose your turn!");
                        Console.WriteLine("1.Knife");
                        Console.WriteLine("2.Block");
                        Console.WriteLine("3.Stun Rifle");
                        Console.WriteLine("4.revolver");

                        playerInput = Console.ReadLine();
                        
                        #region knife
                        if (playerInput == "1")
                        {
                            bool knife = true;
                            while (knife == true)
                            {
                                Console.WriteLine("1.slash");//players get a choice for what they wish to do wit the knife.
                                Console.WriteLine("2.lunge");
                                Console.WriteLine("3.back");
                                playerInput = Console.ReadLine();
                                if (playerInput == "1")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Slashing your enemy is guaranteed 25 damage ");
                                    bool conf = true;
                                    while (conf == true)
                                    {
                                        Console.WriteLine("Are you sure you want to slash? y/n");
                                        playerInput = Console.ReadLine();
                                        if (playerInput == "y")
                                        {
                                            Console.Clear();
                                            Console.WriteLine("you slashed the creature for 25 damage");
                                            enemyHealth = enemyHealth - 25;
                                            knife = false;
                                            combatOptions = false;//this will move the game to the enemies choice.
                                            conf = false;

                                        }

                                        else if (playerInput == "n")
                                        {
                                            knife = true;
                                            conf = false;
                                        }

                                        else
                                        {
                                            Console.WriteLine("please respond with 'y' or 'n'");
                                        }
                                    }
                                }

                                else if (playerInput == "2")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Lunging at your enemy can do 50 damage (65% chance of connecting)");
                                    bool conf = true;
                                    while (conf == true)
                                    {
                                        Console.WriteLine("Are you sure you want to lunge? y/n");
                                        playerInput = Console.ReadLine();
                                        if (playerInput == "y")
                                        {
                                            Console.Clear();
                                            int knifeLungeDice = rnd.Next(1, 101);// this is a dice that will determine whether the player misses or not, on this occasion, its a 65% chance of hitting.
                                            if (knifeLungeDice <= 65)
                                            {
                                                Console.WriteLine("you lunge at the creature dealing 50 damage");
                                                enemyHealth = enemyHealth - 50;
                                                knife = false;
                                                combatOptions = false;
                                                conf = false;
                                            }

                                            else if (knifeLungeDice > 65)
                                            {
                                                Console.WriteLine("you lunge at the creature but you stumble and miss");
                                                knife = false;
                                                combatOptions = false;
                                                conf = false;
                                            }
                                        }

                                        else if (playerInput == "n")
                                        {
                                            knife = true;
                                            conf = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please respond with 'y' or 'n' for yes or no");
                                        }
                                    }

                                }

                                else if (playerInput == "3")
                                {
                                    knife = false;
                                }

                                else
                                {
                                    Console.WriteLine("please respond with the number of the option you wish to pick");
                                }
                            }
                        }
                        #endregion

                        #region block
                        else if (playerInput == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("using Block will make it so you only take 25 damage from your opponents next turn.\n once Blocked, you have a 50/50 chance of dealing a devastating counter attack of 75 damage to your opponent, or causing damage to yourself(-50).");
                            bool conf = true;
                            while (conf == true)
                            {
                                Console.WriteLine("Are you sure you want to block? y/n ");
                                playerInput = Console.ReadLine();

                                if (playerInput == "y")
                                {
                                    Console.Clear();
                                    Console.WriteLine("you have your arms ready for whatever the creature is going to throw at you!");
                                    playerDodge = true;// player does not do damage this turn. because this bool is activated, the damage is determined in the enemies turn. 50% chance of working, 50% chance of the player injuring themselves.
                                    combatOptions = false;
                                    conf = false;
                                }
                                else if (playerInput == "n")
                                {
                                    combatOptions = true;
                                    conf = false;
                                }
                                else
                                {
                                    Console.WriteLine("Please respond with 'y' or 'n' for yes or no");
                                }
                            }
                        }
                        #endregion

                        #region stun rifle
                        else if (playerInput == "3")
                        {
                            Console.Clear();
                            Console.WriteLine("for every opponent turn, the stun rifle drains 25 health from your opponent for 3 turns");
                            bool conf = true;
                            while (conf == true)
                            {
                                Console.WriteLine("Are you sure you want to use Stun rifle? y/n");
                                playerInput = Console.ReadLine();

                                if (playerInput == "y")
                                {
                                    Console.Clear();
                                    if (stun == false)// checks if the creature is already stunned from a previous turn.
                                    {
                                        Console.WriteLine("you stun the creature, it starts to twitch violently!");
                                        stun = true;// with this turned on, the creature will have 25 health drained on every start of their turn.
                                        conf = false;
                                        combatOptions = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("your stun rifle needs time to recharge and the creature seems to already be zapped!");
                                    }
                                }
                                else if (playerInput == "n")
                                {
                                    conf = false;
                                }
                                else
                                {
                                    Console.WriteLine("Please respond with 'y' or 'n' for yes or no");
                                }
                            }
                        }
                        #endregion

                        #region revolver
                        else if (playerInput == "4")
                        {
                            Console.Clear();
                            Console.WriteLine("the revolver is a 100 damage but is a low chance of 20% to connect");
                            bool conf = true;
                            while (conf == true)
                            {
                                Console.WriteLine("Are you sure you want to use revolver? y/n");
                                playerInput = Console.ReadLine();

                                if (playerInput == "y")
                                {
                                    Console.Clear();
                                    int gunDice = rnd.Next(1, 100);// another dice to determine if the shot connected. much lower chance of success.
                                    if (gunDice <= 20)
                                    {
                                        Console.WriteLine("you shot the creature, dealing 100 damage!");
                                        enemyHealth = enemyHealth - 100;
                                        combatOptions = false;
                                        conf = false;

                                    }
                                    else
                                    {
                                        Console.WriteLine("you aimed and took your shot, but you missed and hit the wall behind the creature.");
                                        combatOptions = false;
                                        conf = false;
                                    }

                                }
                                else if (playerInput == "n")
                                {
                                    conf = false;
                                }
                                else
                                {
                                    Console.WriteLine("Please respond with 'y' or 'n' for yes or no");
                                }
                            }

                        }
                        #endregion

                        else if (playerInput == "save")
                        {
                            Console.WriteLine("you can't save while in combat!");
                        }
                        else if (playerInput == "load")
                        {
                            Console.WriteLine("you can't load while in combat!");
                        }
                        else
                        {
                            Console.WriteLine("wrong input, please respond with the number of your choice.");
                        }
                    }
                    #endregion

                    #region enemy choice
                    bool enemyOptions = true;// turns the enemies options on.
                    while (enemyOptions == true && enemyHealth > 0 && playerHealth > 0)// checks whether anyone is below 0 and option is set to true.
                    {
                        if (stun == true)// if the player chose to stun the creature, then this if statement activates
                        {
                            if (stunCount <= 3)// the creature gets tazed for 3 turns through this stun count
                            {
                                enemyHealth = enemyHealth - 25;
                                stunCount = stunCount + 1;// adds 1 each time
                                Console.WriteLine("your stun Rifle dealt 25 damage, the creature is now at " + enemyHealth + ".");
                            }
                            else
                            {
                                stunCount = 0;//once the stun count is past 3, it will reset to 0
                                stun = false;// and stun is set back to false
                            }
                        }

                        if (playerDodge == true)//checks whether the player blocked previous turn.
                        {
                            playerHealth = playerHealth - 25;
                            Console.WriteLine("you decided to block, you take 25 damage from the creature's attack. you are now at " + playerHealth + " health.");
                            int BlockDice = rnd.Next(1, 3);// dice to determine a counter attack after the block

                            if (BlockDice == 1)// player is rewarded if gamble succeeds.
                            {
                                Console.WriteLine("when blocking the creature's attack, you found an opportunity to plunge your knife deep into its back. you dealt 100 damage");
                                enemyHealth = enemyHealth - 100;
                                playerDodge = false;
                                enemyOptions = false;// because its a block, the enemy used its turn.

                            }
                            else if (BlockDice == 2)// player is punished if gamble fails.
                            {
                                playerHealth = playerHealth - 50;
                                Console.WriteLine("in an attempt to counter the creature, you tried stabbing it but you stumble to the floor and you injure yourself. you lose - 50 health ");
                                playerDodge = false;
                                enemyOptions = false;

                            }

                        }
                        int enemyChoice = rnd.Next(1, 4);// a dice to determine the creature choice of attack.

                        if (enemyChoice == 1)// bite attack. gaurenteed hit
                        {
                            playerHealth = playerHealth - 25;
                            Console.WriteLine("The creature bites you, dealing 25 damage");
                            enemyOptions = false;
                        }
                        else if (enemyChoice == 2)//spit attack
                        {
                            enemyDice = rnd.Next(1, 3);// 50% chance of hitting with dice.
                            if (enemyDice == 1)
                            {
                                playerHealth = playerHealth - 50;
                                Console.WriteLine("The creature spits at you, dealing 50 damage");
                                enemyOptions = false;
                            }
                            else
                            {
                                Console.WriteLine("The creature spits at you, but it missed");
                                enemyOptions = false;
                            }

                        }
                        else if (enemyChoice == 3)// claw attack, most damage but low chance of hitting
                        {
                            enemyDice = rnd.Next(1, 5);//dice
                            if (enemyDice == 1)
                            {
                                playerHealth = playerHealth - 100;
                                Console.WriteLine("The creature cuts you with its claws, dealing 100 damage");
                                enemyOptions = false;
                            }
                            else
                            {
                                Console.WriteLine("The creature swings at you, but it missed");
                                enemyOptions = false;
                            }
                        }
                    }
                    #endregion
                }
                if (enemyHealth <= 0)// if the player wins, this shows
                {
                    Console.WriteLine("The creature falls to the ground as you make your final strike.");
                    liveEnemy = false;
                    System.Threading.Thread.Sleep(6000);
                    playerDefeat = false;
                }
                else if (playerHealth <= 0)//if the player looses this shows
                {
                    Console.WriteLine("you fought strong, but you were overcumbered by the creature. Your vision slowly fades as you see the creature gives you a sinister grin.");
                    playerDefeat = true;//with this true, player is taken to the GAME OVER screen.
                    liveEnemy = false;
                    System.Threading.Thread.Sleep(6000);
                }
            }


        }
    }
    
}
