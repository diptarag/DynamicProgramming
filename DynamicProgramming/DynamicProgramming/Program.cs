using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                switch (CUI.StartScreen())
                {
                    case "1": string str = CUI.ShowProblemListings();
                        break;
                    case "2": CUI.About();
                        break;
                    case "3": System.Environment.Exit(0);
                        break;
                    default: CUI.ErrorMessage();
                        break;
                }
            }
        }
    }
}
