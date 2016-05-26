using System;

namespace VSCode
{
    public class FeatureActivationException : Exception
    {
        public FeatureActivationException(Exception innerException)
            : base("An exception was thrown while initializing the requested feature. Please see the InnerException property for details.", innerException)
        { }
    }
}
