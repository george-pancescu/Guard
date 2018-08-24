using System;

namespace GuardNet
{
    public static partial class Guard
    {
        private const string NotGreaterThanTemplate = @"[{0}] cannot be greater than {1}.";

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being greater than the specified <paramref name="threshold"/> by 
        /// throwing an exception of type <see cref="ArgumentOutOfRangeException"/>
        /// when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        public static void NotGreaterThan<TParam>(TParam param, TParam threshold, string paramName)
            where TParam : IComparable<TParam>
        {
            NotGreaterThan(param, threshold, paramName, null);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being greater than the specified <paramref name="threshold"/> by 
        /// throwing an exception of type <see cref="ArgumentOutOfRangeException"/> with a specific <paramref name="message"/>
        /// when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="paramName">The name of the param to be checked, that will be included in the exception</param>
        /// <param name="message">The message that will be included in the exception</param>
        public static void NotGreaterThan<TParam>(TParam param, TParam threshold, string paramName, string message)
            where TParam : IComparable<TParam>
        {
            if (String.IsNullOrWhiteSpace(paramName))
            {
                paramName = GenericParameterName;
            }

            if (message == null)
            {
                message = String.Format(NotGreaterThanTemplate, paramName, threshold);
            }

            var argumentOutOfRangeException = new ArgumentOutOfRangeException(paramName, message);
            Guard.NotGreaterThan(param, threshold, argumentOutOfRangeException);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being greater than the specified <paramref name="threshold"/> by 
        /// throwing an exception of type <typeparamref name="TException"/>
        /// when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        public static void NotGreaterThan<TParam, TException>(TParam param, TParam threshold)
            where TParam : IComparable<TParam>
            where TException : Exception, new()
        {
            var message = String.Format(NotGreaterThanTemplate, GenericParameterName, threshold);

            Guard.NotGreaterThan<TParam, TException>(param, threshold, message);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being greater than the specified <paramref name="threshold"/> by 
        /// throwing an exception of type <typeparamref name="TException"/> with a specific <paramref name="message"/>
        /// when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="message">The message that will be included in the exception</param>
        public static void NotGreaterThan<TParam, TException>(TParam param, TParam threshold, string message)
            where TParam : IComparable<TParam>
            where TException : Exception, new()
        {
            if (message == null)
            {
                message = String.Format(NotGreaterThanTemplate, GenericParameterName, threshold);
            }

            TException exception = CreateException<TException>(message);

            Guard.NotGreaterThan(param, threshold, exception);
        }

        /// <summary>
        /// Guards the specified <paramref name="param"/> from being greater than the specified <paramref name="threshold"/> by 
        /// throwing an exception of type <typeparamref name="TException"/>
        /// when the precondition has not been met
        /// </summary>
        /// <typeparam name="TParam">The param Type (any value type)</typeparam>
        /// <typeparam name="TException">The exception Type (Exception)</typeparam>
        /// <param name="param">The param to be checked</param>
        /// <param name="threshold">The threshold against which the param will be checked</param>
        /// <param name="exception">The exception to be thrown when the precondition has not been met</param>
        public static void NotGreaterThan<TParam, TException>(TParam param, TParam threshold, TException exception)
            where TParam : IComparable<TParam>
            where TException : Exception, new()
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Guard.For(() => param.CompareTo(threshold) > 0, exception);
        }
    }
}
