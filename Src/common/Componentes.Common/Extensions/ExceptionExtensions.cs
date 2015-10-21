using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

public static class ExceptionExtensions
{

    /// <summary>
    /// <para>Creates a log-string from the Exception.</para>
    /// <para>The result includes the stacktrace, innerexception et cetera, separated by <seealso cref="Environment.NewLine"/>.</para>
    /// </summary>
    /// <param name="ex">The exception to create the string from.</param>
    /// <param name="additionalMessage">Additional message to place at the top of the string, maybe be empty or null.</param>
    /// <returns></returns>
    public static string ToLogString(this Exception ex, string additionalMessage)
    {
        StringBuilder msg = new StringBuilder();

        if (!string.IsNullOrEmpty(additionalMessage))
        {
            msg.Append(additionalMessage);
            msg.Append(Environment.NewLine);
        }

        if (ex != null)
        {
            try
            {
                Exception orgEx = ex;

                msg.Append("Exception:");
                msg.Append(Environment.NewLine);
                while (orgEx != null)
                {
                    msg.Append(orgEx.Message);
                    msg.Append(Environment.NewLine);
                    orgEx = orgEx.InnerException;
                }

                if (ex.Data != null)
                {
                    foreach (object i in ex.Data)
                    {
                        msg.Append("Data :");
                        msg.Append(i.ToString());
                        msg.Append(Environment.NewLine);
                    }
                }

                if (ex.StackTrace != null)
                {
                    msg.Append("StackTrace:");
                    msg.Append(Environment.NewLine);
                    msg.Append(ex.StackTrace.ToString());
                    msg.Append(Environment.NewLine);
                }

                if (ex.Source != null)
                {
                    msg.Append("Source:");
                    msg.Append(Environment.NewLine);
                    msg.Append(ex.Source);
                    msg.Append(Environment.NewLine);
                }

                if (ex.TargetSite != null)
                {
                    msg.Append("TargetSite:");
                    msg.Append(Environment.NewLine);
                    msg.Append(ex.TargetSite.ToString());
                    msg.Append(Environment.NewLine);
                }

                Exception baseException = ex.GetBaseException();
                if (baseException != null)
                {
                    msg.Append("BaseException:");
                    msg.Append(Environment.NewLine);
                    msg.Append(ex.GetBaseException());
                }
            }
            finally
            {
            }
        }
        return msg.ToString();
    }
    
    public static Exception GetMostInner(this Exception ex)
    {
        Exception ActualInnerEx = ex;

        while (ActualInnerEx != null)
        {
            ActualInnerEx = ActualInnerEx.InnerException;
            if (ActualInnerEx != null)
                ex = ActualInnerEx;
        }
        return ex;
    }
    public static string ToStringReccurent(this Exception exception)
    {
        if (exception == null)
        {
            return "empty";
        }
        return string.Format
            ("Exception: {0}\nMessage: {1}\nStack Trace: {2}\nInner {3}", exception.GetType(),
                exception.Message, exception.StackTrace, exception.InnerException.ToStringReccurent());
    }

    /// <summary>
    /// Returns a list of all the exception messages from the top-level
    /// exception down through all the inner exceptions. Useful for making
    /// logs and error pages easier to read when dealing with exceptions.
    /// Usage: Exception.Messages()
    /// </summary>
    public static IEnumerable<string> Messages(this Exception ex)
    {
        // return an empty sequence if the provided exception is null
        if (ex == null) { yield break; }
        // first return THIS exception's message at the beginning of the list
        yield return ex.Message;
        // then get all the lower-level exception messages recursively (if any)
        IEnumerable<Exception> innerExceptions = Enumerable.Empty<Exception>();

        if (ex is AggregateException && (ex as AggregateException).InnerExceptions.Any())
        {
            innerExceptions = (ex as AggregateException).InnerExceptions;
        }
        else if (ex.InnerException != null)
        {
            innerExceptions = new Exception[] { ex.InnerException };
        }

        foreach (var innerEx in innerExceptions)
        {
            foreach (string msg in innerEx.Messages())
            {
                yield return msg;
            }
        }
    }


}