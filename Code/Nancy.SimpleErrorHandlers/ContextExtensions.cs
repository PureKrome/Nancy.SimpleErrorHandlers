namespace Nancy.SimpleErrorHandlers
{
    public static class ContextExtensions
    {
        public static void IncludeSecretInformationForJsonErrorExceptions(this NancyContext context, bool value)
        {
            if (!value &&
                context.Items.ContainsKey(BootstrapperExtensions.IncludeExtraInformationForExceptionsKey))
            {
                context.Items.Remove(BootstrapperExtensions.IncludeExtraInformationForExceptionsKey);
            }
            else
            {
                context.Items.Add(BootstrapperExtensions.IncludeExtraInformationForExceptionsKey, value);
            }
        }
    }
}