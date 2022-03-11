using System;

namespace AdventureGame
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "æ", "ø", "å" };
            string[] symbols = { "!", "@", "#", "$", "%", "&", "/", "=", "?" };

            Welcome();
            string player_name = PlayerName(); 
            string noteForPlayer = null;                                 //store hint displayed as message for player
            string solution = null;                                      //store actual password

            Random rand = new Random();

            int password_length = player_name.Length + 2;                 //length of array 'password'
            string[] pass = new string[password_length];                  //password array with a length corresponding to the number of letters in player's name.
            string[] decrypted_pass = new string[password_length];
            int arr_index = 0;                                            //used to specify an index-number
            int shift = 0;                                                //shift amount


            foreach (char c in player_name)
            {

                shift++;
                int index = Array.IndexOf(alphabet, c.ToString());        //get hold of letter in name and find index number of corresponding letter in alphabet
                int position = index + shift;
                int rand_choice = rand.Next(0, 3);
                pass[arr_index] = alphabet[position % 29];

                if (rand_choice == 2)
                {

                    pass[arr_index] = alphabet[position % 29].ToUpper();
                    decrypted_pass[arr_index] = alphabet[index].ToUpper();

                }
                else
                {

                    pass[arr_index] = alphabet[position % 29];
                    decrypted_pass[arr_index] = alphabet[index];

                }

                arr_index++;

            }

  

            //add first symbol and append to arrays
            int first_symbol_index = rand.Next(0, 9);
            string symbol_one = symbols[first_symbol_index];
            pass[arr_index] = symbol_one;
            decrypted_pass[arr_index] = symbol_one;

            //add second symbol and append to arrays
            arr_index++;
            int second_symbol_index = rand.Next(0, 9);
            string symbol_two = symbols[second_symbol_index];
            pass[arr_index] = symbol_two;
            decrypted_pass[arr_index] = symbol_two;

            //create strings from decrypted/encrypted password arrays
            string[] PW = new string[2];
            PW[0] = string.Join("", decrypted_pass);
            PW[1] = string.Join("", pass);

            noteForPlayer = PW[0];
            solution = PW[1];

            StartConversation(player_name, noteForPlayer);

            bool device_locked = true;
            while (device_locked)
            {
                string passAttempt = (PasswordAttempt(noteForPlayer));

                if (passAttempt == "h")
                {
                    Help();
                }
                else if (passAttempt == solution)
                {
                    device_locked = Unlocked();
                }
                else
                {
                    TryAgain();
                }
            }

        }
        public static void Welcome()
        {
            Console.WriteLine(@"    
    XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 XXXXXXXXXXXXXXXXXX         XXXXXXXX
XXXXXXXXXXXXXXXX              XXXXXXX
XXXXXXXXXXXXX                   XXXXX
 XXX     _________ _________     XXX      
  XX    I  _xxxxx I xxxxx_  I    XX        
 ( X----I         I         I----X )       <---- CEASAR
( +I    I      00 I 00      I    I+ )
 ( I    I    __0  I  0__    I    I )
  (I    I______ /   \_______I    I)
   I           ( ___ )           I        ____________________________
   I    _  :::::::::::::::  _    i       /
    \    \___ ::::::::: ___/    /       < .. Greetings, stranger! I'm Ceasar. What is your name? 
     \_      \_________/      _/         \____________________________
       \        \___,        /
         \                 /
          |\             /|
          |  \_________/  |");
        }
        public static string PlayerName()
        {
            string player_input = null;
            bool name_incorrect = true;
            while (name_incorrect)
            {

                Console.Write("\nEnter your name (letters only): ");
                player_input = Console.ReadLine();

                Console.WriteLine("\nYou have entered '" + player_input + "'. Do you want to continue? 'y'/'n'");
                string name_confirm = Console.ReadLine();

                if (name_confirm == "y" || name_confirm == "Y")
                {
                    name_incorrect = false;
                }
                else
                {
                    Console.Write("Try again. ");
                }
                player_input = player_input.ToLower();
            }
            return player_input;

        }
        public static void StartConversation(string name, string note)
        {
            Console.Clear();
            Console.WriteLine(@$"    
    XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 XXXXXXXXXXXXXXXXXX         XXXXXXXX
XXXXXXXXXXXXXXXX              XXXXXXX
XXXXXXXXXXXXX                   XXXXX
 XXX     _________ _________     XXX      
  XX    I  _xxxxx I xxxxx_  I    XX        
 ( X----I         I         I----X )           
( +I    I      00 I 00      I    I+ )
 ( I    I    __0  I  0__    I    I )        ___________________________________
  (I    I______ /   \_______I    I)        /  
   I           ( ___ )           I        / 
   I    _  :::::::::::::::  _    i       /  .. Ah, {name}! Yes, I've been waiting for you!
    \    \___ ::::::::: ___/    /       <  You are allegedly becomming a skillful coder - or so my intel says. 
     \_      \_________/      _/         \  Have you sought me out to advance your skills?
       \        \___,        /            \
         \                 /               \___________________________________
          |\             /|                 
          |  \_________/  |");
            Console.WriteLine("\n");
            Console.Write("You: ");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(@$"    
    XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 XXXXXXXXXXXXXXXXXX         XXXXXXXX
XXXXXXXXXXXXXXXX              XXXXXXX
XXXXXXXXXXXXX                   XXXXX
 XXX     _________ _________     XXX      
  XX    I  _xxxxx I xxxxx_  I    XX        
 ( X----I         I         I----X )           
( +I    I      00 I 00      I    I+ )
 ( I    I    __0  I  0__    I    I )        ___________________________________
  (I    I______ /   \_______I    I)        /  
   I           ( ___ )           I        / 
   I    _  :::::::::::::::  _    i       /  .. Oh! Okay. Well you may or you may not like it -
    \    \___ ::::::::: ___/    /       <  but I have a cipher-challenge for you. Decrypt the password! 
     \_      \_________/      _/         \  Here! Take this device and break into it if you can.
       \        \___,        /            \
         \                 /               \___________________________________
          |\             /|                 
          |  \_________/  |");
            Console.WriteLine("\nPress any key to accept the device from Ceasar");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(@$"    
    XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 XXXXXXXXXXXXXXXXXX         XXXXXXXX
XXXXXXXXXXXXXXXX              XXXXXXX
XXXXXXXXXXXXX                   XXXXX
 XXX     _________ _________     XXX      
  XX    I  _xxxxx I xxxxx_  I    XX        
 ( X----I         I         I----X )           
( +I    I      00 I 00      I    I+ )
 ( I    I    __0  I  0__    I    I )        ___________________________________
  (I    I______ /   \_______I    I)        /  
   I           ( ___ )           I        / 
   I    _  :::::::::::::::  _    i       /  Oh, by the way. Here's a note. 
    \    \___ ::::::::: ___/    /       <  I'm not going to tell you that it holds an encrypted stri..
     \_      \_________/      _/         \  Oops. Oh well. 
       \        \___,        /            \
         \                 /               \___________________________________
          |\             /|                 
          |  \_________/  |");
            Console.WriteLine("\nPress any key to take note.\n");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(@$"
 _ _ _ _ _ _ _ _
|               |
                                    
|               |
     {note}      
|               |
                 
|_ _ _ _ _ _ _ _|");
            Console.WriteLine("\n\nPress any key to open the device\n\n");
            Console.ReadKey();
        }
        public static string PasswordAttempt(string note)
        {
            Console.Clear();
            Console.Write(@$"	
             ____________________________________________________
            /                                                    \
           |    _____________________________________________     |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |            Username:Lrd_Csar_46             |    |
           |   |            Password:___________             |    |                   Note
           |   |                                             |    |              _ _ _ _ _ _ _ _
           |   |            ('h' for help)                   |    |             |               |
           |   |                                             |    |
           |   |                                             |    |             |               |
           |   |                                             |    |                  {note} 
           |   |                                             |    |             |               |
           |   |_____________________________________________|    |
           |                                                      |             |_ _ _ _ _ _ _ _|
            \_____________________________________________________/
                   \_______________________________________/
                _______________________________________________                     
             _-'    .-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.  --- `-_
          _-'.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.--.  .-.-.`-_
       _-'.-.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-`__`. .-.-.-.`-_
    _-'.-.-.-.-. .-----.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-----. .-.-.-.-.`-_
 _-'.-.-.-.-.-. .---.-. .-----------------------------. .-.---. .---.-.-.-.`-_
:-----------------------------------------------------------------------------");

            Console.Write("\nPassword:");

            string password_attempt = Console.ReadLine();
            return password_attempt;
        }
        public static void Help()
        {
            Console.Clear();
            Console.Write(@"	
             ____________________________________________________
            /                                                    \
           |    _____________________________________________     |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |   Lrd_Csar_46: Symbols stay the same,       |    |
           |   |                letters they change,         |    |
           |   |                each time differently,       |    |
           |   |                but you are too lame!        |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |_____________________________________________|    |
           |                                                      |
            \_____________________________________________________/
                   \_______________________________________/
                _______________________________________________
             _-'    .-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.  --- `-_
          _-'.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.--.  .-.-.`-_
       _-'.-.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-`__`. .-.-.-.`-_
    _-'.-.-.-.-. .-----.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-----. .-.-.-.-.`-_
 _-'.-.-.-.-.-. .---.-. .-----------------------------. .-.---. .---.-.-.-.`-_
:-----------------------------------------------------------------------------");

            Console.WriteLine("\nHint 1: \nNotice the casing.");
            Console.WriteLine(@"Hint 2: 
[1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16  17  18  19  20  21  22  23  24  25  26  27  28  29]
[a   b   c   d   e   f   g   h   i   j   k   l   m   n   o   p   q   r   s   t   u   v   w   x   y   z   æ   ø   å]");
            Console.Write("Double tap a key to return to previous screen or press 'm' to show more hints (spoiler alert): ");
            string spoiler = Console.ReadLine();
            if (spoiler == "m")
            {
                Console.Write("\nHint 4: Each letter in the string represents another letter.");
                Console.Write("\nHint 5: https://en.wikipedia.org/wiki/Caesar_cipher");
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
            Console.ReadKey();
        }
        public static void TryAgain()
        {
            Console.Clear();
            Console.Write(@"	
             ____________________________________________________
            /                                                    \
           |    _____________________________________________     |
           |   |                                             |    |
           |   |                  WRONG! TRY AGAIN!          |    |
           |   |              ~       . ...... .        ~    |    |
           |   |               ~   .               .   ~     |    |
           |   |                ~.    __       __    .~      |    |
           |   |               .     @@@       @@@     .     |    |
           |   |              .            /            .    |    |
           |   |              .           /             .    |    |
           |   |              .          /              .    |    |
           |   |               .     *  ----     *     .     |    |
           |   |                .     *         *     .      |    |
           |   |                  .     NOOOOOB     .        |    |  
           |   |                     .           .           |    | 
           |   |                          ...                |    |  
           |   |_____________________________________________|    |
           |                                                      |
            \_____________________________________________________/
                   \_______________________________________/
                _______________________________________________
             _-'    .-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.  --- `-_
          _-'.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.--.  .-.-.`-_
       _-'.-.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-`__`. .-.-.-.`-_
    _-'.-.-.-.-. .-----.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-----. .-.-.-.-.`-_
 _-'.-.-.-.-.-. .---.-. .-----------------------------. .-.---. .---.-.-.-.`-_
:-----------------------------------------------------------------------------");

            Console.WriteLine("\nPress a key to return to try again");
            Console.ReadKey();
      

        }
        public static bool Unlocked()
        {
            Console.Clear();
            Console.WriteLine(@"//
             ____________________________________________________
            /                                                    \
           |    _____________________________________________     |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                    SUCCES!                  |    |
           |   |              You have completed             |    |
           |   |                  this level!                |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |                                             |    |
           |   |_____________________________________________|    |
           |                                                      |
            \_____________________________________________________/
                   \_______________________________________/
                _______________________________________________
             _-'    .-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.  --- `-_
          _-'.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.--.  .-.-.`-_
       _-'.-.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-`__`. .-.-.-.`-_
    _-'.-.-.-.-. .-----.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-----. .-.-.-.-.`-_
 _-'.-.-.-.-.-. .---.-. .-----------------------------. .-.---. .---.-.-.-.`-_
:-----------------------------------------------------------------------------");
            Console.WriteLine("\nPress a key to continue");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(@"    
    XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 XXXXXXXXXXXXXXXXXX         XXXXXXXX
XXXXXXXXXXXXXXXX              XXXXXXX
XXXXXXXXXXXXX                   XXXXX
 XXX     _________ _________     XXX      
  XX    I  _xxxxx I xxxxx_  I    XX        
 ( X----I         I         I----X )       
( +I    I      00 I 00      I    I+ )
 ( I    I    __0  I  0__    I    I )
  (I    I______ /   \_______I    I)
   I           ( ___ )           I        ____________________________
   I    _  :::::::::::::::  _    i       /
    \    \___ ::::::::: ___/    /       < .. Yikes! I'm glad I deleted my browser-history. You're too good!  
     \_      \_________/      _/         \____________________________
       \        \___,        /
         \                 /
          |\             /|
          |  \_________/  |");

            return false;
        }
        
    }
}