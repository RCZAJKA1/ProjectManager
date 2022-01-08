namespace ProjectManager.Common
{
    using System;

    /// <summary>
    ///     Supporting extension methods for various object types.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Throws an exception if the object is null.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <exception cref="ArgumentNullException">The exception thrown if the object is null.</exception>
        public static void ThrowIfNull(this object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        ///     Throws an exception if the string is empty. Does not check for null.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <exception cref="ArgumentException">The exception thrown if the string is empty.</exception>
        public static void ThrowIfEmpty(this string value)
        {
            if (value.Trim().Length == 0)
            {
                throw new ArgumentException("The value cannot be empty or only contain white space.", nameof(value));
            }
        }

        /// <summary>
        ///     Throws an exception if the string is null or empty.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <exception cref="ArgumentException">The exception thrown if the string is null or empty.</exception>
        public static void ThrowIfNullOrEmpty(this string value)
        {
            value.ThrowIfNull();

            if (value.Trim().Length == 0)
            {
                throw new ArgumentException("The value cannot be empty or only contain white space.", nameof(value));
            }
        }

        /// <summary>
        ///     Throws an exception if the string contains a character other than a letter or number.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <exception cref="ArgumentException">The exception thrown if the string doesn't contain letters or numbers.</exception>
        public static void EnsureOnlyLettersAndNumbers(this string value)
        {
            value.ThrowIfNull();

            foreach (char c in value)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    throw new InvalidOperationException("The value must only contain letters or numbers.");
                }
            }
        }
    }
}
