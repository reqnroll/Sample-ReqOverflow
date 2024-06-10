// This file is a copy of ReqOverflow.Specs.Controller/Support/ErrorMessageProvider.cs.
// It has been copied for the sake of the demonstration. So that the different 
// automation targets can be studied independently.

// ReSharper disable once CheckNamespace
namespace ReqOverflow.Specs.Support
{
    public class ErrorMessageProvider
    {
        public string GetExpectedErrorMessage(string messageKey)
        {
            if (messageKey.Contains(" "))
                return messageKey; // this is the ream message
            
            //TODO: load message from resource if message key like this-is-the-key is provided
            return messageKey.Replace("-", " ");
        }
    }
}
