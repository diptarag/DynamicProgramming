using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace DynamicProgramming
{
    public class RodCutting : Problem
    {
        #region [Private Member Variable]
        /// <summary>
        /// Implementations ID chosen by user
        /// </summary>
        int methodNumber;        

        /// <summary>
        /// length of cut, available from input.txt
        /// </summary>        
        int lengthOfCut;

        /// <summary>
        /// Array of cost, available from input.txt
        /// </summary>
        double[] costs;

        /// <summary>
        /// Stopwatch to measure time taken by each method
        /// </summary>
        Stopwatch stopWatch;
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initialize metadata regarding Rod-Cutting problem
        /// Get the Implementation Id chosen by user
        /// Read and Parse Input file
        /// </summary>
        public RodCutting()
            : base(Global.Problems.RodCutting)
        {
            methodNumber = base.GetImplementationID();
            stopWatch = new Stopwatch();
            ReadInputFile();
        }
        #endregion

        #region [Overriden Methods]
        /// <summary>
        /// Solve the problem and store the result using the implementations chosen by user
        /// </summary>
        public override void Compute()
        {
            if (lengthOfCut > costs.Length)
                throw new DynamicProgrammingException("\nLength of Cut can not be more than the cost array provided, if you want to cut the rod into a length x then u must provide separate cost of each length cut i.e. cost array must be greater than x.");
            switch (methodNumber)
            {
                case 1: base.StoreResult(SimpleRecursive());                    
                    break;
                case 2: base.StoreResult(TopDownMemoization());
                    break;
                case 3: base.StoreResult(BottomUpMemoization());
                    break;
                case 4: StoreResult(BottomUpSolution());
                    break;
            }
            CUI.ShowTimeElapsed(stopWatch.Elapsed);
                        
        }

        /// <summary>
        /// Read and Parse Input File
        /// </summary>
        public override void ReadInputFile()
        {
            using (FileHandler inputFile = new FileHandler(Global.IOMode.Input, true))
            {
                try
                {
                    lengthOfCut = int.Parse(inputFile.FetchLine(1));
                }
                catch
                {
                    throw new DynamicProgrammingException("In Rod-Cutting problem first line of the input text should always be a valid number which represent number of cuts to make.");
                }

                try
                {
                    costs = inputFile.FetchLine(2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => double.Parse(x)).ToArray();
                }
                catch
                {
                    throw new DynamicProgrammingException("In Rod-Cutting problem second line of the input text should always be the array of cost (number) seperated by blankspace");
                }                
            }
        }

        public override void StoreResult(params object[] obj)
        {            
            Tuple<double[], int[]> solutions;
            
            try
            {
                solutions = (Tuple<double[], int[]>)obj[0];
            }
            catch
            {
                throw new DynamicProgrammingException("\n\nLogical Error at Rod Cutting Default Problem. The Optimal Solution Table is not in a correct format.");
            }            

            using (FileHandler outputFile = new FileHandler(Global.IOMode.Output))
            {
                outputFile.WriteContent("If you cut a " + lengthOfCut + " length rod you can generate a maximum revenue : " + solutions.Item1[lengthOfCut - 1] + ". And you will get this result if you cut the rod at these pieces -");
                while (lengthOfCut > 0)
                {
                    outputFile.WriteContent(solutions.Item2[lengthOfCut - 1]);
                    lengthOfCut -= solutions.Item2[lengthOfCut - 1];
                }
            }            
        }
        #endregion

        #region [Private Implmentations]
        /*
         * If you are looking into the code, beware! In this implementation section you will often find a piece of code or one function calling another function which could be easily avoided
         * Actually here my key focus point was to measure the timings and performace of the algorithm, not C# coding
         * So dont go by the coding
         */

        /// <summary>
        /// Simple recursive implementation
        /// </summary>
        /// <returns>Optimal cost for the cut</returns>
        private double SimpleRecursive()
        {
            double optimalCost;
            stopWatch.Start();
            optimalCost = TopDownRecursiveCutRod(lengthOfCut, costs);
            stopWatch.Stop();
            return optimalCost;
        }

        /// <summary>
        /// Simple recursive approach to solve the problem
        /// Here the approach is - 
        /// To solve the original problem of size n, we solve smaller problems of
        /// the same type, but of smaller sizes. Once we make the first cut, we may consider
        /// the two pieces as independent instances of the rod-cutting problem. The overall
        /// optimal solution incorporates optimal solutions to the two related subproblems,
        /// maximizing revenue from each of those two pieces. We say that the rod-cutting
        /// problem exhibits optimal substructure: optimal solutions to a problem incorporate
        /// optimal solutions to related subproblems, which we may solve independently.
        /// In a related, but slightly simpler, way to arrange a recursive structure for the rodcutting
        /// problem, we view a decomposition as consisting of a first piece of length i
        /// cut off the left-hand end, and then a right-hand remainder of length n-i. Only
        /// the remainder, and not the first piece, may be further divided. We may view every
        /// decomposition of a length-n rod in this way: as a first piece followed by some
        /// decomposition of the remainder. This second method is implemented here.
        /// Here running time of program is T(n)=1+Summation(j=0:n-1)T(j) i.e. 2^n (initial 1 is for the call at the root
        /// </summary>
        /// <param name="lengthOfCut">Length of the total cut</param>
        /// <param name="costs">Cost array</param>
        /// <returns>Optimal Cost for the cut</returns>
        private double TopDownRecursiveCutRod(int lengthOfCut, double[] costs)
        {            
            if (lengthOfCut == 0)
                return 0;
            double cost = double.NegativeInfinity;
            for (int i = 1; i <= lengthOfCut; i++)
                cost = Math.Max(cost, costs[i - 1] + TopDownRecursiveCutRod(lengthOfCut - i, costs));
            return cost;
        }


        /// <summary>
        /// Top down with memoization approach
        /// </summary>
        /// <returns>Optimal Cost for the cut</returns>
        private double TopDownMemoization()
        {
            double optimalCost;            
            stopWatch.Start();
            optimalCost = TopDownRecursiveMemoization(lengthOfCut, costs, new Dictionary<int, double>());
            stopWatch.Stop();
            return optimalCost;
        }

        /// <summary>
        /// The dynamic-programming method works as follows. Having observed that a
        /// naive recursive solution is inefficient because it solves the same subproblems repeatedly,
        /// we arrange for each subproblem to be solved only once, saving its solution.
        /// If we need to refer to this subproblem’s solution again later, we can just look it
        /// up, rather than recompute it. Dynamic programming thus uses additional memory
        /// to save computation time; it serves an example of a time-memory trade-off. The
        /// savings may be dramatic: an exponential-time solution may be transformed into a
        /// polynomial-time solution. A dynamic-programming approach runs in polynomial
        /// time when the number of distinct subproblems involved is polynomial in the input
        /// size and we can solve each such subproblem in polynomial time.
        ///    The first approach is top-down with memoization.2 In this approach, we write
        /// the procedure recursively in a natural manner, but modified to save the result of
        /// each subproblem (usually in an array or hash table). The procedure now first checks
        /// to see whether it has previously solved this subproblem. If so, it returns the saved
        /// value, saving further computation at this level; if not, the procedure computes the
        /// value in the usual manner. We say that the recursive procedure has been memoized;
        /// it “remembers” what results it has computed previously.
        ///     The running time of this algorithm is also O(n^2), although this running time may be a little
        /// harder to see. Because a recursive call to solve a previously solved subproblem
        /// returns immediately, this algorithm solves each subproblem just once. It
        /// solves subproblems for sizes 0,1,....n. To solve a subproblem of size n, the for
        /// loop iterates n times. Thus, the total number of iterations of this for
        /// loop, over all recursive calls forms an arithmetic series,
        /// giving a total of O(n^2) iterations.
        /// </summary>
        /// <param name="lengthOfCut">Length of the total cut</param>
        /// <param name="costs">Cost array</param>
        /// <param name="internalCost">Auxilary memory to 'memoize'</param>
        /// <returns>Optimal Cost for the cut</returns>
        private double TopDownRecursiveMemoization(int lengthOfCut, double[] costs, Dictionary<int, double> internalCost)
        {
            double cost;
            if (internalCost.TryGetValue(lengthOfCut, out cost))
                return cost;
            if (lengthOfCut == 0)
                cost = 0;
            else
            {
                cost = double.NegativeInfinity;
                for (int i = 1; i <= lengthOfCut; i++)
                    cost = Math.Max(cost, costs[i - 1] + TopDownRecursiveMemoization(lengthOfCut - i, costs, internalCost));
                internalCost.Add(lengthOfCut, cost);
            }
            return cost;
        }

        /// <summary>
        /// Bottom Up With Memoization
        /// </summary>
        /// <returns>Optimal Cost for the cut</returns>
        private double BottomUpMemoization()
        {
            double optimalCost;
            double[] optimalCosts = new double[lengthOfCut + 1];
            stopWatch.Start();
            optimalCost = BottomUpCutRod(lengthOfCut, costs, optimalCosts);
            stopWatch.Stop();
            return optimalCost;
        }

        /// <summary>
        /// This approach typically depends
        /// on some natural notion of the “size” of a subproblem, such that solving any particular
        /// subproblem depends only on solving “smaller” subproblems. We sort the
        /// subproblems by size and solve them in size order, smallest first. When solving a
        /// particular subproblem, we have already solved all of the smaller subproblems its
        /// solution depends upon, and we have saved their solutions. We solve each subproblem
        /// only once, and when we first see it, we have already solved all of its
        /// prerequisite subproblems.
        /// Here running time of the algorithm is O(n^2) (theta is difficult to write here :P)
        /// This running time is due to the doubly loop strcuture of the algorithm
        /// </summary>
        /// <param name="lengthOfCut">Length of the total cut</param>
        /// <param name="costs">Cost array</param>
        /// <param name="optimalCosts">Auxilary memory to memoize</param>
        /// <returns>Optimal Cost for the cut</returns>
        private double BottomUpCutRod(int lengthOfCut, double[] costs, double[] optimalCosts)
        {
            /*
             * optimalCosts is an array which stores the optimal cost for the respective length, like optimal cost for length 0 is 0 (that's one main reason for using 1 as starting variable)
             * here the approach is solving smaller sub problems first, and then combine these smaller sub problem's result to generate bigger sub problem
             */            
            double cost;
            for (int i = 1; i <= lengthOfCut; i++)
            {
                cost = double.NegativeInfinity;
                for (int j = 1; j <= i; j++)
                    cost = Math.Max(cost, costs[j-1] + optimalCosts[i - j]);
                optimalCosts[i] = cost;
            }
            return optimalCosts[lengthOfCut];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Tuple<double[], int[]> BottomUpSolution()
        {
            
            double[] optimalCosts = new double[lengthOfCut + 1];
            int[] solutionPath = new int[lengthOfCut];
            stopWatch.Start();
            BottomUpCutRodValueAndSolution(lengthOfCut, costs, ref optimalCosts, ref solutionPath);
            stopWatch.Stop();
            return new Tuple<double[], int[]>(optimalCosts, solutionPath);
        }

        private void BottomUpCutRodValueAndSolution(int lengthOfCut, double[] costs, ref double[] optimalCosts, ref int[] solutionPath)
        {
            double cost;
            for (int i = 1; i <= lengthOfCut; i++)
            {
                cost = double.NegativeInfinity;
                for (int j = 1; j <= i; j++)                
                    if (cost < costs[j - 1] + optimalCosts[i - j])
                    {
                        cost = costs[j - 1] + optimalCosts[i - j];
                        solutionPath[i - 1] = j;
                    }
                optimalCosts[i] = cost;
            }
        }
        #endregion
    }
}
