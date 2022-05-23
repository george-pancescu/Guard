using System;
using System.Text.RegularExpressions;

namespace GuardNet
{
    public static partial class Guard
    {
        private const string NotIsMatchTemplate = @"[{0}] does not match the pattern [{1}].";

        /// <summary>
        /// Guards the specified <paramref name="param"/> from not matching the specified regular expression (<paramref name="regexPattern"/>) 
        /// by throwing an an exception of type <see cref="ArgumentException"/>
        /// when the precondition has not been met
        /// </summary>
        /// <param name="param">The param to be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        /// <param name="regexPattern">The regular expression pattern</param>
        /// <param name="regexOptions">(optional) The regular expression options to be used during the matching process</param>
        public static void NotIsMatch(
            string param,
            string paramName,
            string regexPattern,
            RegexOptions regexOptions = RegexOptions.None)
        {
            var message = string.Format(NotIsMatchTemplate, GenericParameterName, regexPattern);

            Guard.NotIsMatch(param, paramName, regexPattern, message, regexOptions);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from not matching the specified regular expression (<paramref name="regexPattern"/>) 
        /// by throwing an an exception of type <see cref="ArgumentException"/> with a specific <paramref name="message"/>
        /// when the precondition has not been met
        /// </summary>
        /// <param name="param">The param to be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        /// <param name="regexPattern">The regular expression pattern</param>
        /// <param name="message">The message that will be included in the exception</param>
        /// <param name="regexOptions">(optional) The regular expression options to be used during the matching process</param>
        public static void NotIsMatch(
            string param,
            string paramName,
            string regexPattern,
            string message,
            RegexOptions regexOptions = RegexOptions.None)
        {
            if (string.IsNullOrWhiteSpace(paramName))
            {
                paramName = GenericParameterName;
            }

            if (message == null)
            {
                message = string.Format(NotIsMatchTemplate, paramName, regexPattern);
            }

            var argumentException = new ArgumentException(message, paramName);
            Guard.NotIsMatch(param, regexPattern, argumentException, regexOptions);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from not matching the specified regular expression (<paramref name="regexPattern"/>) 
        /// by throwing an an exception of type <typeparamref name="TException"/>
        /// when the precondition has not been met
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="regexPattern">The regular expression pattern</param>
        /// <param name="regexOptions">(optional) The regular expression options to be used during the matching process</param>
        public static void NotIsMatch<TException>(
            string param,
            string regexPattern,
            RegexOptions regexOptions = RegexOptions.None)
            where TException : Exception, new()
        {
            var message = string.Format(NotIsMatchTemplate, GenericParameterName, regexPattern);            

            Guard.NotIsMatch<TException>(param, regexPattern, message, regexOptions);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from not matching the specified regular expression (<paramref name="regexPattern"/>) 
        /// by throwing an an exception of type <typeparamref name="TException"/> with a specific <paramref name="message"/>
        /// when the precondition has not been met
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="regexPattern">The regular expression pattern</param>
        /// <param name="message">The message that will be included in the exception</param>
        /// <param name="regexOptions">(optional) The regular expression options to be used during the matching process</param>
        public static void NotIsMatch<TException>(
            string param,
            string regexPattern,
            string message,
            RegexOptions regexOptions = RegexOptions.None)
            where TException : Exception, new()
        {
            if (message == null)
            {
                message = string.Format(NotIsMatchTemplate, GenericParameterName, regexPattern);
            }

            var exception = CreateException<TException>(message);

            Guard.NotIsMatch(param, regexPattern, exception, regexOptions);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from not matching the specified regular expression (<paramref name="regexPattern"/>) 
        /// by throwing an an exception of type <typeparamref name="TException"/>
        /// when the precondition has not been met
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="regexPattern">The regular expression pattern</param>
        /// <param name="exception">The exception to be thrown when the precondition has not been met</param>
        /// <param name="regexOptions">(optional) The regular expression options to be used during the matching process</param>
        public static void NotIsMatch<TException>(
            string param,
            string regexPattern,
            TException exception,
            RegexOptions regexOptions = RegexOptions.None)
            where TException : Exception, new()
        {
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            if (regexPattern == null)
            {
                throw new ArgumentNullException(nameof(regexPattern));
            }

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Guard.For(() => !Regex.IsMatch(param, regexPattern, regexOptions), exception);
        }
    }
}
