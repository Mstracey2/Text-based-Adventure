using System;
using System.Collections.Generic;
using System.Text;

namespace Artifact_Project
{
    public class IntroTutorial
    {
        // intro to the game
        #region Intro
        public void Intro()
        {
            
            Console.WriteLine("You awaken from your unconscious state");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("with all your effort, you get up on your feet. You look around to realise you are in an open room with corridors facing every direction,");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Corridors winding round and round they make your stomach churn. above you, a big glass dome and on the other side, the deep blue ocean staring back at you with merciless eyes.");
            System.Threading.Thread.Sleep(2000);
            
            bool conf = true;
            while (conf == true)
            {
                Console.WriteLine("To give yourself comfort, you remind yourself of your name:");
                string name = Console.ReadLine();
                bool yn = true;
                while(yn == true)
                {
                    Console.WriteLine("your name is " + name + " is this correct? respond with y/n");
                    string nameInput = Console.ReadLine();
                    if (nameInput == "y")
                    {
                        conf = false;
                        yn = false;
                    }
                    else if (nameInput == "n")
                    {
                        conf = true;
                        yn = false;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, wrong input.");
                    }
                }
                
            }
            
            Tutorial();
        }
        #endregion

        // a process that gives the player a tutorial to understand the controls
        #region tutorial
        public void Tutorial()
        {
            #region view tutorial
            Console.WriteLine("To get your bearings, you decide to look around");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Type 'view' to look around");
            
            string playerVariable = Console.ReadLine();// player input
            bool lookAround = false;

            while (lookAround == false)
            {

                if (playerVariable == ("view"))
                {
                    Console.WriteLine("you look around to find yourself in a control room, red lights flash and flicker around the room and you appear to be standing in a puddle of sea water.");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("In the control room you see the central_computer that is still powered by a backup generator.");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("In the corner is a beaten up security_guard sitting in a pool of blood");
                    System.Threading.Thread.Sleep(2000);
                    lookAround = true;
                }
                else
                {
                    Console.WriteLine("sorry, I didnt understand that. Please type 'view' to look around");
                    playerVariable = Console.ReadLine();
                }
            }
            #endregion

            #region examine and pickup tutorial
            bool lookAt = false;
            bool guard = false;
            bool computer = false;
            while (lookAt == false)
            {

                if (guard == true)
                {
                    Console.WriteLine("\nyou have checked the security guard but something draws you to the central_computer, it might have infomation about whats going on!");
                }
                else if (computer == true)
                {
                    Console.WriteLine("\nyou have checked the computer but something draws you to the security_guard, he might have something useful");
                }

                if (guard != true || computer != true)
                {
                    Console.WriteLine("\nTo look at something, type 'examine [object]' hint: you are drawn towards the central_computer and the security_guard.");
                    playerVariable = Console.ReadLine();
                }

                if (playerVariable == "examine security_guard" && guard == false)
                {
                    Console.WriteLine("\nThe security_guard is drenched in blood, you are unable to figure out what could have possibly done this.\nIn the guards hands is a combat_knife, to pick up items type 'pickup [object name]'");
                    bool pickup = true;
                    while (pickup == true)
                    {
                        playerVariable = Console.ReadLine();
                        if (playerVariable == "pickup combat_knife")
                        {
                            Console.WriteLine("\nyou manage to pick up the knife from the guard's ice cold hands.");
                            guard = true;
                            pickup = false;
                        }
                        else
                        {
                            Console.WriteLine("Sorry I didn't understand that, please type 'pickup [object]' to pick up anything useful. remember to type '_' for the spaces.");
                        }
                    }

                }
                else if (playerVariable == "examine central_computer" && computer == false)
                {
                    Console.WriteLine("\nThe Central computer is not in the best shape with cracks on the monitor and missing keys on the keyboard, however despite all that, it is still working which is surprising with all the sea water surrounding it.");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\non the computer is a text recorded conversation between a company called offshore marine research and OMR sector C central");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\nOMR sector C central: What do you mean you have terminated the surface route out of our escape pod?! reroute them now!");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOffshore Marine Research: I'm sorry but we cannot risk these creatures reaching the surface without information on how we can stop them from spreading,\nsend us the intel you have gathered on these things and we will reroute your life pods course.");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOMR sector C central: To hell with that, our power is running out! we don't have much time before we are stranded in darkness!");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOffshore Marine Research: If thats the case, we will give permission to activate the escape pod on the central_computer once we are sure humanity is not in threat by these creatures.");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOMR sector C central: And how long will that take?");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOffshore Marine Research: With the depth of your sector, and the the infomation we currently have gathered. expect 4 hours until a response.");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOMR sector C central: 4 hours?! in the unlikely chance we are not dead our power will be offline by then.");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOffshore Marine Research: what about your backups?");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOMR sector C central: the backups? those rusty tin cans? you think I'm going to put my life on those ancient artifacts?!");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOffshore Marine Research: Unless you can propose another idea, that's your only choice.");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOMR sector C central: Damn you! damn this whole company! the only thing you care about is your rep...");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOffshore Marine Research: Hello? sector C do you copy?");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOMR sector C central:.......*arh*..........I can't.......................*ARRRHHHGGG*");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\n\nOffshore Marine Research: Sector C...?");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\nAs you finish reading the last message, you catch a glimpse of a figure rush past one of the security cameras shown on the moniter above.\n As you stare at the monitor you begin to realise that there are multiple figures on the cameras, darting back and forth.\n Long, shadowy, and thin creatures scurry across the base. just as you were about to get a good look at one, the power shuts off.");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("damn! if you were quick enough, maybe you could have activated the pod before the power went off. the conversation does not have a time it was recorded, and you do not how long you were past out for. It's a possiblity 4 hours might have already past.");
                    System.Threading.Thread.Sleep(5000);
                    computer = true;
                }
                else if (playerVariable == "examine security_guard" && guard == true)
                {
                    Console.WriteLine("you already examined the guard");
                }
                else if (playerVariable == "examine central_computer" && computer == true)
                {
                    Console.WriteLine("you cant examine the computer, the power shut off");
                }
                else
                {
                    Console.WriteLine("\nsorry, I didnt understand that. remember to type '_' for the spaces.");
                }

                if (computer == true && guard == true)
                {
                    Console.WriteLine("\nIt would seem like there is no easy way to escape this place, your going to have to figure a way out on your own.\n\n However this is not the only room available to you, the base is an amalgamation of corridors and rooms. If you wish to move, the command is move and then choose your directiong by typing N,S,E or W. ");
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("\nthis is where your story begins, remember if you are ever stuck, type 'help' for any guidence (its recommended that you check out the help section for further commands).");
                    System.Threading.Thread.Sleep(5000);
                    return;
                }

            }
            #endregion
        }
        #endregion
    }
}
