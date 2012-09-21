using System;

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
        }

        public override void Compute()
        {
            throw new NotImplementedException();
        }

        public override void ReadInputFile()
        {
            throw new NotImplementedException();
        }
    }
}
