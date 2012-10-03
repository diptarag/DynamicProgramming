using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace DynamicProgramming
{
    public class ProblemMetaData
    {

#if DEBUG
        readonly string xmlFilePath="..\\..\\ProblemDescription.xml";
#else
        readonly string xmlFilePath="ProblemDescription.xml";
#endif

        #region [Data Members]
        /// <summary>
        /// XML Document where problem metadata is stored
        /// </summary>
        XDocument doc;

        /// <summary>
        /// Particular problem element
        /// </summary>
        XElement problemElement;

        /// <summary>
        /// Particular Problem ID, the type of ID is Global.Problems enum
        /// </summary>
        public Global.Problems ProblemID { get; private set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Constructor to initialize properties
        /// </summary>
        /// <param name="problemID">ID of the particular problem, must be of type Problems enum</param>
        public ProblemMetaData(Global.Problems problemID)
        {            
            doc = XDocument.Load(xmlFilePath);
            ProblemID = problemID;
            /*It is proved that .Where(predicate).First() is faster than .First(predicate), though it may sound very much counter-intuitive but it is true,
            * If any one can prove me otherwise or explain this weird behaviour, I am all ears
            * My guess is .First uses simple foreach loop whereas .Where uses many different iterators for different purposes, it may give some edge (by optimizing) to .Where
                * - Diptarag
            */
            try
            {
                problemElement = doc.Descendants("Problem").Where(node => Convert.ToInt32(node.Attribute("Id").Value) == (int)ProblemID).First();
            }
            catch
            {
                throw new DynamicProgrammingException("It seems Id attribute of Problem element has been changed in ProblemDescription.xml or some element is deleted. Please do not change the content of the xml");
            }
        }
        #endregion

        #region [Public Methods]
        /// <summary>
        /// Get the Title or Name of the problem
        /// </summary>
        /// <returns>Name of the problem</returns>
        public string GetName()
        {
            try
            {
                return problemElement.Element("Name").Value;
            }
            catch
            {
                throw new DynamicProgrammingException("It seems Name element of the particular Problem is deleted in ProblemDescription.xml. Please do not change the content of the xml");
            }
        }

        /// <summary>
        /// Get the Description of the Problem
        /// </summary>
        /// <returns>Description of the problem</returns>
        public string GetDescription()
        {
            try
            {
                return problemElement.Element("Description").Value;
            }
            catch
            {
                throw new DynamicProgrammingException("It seems Description element of the particular Problem is deleted in ProblemDescription.xml. Please do not change the content of the xml");
            }
        }

        /// <summary>
        /// Get Instruction for user, how to execute and how to give input 
        /// </summary>
        /// <returns>Instruction for user</returns>
        public string GetInstruction()
        {
            try
            {
                return problemElement.Element("Instruction").Value;
            }
            catch
            {
                throw new DynamicProgrammingException("It seems Instruction element of the particular Problem is deleted in ProblemDescription.xml. Please do not change the content of the xml");
            }
        }

        /// <summary>
        /// Get Default Implementation for the problem if user do not select any or select invalid option
        /// </summary>
        /// <returns>Default Sub Method Number</returns>
        public int GetDefaultSubMethod()
        {
            try
            {
            return Convert.ToInt32(problemElement.Element("Implementations").Elements("Method").Where(node => node.Attribute("Default") != null 
                && node.Attribute("Default").Value.Equals("True", StringComparison.OrdinalIgnoreCase)).First().Attribute("Id").Value);
            }
            catch
            {
                throw new DynamicProgrammingException("It seems Default attribute has been removed/changed in Implementation element of the particular Problem in ProblemDescription.xml. Please do not change the content of the xml");
            }

            /* I used a very simple approach here to check for the existence of the Attribute Default &
             * if you find this unconvincing, here is a more LINQ-ish approach for you - 
             * string defaultMethod = (from elem in problemElement.Element("Implementations").Elements("Method")
                                        where elem.Attributes("Default").Any(attr => attr.Value.Equals("True", StringComparison.OrdinalIgnoreCase))
                                        select elem.Attribute("Id").Value).First();
             * 
             * Another thing to note instead of using problemElement.Element("Implementations").Elements("Method") I could have simply used
             * problemElement.Descendants("Method") but that would give me all the "Method" elements regardless of it's parent, it's true that currently only Implementations
             * has Method, so with present structure it would work like a charm, but I could not decide whether I will user Method for some other purpose inside some other element
             * in future, so u know better to be safe than .. */
        }

        /// <summary>
        /// Get various implementations available for this particular problem
        /// </summary>
        /// <returns>a key value pair of implementations id and implementations name</returns>
        public Dictionary<int, string> GetImplementations()
        {
            try
            {
                return (from method in problemElement.Element("Implementations").Elements("Method")
                        select new { key = Convert.ToInt32(method.Attribute("Id").Value), value = method.Attribute("Name").Value }).ToDictionary(e => e.key, e => e.value);
            }
            catch
            {
                throw new DynamicProgrammingException("It seems Implementation element of the particular Problem has been changed in ProblemDescription.xml. Please do not change the content of the xml");
            }
        }
        #endregion

    }
}
