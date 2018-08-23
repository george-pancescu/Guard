using System;
using System.Collections.Generic;

namespace GuardNet
{
    /// <summary>
    /// Facilitates runtime checks of code and allows to define preconditions and invariants within a method.
    /// </summary>
    public static class Guard
    {
        private const string GenericParameterName = "parameter";

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null by 
        /// throwing an <see cref="ArgumentNullException"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any reference type)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>        
        public static void NotNull<TParam>(TParam param, string paramName)
            where TParam : class
        {
            NotNull(param, paramName, null);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null by 
        /// throwing an <see cref="ArgumentNullException"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any reference type)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        /// <param name="message">The message that will be included in the exception</param>
        public static void NotNull<TParam>(TParam param, string paramName, string message)
            where TParam : class
        {
            if (String.IsNullOrWhiteSpace(paramName))
            {
                paramName = GenericParameterName;
            }

            if (message == null)
            {
                message = $"[{paramName}] cannot be Null.";
            }

            var argumentNullException = new ArgumentNullException(paramName, message);
            Guard.NotNull(param, argumentNullException);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null by 
        /// throwing an <paramref name="exception"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any reference type)</typeparam>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="exception">The exception to be thrown when the precondition has not been met</param>
        public static void NotNull<TParam, TException>(TParam param, TException exception)
            where TParam : class
            where TException : Exception
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Guard.For(() => param == null, exception);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null, empty or white-space by 
        /// throwing an <see cref="ArgumentException"/> when the precondition has not been met
        /// </summary>
        /// <param name="param">The param to be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        public static void NotNullOrWhitespace(string param, string paramName)
        {
            NotNullOrWhitespace(param, paramName, null);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null, empty or white-space by 
        /// throwing an <see cref="ArgumentException"/> when the precondition has not been met
        /// </summary>
        /// <param name="param">The param to be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        /// <param name="message">The message that will be included in the exception</param>
        public static void NotNullOrWhitespace(string param, string paramName, string message)
        {
            if (String.IsNullOrWhiteSpace(paramName))
            {
                paramName = GenericParameterName;
            }

            if (message == null)
            {
                message = $"[{paramName}] cannot be Null, empty or white-space.";
            }

            var argumentException = new ArgumentException(message, paramName);
            Guard.NotNullOrWhitespace(param, argumentException);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null, empty or white-space by 
        /// throwing an <paramref name="exception"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="exception">The exception to be thrown when the precondition has not been met</param>
        public static void NotNullOrWhitespace<TException>(string param, TException exception) 
            where TException : Exception
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Guard.For(() => String.IsNullOrWhiteSpace(param), exception);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null or empty (white-space allowed) by 
        /// throwing an <see cref="ArgumentException"/> when the precondition has not been met
        /// </summary>
        /// <param name="param">The param to be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        public static void NotNullOrEmpty(string param, string paramName)
        {
            NotNullOrEmpty(param, paramName, null);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null or empty (white-space allowed) by 
        /// throwing an <see cref="ArgumentException"/> when the precondition has not been met
        /// </summary>
        /// <param name="param">The param to be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        /// <param name="message">The message that will be included in the exception</param>
        public static void NotNullOrEmpty(string param, string paramName, string message)
        {
            if (String.IsNullOrWhiteSpace(paramName))
            {
                paramName = GenericParameterName;
            }

            if (message == null)
            {
                message = $"[{paramName}] cannot be Null or empty.";
            }

            var argumentException = new ArgumentException(message, paramName);
            Guard.NotNullOrEmpty(param, argumentException);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being null or empty (white-space allowed) by 
        /// throwing an <paramref name="exception"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="exception">The exception to be thrown when the precondition has not been met</param>
        public static void NotNullOrEmpty<TException>(string param, TException exception)
            where TException : Exception
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Guard.For(() => String.IsNullOrEmpty(param), exception);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being less than the specified <paramref name="threshold"/> by 
        /// throwing an <see cref="ArgumentOutOfRangeException"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        public static void NotLessThan<TParam>(TParam param, TParam threshold, string paramName)
            where TParam : struct
        {
            NotLessThan(param, threshold, paramName, null);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being less than the specified <paramref name="threshold"/> by 
        /// throwing an <see cref="ArgumentOutOfRangeException"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        /// <param name="message">The message that will be included in the exception</param>
        public static void NotLessThan<TParam>(TParam param, TParam threshold, string paramName, string message)
            where TParam : struct
        {
            if (String.IsNullOrWhiteSpace(paramName))
            {
                paramName = GenericParameterName;
            }

            if (message == null)
            {
                message = $"[{paramName}] is out of range.";
            }

            var argumentOutOfRangeException = new ArgumentOutOfRangeException(paramName, message);
            Guard.NotLessThan(param, threshold, argumentOutOfRangeException);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being less than the specified <paramref name="threshold"/> by 
        /// throwing an <paramref name="exception"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="exception">The exception to be thrown when the precondition has not been met</param>
        public static void NotLessThan<TParam, TException>(TParam param, TParam threshold, TException exception)
            where TParam : struct
            where TException : Exception
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var comparer = Comparer<TParam>.Default;
            
            Guard.For(() => comparer.Compare(param, threshold) < 0, exception);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being greater than the specified <paramref name="threshold"/> by 
        /// throwing an <see cref="ArgumentOutOfRangeException"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        public static void NotGreaterThan<TParam>(TParam param, TParam threshold, string paramName)
            where TParam : struct
        {
            NotGreaterThan(param, threshold, paramName, null);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being greater than the specified <paramref name="threshold"/> by 
        /// throwing an <see cref="ArgumentOutOfRangeException"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        /// <param name="message">The message that will be included in the exception</param>
        public static void NotGreaterThan<TParam>(TParam param, TParam threshold, string paramName, string message)
            where TParam : struct
        {
            if (String.IsNullOrWhiteSpace(paramName))
            {
                paramName = GenericParameterName;
            }

            if (message == null)
            {
                message = $"[{paramName}] is out of range.";
            }

            var argumentOutOfRangeException = new ArgumentOutOfRangeException(paramName, message);
            Guard.NotGreaterThan(param, threshold, argumentOutOfRangeException);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being greater than the specified <paramref name="threshold"/> by 
        /// throwing an <paramref name="exception"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="exception">The exception to be thrown when the precondition has not been met</param>
        public static void NotGreaterThan<TParam, TException>(TParam param, TParam threshold, TException exception)
            where TParam : struct
            where TException : Exception
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var comparer = Comparer<TParam>.Default;

            Guard.For(() => comparer.Compare(param, threshold) > 0, exception);
        }

        /// <summary>
        /// Guards the specified <paramref name="predicate"/> from being violated by 
        /// throwing an <paramref name="exception"/> when the precondition has not been met
        /// </summary>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="predicate">The precondition that has to be met</param>
        /// <param name="exception">The exception to be thrown when the precondition has not been met</param>
        public static void For<TException>(Func<bool> predicate, TException exception) 
            where TException : Exception
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            bool conditionNotMet = predicate.Invoke();
            if (conditionNotMet)
            {
                throw exception;
            }
        }
    }
}
