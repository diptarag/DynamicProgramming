using System;
using System.Linq;

namespace DynamicProgramming
{
    public class RodCutting : Problem
    {        
        int methodNumber;        
        int numberOfCuts;
        int[] cost;        

        public RodCutting()
            : base(Global.Problems.RodCutting)
        {
            methodNumber = base.GetImplementationID();
            ReadInputFile();
        }

        public override void Compute()
        {
                        
        }

        public override void ReadInputFile()
        {
            using (FileHandler inputFile = new FileHandler(Global.IOMode.Input, true))
            {
                try
                {
                    numberOfCuts = int.Parse(inputFile.FetchLine(1));
                }
                catch
                {
                    throw new DynamicProgrammingException("In Rod-Cutting problem first line of the input text should always be a valid number which represent number of cuts to make.");
                }

                try
                {
                    cost = inputFile.FetchLine(2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                }
                catch
                {
                    throw new DynamicProgrammingException("In Rod-Cutting problem second line of the input text should always be the array of cost (integer) seperated by blankspace");
                }                
            }
        }
    }
}
