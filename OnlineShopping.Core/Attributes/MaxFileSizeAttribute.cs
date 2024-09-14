namespace OnlineShopping.Core.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var isAllowed = file.Length <= _maxFileSize;
                if (!isAllowed)
                {
                    var message = $"File size should not exceed {_maxFileSize / (1024 * 1024)} MB";
                    var notAllowedSizeResult = new ValidationResult(message);
                    return notAllowedSizeResult;
                }
            }
            // Note that we are returning ValidationResult.Success when the file is null
            return ValidationResult.Success;
        }
    }
}
