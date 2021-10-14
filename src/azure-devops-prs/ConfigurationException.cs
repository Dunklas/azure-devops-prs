using System;

namespace AzureDevOpsPrs
{
    public class ConfigurationException
        : Exception
    {
        public ConfigurationException(string message)
            : base(message)
        {
        }
    }

    public class MissingConfigurationException
        : ConfigurationException
    {
        public MissingConfigurationException(string message)
            : base(message)
        {
        }
    }

    public class InvalidJsonException
        : ConfigurationException
    {
        public InvalidJsonException(string message)
            : base(message)
        {
        }
    }

    public class MissingPropertyException
        : ConfigurationException
    {
        public MissingPropertyException(string message)
            : base(message)
        {
        }
    }

    public class InvalidValueException
        : ConfigurationException
    {
        public InvalidValueException(string message)
            : base(message)
        {
        }
    }
}
