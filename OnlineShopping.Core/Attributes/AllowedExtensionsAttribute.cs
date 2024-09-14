namespace OnlineShopping.Core.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    var message = $"Invalid file extension. Only these extensions are allowed: {string.Join(", ", _allowedExtensions)}";
                    var notAllowedExtensionResult = new ValidationResult(message);
                    return notAllowedExtensionResult;
                }

            }
            // Note that we are returning ValidationResult.Success when the file is null
            return ValidationResult.Success;
        }
    }
}
