using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicProgramming
{
    class CUI
    {
        /// <summary>
        /// Screen which will be shown at the start of the program
        /// </summary>
        /// <returns>Option chosen by user</returns>
        public static string StartScreen()
        {
            Console.Clear();
            Console.WriteLine("Hi \n This is an effort to analyze and implement some popular problems which can be solved using Dynamic Programming approach.");
            Console.WriteLine("\n\n1. Problem Listings\n2. About Author & Disclaimer\n3. Quit");
            Console.Write("\n Please enter your choice (number only) : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// List all problems
        /// </summary>
        /// <returns>Option chosen by user</returns>
        public static string ShowProblemListings(Dictionary<Global.Problems, string> problems)
        {            
            Console.Clear();
            Console.WriteLine("\nAvailable Problems : \n");
            foreach (KeyValuePair<Global.Problems, string> problem in problems)
            {
                Console.WriteLine("\n" + (int)problem.Key + ". " + problem.Value);
            }
            Console.Write("\n\nEnter the index of the problem you want to solve : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// About Author & Disclaimer section
        /// </summary>
        public static void About()
        {
            Console.Clear();
            Console.WriteLine("Hi\nMy name is Diptarag.\n\nIf you care to go through the code then I suggest you read the below section.\n\nI initiated this childish stuff in order to satisfy my love for algorithm analysis & programming.\nI do not claim that all the coding done here are optimal, in fact little attention is paid towards optimizing the code, my initial attempt was to study some popular problems.");
            Console.WriteLine("\nI took a lot of help from several books and internet resources, I will try my best to mention those resources here, but if I miss some, you notice and you get angry (in that order), please inform me.");
            Console.WriteLine("\nFor any reason if you think you need to contact me (be it for threatening me not to write another single line of crap or anything similar) Please feel free to contact me at :: diptarag@gmail.com.");
            Console.WriteLine("\nAny constructive criticism, suggestion will be taken gratefully.");
            Console.Write("\n\n\nPress any key to continue....");
            Console.ReadKey();
        }

        /// <summary>
        /// General error message when user chooses wrong option
        /// </summary>
        public static void ErrorMessage()
        {
            Console.Write("\nOption specified is not recognized. Press any key to try again ... ");
            Console.ReadKey();
        }

        /// <summary>
        /// Show custom exception message on the screen
        /// </summary>
        /// <param name="errorMessage">Custom exception message</param>
        public static void ShowException(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Console.WriteLine("\nPlease try to rectify any problem that may result this error. \nIf you are getting this error message repeatedly kindly contact diptarag@gmail.com (maybe you are running this application for fun but I need to know my errors, please mail me, it's my request) \nPress any key to try again ..... ");
            Console.ReadKey();
        }

        /// <summary>
        /// Show available implementations of a particular problem
        /// </summary>
        /// <param name="problemName">Name of the Problem</param>
        /// <param name="methods">Available implementations</param>
        /// <returns>Option chosen by user</returns>
        public static string ShowImplementations(string problemName, Dictionary<int, string> methods)
        {
            StringBuilder listImplementations = new StringBuilder();
            Console.Clear();
            Console.WriteLine(problemName);
            listImplementations.Append("\nBelow are some implementations for this specific problem : \n");
            foreach (int key in methods.Keys)            
                listImplementations.Append("\n").Append(key).Append(". ").Append(methods[key]);
            listImplementations.Append("\nPlease choose your option and hit enter (Hit enter if you want me to choose the best method for you ... ");
            Console.WriteLine(listImplementations.ToString());
            return Console.ReadLine();
        }

        /// <summary>
        /// Show total time taken to complete one operations
        /// </summary>
        /// <param name="elapsedTime">Elapsed time</param>
        public static void ShowTimeElapsed(TimeSpan elapsedTime)
        {
            Console.WriteLine("\nTime taken to compute the result : " + elapsedTime.TotalMilliseconds+" ms");
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Show any information
        /// </summary>
        /// <param name="objInfo">Information to show</param>
        public static void ShowGeneralInformation(object objInfo)
        {
            Console.WriteLine(objInfo);
        }       
    }
}
