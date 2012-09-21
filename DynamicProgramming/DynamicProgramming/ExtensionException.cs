using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Security;

namespace DynamicProgramming
{
    public static class ExtensionException
    {
        /// <summary>
        /// Determines if one exception is fatal and should not be handled
        /// </summary>
        /// <param name="exception">Exception that needs to be determined for fatality</param>
        /// <returns>true if the exception is fatal, false otherwise</returns>
        public static bool IsFatal(this Exception exception)
        {
            while (exception != null)
            {
                if (exception as OutOfMemoryException != null && exception as InsufficientMemoryException == null
                    || exception as ThreadAbortException != null || exception as AccessViolationException != null
                    || exception as SEHException != null || exception as StackOverflowException != null)
                    return true;
                else
                {
                    if (exception as TypeInitializationException == null && exception as TargetInvocationException == null)
                        break;
                    exception = exception.InnerException;
                }
            }
            return false;
        }

        /// <summary>
        /// Handle exception with custom error message 
        /// </summary>
        /// <param name="exception">Exception that needs to handled</param>
        public static void Handle(this Exception exception)
        {
            string errorMessage;
            if (exception as FileNotFoundException != null)
                errorMessage = "\n\nOne of the required file is not found. Please do not remove, change, relocate input.txt and ProblemDescription.xml.\n" + exception.Message;
            else if (exception as ArgumentException != null || exception as ArgumentNullException != null || exception as NotSupportedException != null)
                errorMessage = "\n\nOne of the required files has invalid file name. Please do not change, rename input.txt and ProblemDescription.xml.\n" + exception.Message;
            else if (exception as PathTooLongException != null || exception as DirectoryNotFoundException != null)
                errorMessage = "\n\nIt seems one of the required files has been relocated. Please do not change the original location of input.txt and ProblemDescription.xml.\n" + exception.Message;
            else if (exception as UnauthorizedAccessException != null || exception as SecurityException != null)
                errorMessage = "\n\nIt seems you do not have necessary access to perform one or many action(s). Please check your security settings.\n" + exception.Message;
            else if (exception as IOException != null)
                errorMessage = "\n\nIt seems one IO Exception is occurred while processing your request. Please check your settings.\n" + exception.Message;
            else if (exception as InvalidOperationException != null || exception as NullReferenceException != null)
                errorMessage = "\n\nAn exception occurred while processing your request. Please do not change anything in ProblemDescription.xml. Leave it as it is.\n" + exception.Message;
            else
                errorMessage = "\n\nAn exception occured while processing your request, please find below the exception.\n" + exception.Message;

            CUI.ShowException(errorMessage);
        }
    }
}
