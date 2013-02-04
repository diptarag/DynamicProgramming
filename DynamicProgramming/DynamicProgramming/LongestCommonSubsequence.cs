using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace DynamicProgramming
{
    public class LongestCommonSubsequence : Problem
    {
        #region [Private Member Variable]
        /// <summary>
        /// First sequence : available from input.txt
        /// </summary>
        string[] firstSequence;

        /// <summary>
        /// Second sequence : available from input.txt
        /// </summary>
        string[] secondSequence;        

        /// <summary>
        /// Available direction which will be used in directions table
        /// </summary>
        enum Direction
        {
            Up,
            Diagonal,
            Left
        }

        /// <summary>
        /// Auxilary length table to be used for calculation
        /// </summary>
        int[,] length;

        /// <summary>
        /// Auxilary direction table to be used for calculation
        /// </summary>
        Direction[,] directions;

        /// <summary>
        /// Stopwatch to measure time taken by each method
        /// </summary>
        Stopwatch stopWatch;
        #endregion

        #region [Constructor]
        public LongestCommonSubsequence()
            : base(Global.Problems.LongestCommonSubSequence)        
        {
            stopWatch = new Stopwatch();
            ReadInputFile();
        }
        #endregion

        #region [Overridden Methods]
        public override void ReadInputFile()
        {
            using (FileHandler inputFile = new FileHandler(Global.IOMode.Input, true))
            {
                try
                {
                    firstSequence = inputFile.FetchLine(1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                }
                catch
                {
                    throw new DynamicProgrammingException("In Longest Common Subsequence problem first line of the input text should always be a valid sequence seperated by blank space.");
                }

                try
                {
                    secondSequence = inputFile.FetchLine(2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                }
                catch
                {
                    throw new DynamicProgrammingException("In Longest Common Subsequence problem second line of the input text should always be a valid sequence seperated by blank space.");
                }
            }
        }

        public override void Compute()
        {
            switch (base.GetImplementationID())
            {
                case 1: base.StoreResult(LCSGeneral());
                    break;
            }
            CUI.ShowTimeElapsed(stopWatch.Elapsed);
        }
        #endregion

        #region [Implementaions]        
        private List<string> LCSGeneral()
        {
            stopWatch.Start();
            length = new int[firstSequence.Length + 1, secondSequence.Length + 1];
            directions = new Direction[firstSequence.Length + 1, secondSequence.Length + 1];
            List<string> resultSequence = new List<string>();            

            for(int i = 1; i <= firstSequence.Length; i++)
                for (int j = 1; j <= secondSequence.Length; j++)
                {
                    if (firstSequence[i-1].Equals(secondSequence[j-1]))
                    {
                        length[i, j] = length[i - 1, j - 1] + 1;
                        directions[i, j] = Direction.Diagonal;
                    }
                    else if (length[i - 1, j] >= length[i, j - 1])
                    {
                        length[i, j] = length[i - 1, j];
                        directions[i, j] = Direction.Up;
                    }
                    else 
                    {
                        length[i, j] = length[i, j - 1];
                        directions[i, j] = Direction.Left;
                    }
                }

            GenerateLCS(resultSequence, firstSequence.Length, secondSequence.Length);
            stopWatch.Stop();
            return resultSequence;
        }

        private void GenerateLCS(List<string> resultSequence, int i, int j)
        {
            if (i == 0 || j == 0)
                return;
            else if (directions[i, j] == Direction.Diagonal)
            {
                GenerateLCS(resultSequence, i - 1, j - 1);
                resultSequence.Add(firstSequence[i - 1]);
            }
            else if (directions[i, j] == Direction.Up)
                GenerateLCS(resultSequence, i - 1, j);
            else
                GenerateLCS(resultSequence, i, j - 1);
        }
        #endregion

    }
}
