using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DynamicProgramming
{
    public abstract class Problem
    {
        #region [Member Variable]
        /// <summary>
        /// Metadata about the problem
        /// </summary>
        ProblemMetaData metaData;

        /// <summary>
        /// Problem Name
        /// </summary>
        protected string Name { get; private set; }

        /// <summary>
        /// Description of the Problem
        /// </summary>
        protected string Description { get; private set; }

        /// <summary>
        /// A key value pair for the available implementations of the problem
        /// </summary>
        protected Dictionary<int, string> Implementations { get; private set; }

        /// <summary>
        /// Instruction given to user, generally input specification
        /// </summary>
        protected string Instruction { get; private set; }

        /// <summary>
        /// If no implementation is chosen by the user, this is the default implementation to execute
        /// </summary>
        protected int DefaultSubMethod { get; private set; }

        /// <summary>
        /// ID of the Problem
        /// </summary>
        protected Global.Problems ProblemID { get; private set; }        
        #endregion

        #region [Constructor]
        /// <summary>
        /// Constructor for the Problem class, initializes all data regarding one Particular problem
        /// </summary>
        /// <param name="problemID">ID of the Problem</param>
        protected Problem(Global.Problems problemID)
        {            
            ProblemID = problemID;
            metaData = new ProblemMetaData(problemID);
            Name = metaData.GetName();
            Description = metaData.GetDescription();
            Implementations = metaData.GetImplementations();
            Instruction = metaData.GetInstruction();
            DefaultSubMethod = metaData.GetDefaultSubMethod();
        }
        #endregion

        #region [Virtual Methods]
        /// <summary>
        /// Get The Implementation ID of the available implementations from user
        /// </summary>
        /// <returns>Index of the Implementations chosen by user</returns>
        public virtual int GetImplementationID()
        {
            string returnVal;
            int retVal;
            while (true)
            {
                returnVal = CUI.ShowImplementations(Name, Implementations);
                if (string.IsNullOrEmpty(returnVal))
                    return DefaultSubMethod;
                else
                {
                    if (int.TryParse(returnVal, out retVal))
                    {
                        if (retVal <= Implementations.Count)
                            return retVal;
                    }
                    else
                        CUI.ErrorMessage();
                }
            }
        }

        /// <summary>
        /// Store the result of the execution
        /// </summary>
        /// <param name="output">Result to be stored</param>
        public virtual void StoreResult(params object[] output)
        {
            using (FileHandler outputFile = new FileHandler(Global.IOMode.Output))
            {
                foreach (object obj in output)
                    outputFile.WriteContent(obj);
            }
            CUI.ShowGeneralInformation("\n\nPlease open output.txt to view the result.");
        }
        #endregion 

        #region [Abstract signature]
        /// <summary>
        /// Read and parse input file
        /// </summary>
        public abstract void ReadInputFile();
        /// <summary>
        /// Solve the problem
        /// </summary>
        public abstract void Compute();
        #endregion
    }
}
