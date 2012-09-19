using System;

namespace DynamicProgramming
{
    public class RodCutting : IDynamic
    {
        int methodNumber;
        int totalMethods;
        int numberOfCuts;
        int[] cost;
        public int MethodNumber
        {
            get
            {
                return methodNumber;
            }
            set
            {
                if (value > totalMethods)
                    methodNumber = 0;
                else
                    methodNumber = value;
            }
        }

        public void ComputeAndShow()
        {
            throw new NotImplementedException();
        }
    }
}
