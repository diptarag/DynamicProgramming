using System;
using System.Collections.Generic;

namespace DynamicProgramming
{
    public abstract class Problem
    {
        ProblemMetaData metaData;
        protected string Name { get; private set; }
        protected string Description { get; private set; }
        protected Dictionary<int, string> Implementations { get; private set; }
        protected string Instruction { get; private set; }
        protected int DefaultSubMethod { get; private set; }
        protected Global.Problems ProblemID { get; private set; }

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
                        if (retVal < Implementations.Count)
                            return retVal;
                    }
                    else
                        CUI.ErrorMessage();
                }
            }
        }

        public abstract void ReadInputFile();
        public abstract void Compute();
    }
}
