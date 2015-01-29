namespace Nancy.SimpleErrorHandlers
{
    public static class ContextExtensions
    {
        public static void IncludeSecretInformationForJsonErrorExceptions(this NancyContext context, bool value)
        {
            if (!value &&
                context.Items.ContainsKey(BootstrapperExtensions.IncludeSecretInformationForExceptionsKey))
            {
                context.Items.Remove(BootstrapperExtensions.IncludeSecretInformationForExceptionsKey);
            }
            else
            {
                context.Items.Add(BootstrapperExtensions.IncludeSecretInformationForExceptionsKey, value);
            }
        }
    }
}