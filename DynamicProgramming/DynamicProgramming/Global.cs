using System;

namespace DynamicProgramming
{
    public class Global
    {
        /// <summary>
        /// List of problems
        /// </summary>
        public enum Problems
        {
            None,
            RodCutting,
            AssemblyLineScheduling,
            MatrixChainMultiplication,
            LongestCommonSubSequence,
            OptimalBST,
            MultiStageGraphs,
            AllPairShortestPath,
            SingleSourceShortestPath,
            Knapsack01,
            TravellingSlesmanProblem
        }

        /// <summary>
        /// Possible IO Mode for File Operations
        /// </summary>
        public enum IOMode
        {
            Input,
            Output
        }
    }
}
